Imports System.Windows.Media.Imaging
Imports System.Windows.Media
Imports System.Windows
Imports System.Windows.Documents
Imports System.Windows.Markup

''' <summary>
''' Helper class for XAML
''' </summary>
Public NotInheritable Class XamlHelper1
	Private Sub New()
	End Sub
	''' <summary>
	''' Loads a XAML object from string
	''' </summary>
	''' <param name="s">string containing the XAML object</param>
	''' <returns>XAML object or null, if string was empty</returns>
	Public Shared Function LoadXamlFromString(s As String) As Object
		If [String].IsNullOrEmpty(s) Then
			Return Nothing
		End If
		Dim stringReader As New IO.StringReader(s)
		Dim xmlReader As Xml.XmlReader = Xml.XmlTextReader.Create(stringReader, New Xml.XmlReaderSettings())
		Return XamlReader.Load(xmlReader)
	End Function

	''' <summary>
	''' Clones a table row
	''' </summary>
	''' <param name="orig">original table row</param>
	''' <returns>cloned table row</returns>
	Public Shared Function CloneTableRow(orig As TableRow) As TableRow
		If orig Is Nothing Then
			Return Nothing
		End If
		Dim s As String = XamlWriter.Save(orig)
		Return DirectCast(LoadXamlFromString(s), TableRow)
	End Function

	''' <summary>
	''' Clones a complete block
	''' </summary>
	''' <param name="orig">orininal block</param>
	''' <returns>cloned block</returns>
	Public Shared Function CloneBlock(orig As Block) As Block
		If orig Is Nothing Then
			Return Nothing
		End If
		Dim s As String = XamlWriter.Save(orig)
		Return DirectCast(LoadXamlFromString(s), Block)
	End Function

	''' <summary>
	''' Clones a complete UIElement
	''' </summary>
	''' <param name="orig">original UIElement</param>
	''' <returns>cloned UIElement</returns>
	Public Shared Function CloneUIElement(orig As UIElement) As UIElement
		If orig Is Nothing Then
			Return Nothing
		End If
		Dim s As String = XamlWriter.Save(orig)
		Return DirectCast(LoadXamlFromString(s), UIElement)
	End Function

	''' <summary>
	''' Saves a visual to bitmap into stream
	''' </summary>
	''' <param name="visual">visual</param>
	''' <param name="stream">stream</param>
	''' <param name="width">width</param>
	''' <param name="height">height</param>
	''' <param name="dpiX">X DPI resolution</param>
	''' <param name="dpiY">Y DPI resolution</param>
	Public Shared Sub SaveImageBmp(visual As Visual, stream As IO.Stream, width As Integer, height As Integer, dpiX As Double, dpiY As Double)
		Dim bitmap As New RenderTargetBitmap(CInt(Math.Truncate(width * dpiX / 96.0)), CInt(Math.Truncate(height * dpiY / 96.0)), dpiX, dpiY, PixelFormats.Pbgra32)
		bitmap.Render(visual)

		Dim image As New BmpBitmapEncoder()
		image.Frames.Add(BitmapFrame.Create(bitmap))
		image.Save(stream)
	End Sub

	''' <summary>
	''' Saves a visual to PNG into stream
	''' </summary>
	''' <param name="visual">visual</param>
	''' <param name="stream">stream</param>
	''' <param name="width">width</param>
	''' <param name="height">height</param>
	''' <param name="dpiX">X DPI resolution</param>
	''' <param name="dpiY">Y DPI resolution</param>
	Public Shared Sub SaveImagePng(visual As Visual, stream As IO.Stream, width As Integer, height As Integer, dpiX As Double, dpiY As Double)
		Dim bitmap As New RenderTargetBitmap(CInt(Math.Truncate(width * dpiX / 96.0)), CInt(Math.Truncate(height * dpiY / 96.0)), dpiX, dpiY, PixelFormats.Pbgra32)
		bitmap.Render(visual)

		Dim image As New PngBitmapEncoder()
		image.Frames.Add(BitmapFrame.Create(bitmap))
		image.Save(stream)
	End Sub
End Class
