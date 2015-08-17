Imports D = System.Windows.Documents, E = System.Runtime.CompilerServices.ExtensionAttribute, SC = System.ComponentModel

''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
''' <stepthrough></stepthrough>
Public Module Extensions

    '*************************************************************
    'Module Attributes crash the program

    '<HideModuleName()>
    '<SC.EditorBrowsable(SC.EditorBrowsableState.Advanced)>
    '*************************************************************

    ''' <summary>
    ''' Adds A Range of Strings to A <see cref="D.InlineCollection">InlineCollection</see> Converting Each NewLine into A {LineBreak/} and 
    ''' Adding A {LineBreak/} After each Section
    ''' </summary>
    ''' <param name="InlineCollection">The InlineCollection you want to Add to</param>
    ''' <param name="Sections">The Sections you want to Add to the InlineCollection</param>
    ''' <remarks></remarks>
    ''' <stepthrough></stepthrough>
    ''' <filterpriority>2</filterpriority>
    <E()> <SC.EditorBrowsable(SC.EditorBrowsableState.Advanced)> <DebuggerNonUserCode()>
    Sub AddRange(ByRef InlineCollection As D.InlineCollection, ParamArray Sections As String())
        For Each Item In Sections
            InlineCollection.Add(Item)
            InlineCollection.Add(New D.LineBreak)
        Next
        InlineCollection.Remove(InlineCollection.LastInline)
    End Sub


    ' ''' <summary>
    ' ''' Adds A Row into the First RowGroup With Cells Emulating <paramref name="Cells"/> With Default TextAlignment = Center
    ' ''' </summary>
    ' ''' <param name="Table">The Table you want to Use</param>
    ' ''' <param name="RowGroupIndex">The Index of the <see cref="Windows.Documents.TableRowGroup">TableRowGroup</see> You want to add the Row to</param>
    ' ''' <param name="Cells"></param>
    ' ''' <remarks></remarks>
    ' ''' <stepthrough>The Cells you want to Add</stepthrough>
    ' ''' <filterpriority>2</filterpriority>
    '<E()> <SC.EditorBrowsable(SC.EditorBrowsableState.Advanced)> <DebuggerNonUserCode()>
    'Sub AddRow(ByRef Table As D.Table, RowGroupIndex As Integer, ParamArray Cells As String())
    '    If Table.RowGroups.Count = 0 Then Table.RowGroups.Add(New D.TableRowGroup)

    '    Dim Row As New D.TableRow
    '    For Each Item In Cells
    '        Row.Cells.Add(New D.TableCell(New D.Paragraph(New D.Run(Item)) With {.TextAlignment = TextAlignment.Center}))
    '    Next
    '    Table.RowGroups(RowGroupIndex).Rows.Add(Row)
    'End Sub


    ''' <summary>
    ''' Adds A Row into the First RowGroup With Cells Emulating <paramref name="Cells"/>
    ''' </summary>
    ''' <param name="Table">The Table you want to Use</param>
    ''' <param name="RowGroupIndex">The Index of the <see cref="Windows.Documents.TableRowGroup">TableRowGroup</see> You want to add the Row to</param>
    ''' <param name="Default_Alignment"></param>
    ''' 
    ''' <param name="Cells"></param>
    ''' <remarks></remarks>
    ''' <stepthrough>The Cells you want to Add</stepthrough>
    ''' <filterpriority>2</filterpriority>
    <E()> <SC.EditorBrowsable(SC.EditorBrowsableState.Advanced)> <DebuggerNonUserCode()>
    Sub AddRow(ByRef Table As D.Table, RowGroupIndex As Integer, Default_Alignment As Windows.TextAlignment, ParamArray Cells As String())
        If Table.RowGroups.Count = 0 Then Table.RowGroups.Add(New D.TableRowGroup)

        Dim Row As New D.TableRow
        For Each Item In Cells
            Row.Cells.Add(New D.TableCell(New D.Paragraph(New D.Run(Item)) With {.TextAlignment = Default_Alignment}))
        Next

        Table.RowGroups(RowGroupIndex).Rows.Add(Row)
    End Sub


    ''' <summary>
    ''' Adds A Row into the First RowGroup With Cells Emulating <paramref name="Cells"/>
    ''' </summary>
    ''' <param name="Table">The Table you want to Use</param>
    ''' <param name="RowGroupIndex">The Index of the <see cref="Windows.Documents.TableRowGroup">TableRowGroup</see> You want to add the Row to</param>
    ''' <param name="Default_Alignment"></param>
    ''' 
    ''' <param name="Cells"></param>
    ''' <remarks></remarks>
    ''' <stepthrough>The Cells you want to Add</stepthrough>
    ''' <filterpriority>2</filterpriority>
    <E()> <SC.EditorBrowsable(SC.EditorBrowsableState.Advanced)> <DebuggerNonUserCode()>
    Sub AddRow(ByRef Table As D.Table, RowGroupIndex As Integer, FontSize As Integer, Default_Alignment As Windows.TextAlignment, ParamArray Cells As String())
        If Table.RowGroups.Count = 0 Then Table.RowGroups.Add(New D.TableRowGroup)

        Dim Row As New D.TableRow
        For Each Item In Cells
            Row.Cells.Add(New D.TableCell(New D.Paragraph(New D.Run(Item)) With {.TextAlignment = Default_Alignment, .FontSize = FontSize}))
        Next

        Table.RowGroups(RowGroupIndex).Rows.Add(Row)
    End Sub


    ''' <summary>
    ''' Standardizes the Actual Thickness of Cell Borders when the Cell Padding is 0
    ''' </summary>
    ''' <param name="RowGroup"></param>
    ''' <param name="Thickness"></param>
    ''' <remarks></remarks>
    ''' <stepthrough></stepthrough>
    <E()> <SC.EditorBrowsable(SC.EditorBrowsableState.Advanced)> <DebuggerNonUserCode()>
    Sub Standardize_CellBorderThickness(RowGroup As D.TableRowGroup, Thickness As Single)
        For Each Item In RowGroup.Rows
            'Dim Thickness As New Windows.Thickness(0.25)
            For Each Cell In Item.Cells
                Cell.BorderThickness = New Windows.Thickness(Thickness)
            Next
            Item.Cells.First.BorderThickness = New Windows.Thickness(Thickness * 2, Thickness, Thickness, Thickness)
            Item.Cells.Last.BorderThickness = New Windows.Thickness(Thickness, Thickness, Thickness * 2, Thickness)
        Next
        For Each Cell In RowGroup.Rows.Last.Cells
            Cell.BorderThickness = New Windows.Thickness(Thickness * 2, Thickness, Thickness * 2, Thickness * 2)
        Next
    End Sub 'TODO: Upgrade All Quotes to use this!!

    ''' <summary>
    ''' Converts A <see cref="DependencyObject">DependencyObject</see> into an XElement
    ''' </summary>
    ''' <param name="DependencyObject"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <stepthrough></stepthrough>
    ''' <filterpriority>2</filterpriority>
    <E()> <SC.EditorBrowsable(SC.EditorBrowsableState.Advanced)> <DebuggerNonUserCode()>
    Function ToXML(DependencyObject As DependencyObject) As XElement
        Return XElement.Parse(Windows.Markup.XamlWriter.Save(DependencyObject))
    End Function

End Module


