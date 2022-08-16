Imports System.IO
Imports System.IO.Packaging
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Markup
Imports System.Windows.Xps.Packaging
Imports System.Windows.Xps.Serialization

''' <summary>
''' Contains a complete report template without data
''' </summary>
Public Class Basic

#Region "       Properties	>>>"
	Public Const XML_Namespace = "xmlns:xrd=""clr-namespace:Aaron.Xaml;assembly=Aaron.Xaml"""

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
	Friend ReadOnly Property PageHeight_Remember As Double = Double.NaN

	''' <summary>
	''' Gets the original page width of the FlowDocument
	''' </summary>
	Friend ReadOnly Property PageWidth_Remember As Double = Double.NaN

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

	Overridable Property CustomXAML As String

	''' <summary>The Sections that make up the Report</summary> 
	Property Sections As New List(Of Sections.Base)


	''' <summary>The Title of the Report</summary> 
	ReadOnly Property Title As Documents.Paragraph

	''' <summary>An Optional Paragraph Detailing the Report</summary> 
	ReadOnly Property Details As Documents.Paragraph


	Dim _Resources As XElement
	Overridable ReadOnly Property Resources As XElement
		Get
			If _Resources Is Nothing Then
				_Resources =
						<FlowDocument.Resources>
							<!-- Style for header/footer rows. -->
							<Style Key="headerFooterRowStyle" TargetType="{x:Type TableRowGroup}">
								<Setter Property="FontWeight" Value="DemiBold"/>
								<Setter Property="FontSize" Value="16"/>
								<Setter Property="Background" Value="LightGray"/>
							</Style>

							<!-- Style for data rows. -->
							<Style Key="dataRowStyle" TargetType="{x:Type TableRowGroup}">
								<Setter Property="FontSize" Value="12"/>
							</Style>

							<!-- Style for data cells. -->
							<Style TargetType="{x:Type TableCell}">
								<Setter Property="Padding" Value="0.1cm"/>
								<Setter Property="BorderBrush" Value="Black"/>
								<Setter Property="BorderThickness" Value="0.01cm"/>
							</Style>

							<!-- Style for Table Headers [AKA the First Row in the Table. -->
							<Style Key="TableHeader" TargetType="{x:Type TableRow}">
								<Setter Property="FontWeight" Value="DemiBold"/>
								<Setter Property="FontSize" Value="16"/>
								<Setter Property="Background" Value="LightGray"/>
							</Style>

						</FlowDocument.Resources>
			End If
			Return _Resources
		End Get
	End Property

	''' <summary>The Text for the Bottom Left of the Page</summary> 
	Property Bottom_Left As New Documents.Paragraph

	''' <summary>The Text for the Bottom Right of the Page</summary> 
	Property Bottom_Right As New Documents.Paragraph

	''' <summary>The page height for non-CustomXAML</summary> 
	Public Property PageHeight As String = "29.7cm"

	''' <summary>The page width for non-CustomXAML</summary> 
	Public Property PageWidth As String = "21cm"


#End Region

#Region "       Constructors	>>>"

	Sub New()
	End Sub

	''' <summary>
	''' 
	''' </summary>  
	''' <param name="Title">The Title of the Report</param>
	''' <param name="Details">The Details of the Report</param>
	''' <param name="Paragraph_Text_Alignment"></param>
	''' <remarks></remarks>
	''' <stepthrough>Enabled</stepthrough>
	<DebuggerNonUserCode()>
	Sub New(Optional Title As String = "", Optional Details As String = "", Optional Paragraph_Text_Alignment As TextAlignment = TextAlignment.Left)
		_Title = New Documents.Paragraph(New Documents.Run(Title)) With {.FontSize = 24, .TextAlignment = TextAlignment.Center, .FontWeight = FontWeights.Bold}
		_Details = New Documents.Paragraph(New Documents.Run(Details)) With {.FontSize = 12, .TextAlignment = Paragraph_Text_Alignment}
	End Sub


	''' <summary>
	''' 
	''' </summary>
	''' <param name="Title">The Title of the Report</param>
	''' <param name="Details">The Details of the Report</param>
	''' <param name="Sections"></param>
	''' <remarks></remarks>
	''' <stepthrough>Enabled</stepthrough>
	<DebuggerNonUserCode()>
	Sub New(Title As String, Details As String, ParamArray Sections As Sections.Base())
		_Title = New Documents.Paragraph(New Documents.Run(Title)) With {.FontSize = 24, .TextAlignment = TextAlignment.Center, .FontWeight = FontWeights.Bold}
		_Details = New Documents.Paragraph(New Documents.Run(Details)) With {.FontSize = 12, .TextAlignment = TextAlignment.Center}
		Me.Sections.AddRange(Sections)
	End Sub

#End Region

#Region "       As PDF		>>>"

	''' <summary>
	''' Creates a PDF and saves it to a temporary file and returns that files name.
	''' </summary>
	''' <param name="Document"></param>
	''' <returns></returns>
	Public Shared Function AsPDF(Document As XpsDocument) As String
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


	Private Shared Function AsStream(Document As XpsDocument, Page As Integer) As IO.MemoryStream
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

