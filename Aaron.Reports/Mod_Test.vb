'Imports <xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation">
'Imports <xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
'Imports <xmlns:xrd="clr-namespace:Aaron.Xaml;assembly=Aaron.Xaml">


Public NotInheritable Class Mod_Test


    'Public Class BasicReport

    '	Shared Sub A(XAML As String, DocumentValues As Dictionary(Of String, Object))
    '		'Dim T As New ReportDocument_Child("Test", "AB", Example.Sections.ToArray)
    '		'T.Sections.ToList()

    '		'T.Show()

    '		XAML = XAML.Replace("xmlns:xrd=""clr-namespace:CodeReason.Reports.Document;assembly=CodeReason.Reports""", "xmlns:xrd=""clr-namespace:Aaron.Xaml;assembly=Aaron.Xaml""")


    '		Dim T = RawReport.Get(XAML, DocumentValues)
    '		T.Show()

    '		Dim T1 = From x In Example.Sections Select x.ToXML
    '		'Example.Bottom_Left
    '		T.Get_Default_Template()
    '		Example.GetString()
    '	End Sub

    'End Class

    <DebuggerStepThrough>
    Public Class RawReport
        Inherits ReportDocument_Child

        Public Shared Function [Get](XAML As String, Optional DocumentValues As Dictionary(Of String, Object) = Nothing) As RawReport
            Return New RawReport(XAML, DocumentValues)
        End Function



        Private Sub New(XAML As String, DocumentValues As Dictionary(Of String, Object))
            Me.XamlData = XAML
            Me.DocumentValues = If(DocumentValues Is Nothing, New Dictionary(Of String, Object), DocumentValues)
        End Sub

    End Class

    Public Class CustomReport
        Inherits ReportDocument_Child

        Public Property Header As String

        Public Property Footer As String

        Public Property Formatter As Func(Of String, String)

        Public Property FlowDocument_Settings As String

        Dim _Details As Documents.Paragraph
        ''' <summary>An Optional Paragraph Detailing the Report</summary> 
        Overrides ReadOnly Property Details As Documents.Paragraph
            Get
                Return _Details
            End Get
        End Property



        Public Shadows Property Resources As XElement
            Get
                Return MyBase.Resources
            End Get
            Set(value As XElement)
                MyBase._Resources = value
            End Set
        End Property


        Sub New()
            Header =
                <Table xmlns:xrd="clr-namespace:Aaron.Xaml;assembly=Aaron.Xaml">
                    <TableRowGroup>
                        <TableRow>
                            <TableCell>
                                <Paragraph>
                                                Page
                                                <xrd:InlineContextValue PropertyName="PageNumber" FontWeight="Bold"/> of 
                                                <xrd:InlineContextValue PropertyName="PageCount" FontWeight="Bold"/>
                                </Paragraph>
                            </TableCell>
                            <TableCell>
                                <Paragraph TextAlignment="Right">
                                    <xrd:InlineContextValue PropertyName="ReportDate" Format="dd.MM.yyyy"/>
                                </Paragraph>
                            </TableCell>
                        </TableRow>
                    </TableRowGroup>
                </Table>

        End Sub


        Public Overrides Function Get_Default_Template() As String
            Dim IsXML =
                <FlowDocument
                    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                    xmlns:xrd="clr-namespace:Aaron.Xaml;assembly=Aaron.Xaml"
                    ColumnWidth="21cm" PageHeight="27.0cm" PageWidth="21cm"
                    <%= FlowDocument_Settings %>
                    FontFamily="Century Gothic"
                    >

                    <%= Me.Resources %>

                    <xrd:SectionReportHeader PageHeaderHeight="2" Padding="10,10,10,0" FontSize="12">
                        <%= Header %>
                    </xrd:SectionReportHeader>


                    <!--Padding="80,10,40,10"-->
                    <Section Padding="40,10,40,10" FontSize="12">
                        <%= Me.Title.ToXML %>
                        <%= If(CType(_Details.Inlines(0), Windows.Documents.Run).Text.Length > 0, Me.Details.ToXML, Nothing) %>
                    </Section>

                    <%= From x In Sections Select x.ToXML %>

                    <xrd:SectionReportFooter PageFooterHeight="2" Padding="10,0,10,10" FontSize="12">
                        <%= Footer %>
                    </xrd:SectionReportFooter>
                </FlowDocument>



            Return Formatter()(IsXML.ToString)
        End Function


    End Class


    Public Class ReportDocument_Child
        Inherits Aaron.Xaml.ReportDocument

#Region "       Properties     >>>"

        ''' <summary>The Text for the Bottom Left of the Page</summary> 
        Property Bottom_Left As New Documents.Paragraph With {.TextAlignment = TextAlignment.Left}

        ''' <summary>The Text for the Bottom Center of the Page</summary> 
        Property Bottom_Center As New Documents.Paragraph With {.TextAlignment = TextAlignment.Center}

        ''' <summary>The Text for the Bottom Right of the Page</summary> 
        Property Bottom_Right As New Documents.Paragraph With {.TextAlignment = TextAlignment.Right}

        ''' <summary>The Sections that make up the Report</summary> 
        Property Sections As New List(Of Sections.Base)

        Dim _Title As Documents.Paragraph
        ''' <summary>The Title of the Report</summary> 
        ReadOnly Property Title As Documents.Paragraph
            Get
                Return _Title
            End Get
        End Property

        Dim _Details As Documents.Paragraph
        ''' <summary>An Optional Paragraph Detailing the Report</summary> 
        Overridable ReadOnly Property Details As Documents.Paragraph
            Get
                Return _Details
            End Get
        End Property

#End Region


        ''' <summary>
        ''' 
        ''' </summary>  
        ''' <param name="Title">The Title of the Report</param>
        ''' <param name="Details">The Details of the Report</param>
        ''' <param name="Paragraph_Text_Alignment"></param>
        ''' <remarks></remarks>
        ''' <stepthrough>Enabled</stepthrough>
        <DebuggerNonUserCode()>
        Sub New(Optional Title As String = "", Optional Details As String = "", Optional Paragraph_Text_Alignment As TextAlignment = TextAlignment.Left)
            _Title = New Documents.Paragraph(New Documents.Run(Title)) With {.FontSize = 24, .TextAlignment = TextAlignment.Center, .FontWeight = FontWeights.Bold}
            _Details = New Documents.Paragraph(New Documents.Run(Details)) With {.FontSize = 12, .TextAlignment = Paragraph_Text_Alignment}
        End Sub


        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="Title">The Title of the Report</param>
        ''' <param name="Details">The Details of the Report</param>
        ''' <param name="Sections"></param>
        ''' <remarks></remarks>
        ''' <stepthrough>Enabled</stepthrough>
        <DebuggerNonUserCode()>
        Sub New(Title As String, Details As String, ParamArray Sections As Sections.Base())
            _Title = New Documents.Paragraph(New Documents.Run(Title)) With {.FontSize = 24, .TextAlignment = TextAlignment.Center, .FontWeight = FontWeights.Bold}
            _Details = New Documents.Paragraph(New Documents.Run(Details)) With {.FontSize = 12, .TextAlignment = TextAlignment.Center}
            Me.Sections.AddRange(Sections)

            'Me.PageFooterHeight = 2
            'Me.PageHeaderHeight = 2
        End Sub



        Protected _Resources As XElement
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <remarks>Already set with Default Resources</remarks>
        Overridable ReadOnly Property Resources As XElement
            Get
                If _Resources Is Nothing Then
                    _Resources =
                        <FlowDocument.Resources xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
                            <!-- Style for header/footer rows. Removed {x:Type...} -->
                            <Style x:Key="headerFooterRowStyle" TargetType="TableRowGroup">
                                <Setter Property="FontWeight" Value="DemiBold"/>
                                <Setter Property="FontSize" Value="16"/>
                                <Setter Property="Background" Value="LightGray"/>
                            </Style>

                            <!-- Style for data rows. Removed {x:Type...} -->
                            <Style x:Key="dataRowStyle" TargetType="TableRowGroup">
                                <Setter Property="FontSize" Value="12"/>
                            </Style>

                            <!-- Style for data cells. Removed {x:Type...} -->
                            <Style TargetType="TableCell">
                                <Setter Property="Padding" Value="0.1cm"/>
                                <Setter Property="BorderBrush" Value="Black"/>
                                <Setter Property="BorderThickness" Value="0.01cm"/>
                            </Style>

                            <!-- Style for Table Headers [AKA the First Row in the Table. Removed {x:Type...} -->
                            <Style x:Key="TableHeader" TargetType="TableRow">
                                <Setter Property="FontWeight" Value="DemiBold"/>
                                <Setter Property="FontSize" Value="16"/>
                                <Setter Property="Background" Value="LightGray"/>
                            </Style>

                            <!--
							<Style x:Key="Section" TargetType="{x:Type Section}">
								<Setter Property="LineHeight" Value="0.0034"/>
								<Setter Property="FontSize" Value="12"/>
							</FlowDocument.Resources>
							-->

                        </FlowDocument.Resources>
                    _Resources.Attributes.First.Remove()
                End If
                Return _Resources
            End Get
        End Property



        Overrides Function Get_Default_Template() As String
            'Remember about Using <Bold>Text</Bold> and <Italic>Text</Italic>
            'If Not String.IsNullOrWhiteSpace(Me.XamlData) Then Return Me.XamlData

            '**********************************************************************
            'Warning !!!!!
            'Adding the Namespaces in the XML Directly might cause an Error!!!!
            '**********************************************************************

            Debug.Print("FlowDocument Must have PageHeight='270.0cm' or Greater; If not the SectionFooter Will be Missing")
            Dim IsXML =
                <FlowDocument
                    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                    xmlns:xrd="clr-namespace:Aaron.Xaml;assembly=Aaron.Xaml"
                    ColumnWidth="21cm" PageHeight="27.0cm" PageWidth="21cm"
                    FontFamily="Century Gothic"
                    >

                    <%= Me.Resources %>

                    <xrd:SectionReportHeader PageHeaderHeight="2" Padding="10,10,10,0" FontSize="12">
                        <Table>
                            <TableRowGroup>
                                <TableRow>
                                    <TableCell>
                                        <Paragraph>
                                                Page
                                                <xrd:InlineContextValue PropertyName="PageNumber" FontWeight="Bold"/> of 
                                                <xrd:InlineContextValue PropertyName="PageCount" FontWeight="Bold"/>
                                        </Paragraph>
                                    </TableCell>
                                    <TableCell>
                                        <Paragraph TextAlignment="Right">
                                            <xrd:InlineContextValue PropertyName="ReportDate" Format="dd.MM.yyyy"/>
                                        </Paragraph>
                                    </TableCell>
                                </TableRow>
                            </TableRowGroup>
                        </Table>
                    </xrd:SectionReportHeader>

                    <Section Padding="40,10,40,10" FontSize="12">
                        <%= Me.Title.ToXML %>
                        <%= If(CType(_Details.Inlines(0), Windows.Documents.Run).Text.Length > 0, Me.Details.ToXML, Nothing) %>
                    </Section>

                    <%= From x In Sections Select x.ToXML %>

                    <xrd:SectionReportFooter PageFooterHeight="2" Padding="10,0,10,10" FontSize="12">
                        <Table>
                            <TableRowGroup>
                                <TableRow>
                                    <TableCell>
                                        <%= Me.Bottom_Left.ToXML %>
                                    </TableCell>
                                    <TableCell>
                                        <%= Me.Bottom_Center.ToXML %>
                                    </TableCell>
                                    <TableCell>
                                        <%= Me.Bottom_Right.ToXML %>
                                    </TableCell>
                                </TableRow>
                            </TableRowGroup>
                        </Table>
                    </xrd:SectionReportFooter>
                </FlowDocument>


            Dim Builder As New Text.StringBuilder(IsXML.ToString)


            Builder.Replace("TableRowFor", "xrd:TableRowFor")
            Builder.Replace("xmlns=""""", "")

            Return Builder.ToString
        End Function


        Overridable Sub Setup_Bottom(Left As String, Center As String, Right As String)
            Me.Bottom_Left.Inlines.Add(Left)
            Me.Bottom_Center.Inlines.Add(Center)
            Me.Bottom_Right.Inlines.Add(Right)
        End Sub
    End Class
End Class
