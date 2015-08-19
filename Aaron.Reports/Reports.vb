Imports <xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation">
Imports <xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">

''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
''' <features></features>
''' <stepthrough>Disabled</stepthrough>
Public NotInheritable Class Reports
    Private Sub New()
    End Sub


    ''' <summary>
    ''' A Basic Report and the Base Class for All Reports
    ''' </summary>
    ''' <remarks>
    ''' 'TODO Upgrade to use Aaron.XAML(I think the Report Document)
    ''' </remarks>
    ''' <features></features>
    ''' <stepthrough></stepthrough>
    Public Class Basic

#Region "       Properties  >>>"

        Overridable Property CustomXAML As String

        ''' <summary>The Text for the Bottom Left of the Page</summary> 
        Property Bottom_Left As New Documents.Paragraph

        ''' <summary>The Text for the Bottom Right of the Page</summary> 
        Property Bottom_Right As New Documents.Paragraph

        ''' <summary>Use this to setup how the SectionReportHeader is Constructed</summary>
        Property Header As New CodeReason.Reports.Document.SectionReportHeader

        ''' <summary>Use this to setup how the SectionReportFooter is Constructed</summary>
        Property Footer As New CodeReason.Reports.Document.SectionReportHeader

        ''' <summary>The Sections that make up the Report</summary> 
        Property Sections As New List(Of Sections.Base)

        Property Data As New CodeReason.Reports.ReportData

        ReadOnly Property ReportDocumentValues As Dictionary(Of String, Object)
            Get
                Return Data.ReportDocumentValues
            End Get
        End Property



        Dim _Title As Documents.Paragraph
        ''' <summary>The Title of the Report</summary> 
        ReadOnly Property Title As Documents.Paragraph
            Get
                Return _Title
            End Get
        End Property


        Dim _Details As Documents.Paragraph
        ''' <summary>An Optional Paragraph Detailing the Report</summary> 
        ReadOnly Property Details As Documents.Paragraph
            Get
                Return _Details
            End Get
        End Property


        Dim _Resources As XElement
        Overridable ReadOnly Property Resources As XElement
            Get
                If _Resources Is Nothing Then
                    _Resources =
                        <FlowDocument.Resources>
                            <!-- Style for header/footer rows. -->
                            <Style Key="headerFooterRowStyle" TargetType="{x:Type TableRowGroup}">
                                <Setter Property="FontWeight" Value="DemiBold"/>
                                <Setter Property="FontSize" Value="16"/>
                                <Setter Property="Background" Value="LightGray"/>
                            </Style>

                            <!-- Style for data rows. -->
                            <Style Key="dataRowStyle" TargetType="{x:Type TableRowGroup}">
                                <Setter Property="FontSize" Value="12"/>
                            </Style>

                            <!-- Style for data cells. -->
                            <Style TargetType="{x:Type TableCell}">
                                <Setter Property="Padding" Value="0.1cm"/>
                                <Setter Property="BorderBrush" Value="Black"/>
                                <Setter Property="BorderThickness" Value="0.01cm"/>
                            </Style>

                            <!-- Style for Table Headers [AKA the First Row in the Table. -->
                            <Style Key="TableHeader" TargetType="{x:Type TableRow}">
                                <Setter Property="FontWeight" Value="DemiBold"/>
                                <Setter Property="FontSize" Value="16"/>
                                <Setter Property="Background" Value="LightGray"/>
                            </Style>

                        </FlowDocument.Resources>
                End If
                Return _Resources
            End Get
        End Property

#End Region


#Region "       Sub New     >>>"

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
        End Sub

#End Region


#Region "       Utilities   >>>"

        '''' <summary>
        '''' 
        '''' </summary>
        '''' <returns></returns>
        '''' <remarks></remarks>
        '''' <stepthrough></stepthrough>
        ''<DebuggerNonUserCode()>
        'Overridable Function AsStream(Report As CodeReason.Reports.ReportDocument, Page As Integer) As IO.MemoryStream
        '    Dim bitmapEncoder As New Media.Imaging.JpegBitmapEncoder
        '    Dim documentPage As Documents.DocumentPage = Report.CreateXpsDocument(Me.Data).GetFixedDocumentSequence.DocumentPaginator.GetPage(Page)
        '    Dim targetBitmap As New Media.Imaging.RenderTargetBitmap(documentPage.Size.Width * 5, documentPage.Size.Height * 5, 96.0 * 5, 96.0 * 5, Media.PixelFormats.Pbgra32)

        '    targetBitmap.Render(documentPage.Visual)
        '    bitmapEncoder.Frames.Add(Media.Imaging.BitmapFrame.Create(targetBitmap))

        '    Dim Stream As New IO.MemoryStream
        '    bitmapEncoder.Save(Stream)
        '    Return Stream
        'End Function


        '''' <summary>
        '''' Converts the report into a PDF file and returns the files's path
        '''' </summary>
        '''' <returns>The Full Name of the PDF File</returns>
        '''' <remarks></remarks>
        '''' <stepthrough></stepthrough>
        'Overridable Function AsPDF() As String
        '    Dim TempFile As String = My.Computer.FileSystem.GetTempFileName
        '    Dim Stream As New IO.FileStream(TempFile, IO.FileMode.Create)
        '    Dim oPdfDoc As New iTextSharp.text.Document()
        '    Dim oPdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(oPdfDoc, Stream)
        '    oPdfDoc.Open()

        '    Dim Report As New CodeReason.Reports.ReportDocument With {.ReportTitle = "Test", .PageFooterHeight = 2, .PageHeaderHeight = 2}
        '    If Not Data.ReportDocumentValues.ContainsKey("PrintDate") Then Data.ReportDocumentValues.Add("PrintDate", DateTime.Now)
        '    Report.XamlData = Me.GetString      'Check to see if this Works!

        '    For I = 0 To Report.CreateXpsDocument(Me.Data).GetFixedDocumentSequence.DocumentPaginator.PageCount - 1
        '        Aaron.Xaml.Base_Report_Temp.AddPage(True, AsStream(Report, I), oPdfDoc, oPdfWriter)
        '    Next

        '    oPdfDoc.Close()
        '    oPdfWriter.Close()
        '    Stream.Close()

        '    Return TempFile
        'End Function


        ''' <summary>
        ''' Converts the report into a PDF file and returns the files's path
        ''' </summary>
        ''' <returns>The Full Name of the PDF File</returns>
        ''' <remarks></remarks>
        ''' <stepthrough></stepthrough>
        Overridable Function AsPDF() As String
            Dim Report As New CodeReason.Reports.ReportDocument With {.ReportTitle = "Test", .PageFooterHeight = 2, .PageHeaderHeight = 2}
            If Not Data.ReportDocumentValues.ContainsKey("PrintDate") Then Data.ReportDocumentValues.Add("PrintDate", DateTime.Now)
            Report.XamlData = Me.GetString
            Return Aaron.Xaml.Base_Report_Temp.AsPDF(Report.CreateXpsDocument(Me.Data))
        End Function


        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="Hidden"></param>
        ''' <remarks></remarks>
        ''' <stepthrough></stepthrough>
        Overridable Sub Print(Hidden As Boolean)
            Dim Report As New CodeReason.Reports.ReportDocument With {.ReportTitle = "Test", .PageFooterHeight = 2, .PageHeaderHeight = 2}
            If Not Data.ReportDocumentValues.ContainsKey("PrintDate") Then Data.ReportDocumentValues.Add("PrintDate", DateTime.Now)
            Dim Text As String = Me.GetString
            Report.XamlData = Text

            If Hidden Then
                Dim PD As New Windows.Controls.PrintDialog
                PD.PrintDocument(Report.CreateXpsDocument(Data).GetFixedDocumentSequence.DocumentPaginator, Nothing)
            Else
                Dim this As New Windows.Controls.DocumentViewer With {.Document = Report.CreateXpsDocument(Data).GetFixedDocumentSequence}
                this.Print()
            End If
        End Sub

        ''' <summary>
        ''' Displays the Report that was Generated in <see cref="Show"/>
        ''' </summary>
        ''' <param name="Report"></param>
        ''' <param name="Data"></param>
        ''' <remarks></remarks>
        ''' <stepthrough>Enabled</stepthrough>
        <DebuggerStepThrough()>
        Protected Overridable Sub ShowHelper(Report As CodeReason.Reports.ReportDocument, Data As CodeReason.Reports.ReportData)
            Dim IsForm As New Forms.Form With {.WindowState = Forms.FormWindowState.Maximized}
            Dim this As New Windows.Controls.DocumentViewer With {.Document = Report.CreateXpsDocument(Data).GetFixedDocumentSequence}
            IsForm.Controls.Add(New Forms.Integration.ElementHost With {.Dock = Forms.DockStyle.Fill, .Child = this})
            IsForm.ShowDialog()
        End Sub

        ''' <summary>
        ''' Prepares A <see cref="CodeReason.Reports.ReportDocument">ReportDocument</see> and <see cref="CodeReason.Reports.ReportData">ReportData</see>
        ''' and Uses then in <see cref="ShowHelper"/>
        ''' </summary>
        ''' <remarks></remarks>
        ''' <stepthrough>Enabled</stepthrough>
        <DebuggerStepThrough()>
        Overridable Sub Show()
            Dim Report As New CodeReason.Reports.ReportDocument With {.ReportTitle = "Test", .PageFooterHeight = 2, .PageHeaderHeight = 2}

            If Not Data.ReportDocumentValues.ContainsKey("PrintDate") Then Data.ReportDocumentValues.Add("PrintDate", DateTime.Now)
            Dim Text As String = Me.GetString

            'Temp Testing Only
            'If False Then
            '    Text = My.Computer.FileSystem.ReadAllText("C:\Hand_Made_Quote.xml")
            'End If


            Report.XamlData = Text
            ShowHelper(Report, Data)
        End Sub

