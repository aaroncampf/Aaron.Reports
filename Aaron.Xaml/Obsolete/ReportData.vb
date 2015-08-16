''' <summary>
''' TODO Add more Properties
''' </summary>
''' <remarks></remarks>
''' <features></features>
''' <stepthrough></stepthrough>
<Obsolete("In-lining", True)>
Public Class ReportData

	''' <summary>
	''' 
	''' </summary>
	''' <value></value>
	''' <remarks></remarks>
	Public Property ReportDate As String

	' ''' <summary>
	' ''' 
	' ''' </summary>
	' ''' <value></value>
	' ''' <remarks></remarks>
	'Public Property ReportTitle As String


	''' <summary>
	''' Gets a list of document values
	''' </summary>
	Public Property DocumentValues As New Dictionary(Of String, Object)


	''' <summary>
	''' Shows all unknown values on the page
	''' </summary>
	Public Property ShowUnknownValues As Boolean

End Class
