''' <summary>
''' Contains all report data
''' </summary>
<Obsolete("Replacing with ReportData", True)>
Public Class ReportData1
	Private _reportDocumentValues As New Dictionary(Of String, Object)()
	''' <summary>
	''' Gets a list of document values
	''' </summary>
	Public ReadOnly Property ReportDocumentValues() As Dictionary(Of String, Object)
		Get
			Return _reportDocumentValues
		End Get
	End Property

	Private _dataTables As New List(Of DataTable)()
	''' <summary>
	''' Gets a list of data tables
	''' </summary>
	Public ReadOnly Property DataTables() As List(Of DataTable)
		Get
			Return _dataTables
		End Get
	End Property

	''' <summary>
	''' Shows all unknown values on the page
	''' </summary>
	Public Property ShowUnknownValues As Boolean


	''' <summary>
	''' Gets a data table by table name
	''' </summary>
	''' <param name="tableName">table name (case insensitive)</param>
	''' <returns>null, if DataTable not found</returns>
	Public Function GetDataTableByName(tableName As String) As DataTable
		For Each table As DataTable In _dataTables
			If table Is Nothing Then
				Continue For
			End If
			If table.TableName Is Nothing Then
				Continue For
			End If
			If table.TableName.Equals(tableName, System.StringComparison.InvariantCultureIgnoreCase) Then
				Return table
			End If
		Next
		Return Nothing
	End Function

	''' <summary>
	''' Sets all DataRow values to document values
	''' </summary>
	''' <param name="dataRow">data row containing the document values</param>
	''' <param name="prefix">add prefix to name</param>
	Public Sub SetDataRowValuesToDocumentValues(dataRow As DataRow, prefix As String)
		If prefix Is Nothing Then
			prefix = ""
		End If
		For Each column As DataColumn In dataRow.Table.Columns
			_reportDocumentValues(prefix + column.ColumnName) = dataRow(column)
		Next
	End Sub

	''' <summary>
	''' Sets all DataRow values to document values
	''' </summary>
	''' <param name="dataRow">data row containing the document values</param>
	Public Sub SetDataRowValuesToDocumentValues(dataRow As DataRow)
		SetDataRowValuesToDocumentValues(dataRow, "")
	End Sub

	''' <summary>
	''' Sets all DataRow values to document values
	''' </summary>
	''' <param name="dataRowView">data row containing the document values</param>
	''' <param name="prefix">add prefix to name</param>
	Public Sub SetDataRowValuesToDocumentValues(dataRowView As DataRowView, prefix As String)
		If prefix Is Nothing Then
			prefix = ""
		End If
		For Each column As DataColumn In dataRowView.Row.Table.Columns
			_reportDocumentValues(prefix + column.ColumnName) = dataRowView.Row(column)
		Next
	End Sub

	''' <summary>
	''' Sets all DataRow values to document values
	''' </summary>
	''' <param name="dataRowView">data row containing the document values</param>
	Public Sub SetDataRowValuesToDocumentValues(dataRowView As DataRowView)
		SetDataRowValuesToDocumentValues(dataRowView, "")
	End Sub
End Class
