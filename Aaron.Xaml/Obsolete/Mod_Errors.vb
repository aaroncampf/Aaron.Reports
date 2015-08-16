Imports System.Windows.Documents
Imports System.Windows.Controls

Public Module Mod_Errors

	' ''' <summary>
	' ''' Special event args for data row bound event
	' ''' </summary>
	'Public Class DataRowBoundEventArgs1
	'	Inherits EventArgs

	'	''' <summary>
	'	''' Gets the DataRow object being processed
	'	''' </summary>
	'	Public Property DataRow As DataRow

	'	''' <summary>
	'	''' Gets the associated ReportDocument
	'	''' </summary>
	'	Public Property ReportDocument As ReportDocument

	'	''' <summary>
	'	''' Gets or sets the table name
	'	''' </summary>
	'	Public Property TableName As String

	'	''' <summary>
	'	''' Gets or sets the newly created table row
	'	''' </summary>
	'	Public Property TableRow As TableRow


	'	' ''' <summary>
	'	' ''' Constructor
	'	' ''' </summary>
	'	'Public Sub New()
	'	'	Me.New(Nothing, Nothing)
	'	'End Sub

	'	''' <summary>
	'	''' Constructor
	'	''' </summary>
	'	''' <param name="report">associated report document</param>
	'	Public Sub New(Optional report As ReportDocument = Nothing, Optional row As DataRow = Nothing)
	'		ReportDocument = report
	'		DataRow = row
	'	End Sub

	'	' ''' <summary>
	'	' ''' Constructor
	'	' ''' </summary>
	'	' ''' <param name="report">associated report document</param>
	'	' ''' <param name="row">DataRow object being processed</param>
	'	'Public Sub New(report As ReportDocument, row As DataRow)
	'	'	ReportDocument = report
	'	'	DataRow = row
	'	'End Sub

	'End Class


	' ''' <summary>
	' ''' Special event args for image errors
	' ''' </summary>
	'Public Class ImageErrorEventArgs1
	'	Inherits EventArgs
	'	''' <summary>
	'	''' Gets the exception
	'	''' </summary>
	'	Public Property Exception As Exception

	'	''' <summary>
	'	''' Gets or sets the handled state. If handled is true the current image processing exception is 
	'	''' suppressed and report generation will continue.
	'	''' </summary>
	'	Public Property Handled As Boolean

	'	''' <summary>
	'	''' Gets the Image object being processed
	'	''' </summary>
	'	Public Property Image As Image

	'	''' <summary>
	'	''' Gets the associated ReportDocument
	'	''' </summary>
	'	Public Property ReportDocument As ReportDocument


	'	' ''' <summary>
	'	' ''' Constructor
	'	' ''' </summary>
	'	'Public Sub New()
	'	'	Me.New(Nothing, Nothing, Nothing)
	'	'End Sub

	'	' ''' <summary>
	'	' ''' Constructor
	'	' ''' </summary>
	'	' ''' <param name="exception">exception</param>
	'	'Public Sub New(Optional exception As Exception = Nothing)
	'	'	Me.New(exception, Nothing, Nothing)
	'	'End Sub

	'	''' <summary>
	'	''' Constructor
	'	''' </summary>
	'	''' <param name="exception">exception</param>
	'	''' <param name="report">associated report document</param>
	'	Public Sub New(Optional exception As Exception = Nothing, Optional report As ReportDocument = Nothing, Optional Image As Image = Nothing)
	'		'Me.New(exception, report, Nothing)
	'		Me.Exception = exception
	'		Me.Image = Image
	'		Handled = False
	'		ReportDocument = report
	'	End Sub

	'	' ''' <summary>
	'	' ''' Constructor
	'	' ''' </summary>
	'	' ''' <param name="exception">exception</param>
	'	' ''' <param name="report">associated report document</param>
	'	' ''' <param name="image">image object being processed</param>
	'	'Public Sub New(exception As Exception, report As ReportDocument, Image As Image)
	'	'	Me.Exception = exception
	'	'	Me.Image = Image
	'	'	Handled = False
	'	'	ReportDocument = report
	'	'End Sub

	'End Class


	''' <summary>
	''' Special event args for image processing events
	''' </summary>
	<Obsolete("Removing", True)>
	Public Class ImageEventArgs1
		Inherits EventArgs
		''' <summary>
		''' Gets the Image object being processed
		''' </summary>
		Public Property Image As Image

		''' <summary>
		''' Gets the associated ReportDocument
		''' </summary>
		Public Property ReportDocument As ReportDocument


		' ''' <summary>
		' ''' Constructor
		' ''' </summary>
		'Public Sub New()
		'	Me.New(Nothing, Nothing)
		'End Sub

		''' <summary>
		''' Constructor
		''' </summary>
		''' <param name="report">associated report document</param>
		Public Sub New(Optional report As ReportDocument = Nothing, Optional Image As Image = Nothing)
			'Me.New(report, Nothing)
			ReportDocument = report
			Me.Image = Image
		End Sub

		' ''' <summary>
		' ''' Constructor
		' ''' </summary>
		' ''' <param name="report">associated report document</param>
		' ''' <param name="Image">Image object being processed</param>
		'Public Sub New(report As ReportDocument, Image As Image)
		'	ReportDocument = report
		'	Me.Image = Image
		'End Sub

	End Class


End Module