#Region "       Methods		>>>"

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
		Dim buf As Byte() = Text.Encoding.UTF8.GetBytes(GetString())
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
		_PageHeight_Remember = res.PageHeight
		_PageWidth_Remember = res.PageWidth

		Dim headers As List(Of SectionReportHeader) = DocWalker.Walk(Of SectionReportHeader)(res)
		Dim footers As List(Of SectionReportFooter) = DocWalker.Walk(Of SectionReportFooter)(res)

		' make height smaller to have enough space for page header and page footer
		'res.PageHeight = _pageHeight - _pageHeight * (PageHeaderHeight + PageFooterHeight) / 100.0
		res.PageHeight = _PageHeight_Remember - _PageHeight_Remember * (
			If(headers.Count = 0, 0, headers.First.PageHeaderHeight) +
			If(footers.Count = 0, 0, footers.First.PageFooterHeight)) / 100.0

		Return res
	End Function

	'<DebuggerStepThrough>
	Overridable Sub Show()
		'If Not Data.ReportDocumentValues.ContainsKey("PrintDate") Then Data.ReportDocumentValues.Add("PrintDate", DateTime.Now)
		If String.IsNullOrEmpty(ReportDate) Then ReportDate = DateTime.Today
		If Not DocumentValues.ContainsKey("PrintDate") Then DocumentValues.Add("PrintDate", ReportDate)
		'If Not Data.DocumentValues.ContainsKey("ReportTitle") Then Data.DocumentValues.Add("ReportTitle", Me.Data.ReportTitle)
		ShowHelper(CreateXpsDocument)
	End Sub

	''' <summary>
	''' Creates a PDF and saves it to a temporary file and returns that files name.
	''' </summary>
	''' <returns>The Full Name of the PDF File</returns>
	''' <remarks></remarks>
	''' <stepthrough></stepthrough>
	Overridable Function AsPDF() As String
		If String.IsNullOrEmpty(ReportDate) Then ReportDate = DateTime.Today
		If Not DocumentValues.ContainsKey("PrintDate") Then DocumentValues.Add("PrintDate", ReportDate)
		Return AsPDF(Me.CreateXpsDocument())
	End Function


	''' <summary>
	''' Prints this document automatically or with a <see cref="PrintDialog">PrintDialog</see>
	''' </summary>
	''' <param name="Hidden">If <c>True</c> Print the document immediately <c>Else</c> show the user a <see cref="PrintDialog">PrintDialog</see> for printing purposes</param>
	Overridable Sub Print(Hidden As Boolean)
		If String.IsNullOrEmpty(ReportDate) Then ReportDate = DateTime.Today
		If Not DocumentValues.ContainsKey("PrintDate") Then DocumentValues.Add("PrintDate", ReportDate)
		Print(Hidden, Me.CreateXpsDocument())
	End Sub


	''' <summary>
	''' 
	''' </summary>
	''' <returns></returns>
	''' <remarks>
	''' Aaron Campf: 6/7/2013 Added [Formatted_Sections]
	''' </remarks>
	''' <stepthrough>Disabled</stepthrough>
	Overridable Function GetString() As String
		'Remember about Using <Bold>Text</Bold> and <Italic>Text</Italic>
		If Not String.IsNullOrWhiteSpace(Me.CustomXAML) Then Return Me.CustomXAML

		Dim Formatted_Sections As New List(Of XElement) 'Aaron Campf: 6/7/2013 Added [Formatted_Sections]
		For Each Item In Me.Sections
			Formatted_Sections.Add(Item.ToXML)

			If Item.BreakPageAfter Then
				Formatted_Sections.Add(<Section BreakPageBefore="true"/>)
			End If
		Next

		Dim IsXML =
			<FlowDocument
				xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
				xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
				xmlns:xrd="clr-namespace:Aaron.Reports;assembly=Aaron.Reports"
				PageHeight=<%= PageHeight %> PageWidth=<%= PageWidth %> ColumnWidth=<%= PageWidth %>>
				<!--
                xmlns:xrd="clr-namespace:Aaron.Xaml;assembly=Aaron.Xaml"
                xmlns:crcv="clr-namespace:CodeReason.Reports.Charts.Visifire;assembly=CodeReason.Reports.Charts.Visifire"
                -->

				<%= Me.Resources %>

				<SectionReportHeader PageHeaderHeight="2" Padding="10,10,10,0" FontSize="12">
					<Table>
						<TableRowGroup>
							<TableRow>
								<TableCell>
									<Paragraph>
                                                Page
                                                <InlineContextValue PropertyName="PageNumber" FontWeight="Bold"/> of
                                                <InlineContextValue PropertyName="PageCount" FontWeight="Bold"/>
									</Paragraph>
								</TableCell>
								<TableCell>
									<Paragraph TextAlignment="Right">
										<InlineDocumentValue PropertyName=<%= Today.ToShortDateString %> Format="dd.MM.yyyy"/>
									</Paragraph>
								</TableCell>
							</TableRow>
						</TableRowGroup>
					</Table>
				</SectionReportHeader>

				<Section Padding="40,10,40,10" FontSize="12">
					<%= Me.Title?.ToXML %>
					<!--
                    <%= If(CType(_Details.Inlines(0), Windows.Documents.Run).Text.Length > 0, Me.Details.ToXML, Nothing) %>
                    -->
					<%= Me.Details?.ToXML %>
				</Section>

				<%= From x In Formatted_Sections %>
				<SectionReportFooter PageFooterHeight="2" Padding="10,0,10,10" FontSize="12">
					<Table>
						<TableRowGroup>
							<TableRow>
								<TableCell>
									<%= Me.Bottom_Left.ToXML %>
								</TableCell>
								<TableCell>
									<%= Me.Bottom_Right.ToXML %>
								</TableCell>
							</TableRow>
						</TableRowGroup>
					</Table>
				</SectionReportFooter>
			</FlowDocument>


		Dim Builder As New Text.StringBuilder(IsXML.ToString)


		Builder.Replace("ReportProperties", "xrd:ReportProperties")
		Builder.Replace("SectionReport", "xrd:SectionReport")
		Builder.Replace("InlineContextValue", "xrd:InlineContextValue")
		Builder.Replace("InlineDocumentValue", "xrd:InlineDocumentValue")
		Builder.Replace("<Style Key", "<Style x:Key")
		Builder.Replace("SectionDataGroup", "xrd:SectionDataGroup")
		Builder.Replace("TableRowFor", "xrd:TableRowFor")

		Builder.Replace("xmlns=""""", "")
		Return Builder.ToString
	End Function

	''' <summary>
	''' Prints the <paramref name="Document"/> automatically or with a <see cref="PrintDialog">PrintDialog</see>
	''' </summary>
	''' <param name="Hidden">If <c>True</c> Print the document immediately <c>Else</c> show the user a <see cref="PrintDialog">PrintDialog</see> for printing purposes</param>
	''' <param name="Document">The document you want to print</param>
	Public Shared Sub Print(Hidden As Boolean, Document As XpsDocument)
		If Hidden Then
			Dim PD As New PrintDialog
			PD.PrintDocument(Document.GetFixedDocumentSequence.DocumentPaginator, Nothing)
		Else
			Dim this As New DocumentViewer With {.Document = Document.GetFixedDocumentSequence}
			this.Print()
		End If
	End Sub

	Public Shared Sub ShowHelper(Document As XpsDocument)
		Dim IsForm As New Forms.Form With {.WindowState = Forms.FormWindowState.Maximized}
		Dim this As New DocumentViewer With {.Document = Document.GetFixedDocumentSequence}
		IsForm.Controls.Add(New Forms.Integration.ElementHost With {.Dock = Forms.DockStyle.Fill, .Child = this})
		IsForm.ShowDialog()
	End Sub

#End Region

End Class