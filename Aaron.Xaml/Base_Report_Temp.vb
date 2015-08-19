Public Class Base_Report_Temp


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Resize"></param>
    ''' <param name="Image"></param>
    ''' <param name="oPdfDoc"></param>
    ''' <param name="oPdfWriter"></param>
    ''' <remarks></remarks>
    ''' <stepthrough></stepthrough>
    Friend Shared Sub AddPage(Resize As Boolean, Image As IO.MemoryStream, ByRef oPdfDoc As iTextSharp.text.Document, oPdfWriter As iTextSharp.text.pdf.PdfWriter)
        Dim oImage As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(Image)
        Dim iWidth As Single = oImage.Width, iHeight As Single = oImage.Height

        If Resize Then
            oImage.SetAbsolutePosition(1, 1)
            oPdfDoc.SetPageSize(New iTextSharp.text.Rectangle(iWidth, iHeight))
            oPdfDoc.NewPage()
            oPdfWriter.DirectContent.AddImage(oImage)
            Exit Sub
        End If

        Dim iAspectRatio As Double = iWidth / iHeight

        Dim iWidthPage As Single = iTextSharp.text.PageSize.LETTER.Width
        Dim iHeightPage As Single = iTextSharp.text.PageSize.LETTER.Height
        Dim iPageAspectRatio As Double = iWidthPage / iHeightPage

        Dim iWidthGoal As Single = 0, iHeightGoal As Single = 0

        If iWidth < iWidthPage And iHeight < iHeightPage Then
            'Image fits within the page
            iWidthGoal = iWidth
            iHeightGoal = iHeight
        ElseIf iAspectRatio > iPageAspectRatio Then
            'Width is too big
            iWidthGoal = iWidthPage
            iHeightGoal = iWidthPage * (iHeight / iWidth)
        Else
            'Height is too big
            iWidthGoal = iHeightPage * (iWidth / iHeight)
            iHeightGoal = iHeightPage
        End If

        oImage.SetAbsolutePosition(1, 1)
        oPdfDoc.SetPageSize(iTextSharp.text.PageSize.LETTER)

        oPdfDoc.NewPage()
        oImage.ScaleAbsolute(iWidthGoal, iHeightGoal)
        oPdfWriter.DirectContent.AddImage(oImage)
    End Sub


End Class
