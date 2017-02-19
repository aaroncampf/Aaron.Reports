Namespace Sections
	''' <summary>
	''' The Base Section that all Sections Derive From
	''' </summary>
	''' <remarks></remarks>
	''' <features></features>
	''' <stepthrough>Enabled</stepthrough>
	<DebuggerNonUserCode()>
	Public MustInherit Class Base

		''' <summary>
		''' Gets the specialized content for this specific section type
		''' </summary>
		''' <returns></returns>
		''' <remarks></remarks>
		''' <stepthrough></stepthrough>
		Protected MustOverride Function Content() As XElement

		'Protected Property Name As String = "Section"

		''' <summary>Update this so it Uses 4 Singles not 1 String</summary>
		Property Padding As New Thickness(0, 10, 0, 0)

		Property FontSize As Single = 12
		'Property Title As String
		'Property TitleAlignment As TextAlignment
		'Property Details As String
		Property BreakPageBefore As Boolean = False
		Property BreakPageAfter As Boolean = False


		Property Header As Documents.Paragraph
		Property Body As Documents.Paragraph
		Property Footer As Documents.Paragraph


		Sub New(Header As String, Optional Body As String = Nothing, Optional Footer As String = Nothing)
			If Not String.IsNullOrWhiteSpace(Header) Then
				Me.Header = New Documents.Paragraph(New Documents.Run(Header)) With {
				.FontSize = 24, .FontWeight = FontWeights.Bold, .TextAlignment = TextAlignment.Center
			}
			End If

			If Not String.IsNullOrWhiteSpace(Body) Then
				Me.Body = New Documents.Paragraph(New Documents.Run(Body))
			End If

			If Not String.IsNullOrWhiteSpace(Footer) Then
				Me.Footer = New Documents.Paragraph(New Documents.Run(Footer)) With {.TextAlignment = TextAlignment.Center}
			End If
		End Sub

		Sub New(Header As Documents.Paragraph, Optional Body As Documents.Paragraph = Nothing, Optional Footer As Documents.Paragraph = Nothing)
			Me.Header = Header
			Me.Body = Body
			Me.Footer = Footer
		End Sub


		''' <summary>
		''' Converts the Class into an <see cref="T:System.Xml.Linq.XElement">XElement</see>.
		''' General Translation: XElement.Name To Me.Name; XElement.Attributes To Me.Properties(As Value); XElement.Elements To Me.Properties(As Collection)
		''' </summary>
		''' <returns>
		''' </returns>
		''' <stepthrough>Enabled</stepthrough>
		''' <History>
		''' Aaron: 6/8/2013 Removed xmlns:xrd="clr-namespace:Aaron.Xaml;assembly=Aaron.Xaml" and xrd:
		''' </History>
		Function ToXML() As XElement
			'<Section Padding="80,10,40,10" FontSize="12">
			'<Section Padding="40,10,40,10" FontSize="12">
			Dim Section =
			<Section
				BreakPageBefore=<%= BreakPageBefore %>
				Padding=<%= Padding.ToString %>
				FontSize=<%= Me.FontSize %>>

				<%= If(Me.Header Is Nothing, Nothing, Me.Header.ToXML) %>
				<%= If(Me.Body Is Nothing, Nothing, Me.Body.ToXML) %>
				<%= Content() %>
				<%= If(Me.Footer Is Nothing, Nothing, Me.Footer.ToXML) %>
			</Section>
			Return Section
		End Function

	End Class
End Namespace