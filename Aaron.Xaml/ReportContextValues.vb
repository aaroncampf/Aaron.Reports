''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
''' <features></features>
''' <stepthrough></stepthrough>
Friend NotInheritable Class ReportContextValues
	Private Shared _reportContextValueTypes As Dictionary(Of String, Types)
	Private Sub New()
	End Sub

	''' <summary>
	''' Enumeration of all available context value types
	''' </summary>
	Public Enum Types
		''' <summary>
		''' Current page number
		''' </summary>
		PageNumber
		''' <summary>
		''' Total page count
		''' </summary>
		PageCount
		''' <summary>
		''' Report name
		''' </summary>
		ReportName
		''' <summary>
		''' Report title
		''' </summary>
		ReportTitle
		''' <summary>
		''' The Default Date associated with the Report
		''' </summary>
		ReportDate

	End Enum 'TODO: Add more Features Here!



	''' <summary>
	''' Static constructor
	''' </summary>
	Shared Sub New()
		' add static cache for report context value names
		_reportContextValueTypes = New Dictionary(Of String, Types)(20)
		For Each fi As Reflection.FieldInfo In GetType(Types).GetFields()
			If (CInt(fi.Attributes) And CInt(Reflection.FieldAttributes.[Static])) = 0 Then
				Continue For
			End If
			_reportContextValueTypes.Add(fi.Name.ToLowerInvariant(), DirectCast(fi.GetRawConstantValue(), Types))
		Next
	End Sub

	''' <summary>
	''' Gets a report context value type by name
	''' </summary>
	''' <param name="name">name of report context value</param>
	''' <returns>null, if it does not exist</returns>
	Public Shared Function GetReportContextValueTypeByName(name As String) As Types?
		If name Is Nothing Then Return Nothing

		name = name.ToLowerInvariant()
		If Not _reportContextValueTypes.ContainsKey(name) Then
			Return Nothing
		End If
		Return _reportContextValueTypes(name)
	End Function
End Class