#End Region


        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>
        ''' Aaron Campf: 6/7/2013 Added [Formatted_Sections]
        ''' </remarks>
        ''' <stepthrough>Disabled</stepthrough>
        Overridable Function GetString() As String
            'Remember about Using <Bold>Text</Bold> and <Italic>Text</Italic>
            If Not String.IsNullOrWhiteSpace(Me.CustomXAML) Then Return Me.CustomXAML

            Dim Formatted_Sections As New List(Of XElement) 'Aaron Campf: 6/7/2013 Added [Formatted_Sections]
            For Each Item In Me.Sections
                Formatted_Sections.Add(Item.ToXML)

                If Item.BreakPageAfter Then
                    Formatted_Sections.Add(<Section BreakPageBefore="true"/>)
                End If
            Next

            Dim IsXML =
                <FlowDocument
                    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                    xmlns:xrd="clr-namespace:CodeReason.Reports.Document;assembly=CodeReason.Reports"
                    xmlns:crcv="clr-namespace:CodeReason.Reports.Charts.Visifire;assembly=CodeReason.Reports.Charts.Visifire"
                    PageHeight="29.7cm" PageWidth="21cm" ColumnWidth="21cm">

                    <%= Me.Resources %>

                    <SectionReportHeader PageHeaderHeight="2" Padding="10,10,10,0" FontSize="12">
                        <Table>
                            <TableRowGroup>
                                <TableRow>
                                    <TableCell>
                                        <Paragraph>
                                                Page
                                                <InlineContextValue PropertyName="PageNumber" FontWeight="Bold"/> of
                                                <InlineContextValue PropertyName="PageCount" FontWeight="Bold"/>
                                        </Paragraph>
                                    </TableCell>
                                    <TableCell>
                                        <Paragraph TextAlignment="Right">
                                            <InlineDocumentValue PropertyName=<%= Today.ToShortDateString %> Format="dd.MM.yyyy"/>
                                        </Paragraph>
                                    </TableCell>
                                </TableRow>
                            </TableRowGroup>
                        </Table>
                    </SectionReportHeader>


                    <!--Padding="80,10,40,10"-->
                    <Section Padding="40,10,40,10" FontSize="12">
                        <%= Me.Title.ToXML %>
                        <%= If(CType(_Details.Inlines(0), Windows.Documents.Run).Text.Length > 0, Me.Details.ToXML, Nothing) %>
                    </Section>

                    <!--
                    <%= From x In Sections Select x.ToXML %>
                    -->
                    <%= From x In Formatted_Sections %>

                    <SectionReportFooter PageFooterHeight="2" Padding="10,0,10,10" FontSize="12">
                        <Table>
                            <TableRowGroup>
                                <TableRow>
                                    <TableCell>
                                        <%= Me.Bottom_Left.ToXML %>
                                    </TableCell>
                                    <TableCell>
                                        <%= Me.Bottom_Right.ToXML %>
                                    </TableCell>
                                </TableRow>
                            </TableRowGroup>
                        </Table>
                    </SectionReportFooter>
                </FlowDocument>


            Dim Builder As New Text.StringBuilder(IsXML.ToString)


            Builder.Replace("ReportProperties", "xrd:ReportProperties")
            Builder.Replace("SectionReport", "xrd:SectionReport")
            Builder.Replace("InlineContextValue", "xrd:InlineContextValue")
            Builder.Replace("InlineDocumentValue", "xrd:InlineDocumentValue")
            Builder.Replace("<Style Key", "<Style x:Key")
            Builder.Replace("SectionDataGroup", "xrd:SectionDataGroup")
            Builder.Replace("TableRowFor", "xrd:TableRowFor")

            Builder.Replace("xmlns=""""", "")
            'Test Fuck Fucc
            ''''Builder.Replace("Style=""{StaticResource TableHeader}""", "")
            Return Builder.ToString
        End Function

    End Class 'TODO Upgrade to use Aaron.XAML(I think the Report Document)

End Class