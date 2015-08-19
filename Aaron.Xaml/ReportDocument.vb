Imports System.IO
Imports System.IO.Packaging
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Markup
Imports System.Windows.Xps.Packaging
Imports System.Windows.Xps.Serialization

''' <summary>
''' Contains a complete report template without data
''' </summary>
Public Class ReportDocument

#Region "       Properties		>>>"
    Public Const XML_Namespace = "xmlns:xrd=""clr-namespace:Aaron.Xaml;assembly=Aaron.Xaml"""

    'Public Property Data As New ReportData


    ''' <summary>
    ''' The Default Date associated with the Report
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' <see cref="ReportContextValues.Types.ReportDate"/>
    ''' </remarks>
    Public Property ReportDate As String

    ''' <summary>
    ''' Gets a list of document values
    ''' </summary>
    Public Property DocumentValues As New Dictionary(Of String, Object)

    ''' <summary>
    ''' Shows all unknown values on the page
    ''' </summary>
    Public Property ShowUnknownValues As Boolean = True

    ''' <summary>
    ''' Gets the original page height of the FlowDocument
    ''' </summary>
    Public ReadOnly Property PageHeight As Double = Double.NaN

    ''' <summary>
    ''' Gets the original page width of the FlowDocument
    ''' </summary>
    Public ReadOnly Property PageWidth As Double = Double.NaN

    ' ''' <summary>
    ' ''' Gets or sets the page header height
    ' ''' </summary>
    'Protected Property PageHeaderHeight As Double

    ' ''' <summary>
    ' ''' Gets or sets the page footer height
    ' ''' </summary>
    'Protected Property PageFooterHeight As Double



    '   Private _pageHeight As Double = Double.NaN
    '''' <summary>
    '''' Gets the original page height of the FlowDocument
    '''' </summary>
    'Public ReadOnly Property PageHeight As Double
    '	Get
    '		Return _pageHeight
    '	End Get
    'End Property


    '   Private _pageWidth As Double = Double.NaN
    '''' <summary>
    '''' Gets the original page width of the FlowDocument
    '''' </summary>
    'Public ReadOnly Property PageWidth As Double
    '	Get
    '		Return _pageWidth
    '	End Get
    'End Property

    ''' <summary>
    ''' Gets or sets the optional report name
    ''' </summary>
    Public Property ReportName As String

    ''' <summary>
    ''' Gets or sets the optional report title.
    ''' </summary>
    ''' <remarks>
    ''' XAML to use: {xrd:InlineContextValue Format="D" PropertyName="ReportTitle" />}
    ''' </remarks>
    Public Property ReportTitle As String

    ' ''' <summary>
    ' ''' XAML image path
    ' ''' </summary>
    'Public Property XamlImagePath As String

    Dim _XamlData As String

    ''' <summary>
    ''' XAML report data
    ''' </summary>
    Public Overridable Property XamlData As String
        Get
            If _XamlData = Nothing Then
                Return Get_Default_Template()
            End If
            Return _XamlData
        End Get
        Set(value As String)
            _XamlData = value
        End Set
    End Property


    ' ''' <summary>
    ' ''' Gets or sets the compression option which is used to create XPS files
    ' ''' </summary>
    '<Obsolete("Being Removed", True)>
    'Public Property XpsCompressionOption() As CompressionOption = CompressionOption.NotCompressed

