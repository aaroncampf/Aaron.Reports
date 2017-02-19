Namespace Sections
	Public NotInheritable Class Basic
		Inherits Base

		''' <summary>Represents the Custom Content for this Section. By Default this is not used</summary> 
		Property Custom_XAML As XElement

		WriteOnly Property Alignment_Header As TextAlignment
			Set(value As TextAlignment)
				Me.Header.TextAlignment = value
			End Set
		End Property

		WriteOnly Property Alignment_Body As TextAlignment
			Set(value As TextAlignment)
				Me.Body.TextAlignment = value
			End Set
		End Property


		WriteOnly Property Alignment_Footer As TextAlignment
			Set(value As TextAlignment)
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
			Return Custom_XAML
		End Function
	End Class
End Namespace