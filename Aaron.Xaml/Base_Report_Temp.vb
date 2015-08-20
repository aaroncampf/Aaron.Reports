﻿Imports System.Windows
Imports System.Windows.Documents

Public Class Base_Report_Temp

#Region "   PDF"

    Public Shared Function AsPDF(Document As Xps.Packaging.XpsDocument) As String
        Dim TempFile As String = My.Computer.FileSystem.GetTempFileName

        'TODO Make sure this Using Works Properly
        Using Stream As New IO.FileStream(TempFile, IO.FileMode.Create)
            Dim oPdfDoc As New iTextSharp.text.Document()
            Dim oPdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(oPdfDoc, Stream)
            oPdfDoc.Open()

            For I = 0 To Document.GetFixedDocumentSequence.DocumentPaginator.PageCount - 1
                AddPage(True, AsStream(Document, I), oPdfDoc, oPdfWriter)
            Next

            oPdfDoc.Close()
            oPdfWriter.Close()
        End Using

        Return TempFile
    End Function


    Private Shared Function AsStream(Document As Xps.Packaging.XpsDocument, Page As Integer) As IO.MemoryStream
        Dim bitmapEncoder As New Media.Imaging.JpegBitmapEncoder
        Dim documentPage As DocumentPage = Document.GetFixedDocumentSequence.DocumentPaginator.GetPage(Page)
        Dim targetBitmap As New Media.Imaging.RenderTargetBitmap(documentPage.Size.Width * 5, documentPage.Size.Height * 5, 96.0 * 5, 96.0 * 5, Media.PixelFormats.Pbgra32)

        targetBitmap.Render(documentPage.Visual)
        bitmapEncoder.Frames.Add(Media.Imaging.BitmapFrame.Create(targetBitmap))

        Dim Stream As New IO.MemoryStream
        bitmapEncoder.Save(Stream)
        Return Stream
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Resize"></param>
    ''' <param name="Image"></param>
    ''' <param name="oPdfDoc"></param>
    ''' <param name="oPdfWriter"></param>
    ''' <remarks></remarks>
    ''' <stepthrough></stepthrough>
    Shared Sub AddPage(Resize As Boolean, Image As IO.MemoryStream, ByRef oPdfDoc As iTextSharp.text.Document, oPdfWriter As iTextSharp.text.pdf.PdfWriter)
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

#End Region


    Public Shared Sub Print(Hidden As Boolean, Document As Xps.Packaging.XpsDocument)
        If Hidden Then
            Dim PD As New Windows.Controls.PrintDialog
            PD.PrintDocument(Document.GetFixedDocumentSequence.DocumentPaginator, Nothing)
        Else
            Dim this As New Windows.Controls.DocumentViewer With {.Document = Document.GetFixedDocumentSequence}
            this.Print()
        End If
    End Sub

    Public Shared Sub ShowHelper(Document As Xps.Packaging.XpsDocument)
        Dim IsForm As New Forms.Form With {.WindowState = Forms.FormWindowState.Maximized}
        Dim this As New Controls.DocumentViewer With {.Document = Document.GetFixedDocumentSequence}
        IsForm.Controls.Add(New Forms.Integration.ElementHost With {.Dock = Forms.DockStyle.Fill, .Child = this})
        IsForm.ShowDialog()
    End Sub



End Class