#End Region


    '#Region "       CreateXpsDocument	>>>"

    '	' ''' <summary>
    '	' ''' Helper method to create page header or footer from flow document template
    '	' ''' </summary>
    '	' ''' <param name="data">enumerable report data</param>
    '	' ''' <returns></returns>
    '	' ''' <exception cref="ArgumentNullException">data</exception>
    '	'Public Function CreateXpsDocument(data As IEnumerable(Of ReportData)) As XpsDocument
    '	'	If data Is Nothing Then Throw New ArgumentNullException("data")

    '	'	Dim count As Integer = 0
    '	'	Dim firstData As ReportData = Nothing
    '	'	For Each rd As ReportData In data
    '	'		If firstData Is Nothing Then
    '	'			firstData = rd
    '	'		End If
    '	'		count += 1
    '	'	Next
    '	'	If count = 1 Then
    '	'		Return CreateXpsDocument(firstData)
    '	'	End If
    '	'	' we have only one ReportData object -> use the normal ReportPaginator instead
    '	'	Dim ms As New MemoryStream()
    '	'	Dim pkg As Package = Package.Open(ms, FileMode.Create, FileAccess.ReadWrite)
    '	'	Dim pack As String = "pack://report.xps"
    '	'	PackageStore.RemovePackage(New Uri(pack))
    '	'	PackageStore.AddPackage(New Uri(pack), pkg)
    '	'	Dim doc As New XpsDocument(pkg, CompressionOption.NotCompressed, pack)
    '	'	Dim rsm As New XpsSerializationManager(New XpsPackagingPolicy(doc), False)
    '	'	Dim paginator As DocumentPaginator = DirectCast(CreateFlowDocument(), IDocumentPaginatorSource).DocumentPaginator

    '	'	Dim rp As New MultipleReportPaginator1(Me, data)
    '	'	rsm.SaveAsXaml(rp)
    '	'	Return doc
    '	'End Function

    '	' ''' <summary>
    '	' ''' Helper method to create page header or footer from flow document template
    '	' ''' </summary>
    '	' ''' <param name="data">report data</param>
    '	' ''' <param name="fileName">file to save XPS to</param>
    '	' ''' <returns></returns>
    '	'Public Function CreateXpsDocument(data As ReportData, fileName As String) As XpsDocument
    '	'	Dim pkg As Package = Package.Open(fileName, FileMode.Create, FileAccess.ReadWrite)
    '	'	Dim pack As String = "pack://report.xps"
    '	'	PackageStore.RemovePackage(New Uri(pack))
    '	'	PackageStore.AddPackage(New Uri(pack), pkg)
    '	'	Dim doc As New XpsDocument(pkg, _XpsCompressionOption, pack)
    '	'	Dim rsm As New XpsSerializationManager(New XpsPackagingPolicy(doc), False)
    '	'	Dim paginator As DocumentPaginator = DirectCast(CreateFlowDocument(), IDocumentPaginatorSource).DocumentPaginator

    '	'	Dim rp As New ReportPaginator(Me, data)
    '	'	rsm.SaveAsXaml(rp)
    '	'	rsm.Commit()
    '	'	pkg.Close()
    '	'	Return New XpsDocument(fileName, FileAccess.Read)
    '	'End Function

    '	' ''' <summary>
    '	' ''' Helper method to create page header or footer from flow document template
    '	' ''' </summary>
    '	' ''' <param name="data">enumerable report data</param>
    '	' ''' <param name="fileName">file to save XPS to</param>
    '	' ''' <returns></returns>
    '	'Public Function CreateXpsDocument(data As IEnumerable(Of ReportData), fileName As String) As XpsDocument
    '	'	If data Is Nothing Then
    '	'		Throw New ArgumentNullException("data")
    '	'	End If
    '	'	Dim count As Integer = 0
    '	'	Dim firstData As ReportData = Nothing
    '	'	For Each rd As ReportData In data
    '	'		If firstData Is Nothing Then
    '	'			firstData = rd
    '	'		End If
    '	'		count += 1
    '	'	Next
    '	'	If count = 1 Then
    '	'		Return CreateXpsDocument(firstData)
    '	'	End If
    '	'	' we have only one ReportData object -> use the normal ReportPaginator instead
    '	'	Dim pkg As Package = Package.Open(fileName, FileMode.Create, FileAccess.ReadWrite)
    '	'	Dim pack As String = "pack://report.xps"
    '	'	PackageStore.RemovePackage(New Uri(pack))
    '	'	PackageStore.AddPackage(New Uri(pack), pkg)
    '	'	Dim doc As New XpsDocument(pkg, _XpsCompressionOption, pack)
    '	'	Dim rsm As New XpsSerializationManager(New XpsPackagingPolicy(doc), False)
    '	'	Dim paginator As DocumentPaginator = DirectCast(CreateFlowDocument(), IDocumentPaginatorSource).DocumentPaginator

    '	'	Dim rp As New MultipleReportPaginator1(Me, data)
    '	'	rsm.SaveAsXaml(rp)
    '	'	rsm.Commit()
    '	'	pkg.Close()
    '	'	Return New XpsDocument(fileName, FileAccess.Read)
    '	'End Function

    '#End Region



    ' ''' <summary>
    ' ''' Event occurs after a page has been completed
    ' ''' </summary>
    'Public Event GetPageCompleted As GetPageCompletedEventHandler ' = Nothing


    ' ''' <summary>
    ' ''' Fire event after a page has been completed
    ' ''' </summary>
    ' ''' <param name="ea">GetPageCompletedEventArgs</param>
    'Public Sub FireEventGetPageCompleted(ea As GetPageCompletedEventArgs)
    '	RaiseEvent GetPageCompleted(Me, ea)
    'End Sub





    '<Obsolete("Removing", True)>
    'Private Sub walker_VisualVisited(sender As Object, visitedObject As Object, start As Boolean)
    '	If Not (TypeOf visitedObject Is Image) Then Exit Sub

    '	Dim walker As DocumentWalker1 = TryCast(sender, DocumentWalker1)
    '	If walker Is Nothing Then Exit Sub

    '	Dim list As List(Of Image) = TryCast(walker.Tag, List(Of Image))
    '	If list Is Nothing Then Exit Sub

    '	list.Add(DirectCast(visitedObject, Image))
    'End Sub


    ''' <summary>
    ''' Helper method to create page header or footer from flow document template
    ''' </summary>
    ''' <returns></returns>
    Public Function CreateXpsDocument() As XpsDocument
        Dim ms As New MemoryStream()
        Dim pkg As Package = Package.Open(ms, FileMode.Create, FileAccess.ReadWrite)
        Dim pack As String = "pack://report.xps"
        PackageStore.RemovePackage(New Uri(pack))
        PackageStore.AddPackage(New Uri(pack), pkg)
        Dim doc As New XpsDocument(pkg, CompressionOption.NotCompressed, pack)
        Dim rsm As New XpsSerializationManager(New XpsPackagingPolicy(doc), False)
        Dim paginator As DocumentPaginator = DirectCast(CreateFlowDocument(), IDocumentPaginatorSource).DocumentPaginator

        Dim rp As New ReportPaginator(Me)
        rsm.SaveAsXaml(rp)
        Return doc
    End Function

    Overridable Function Get_Default_Template() As String
        Return "<FlowDocument/>"
    End Function


    ''' <summary>
    ''' Creates a flow document of the report data
    ''' </summary>
    ''' <returns></returns>
    ''' <exception cref="ArgumentException">Flow document must have a specified page height</exception>
    ''' <exception cref="ArgumentException">Flow document must have a specified page width</exception>
    ''' <exception cref="ArgumentException">"Flow document must have only one ReportProperties section, but it has {0}"</exception>
    Public Function CreateFlowDocument() As FlowDocument
        Dim mem As New MemoryStream()
        Dim buf As Byte() = Text.Encoding.UTF8.GetBytes(XamlData)
        mem.Write(buf, 0, buf.Length)
        mem.Position = 0
        Dim res As FlowDocument = TryCast(XamlReader.Load(mem), FlowDocument)

        If res.PageHeight = Double.NaN Then
            Throw New ArgumentException("Flow document must have a specified page height")
        End If
        If res.PageWidth = Double.NaN Then
            Throw New ArgumentException("Flow document must have a specified page width")
        End If

        ' remember original values
        _PageHeight = res.PageHeight
        _PageWidth = res.PageWidth

        Dim headers As List(Of SectionReportHeader) = DocWalker.Walk(Of SectionReportHeader)(res)
        Dim footers As List(Of SectionReportFooter) = DocWalker.Walk(Of SectionReportFooter)(res)

        ' make height smaller to have enough space for page header and page footer
        'res.PageHeight = _pageHeight - _pageHeight * (PageHeaderHeight + PageFooterHeight) / 100.0
        res.PageHeight = _PageHeight - _PageHeight * (
            If(headers.Count = 0, 0, headers.First.PageHeaderHeight) +
            If(footers.Count = 0, 0, footers.First.PageFooterHeight)) / 100.0

        Return res
    End Function

    <DebuggerStepThrough>
    Overridable Sub Show()
        'If Not Data.ReportDocumentValues.ContainsKey("PrintDate") Then Data.ReportDocumentValues.Add("PrintDate", DateTime.Now)
        If String.IsNullOrEmpty(ReportDate) Then ReportDate = DateTime.Today
        If Not DocumentValues.ContainsKey("PrintDate") Then DocumentValues.Add("PrintDate", ReportDate)
        'If Not Data.DocumentValues.ContainsKey("ReportTitle") Then Data.DocumentValues.Add("ReportTitle", Me.Data.ReportTitle)

        ShowHelper()
    End Sub


    <DebuggerStepThrough()>
    Protected Overridable Sub ShowHelper()
        Dim IsForm As New Forms.Form With {.WindowState = Forms.FormWindowState.Maximized}
        Dim this As New DocumentViewer With {.Document = CreateXpsDocument.GetFixedDocumentSequence}
        IsForm.Controls.Add(New Forms.Integration.ElementHost With {.Dock = Forms.DockStyle.Fill, .Child = this})
        IsForm.ShowDialog()
    End Sub


    '''' <summary>
    '''' Streams a single page of the report
    '''' </summary>
    '''' <param name="Page">The page you want to stream.</param>
    '''' <returns></returns>
    '<DebuggerNonUserCode()>
    'Overridable Function AsStream(Page As Integer) As MemoryStream
    '    Dim bitmapEncoder As New Media.Imaging.JpegBitmapEncoder
    '    Dim documentPage As DocumentPage = Me.CreateXpsDocument().GetFixedDocumentSequence.DocumentPaginator.GetPage(Page)
    '    Dim targetBitmap As New Media.Imaging.RenderTargetBitmap(documentPage.Size.Width * 5, documentPage.Size.Height * 5, 96.0 * 5, 96.0 * 5, Media.PixelFormats.Pbgra32)

    '    targetBitmap.Render(documentPage.Visual)
    '    bitmapEncoder.Frames.Add(Media.Imaging.BitmapFrame.Create(targetBitmap))

    '    Dim Stream As New MemoryStream
    '    bitmapEncoder.Save(Stream)
    '    Return Stream
    'End Function

    '''' <summary>
    '''' 
    '''' </summary>
    '''' <returns>The Full Name of the PDF File</returns>
    '''' <remarks></remarks>
    '''' <stepthrough></stepthrough>
    'Overridable Function AsPDF() As String
    '    Dim TempFile As String = My.Computer.FileSystem.GetTempFileName

    '    'TODO Make sure this Using Works Properly
    '    Using Stream As New IO.FileStream(TempFile, FileMode.Create)
    '        Dim oPdfDoc As New iTextSharp.text.Document()
    '        Dim oPdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(oPdfDoc, Stream)
    '        oPdfDoc.Open()

    '        If String.IsNullOrEmpty(ReportDate) Then ReportDate = DateTime.Today
    '        If Not DocumentValues.ContainsKey("PrintDate") Then DocumentValues.Add("PrintDate", ReportDate)
    '        For I = 0 To Me.CreateXpsDocument().GetFixedDocumentSequence.DocumentPaginator.PageCount - 1
    '            Base_Report_Temp.AddPage(True, AsStream(I), oPdfDoc, oPdfWriter)
    '        Next

    '        oPdfDoc.Close()
    '        oPdfWriter.Close()
    '    End Using

    '    Return TempFile
    'End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns>The Full Name of the PDF File</returns>
    ''' <remarks></remarks>
    ''' <stepthrough></stepthrough>
    Overridable Function AsPDF() As String
        If String.IsNullOrEmpty(ReportDate) Then ReportDate = DateTime.Today
        If Not DocumentValues.ContainsKey("PrintDate") Then DocumentValues.Add("PrintDate", ReportDate)
        Return Aaron.Xaml.Base_Report_Temp.AsPDF(Me.CreateXpsDocument())
    End Function






    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Hidden"></param>
    ''' <remarks></remarks>
    ''' <stepthrough></stepthrough>
    Overridable Sub Print(Hidden As Boolean)
        If String.IsNullOrEmpty(ReportDate) Then ReportDate = DateTime.Today
        If Not DocumentValues.ContainsKey("PrintDate") Then DocumentValues.Add("PrintDate", ReportDate)
        If Hidden Then
            Dim PD As New PrintDialog
            PD.PrintDocument(Me.CreateXpsDocument().GetFixedDocumentSequence.DocumentPaginator, Nothing)
        Else
            Dim this As New DocumentViewer With {.Document = Me.CreateXpsDocument().GetFixedDocumentSequence}
            this.Print()
        End If
    End Sub
End Class