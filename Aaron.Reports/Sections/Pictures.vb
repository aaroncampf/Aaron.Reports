Namespace Sections
	<DebuggerNonUserCode()>
	Public NotInheritable Class Pictures
		Inherits Base


		Private _Pictures As New List(Of XElement)
		''' <summary>
		''' The Pictures that will be used ordered by there place in this list. 
		''' Example: <example>[Image Height="100" Width="100" Source="C:\Test\Untitled.jpg"/]</example>
		''' </summary>
		''' <value></value>
		''' <remarks></remarks>
		ReadOnly Property Pictures As List(Of XElement)
			Get
				Return _Pictures
			End Get
		End Property

		WriteOnly Property Alignment_Header As Windows.TextAlignment
			Set(value As Windows.TextAlignment)
				Me.Header.TextAlignment = value
			End Set
		End Property

		WriteOnly Property Alignment_Body As Windows.TextAlignment
			Set(value As Windows.TextAlignment)
				Me.Body.TextAlignment = value
			End Set
		End Property


		WriteOnly Property Alignment_Footer As Windows.TextAlignment
			Set(value As Windows.TextAlignment)
				Me.Footer.TextAlignment = value
			End Set
		End Property


		Sub New(Header As String, Optional Body As String = Nothing, Optional Footer As String = Nothing)
			MyBase.New(Header, Body, Footer)
		End Sub

		Sub New(Header As Documents.Paragraph, Optional Body As Documents.Paragraph = Nothing, Optional Footer As Documents.Paragraph = Nothing)
			MyBase.New(Header, Body, Footer)
		End Sub

		Protected Overrides Function Content() As XElement
			'Dim Data =
			'	<Paragraph>
			'		<%= From x In Me.Pictures Select x.ToXML %>
			'	</Paragraph>

			Dim Data =
		<Paragraph>
			<%= From x In Me.Pictures Select x %>
		</Paragraph>

			Return Data
		End Function
	End Class
End Namespace