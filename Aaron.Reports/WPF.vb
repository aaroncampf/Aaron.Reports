
<Obsolete("Use Documents.Paragraph", True)>
Public Class Paragraph
    Inherits Documents.Paragraph
    Public Property Text As String


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Text"></param>
    ''' <remarks></remarks>
    ''' <stepthrough></stepthrough>
    <DebuggerNonUserCode()>
    Sub New(ByVal Text As String)
        Me.Text = Text
    End Sub


    Public Function ToXML() As XElement
        Dim Test =
            <Paragraph TextAlignment=<%= Me.TextAlignment.ToString %> FontSize=<%= Me.FontSize %> FontWeight=<%= Me.FontWeight %>
                Background=<%= Me.Background %>>
            </Paragraph>
        ' <%= Me.Text.Replace(C1, "<LineBreak/>") %>
        If String.IsNullOrWhiteSpace(Me.Text) Then Return Nothing

        For Each Item In Text.Split(C1)
            If Item.Length = 1 AndAlso Char.GetUnicodeCategory(Char.Parse(Item)) = Globalization.UnicodeCategory.Control Then
                Test.Add(<LineBreak/>)
            Else
                Test.Add(Item)
            End If
        Next


        Return Test
    End Function
End Class





End Namespace