Imports System.IO
Imports Paytm

Namespace Controllers
    Public Class CallbackController
        Inherits Controller
        ' GET: Callback
        <HttpPost>
        Function Index() As ActionResult
            Using reader As New StreamReader(Request.InputStream)
                Dim parameters As New Dictionary(Of String, String)()
                Dim parametersRow As New Dictionary(Of String, String)()
                Dim paytmChecksum = ""
                Dim PAYTM_MERCHANT_KEY = "PAYTM_MERCHANT_KEY"
                Dim checkSumMatch = False
                If Request.Form.Keys.Count > 0 Then

                    For Each key As String In Request.Form.Keys
                        If Request.Form(key).Contains("|") Then
                            parameters.Add(key.Trim(), "")
                            parametersRow.Add(key.Trim(), "")
                        Else
                            parameters.Add(key.Trim(), Request.Form(key))
                            parametersRow.Add(key.Trim(), Request.Form(key))
                        End If
                    Next

                    If parameters.ContainsKey("CHECKSUMHASH") Then
                        paytmChecksum = parameters("CHECKSUMHASH")
                        parameters.Remove("CHECKSUMHASH")
                    End If

                    If Not String.IsNullOrEmpty(paytmChecksum) AndAlso Checksum.verifySignature(parameters, PAYTM_MERCHANT_KEY, paytmChecksum) Then
                        checkSumMatch = True
                    End If
                End If
                ViewData("verify") = checkSumMatch
                ViewBag.Parameters = parametersRow
                Return View()
            End Using
        End Function
    End Class
End Namespace