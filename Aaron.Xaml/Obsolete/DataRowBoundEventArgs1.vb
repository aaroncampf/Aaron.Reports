Imports System.Windows.Documents

''' <summary>
''' Special event args for data row bound event
''' </summary>
<Obsolete("Removing", True)>
Public Class DataRowBoundEventArgs1
	Inherits EventArgs

	''' <summary>
	''' Gets the DataRow object being processed
	''' </summary>
	Public Property DataRow As DataRow

	''' <summary>
	''' Gets the associated ReportDocument
	''' </summary>
	Public Property ReportDocument As ReportDocument

	''' <summary>
	''' Gets or sets the table name
	''' </summary>
	Public Property TableName As String


	''' <summary>
	''' Gets or sets the newly created table row
	''' </summary>
	Public Property TableRow As TableRow


	' ''' <summary>
	' ''' Constructor
	' ''' </summary>
	'Public Sub New()
	'	Me.New(Nothing, Nothing)
	'End Sub

	' ''' <summary>
	' ''' Constructor
	' ''' </summary>
	' ''' <param name="report">associated report document</param>
	'Public Sub New(report As ReportDocument)
	'	Me.New(report, Nothing)
	'End Sub

	''' <summary>
	''' Constructor
	''' </summary>
	''' <param name="report">associated report document</param>
	''' <param name="row">DataRow object being processed</param>
	Public Sub New(Optional report As ReportDocument = Nothing, Optional row As DataRow = Nothing)
		ReportDocument = report
		DataRow = row
	End Sub
End Class
