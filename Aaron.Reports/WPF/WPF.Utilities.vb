Namespace WPF
    Friend Class Utilities

        ''' <summary>
        ''' Returns An XElement that can Display Text/Paragraph Directly
        ''' </summary>
        ''' <param name="Text">The Text you want to Add</param>
        ''' <remarks></remarks>
        ''' <stepthrough>Enabled</stepthrough>
        Public Shared Function InsertText(ByVal Text As String) As Object()           'XElement
            Dim Test As New ArrayList


            If Text.Contains("<InlineContextValue") Then
                Dim Regex As New System.Text.RegularExpressions.Regex("(?<Text>\s+|[^<]+|\s+)+(?<XElement><[^/>]+/>)+")
                For Each Item As System.Text.RegularExpressions.Match In Regex.Matches(Text)
                    Test.Add(Item.Groups(1).ToString)
                    Test.Add(XElement.Parse(Item.Groups(2).ToString))
                Next

            Else
                Test.Add(<InlineDocumentValue Text=<%= Text %> Value=""/>)
            End If

            Return Test.ToArray
        End Function
        '<DebuggerNonUserCode()>

    End Class


End Namespace
