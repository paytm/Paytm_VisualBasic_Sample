@Code
    Dim parameters As Dictionary(Of String, String) = TryCast(ViewBag.Parameters, Dictionary(Of String, String))
End Code
<h1>
    Checksum Verify : @ViewBag.verify
</h1>
<h2>Response from paytm:</h2>

@If parameters IsNot Nothing Then
    @<ul>
        @For Each kvp As KeyValuePair(Of String, String) In parameters
            @<li>@kvp.Key: @kvp.Value</li>
        Next
    </ul>
End If
