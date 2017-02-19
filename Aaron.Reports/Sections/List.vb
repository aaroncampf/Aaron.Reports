Namespace Sections
	Public Class List
		Inherits Base

		''' <summary>A Bulleted List where the Text of Each Item is the ListItem's Tag</summary>
		Property List As New Documents.List With {.LineHeight = 2}
		Property Item_FontSize As Double = 12

		''' <summary>
		'''
		''' </summary>
		''' <param name="Title">The Title of the List. If Empty then Not Used</param>
		''' <param name="ListItems"></param>
		''' <remarks></remarks>
		''' <stepthrough>Enabled</stepthrough>
		<DebuggerNonUserCode()>
		Sub New(ByVal Title As String, ByVal ParamArray ListItems As String())
			MyBase.New(Title)
			For Each Item In ListItems
				Me.List.ListItems.Add(New Documents.ListItem With {.FontSize = Item_FontSize, .Tag = Item})
			Next
		End Sub

		''' <summary>
		'''
		''' </summary>
		''' <param name="Title">The Title of the List. If Empty then Not Used</param>
		''' <param name="ListItems"></param>
		''' <remarks></remarks>
		''' <stepthrough>Enabled</stepthrough>
		<DebuggerNonUserCode()>
		Sub New(ByVal Title As String, ByVal ParamArray ListItems As Documents.ListItem())
			MyBase.New(Title)
			Me.List.ListItems.AddRange(ListItems)
		End Sub

		Protected Overrides Function Content() As XElement
			Dim _XML As XElement =
					<List LineHeight=<%= List.LineHeight %> Margin=<%= List.Margin %> StartIndex=<%= List.StartIndex %> MarkerOffset=<%= List.MarkerOffset %>>
						<%= From x In Me.List.ListItems Select
							<ListItem LineHeight=<%= Double.NaN %> FontSize=<%= Item_FontSize %> FontWeight=<%= x.FontWeight %> BorderBrush=<%= x.BorderBrush %>>
								<Paragraph><%= x.Tag %></Paragraph>
							</ListItem>
						%>
					</List>

			Return _XML
		End Function
	End Class
End Namespace