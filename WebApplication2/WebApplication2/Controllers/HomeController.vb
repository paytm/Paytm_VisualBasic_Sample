Imports System
Imports Paytm
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Net

Public Class HomeController
    Inherits System.Web.Mvc.Controller

    Function Index() As ActionResult
        Dim PAYTM_MID = "PAYTM_MID"
        Dim PAYTM_MERCHANT_KEY = "PAYTM_MERCHANT_KEY"
        Dim PAYTM_ENVIRONMENT = "https://securegw-stage.paytm.in"
        Dim PAYTM_WEBSITE = "WEBSTAGING"

        Dim body As Dictionary(Of String, Object) = New Dictionary(Of String, Object)()
        Dim head As Dictionary(Of String, String) = New Dictionary(Of String, String)()
        Dim requestBody As Dictionary(Of String, Object) = New Dictionary(Of String, Object)()
        Dim txnAmount As Dictionary(Of String, String) = New Dictionary(Of String, String)()

        Dim oDate As String = DateTime.Now.ToString("yyyyMMddHHmmss")
        txnAmount.Add("value", "1.00")
        txnAmount.Add("currency", "INR")
        Dim userInfo As Dictionary(Of String, String) = New Dictionary(Of String, String)()

        userInfo.Add("custId", "1")
        body.Add("requestType", "Payment")
        body.Add("mid", PAYTM_MID)
        body.Add("websiteName", PAYTM_WEBSITE) ''DEFAULT  for production
        body.Add("orderId", oDate)
        body.Add("txnAmount", "1")
        body.Add("userInfo", userInfo)

        body.Add("callbackUrl", "https://localhost:44352/Callback")
        Dim paytmChecksum As String = Checksum.generateSignature(JsonConvert.SerializeObject(body), PAYTM_MERCHANT_KEY)
        head.Add("signature", paytmChecksum)
        requestBody.Add("body", body)
        requestBody.Add("head", head)

        Dim post_data As String = JsonConvert.SerializeObject(requestBody)
        Dim url = PAYTM_ENVIRONMENT + "/theia/api/v1/initiateTransaction?mid=" + PAYTM_MID + "&orderId=" + oDate
        Dim webRequest As HttpWebRequest = DirectCast(HttpWebRequest.Create(url), HttpWebRequest)
        webRequest.Method = "POST"
        webRequest.ContentType = "application/json"

        webRequest.ContentLength = post_data.Length
        Dim RequestWriter As New IO.StreamWriter(webRequest.GetRequestStream())
        RequestWriter.Write(post_data)
        RequestWriter.Close()
        RequestWriter.Dispose()
        Dim responseData As String = String.Empty
        Dim responseReader As New IO.StreamReader(webRequest.GetResponse().GetResponseStream())
        responseData = responseReader.ReadToEnd()
        Console.WriteLine(responseData)
        Dim responseJson As JObject = JObject.Parse(responseData)
        Dim txnToken As String = responseJson("body")("txnToken").ToString()
        ViewData("Title") = "Visual Basic Paytm Sample Example"
        ViewData("txnToken") = txnToken
        ViewData("orderId") = oDate
        ViewData("amount") = "1"
        ViewData("jsinvokelink") = PAYTM_ENVIRONMENT + "/merchantpgpui/checkoutjs/merchants/" + PAYTM_MID + ".js"

        Return View()
    End Function

End Class
