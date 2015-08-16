Imports System.Windows.Documents
Imports System.Windows.Controls

Public NotInheritable Class DocWalker
	Private Sub New()
	End Sub

	''' <summary>
	''' THe delegate type of the event that will be raised
	''' </summary>
	Public Delegate Sub DocumentVisitedEventHandler(visitedObject As Object, start As Boolean)


	' ''' <summary>
	' ''' Gets or sets the tag associated to this walker
	' ''' </summary>
	'Public Property Tag As Object


	' ''' <summary>
	' ''' This is the event to hook on.
	' ''' </summary>
	'Public Event VisualVisited As DocumentVisitedEventHandler


	'Private Shared Sub DocVisitedDefault(visitedObject As Object, start As Boolean)

	'End Sub

	''' <summary>
	''' Traverses whole document
	''' </summary>
	''' <param name="fd">FlowDocument</param>
	''' <returns>list of inlines</returns>
	Shared Function Walk(fd As FlowDocument, Optional VisualVisited As DocumentVisitedEventHandler = Nothing) As List(Of Inline)
		Return TraverseBlockCollection(Of Inline)(fd.Blocks, VisualVisited)
	End Function

	''' <summary>
	''' Traverses whole document
	''' </summary>
	''' <param name="fd">FlowDocument</param>
	''' <returns>list of inlines</returns>
	Shared Function Walk(Of T As Class)(fd As FlowDocument, Optional VisualVisited As DocumentVisitedEventHandler = Nothing) As List(Of T)
		Return TraverseBlockCollection(Of T)(fd.Blocks, VisualVisited)
	End Function

	''' <summary>
	''' Traverses an InlineCollection
	''' </summary>
	''' <param name="inlines">InlineCollection to be traversed</param>
	''' <returns>list of inlines</returns>
	Shared Function TraverseInlines(Of T As Class)(inlines As InlineCollection, Optional VisualVisited As DocumentVisitedEventHandler = Nothing) As List(Of T)
		Dim res As New List(Of T)()
		If inlines IsNot Nothing AndAlso inlines.Count > 0 Then
			Dim il As Inline = inlines.FirstInline
			While il IsNot Nothing
				If TypeOf il Is T Then
					res.Add(TryCast(il, T))
				End If

				Dim r As Run = TryCast(il, Run)
				If r IsNot Nothing Then
					If VisualVisited IsNot Nothing Then VisualVisited.Invoke(r, True)
					il = il.NextInline
					Continue While
				End If


				Dim sp As Span = TryCast(il, Span)
				If sp IsNot Nothing Then
					If VisualVisited IsNot Nothing Then VisualVisited.Invoke(sp, True)

					res.AddRange(TraverseInlines(Of T)(sp.Inlines, VisualVisited))
					il = il.NextInline
					Continue While
				End If

				Dim uc As InlineUIContainer = TryCast(il, InlineUIContainer)
				If uc IsNot Nothing AndAlso uc.Child IsNot Nothing Then
					If VisualVisited IsNot Nothing Then VisualVisited.Invoke(uc.Child, True)
					If TypeOf uc.Child Is T Then
						res.Add(TryCast(uc.Child, T))
					End If
					Dim tb As TextBlock = TryCast(uc.Child, TextBlock)
					If tb IsNot Nothing Then
						res.AddRange(TraverseInlines(Of T)(tb.Inlines, VisualVisited))
					End If

					il = il.NextInline
					Continue While
				End If
				Dim fg As Figure = TryCast(il, Figure)
				If fg IsNot Nothing Then
					If VisualVisited IsNot Nothing Then VisualVisited.Invoke(fg, True)
					res.AddRange(TraverseBlockCollection(Of T)(fg.Blocks, VisualVisited))
				End If
				il = il.NextInline
			End While
		End If
		Return res
	End Function


	''' <summary>
	''' Traverses only passed paragraph
	''' </summary>
	''' <param name="p">paragraph</param>
	''' <returns>list of inlines</returns>
	Shared Function TraverseParagraph(Of T As Class)(p As Paragraph, Optional VisualVisited As DocumentVisitedEventHandler = Nothing) As List(Of T)
		Return TraverseInlines(Of T)(p.Inlines, VisualVisited)
	End Function

	''' <summary>
	''' Traverses passed block collection
	''' </summary>
	''' <param name="blocks">blocks to be traversed</param>
	''' <returns>list of inlines</returns>
	'''    
	Shared Function TraverseBlockCollection_1(Of T As Class)(blocks As IEnumerable(Of Block), Optional VisualVisited As DocumentVisitedEventHandler = Nothing) As List(Of T)
		Dim res As New List(Of T)()
		For Each b As Block In blocks
			If TypeOf b Is T Then
				If VisualVisited IsNot Nothing Then VisualVisited.Invoke(b, True)
				res.Add(TryCast(b, T))
			End If

			Dim p As Paragraph = TryCast(b, Paragraph)
			If p IsNot Nothing Then
				If VisualVisited IsNot Nothing Then VisualVisited.Invoke(p, True)
				res.AddRange(TraverseParagraph(Of T)(p, VisualVisited))
				Continue For
			End If

			Dim bui As BlockUIContainer = TryCast(b, BlockUIContainer)
			If bui IsNot Nothing Then
				If VisualVisited IsNot Nothing Then VisualVisited.Invoke(bui.Child, True)
				Continue For
			End If

			Dim s As Section = TryCast(b, Section)
			If s IsNot Nothing Then
				If VisualVisited IsNot Nothing Then VisualVisited.Invoke(s, True)
				res.AddRange(TraverseBlockCollection(Of T)(s.Blocks, VisualVisited))
				Continue For
			End If

			Dim TT As Table = TryCast(b, Table)
			If TT IsNot Nothing Then
				If VisualVisited IsNot Nothing Then VisualVisited.Invoke(TT, True)
				For Each trg As TableRowGroup In TT.RowGroups
					If VisualVisited IsNot Nothing Then VisualVisited.Invoke(trg, True)
					For Each tr As TableRow In trg.Rows
						If VisualVisited IsNot Nothing Then VisualVisited.Invoke(tr, True)
						If TypeOf tr Is T Then
							res.Add(TryCast(tr, T))
						End If

						For Each tc As TableCell In tr.Cells
							If VisualVisited IsNot Nothing Then VisualVisited.Invoke(tc, True)
							res.AddRange(TraverseBlockCollection(Of T)(tc.Blocks, VisualVisited))
						Next
					Next
				Next
				Continue For
			End If
		Next
		Return res
	End Function



	''' <summary>
	''' Traverses passed block collection
	''' </summary>
	''' <param name="blocks">blocks to be traversed</param>
	''' <returns>list of inlines</returns>
	'''    
	Shared Function TraverseBlockCollection(Of T As Class)(blocks As IEnumerable(Of Block), Optional VisualVisited As DocumentVisitedEventHandler = Nothing) As List(Of T)
		Dim res As New List(Of T)
		For Each b As Block In blocks
			If TypeOf b Is T Then
				If VisualVisited IsNot Nothing Then VisualVisited.Invoke(b, True)
				res.Add(TryCast(b, T))
			End If



			If TypeOf b Is Paragraph Then
				Dim p1 As Paragraph = b
				If VisualVisited IsNot Nothing Then VisualVisited.Invoke(p1, True)
				res.AddRange(TraverseParagraph(Of T)(p1, VisualVisited))
			ElseIf TypeOf b Is BlockUIContainer Then
				Dim bui1 As BlockUIContainer = b
				If VisualVisited IsNot Nothing Then VisualVisited.Invoke(bui1.Child, True)
			ElseIf TypeOf b Is Section Then
				Dim s1 As Section = b
				If VisualVisited IsNot Nothing Then VisualVisited.Invoke(s1, True)
				res.AddRange(TraverseBlockCollection(Of T)(s1.Blocks, VisualVisited))
			ElseIf TypeOf b Is Table Then
				Dim TT As Table = b
				If VisualVisited IsNot Nothing Then VisualVisited.Invoke(TT, True)
				For Each trg As TableRowGroup In TT.RowGroups
					If VisualVisited IsNot Nothing Then VisualVisited.Invoke(trg, True)
					For Each tr As TableRow In trg.Rows
						If VisualVisited IsNot Nothing Then VisualVisited.Invoke(tr, True)
						If TypeOf tr Is T Then
							res.Add(TryCast(tr, T))
						End If

						For Each tc As TableCell In tr.Cells
							If VisualVisited IsNot Nothing Then VisualVisited.Invoke(tc, True)
							res.AddRange(TraverseBlockCollection(Of T)(tc.Blocks, VisualVisited))
						Next
					Next
				Next
			End If


			'			Select Case b.GetType
			'	Case GetType(Paragraph)
			'		Dim p1 As Paragraph = b
			'		If VisualVisited IsNot Nothing Then VisualVisited.Invoke(p1, True)
			'		res.AddRange(TraverseParagraph(Of T)(p1, VisualVisited))
			'	Case GetType(BlockUIContainer)
			'		Dim bui1 As BlockUIContainer = b
			'		If VisualVisited IsNot Nothing Then VisualVisited.Invoke(bui1.Child, True)
			'	Case GetType(Section)
			'		Dim s1 As Section = b
			'		If VisualVisited IsNot Nothing Then VisualVisited.Invoke(s1, True)
			'		res.AddRange(TraverseBlockCollection(Of T)(s1.Blocks, VisualVisited))
			'	Case GetType(Table)
			'		Dim TT As Table = b
			'		If VisualVisited IsNot Nothing Then VisualVisited.Invoke(TT, True)
			'		For Each trg As TableRowGroup In TT.RowGroups
			'			If VisualVisited IsNot Nothing Then VisualVisited.Invoke(trg, True)
			'			For Each tr As TableRow In trg.Rows
			'				If VisualVisited IsNot Nothing Then VisualVisited.Invoke(tr, True)
			'				If TypeOf tr Is T Then
			'					res.Add(TryCast(tr, T))
			'				End If

			'				For Each tc As TableCell In tr.Cells
			'					If VisualVisited IsNot Nothing Then VisualVisited.Invoke(tc, True)
			'					res.AddRange(TraverseBlockCollection(Of T)(tc.Blocks, VisualVisited))
			'				Next
			'			Next
			'		Next
			'End Select
		Next
		Return res
	End Function


End Class
