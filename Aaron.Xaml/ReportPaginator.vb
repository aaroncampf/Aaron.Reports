Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Documents
Imports System.Windows.Markup
Imports System.Windows.Media

Public Class ReportPaginator
	Inherits DocumentPaginator

#Region "       Properties     >>>"

	''' <summary>
	''' Reference to a original flowdoc paginator
	''' </summary>
	Protected _paginator As DocumentPaginator

	Protected _flowDocument As FlowDocument
	Protected _report As ReportDocument
	'Protected _data As ReportData
	'Protected _blockPageHeader As Block
	'Protected _blockPageFooter As Block

	Protected _blockPageHeader As SectionReportHeader
	Protected _blockPageFooter As SectionReportFooter


	Protected _reportContextValues As ArrayList
	'<Obsolete("Being Removed", False)>
	'Protected _dynamicCache As ReportPaginatorDynamicCache1
	Protected _dynamicCache As DocSuperWalker


	''' <summary>
	''' Determines if the current page count is valid
	''' </summary>
	Public Overrides ReadOnly Property IsPageCountValid As Boolean
		Get
			Return _paginator.IsPageCountValid
		End Get
	End Property

	Private _pageCount As Integer ' = 0
	''' <summary>
	''' Gets the total page count
	''' </summary>
	Public Overrides ReadOnly Property PageCount As Integer
		Get
			Return _pageCount
		End Get
	End Property


	''' <summary>
	''' Gets or sets the page size
	''' </summary>
	Public Overrides Property PageSize As Size = Size.Empty


	''' <summary>
	''' Gets the paginator source
	''' </summary>
	Public Overrides ReadOnly Property Source() As IDocumentPaginatorSource
		Get
			Return _paginator.Source
		End Get
	End Property


	'Protected Property PageHeaderHeight As Double

	'Protected Property PageFooterHeight As Double


#End Region

	''' <summary>
	''' Constructor
	''' </summary>
	''' <param name="report">report document</param>
	''' <exception cref="ArgumentException">Flow document must have a specified page height</exception>
	''' <exception cref="ArgumentException">Flow document must have a specified page width</exception>
	''' <exception cref="ArgumentException">Flow document can have only one report header section</exception>
	''' <exception cref="ArgumentException">Flow document can have only one report footer section</exception>
	Public Sub New(report As ReportDocument)
		_report = report
		_flowDocument = report.CreateFlowDocument()
		_PageSize = New Size(_flowDocument.PageWidth, _flowDocument.PageHeight)

		If _flowDocument.PageHeight = Double.NaN Then
			Throw New ArgumentException("Flow document must have a specified page height")
		End If
		If _flowDocument.PageWidth = Double.NaN Then
			Throw New ArgumentException("Flow document must have a specified page width")
		End If

		_dynamicCache = New DocSuperWalker(_flowDocument)
		Dim listPageHeaders As ArrayList = _dynamicCache.GetFlowDocumentVisualListByType(GetType(SectionReportHeader))
		Dim listPageFooters As ArrayList = _dynamicCache.GetFlowDocumentVisualListByType(GetType(SectionReportFooter))
		If listPageHeaders.Count > 1 Then
			Throw New ArgumentException("Flow document can have only one report header section")
		ElseIf listPageHeaders.Count = 1 Then
			_blockPageHeader = DirectCast(listPageHeaders(0), SectionReportHeader)
		End If
		If listPageFooters.Count > 1 Then
			Throw New ArgumentException("Flow document can have only one report footer section")
		ElseIf listPageFooters.Count = 1 Then
			_blockPageFooter = DirectCast(listPageFooters(0), SectionReportFooter)
		End If

		_paginator = DirectCast(_flowDocument, IDocumentPaginatorSource).DocumentPaginator

		' remove header and footer in our working copy
		Dim block As Block = _flowDocument.Blocks.FirstBlock
		While block IsNot Nothing
			Dim thisBlock As Block = block
			block = block.NextBlock
			If thisBlock.Equals(_blockPageHeader) OrElse thisBlock.Equals(_blockPageFooter) Then
				_flowDocument.Blocks.Remove(thisBlock)
			End If
		End While

		_reportContextValues = _dynamicCache.GetFlowDocumentVisualListByType(GetType(InlineContextValue))
		FillData()
	End Sub


	''' <summary>
	''' Fills document with data
	''' </summary>
	Protected Overridable Sub FillData()
		Dim blockDocumentValues As ArrayList = _dynamicCache.GetFlowDocumentVisualListByType(GetType(InlineDocumentValue))
		Dim blocks As New List(Of Block)()
		If _blockPageHeader IsNot Nothing Then
			blocks.Add(_blockPageHeader)
		End If
		If _blockPageFooter IsNot Nothing Then
			blocks.Add(_blockPageFooter)
		End If
		blockDocumentValues.AddRange(DocWalker.TraverseBlockCollection(Of InlineDocumentValue)(blocks))

		' fill report values
		For Each dv As InlineDocumentValue In blockDocumentValues
			If dv Is Nothing Then Continue For
			Dim obj As Object = Nothing

			If dv.PropertyName IsNot Nothing AndAlso _report.DocumentValues.TryGetValue(dv.PropertyName, obj) Then
				dv.Value = obj
			ElseIf (_report.ShowUnknownValues) AndAlso (dv.Value Is Nothing) Then
				dv.Value = "[" & (If((dv.PropertyName IsNot Nothing), dv.PropertyName, "NULL")) & "]"
			End If
		Next
	End Sub

	Private Function CloneVisualBlock(block As Block, pageNumber As Integer) As ContainerVisual
		Dim tmpDoc As New FlowDocument() With
			{.ColumnWidth = Double.PositiveInfinity, .PageHeight = _report.PageHeight, .PageWidth = _report.PageWidth, .PagePadding = New Thickness(0)}

		Dim xaml As String = XamlWriter.Save(block)
		Dim newBlock As Block = TryCast(XamlReader.Parse(xaml), Block)
		tmpDoc.Blocks.Add(newBlock)

		Dim blockValues As New ArrayList()
		blockValues.AddRange(DocWalker.Walk(Of InlineContextValue)(tmpDoc))

		FillContextValues(blockValues, pageNumber) ' fill context values

		Dim dp As DocumentPage = DirectCast(tmpDoc, IDocumentPaginatorSource).DocumentPaginator.GetPage(0)
		Return DirectCast(dp.Visual, ContainerVisual)
	End Function

	''' <summary>
	''' Fills in Context Vales
	''' </summary>
	''' <param name="list"></param>
	''' <param name="pageNumber"></param>
	''' <remarks></remarks>
	''' <stepthrough></stepthrough>
	Protected Overridable Sub FillContextValues(list As ArrayList, pageNumber As Integer)
		For Each cv As InlineContextValue In list
			If cv Is Nothing Then Continue For
			Dim reportContextValueType__1 As ReportContextValues.Types? = ReportContextValues.GetReportContextValueTypeByName(cv.PropertyName)
			If reportContextValueType__1 Is Nothing Then
				If _report.ShowUnknownValues Then
					cv.Value = "<" & (If((cv.PropertyName IsNot Nothing), cv.PropertyName, "NULL")) & ">"
				Else
					cv.Value = ""
				End If
			Else
				Select Case reportContextValueType__1.Value
					Case ReportContextValues.Types.PageNumber
						cv.Value = pageNumber
					Case ReportContextValues.Types.PageCount
						cv.Value = _pageCount
					Case ReportContextValues.Types.ReportName
						cv.Value = _report.ReportName
					Case ReportContextValues.Types.ReportTitle
						cv.Value = _report.ReportTitle
					Case ReportContextValues.Types.ReportDate
						cv.Value = _report.ReportDate
				End Select
			End If
		Next
	End Sub

	''' <summary>
	''' This is most important method, modifies the original 
	''' </summary>
	''' <param name="pageNumber">page number</param>
	''' <returns></returns>
	Public Overrides Function GetPage(pageNumber As Integer) As DocumentPage
		For i As Integer = 0 To 1
			' do it twice because filling context values could change the page count
			' compute page count
			If pageNumber = 0 Then
				_paginator.ComputePageCount()
				_pageCount = _paginator.PageCount
			End If

			' fill context values
			FillContextValues(_reportContextValues, pageNumber + 1)
		Next

		Dim page As DocumentPage = _paginator.GetPage(pageNumber)
		If page.Equals(DocumentPage.Missing) Then 'If page = DocumentPage.Missing Then
			Return DocumentPage.Missing
		Else
			_PageSize = page.Size
		End If

		' add header block
		Dim newPage As New ContainerVisual()

		If _blockPageHeader IsNot Nothing Then
			Dim v As ContainerVisual = CloneVisualBlock(_blockPageHeader, pageNumber + 1)
			v.Offset = New Vector(0, 0)
			newPage.Children.Add(v)
		End If

		' TODO: process ReportContextValues

		' add content page
		Dim smallerPage As New ContainerVisual
		'smallerPage.Offset = New Vector(0, _report.PageHeaderHeight / 100.0 * _report.PageHeight)
		'TODO: Make sure this works 100% right
		If _blockPageHeader Is Nothing Then
			smallerPage.Offset = New Vector(0, 0)
		Else
			smallerPage.Offset = New Vector(0, _blockPageHeader.PageHeaderHeight / 100.0 * _report.PageHeight)
		End If

		smallerPage.Children.Add(page.Visual)
		newPage.Children.Add(smallerPage)

		' add footer block
		If _blockPageFooter IsNot Nothing Then
			'Here is the Problem!!!!
			Dim v As ContainerVisual = CloneVisualBlock(_blockPageFooter, pageNumber + 1)
			'v.Offset = New Vector(0, _report.PageHeight - _report.PageFooterHeight / 100.0 * _report.PageHeight)
			v.Offset = New Vector(0, _report.PageHeight - _blockPageFooter.PageFooterHeight / 100.0 * _report.PageHeight)
			newPage.Children.Add(v)
		End If

		' create modified BleedBox
		Dim bleedBox As New Rect(page.BleedBox.Left, page.BleedBox.Top, page.BleedBox.Width, _report.PageHeight - (page.Size.Height - page.BleedBox.Size.Height))

		' create modified ContentBox
		Dim contentBox As New Rect(page.ContentBox.Left, page.ContentBox.Top, page.ContentBox.Width, _report.PageHeight - (page.Size.Height - page.ContentBox.Size.Height))

		Dim dp As New DocumentPage(newPage, New Size(_report.PageWidth, _report.PageHeight), bleedBox, contentBox)
		'_report.FireEventGetPageCompleted(New GetPageCompletedEventArgs(page, pageNumber, Nothing, False, Nothing))
		Return dp
	End Function

End Class
