Namespace Sections
	''' <summary>
	'''
	''' </summary>
	''' <remarks></remarks>
	''' <features></features>
	''' <stepthrough></stepthrough>
	Public Class Table
		Inherits Base
		''' <summary>The Column Header Row's values are Created from each TableColumn's Tag</summary>
		Public Property Table As New Documents.Table

		''' <summary>
		'''
		''' </summary>
		''' <param name="Title">The Title of the Section</param>
		''' <param name="Details">The Optional Details of the Section</param>
		''' <remarks></remarks>
		''' <stepthrough>Enabled</stepthrough>
		<DebuggerNonUserCode()>
		Sub New(Optional Title As String = "", Optional Details As String = "", Optional CellSpacing As Double = 2)
			MyBase.New(Title, Details)
			Me.Table.CellSpacing = CellSpacing
		End Sub

		''' <summary>
		'''
		''' </summary>
		''' <param name="Columns">The Column Header Row's values are Created from each TableColumn's Tag</param>
		''' <remarks></remarks>
		''' <stepthrough></stepthrough>
		<DebuggerNonUserCode()>
		Sub New(ParamArray Columns As Documents.TableColumn())
			MyBase.New("", "")
			For Each Item In Columns
				Me.Table.Columns.Add(Item)
			Next
		End Sub


		''' <summary>
		'''
		''' </summary>
		''' <param name="Columns">The Column Header Row's values are Created from each TableColumn's Tag</param>
		''' <remarks></remarks>
		''' <stepthrough></stepthrough>
		<DebuggerNonUserCode()>
		Sub New(Title As String, TitleAlignment As TextAlignment, Details As String, Footer As String, ParamArray Columns As Documents.TableColumn())
			MyBase.New(Title, Details, Footer)
			Me.Header.TextAlignment = TitleAlignment

			For Each Item In Columns
				Me.Table.Columns.Add(Item)
			Next
		End Sub

		Protected Overrides Function Content() As XElement
			'Dim _XML As XElement = MyBase.ToXML, 

			Dim XTable As XElement = Me.Table.ToXML

			XTable.Elements()(0).AddAfterSelf(
			<TableRowGroup>
				<TableRow Style="{StaticResource TableHeader}">
					<%= From x In Table.Columns Select
						<TableCell>
							<Paragraph TextAlignment="Center"><%= x.Tag %></Paragraph>
						</TableCell>
					%>
				</TableRow>
			</TableRowGroup>)
			'_XML.Add(XTable)

			Return XTable
		End Function

	End Class
End Namespace