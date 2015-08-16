Imports System.IO
Imports System.Text
Imports System.Windows.Xps
Imports System.Windows.Xps.Packaging
Imports System.Xml
Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents



Module Module1

	Sub Main3(args As String())
		My.Computer.FileSystem.DeleteFile("HelloWorld.xps")

		Dim xd As New XpsDocument("HelloWorld.xps", FileAccess.ReadWrite)			   'Create the new document
		Dim xdSW As IXpsFixedDocumentSequenceWriter = xd.AddFixedDocumentSequence()		'Create a new FixedDocumentSequence object in the document
		Dim xdW As IXpsFixedDocumentWriter = xdSW.AddFixedDocument()					'Create a new FixedDocument object in in the document sequence
		Dim xpW As IXpsFixedPageWriter = xdW.AddFixedPage()								'Add a new FixedPage to the FixedDocument
		Dim fontURI As String = Mod_Helpers.AddFontResourceToFixedPage(xpW, "C:\Windows\Fonts\Arial.ttf") 'Add a Font to the FixedPage and get back where it ended up
		Dim pageContents As New StringBuilder()

		'#Region "The actual XPS markup"
		' Try changing the Width and Height and see what you get
		pageContents.AppendLine("<FixedPage Width=""793.76"" Height=""1122.56"" xmlns=""http://schemas.microsoft.com/xps/2005/06"" xml:lang=""und"">")

		pageContents.AppendLine("<Glyphs Fill=""#ff000000"" FontRenderingEmSize=""16"" StyleSimulations=""None"" OriginX=""75.68"" OriginY=""90.56""")

		pageContents.AppendFormat(" FontUri=""{0}"" ", fontURI)	' Add the fontURI

		pageContents.AppendLine("  UnicodeString=""Hello XPS World!""/>") ' HERE IT IS
		pageContents.AppendLine("</FixedPage>")
		'#End Region

		'#Region "The shutdown - Commiting all of the objects"

		' Write the XPS markup out to the page
		Dim xmlWriter As XmlWriter = xpW.XmlWriter
		xmlWriter.WriteRaw(pageContents.ToString())


		'My.Computer.FileSystem.WriteAllText("C:\Users\Aaron Campf\AppData\Local\Temporary Projects\ConsoleApplication1\bin\Debug\Test.txt", pageContents.ToString(), False)
		'Dim XAML = My.Computer.FileSystem.ReadAllText("C:\Users\Aaron Campf\AppData\Local\Temporary Projects\ConsoleApplication1\bin\Debug\Test.txt")


		'xmlWriter.WriteRaw(XAML)

		xpW.Commit()		'Commit the page
		xmlWriter.Close()	'Close the XML writer
		xdW.Commit()		'Commit the fixed document
		xdSW.Commit()		'Commite the fixed document sequence writer
		xd.Close()			'Commit the XPS document itself
		'#End Region

		Process.Start("HelloWorld.xps")
	End Sub


	Sub Main2(args As String())
		My.Computer.FileSystem.DeleteFile("HelloWorld.xps")


		Dim xd As New XpsDocument("HelloWorld.xps", FileAccess.ReadWrite)				'Create the new document
		Dim xdSW As IXpsFixedDocumentSequenceWriter = xd.AddFixedDocumentSequence()		'Create a new FixedDocumentSequence object in the document
		Dim xdW As IXpsFixedDocumentWriter = xdSW.AddFixedDocument()					'Create a new FixedDocument object in in the document sequence
		Dim xpW As IXpsFixedPageWriter = xdW.AddFixedPage()								'Add a new FixedPage to the FixedDocument
		Dim fontURI As String = Mod_Helpers.AddFontResourceToFixedPage(xpW, "C:\Windows\Fonts\Arial.ttf") 'Add a Font to the FixedPage and get back where it ended up
		Dim pageContents As New StringBuilder()



		Dim xmlWriter As XmlWriter = xpW.XmlWriter
		Dim XAML = My.Computer.FileSystem.ReadAllText("C:\Users\Aaron Campf\AppData\Local\Temporary Projects\ConsoleApplication1\bin\Debug\Test.txt")

		xmlWriter.WriteRaw(XAML)

		If False Then
			Dim T As New SimpleFlowExample
			Dim Test As XmlWriter = xpW.XmlWriter
			Test.WriteRaw(Mod_Helpers.ToXML(T))

			xmlWriter.WriteRaw(Mod_Helpers.ToXML(T))
		End If


		xpW.Commit()		'Commit the page
		xmlWriter.Close()	'Close the XML writer
		xdW.Commit()		'Commit the fixed document
		xdSW.Commit()		'Commite the fixed document sequence writer
		xd.Close()			'Commit the XPS document itself
		'#End Region

		Process.Start("HelloWorld.xps")
	End Sub

	Sub Main4()
		My.Computer.FileSystem.DeleteFile("HelloWorld.xps")

		Dim xd As New XpsDocument("HelloWorld.xps", FileAccess.ReadWrite)			   'Create the new document
		Dim xdSW As IXpsFixedDocumentSequenceWriter = xd.AddFixedDocumentSequence()		'Create a new FixedDocumentSequence object in the document
		Dim xdW As IXpsFixedDocumentWriter = xdSW.AddFixedDocument()					'Create a new FixedDocument object in in the document sequence
		Dim xpW As IXpsFixedPageWriter = xdW.AddFixedPage()								'Add a new FixedPage to the FixedDocument
		Dim fontURI As String = Mod_Helpers.AddFontResourceToFixedPage(xpW, "C:\Windows\Fonts\Arial.ttf") 'Add a Font to the FixedPage and get back where it ended up
		Dim pageContents As New StringBuilder()

		'#Region "The actual XPS markup"
		' Try changing the Width and Height and see what you get
		'pageContents.AppendLine("<FixedPage Width=""793.76"" Height=""1122.56"" xmlns=""http://schemas.microsoft.com/xps/2005/06"" xml:lang=""und"">")

		'pageContents.AppendLine("<Glyphs Fill=""#ff000000"" FontRenderingEmSize=""16"" StyleSimulations=""None"" OriginX=""75.68"" OriginY=""90.56""")

		'pageContents.AppendFormat(" FontUri=""{0}"" ", fontURI)	' Add the fontURI

		'pageContents.AppendLine("  UnicodeString=""Hello XPS World!""/>") ' HERE IT IS
		'pageContents.AppendLine("</FixedPage>")
		'#End Region

		'#Region "The shutdown - Commiting all of the objects"

		' Write the XPS markup out to the page
		Dim xmlWriter As XmlWriter = xpW.XmlWriter
		'xmlWriter.WriteRaw(pageContents.ToString())


		'My.Computer.FileSystem.WriteAllText("C:\Users\Aaron Campf\AppData\Local\Temporary Projects\ConsoleApplication1\bin\Debug\Test.txt", pageContents.ToString(), False)
		Dim XAML = My.Computer.FileSystem.ReadAllText("C:\Users\Aaron Campf\AppData\Local\Temporary Projects\ConsoleApplication1\bin\Debug\Test.txt")
		xmlWriter.WriteRaw(XAML)

		xpW.Commit()		'Commit the page
		xmlWriter.Close()	'Close the XML writer
		xdW.Commit()		'Commit the fixed document
		xdSW.Commit()		'Commite the fixed document sequence writer
		xd.Close()			'Commit the XPS document itself
		'#End Region

		Process.Start("HelloWorld.xps")
	End Sub


	Sub main()
		Dim Test As New ReportDocument With {.ReportTitle = "Test", .PageFooterHeight = 2, .PageHeaderHeight = 2}
		Test.Show()



	End Sub




	
