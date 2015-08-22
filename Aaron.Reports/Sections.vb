''' <summary>
'''
''' </summary>
''' <remarks></remarks>
''' <features></features>
''' <stepthrough>Disabled</stepthrough>
Public NotInheritable Class Sections
    Private Sub New()
    End Sub

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


    '<DebuggerNonUserCode()>
    Public NotInheritable Class Basic
        Inherits Base

        ''' <summary>Represents the Custom Content for this Section. By Default this is not used</summary> 
        Property Custom_XAML As XElement

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
            Return Custom_XAML
        End Function
    End Class


    <DebuggerNonUserCode()>
    Public NotInheritable Class Pictures
        Inherits Base

        'Private _Pictures As New List(Of System.Windows.Controls.Image)
        'ReadOnly Property Pictures As List(Of System.Windows.Controls.Image)
        '	Get
        '		Return _Pictures
        '	End Get
        'End Property


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
        'Public Property Columns As List(Of String)

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

        ' ''' <summary>
        ' '''
        ' ''' </summary>
        ' ''' <param name="Columns">The Column Header Row's values are Created from each TableColumn's Tag</param>
        ' ''' <remarks></remarks>
        ' ''' <stepthrough></stepthrough>
        '<DebuggerNonUserCode()>
        'Sub New(Title As String, Details As String, ParamArray Columns As Documents.TableColumn())
        '	MyBase.New(Title, Details)
        '	For Each Item In Columns
        '		Me.Table.Columns.Add(Item)
        '	Next
        'End Sub

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


        ' ''' <summary>
        ' ''' Converts the Class into an <see cref="T:System.Xml.Linq.XElement">XElement</see>.
        ' ''' General Translation: XElement.Name To Me.Name; XElement.Attributes To Me.Properties(As Value); XElement.Elements To Me.Properties(As Collection)
        ' ''' </summary><returns></returns>
        ' ''' <stepthrough>Enabled</stepthrough>
        'Overrides Function ToXML() As XElement
        '	Dim _XML As XElement = MyBase.ToXML, XTable As XElement = Me.Table.ToXML

        '	XTable.Elements()(0).AddAfterSelf(
        '		<TableRowGroup>
        '			<TableRow Style="{StaticResource TableHeader}">
        '				<%= From x In Table.Columns Select
        '					<TableCell>
        '						<Paragraph TextAlignment="Center"><%= x.Tag %></Paragraph>
        '					</TableCell>
        '				%>
        '			</TableRow>
        '		</TableRowGroup>)

        '	_XML.Add(XTable)

        '	Return _XML
        'End Function

    End Class

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

        ' ''' <summary>
        ' ''' Converts the Class into an <see cref="T:System.Xml.Linq.XElement">XElement</see>.
        ' ''' General Translation: XElement.Name To Me.Name; XElement.Attributes To Me.Properties(As Value); XElement.Elements To Me.Properties(As Collection)
        ' ''' </summary>
        ' ''' <returns></returns>
        ' ''' <stepthrough>Enabled</stepthrough>
        'Overrides Function ToXML() As XElement
        '	'FontSize=<%= x.FontSize %>
        '	Dim _XML As XElement =
        '		<Section Padding="80,10,40,10" FontSize=<%= Me.FontSize %>>
        '			<%= If(String.IsNullOrWhiteSpace(Me.Title), Nothing,
        '				<Paragraph FontSize="24" FontWeight="Bold">
        '					<InlineDocumentValue Text=<%= Me.Title %> Value=""/>
        '				</Paragraph>) %>

        '			<List LineHeight=<%= List.LineHeight %> Margin=<%= List.Margin %> StartIndex=<%= List.StartIndex %>
        '				MarkerOffset=<%= List.MarkerOffset %>>

        '				<%= From x In Me.List.ListItems Select
        '					<ListItem LineHeight=<%= Double.NaN %> FontSize=<%= Item_FontSize %> FontWeight=<%= x.FontWeight %>
        '						BorderBrush=<%= x.BorderBrush %>>
        '						<Paragraph><%= x.Tag %></Paragraph>
        '					</ListItem>
        '				%>
        '			</List>
        '		</Section>

        '	Return _XML
        'End Function

    End Class

    '''' <summary>
    '''' A Section Containing A Chart
    '''' </summary>
    '''' <remarks></remarks>
    '''' <features></features>
    '''' <stepthrough></stepthrough>
    'Public Class ChartS
    '    Inherits Base

    '    Dim _Chart As Aaron.Reports.Chart
    '    ReadOnly Property Chart As Aaron.Reports.Chart
    '        Get
    '            Return _Chart
    '        End Get
    '    End Property

    '    ''' <summary>
    '    '''
    '    ''' </summary>
    '    ''' <param name="Title"></param>
    '    ''' <param name="Details"></param>
    '    ''' <remarks></remarks>
    '    ''' <stepthrough></stepthrough>
    '    <DebuggerNonUserCode()>
    '    Sub New(TableName As String, Columns As String, Optional ColumnType As Visifire.Charts.RenderAs = Visifire.Charts.RenderAs.Column,
    '            Optional Title As String = "", Optional Details As String = "")

    '        MyBase.New(Title, Details)
    '        'Dim T1 As Documents.Paragraph

    '        _Chart = New Aaron.Reports.Chart(TableName, Columns, ColumnType)
    '    End Sub

    '    Protected Overrides Function Content() As XElement
    '        MyBase.Content()
    '        Dim XE = <Paragraph/>
    '        XE.Add(Chart.ToXML)
    '        Return XE
    '    End Function

    'End Class

End Class