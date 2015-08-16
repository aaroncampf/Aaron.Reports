Imports System.Windows.Xps.Packaging
Imports System.IO
Imports System.Windows.Media.Imaging

Public Class Mod_Helpers

	''' <summary>
	''' Add the Font Resource to the Fixed Page
	''' </summary>
	''' <param name="pageWriter">The IXpsFixedPageWriter object to 'add' the Font Resource too</param>
	''' <param name="fontFileName">Full path the font resource being used</param>
	''' <returns>The fonrURI (as a string) of the Font resource</returns>
	Shared Function AddFontResourceToFixedPage(pageWriter As IXpsFixedPageWriter, fontFileName As [String]) As String
		Dim font As XpsFont = pageWriter.AddFont(False)
		'Using dstFontStream As Stream = font.GetStream()
		'	Using srcFontStream As Stream = File.OpenRead(fontFileName)
		'		CopyStream(srcFontStream, dstFontStream)

		'		' commit font resource to the package file
		'		font.Commit()
		'	End Using
		'End Using

		Using dstFontStream As Stream = font.GetStream, srcFontStream As Stream = File.OpenRead(fontFileName)
			CopyStream(srcFontStream, dstFontStream)
			font.Commit() ' commit font resource to the package file
		End Using

		Return font.Uri.ToString
	End Function

	Shared Function CopyStream(srcStream As Stream, dstStream As Stream) As Int32
		Const size As Integer = 64 * 1024
		' copy using 64K buffers
		Dim localBuffer As Byte() = New Byte(size - 1) {}
		Dim bytesRead As Integer
		Dim bytesMoved As Int32 = 0

		' reset stream pointers
		srcStream.Seek(0, SeekOrigin.Begin)
		dstStream.Seek(0, SeekOrigin.Begin)

		' stream position is advanced automatically by stream object
		While (InlineAssignHelper(bytesRead, srcStream.Read(localBuffer, 0, size))) > 0
			dstStream.Write(localBuffer, 0, bytesRead)
			bytesMoved += bytesRead
		End While
		Return bytesMoved
	End Function

	Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
		target = value
		Return value
	End Function



	Shared Function ToXML(DependencyObject As Windows.DependencyObject) As XElement
		Return XElement.Parse(Windows.Markup.XamlWriter.Save(DependencyObject))
	End Function


	'	Public Function CreateFlowDocument(XAMLData As String, _pageHeight As Double, _pageWidth As Double, ReportName As String, ReportTitle As String,
	'									   PageHeaderHeight As Double, PageFooterHeight As Double) As FlowDocument
	'		Dim mem As New MemoryStream()
	'		Dim buf As Byte() = Text.Encoding.UTF8.GetBytes(XAMLData)
	'		mem.Write(buf, 0, buf.Length)
	'		mem.Position = 0
	'		Dim res As FlowDocument = TryCast(Windows.Markup.XamlReader.Load(mem), FlowDocument)

	'		If res.PageHeight = Double.NaN Then
	'			Throw New ArgumentException("Flow document must have a specified page height")
	'		End If
	'		If res.PageWidth = Double.NaN Then
	'			Throw New ArgumentException("Flow document must have a specified page width")
	'		End If

	'		' remember original values
	'		_pageHeight = res.PageHeight
	'		_pageWidth = res.PageWidth

	'		' search report properties
	'		Dim walker As New CodeReason.Reports.DocumentWalker()
	'		Dim headers As List(Of CRR.Document.SectionReportHeader) = walker.Walk(Of CRRD.SectionReportHeader)(res)
	'		Dim footers As List(Of CRRD.SectionReportFooter) = walker.Walk(Of CRRD.SectionReportFooter)(res)
	'		Dim properties As List(Of CRRD.ReportProperties) = walker.Walk(Of CRRD.ReportProperties)(res)
	'		If properties.Count > 0 Then
	'			If properties.Count > 1 Then
	'				Throw New ArgumentException([String].Format("Flow document must have only one ReportProperties section, but it has {0}", properties.Count))
	'			End If
	'			Dim prop As CRRD.ReportProperties = properties(0)
	'			If prop.ReportName IsNot Nothing Then
	'				ReportName = prop.ReportName
	'			End If
	'			If prop.ReportTitle IsNot Nothing Then
	'				ReportTitle = prop.ReportTitle
	'			End If
	'			If headers.Count > 0 Then
	'				PageHeaderHeight = headers(0).PageHeaderHeight
	'			End If
	'			If footers.Count > 0 Then
	'				PageFooterHeight = footers(0).PageFooterHeight
	'			End If

	'			' remove properties section from FlowDocument
	'			Dim parent As Windows.DependencyObject = prop.Parent
	'			If TypeOf parent Is FlowDocument Then
	'				DirectCast(parent, FlowDocument).Blocks.Remove(prop)
	'				parent = Nothing
	'			End If
	'			If TypeOf parent Is Section Then
	'				DirectCast(parent, Section).Blocks.Remove(prop)
	'				parent = Nothing
	'			End If
	'		End If

	'		' make height smaller to have enough space for page header and page footer
	'		res.PageHeight = _pageHeight - _pageHeight * (PageHeaderHeight + PageFooterHeight) / 100.0

	'		' search image objects
	'		Dim images As New List(Of Windows.Controls.Image)()
	'		walker.Tag = images
	'		'walker.VisualVisited += New CRR.DocumentVisitedEventHandler(walker_VisualVisited)

	'		AddHandler walker.VisualVisited, New CRR.DocumentVisitedEventHandler(AddressOf walker_VisualVisited)
	'		walker.Walk(res)

	'		' load all images
	'		For Each image As Image In images
	'			RaiseEvent ImageProcessing(Me, New ImageEventArgs(Me, image))
	'			Try
	'				If TypeOf image.Tag Is String Then
	'					image.Source = New BitmapImage(New Uri("file:///" & Path.Combine(_xamlImagePath, image.Tag.ToString())))
	'				End If
	'			Catch ex As Exception
	'				' fire event on exception and check for Handled = true after each invoke
	'				If ImageError IsNot Nothing Then
	'					Dim handled As Boolean = False
	'					SyncLock ImageError
	'						Dim eventArgs As New ImageErrorEventArgs(ex, Me, image)
	'						For Each ed In ImageError.GetInvocationList()
	'							ed.DynamicInvoke(Me, eventArgs)
	'							If eventArgs.Handled Then
	'								handled = True
	'								Exit For
	'							End If
	'						Next
	'					End SyncLock
	'					If Not handled Then
	'						Throw
	'					End If
	'				Else
	'					Throw
	'				End If
	'			End Try
	'			' TODO: find a better way to specify file names
	'			RaiseEvent ImageProcessed(Me, New ImageEventArgs(Me, image))
	'		Next

	'		Return res
	'	End Function



	'	Private Sub walker_VisualVisited(sender As Object, visitedObject As Object, start As Boolean)
	'		If Not (TypeOf visitedObject Is Windows.Controls.Image) Then
	'			Return
	'		End If

	'		Dim walker As DocumentWalker = TryCast(sender, DocumentWalker)
	'		If walker Is Nothing Then
	'			Return
	'		End If

	'		Dim list As List(Of Windows.Controls.Image) = TryCast(walker.Tag, List(Of Windows.Controls.Image))
	'		If list Is Nothing Then
	'			Return
	'		End If

	'		list.Add(DirectCast(visitedObject, Windows.Controls.Image))
	'	End Sub

End Class