End Module

Public Class SimpleFlowExample
	Inherits Page
	Public Sub New()

		Dim myParagraph As New Paragraph()

		' Add some Bold text to the paragraph
		myParagraph.Inlines.Add(New Bold(New Run("Some bold text in the paragraph.")))

		' Add some plain text to the paragraph
		myParagraph.Inlines.Add(New Run(" Some text that is not bold."))

		' Create a List and populate with three list items.
		Dim myList As New List()

		' First create paragraphs to go into the list item.
		Dim paragraphListItem1 As New Paragraph(New Run("ListItem 1"))
		Dim paragraphListItem2 As New Paragraph(New Run("ListItem 2"))
		Dim paragraphListItem3 As New Paragraph(New Run("ListItem 3"))

		' Add ListItems with paragraphs in them.
		myList.ListItems.Add(New ListItem(paragraphListItem1))
		myList.ListItems.Add(New ListItem(paragraphListItem2))
		myList.ListItems.Add(New ListItem(paragraphListItem3))

		' Create a FlowDocument with the paragraph and list.
		Dim myFlowDocument As New FlowDocument()
		myFlowDocument.Blocks.Add(myParagraph)
		myFlowDocument.Blocks.Add(myList)

		' Add the FlowDocument to a FlowDocumentReader Control
		Dim myFlowDocumentReader As New FlowDocumentReader()
		myFlowDocumentReader.Document = myFlowDocument

		Me.Content = myFlowDocumentReader
	End Sub
End Class