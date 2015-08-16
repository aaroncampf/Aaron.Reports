'Imports System.Windows.Documents

' ''' <summary>
' ''' Paginator to concat multiple reports
' ''' </summary>
'<Obsolete("Try to Remove", False)>
'Public Class MultipleReportPaginator1
'	Inherits DocumentPaginator
'	Private _reportPaginators As New List(Of ReportPaginator)()
'	Private _firstPages As New List(Of DocumentPage)()

'	''' <summary>
'	''' Constructor
'	''' </summary>
'	''' <param name="report">report document</param>
'	''' <param name="data">multiple report data</param>
'	''' <exception cref="ArgumentException">Need at least two ReportData objects</exception>
'	Public Sub New(report As ReportDocument, data As IEnumerable(Of ReportData))
'		If data Is Nothing Then
'			Throw New ArgumentException("Need at least two ReportData objects")
'		End If

'		' create a list of report paginators and compute page counts
'		_pageCount = 0
'		Dim dataCount As Integer = 0
'		'For Each rd As ReportData1 In data
'		For Each rd In data
'			Dim paginator As New ReportPaginator(report, rd)
'			_reportPaginators.Add(paginator)
'			Dim dp As DocumentPage = paginator.GetPage(0)
'			'If (dp <> DocumentPage.Missing) AndAlso (dp.Size <> Windows.Size.Empty) Then
'			If Not dp.Equals(DocumentPage.Missing) AndAlso (dp.Size <> Windows.Size.Empty) Then
'				_PageSize = paginator.PageSize
'			End If
'			_firstPages.Add(dp)
'			' just cache the generated first page
'			_pageCount += paginator.PageCount
'			dataCount += 1
'		Next
'		If (_reportPaginators.Count <= 0) OrElse (dataCount < 2) Then
'			Throw New ArgumentException("Need at least two ReportData objects")
'		End If
'	End Sub

'	''' <summary>
'	''' Gets a document page of the appropriate generated report
'	''' </summary>
'	''' <param name="pageNumber">page number</param>
'	''' <returns>parsed DocumentPage</returns>
'	Public Overrides Function GetPage(pageNumber As Integer) As DocumentPage
'		' find the appropriate paginator for the page
'		Dim currentPage As Integer = 0
'		Dim paginatorIndex As Integer = 0
'		Dim pagePaginator As ReportPaginator = Nothing
'		For Each paginator As ReportPaginator In _reportPaginators
'			Dim pageCount As Integer = paginator.PageCount
'			If pageNumber >= currentPage + pageCount Then
'				currentPage += pageCount
'				paginatorIndex += 1
'				Continue For
'			End If
'			pagePaginator = paginator
'			Exit For
'		Next
'		If pagePaginator Is Nothing Then
'			Return DocumentPage.Missing
'		End If

'		Dim dp As DocumentPage = Nothing
'		If pageNumber = 0 Then
'			dp = _firstPages(paginatorIndex)
'		Else
'			dp = pagePaginator.GetPage(pageNumber - currentPage)
'		End If
'		'If dp = DocumentPage.Missing Then
'		If dp.Equals(DocumentPage.Missing) Then
'			Return DocumentPage.Missing
'		End If
'		_PageSize = dp.Size
'		Return dp
'	End Function

'	''' <summary>
'	''' Determines if the current page count is valid
'	''' </summary>
'	Public Overrides ReadOnly Property IsPageCountValid() As Boolean
'		Get
'			Return True
'		End Get
'	End Property

'	Private _pageCount As Integer = 0
'	''' <summary>
'	''' Gets the total page count
'	''' </summary>
'	Public Overrides ReadOnly Property PageCount() As Integer
'		Get
'			Return _pageCount
'		End Get
'	End Property

'	''' <summary>
'	''' Gets or sets the page size
'	''' </summary>
'	Public Overrides Property PageSize() As Windows.Size


'	''' <summary>
'	''' We don't have only one paginator source
'	''' </summary>
'	Public Overrides ReadOnly Property Source() As IDocumentPaginatorSource
'		Get
'			Return Nothing
'		End Get
'	End Property
'End Class
