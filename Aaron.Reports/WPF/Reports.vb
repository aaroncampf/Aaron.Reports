''' <summary>
''' Contains Report Generators that use the WPF Report System
''' </summary>
''' <remarks></remarks>
''' <features></features>
''' <stepthrough>Disabled</stepthrough>
<Obsolete("Test", True)>
Public NotInheritable Class WPFReports
    Private Sub New()
    End Sub


    ''' <summary>
    ''' The Base Class for all WPF Reports
    ''' </summary>
    ''' <remarks></remarks>
    ''' <features></features>
    ''' <stepthrough>Enabled</stepthrough>
    Public MustInherit Class Report

        ''' <summary>
        ''' Finishes the Formatting for the Report then you Must Display it
        ''' </summary>
        ''' <param name="Report"></param>
        ''' <param name="Data"></param>
        ''' <remarks></remarks>
        Protected MustOverride Sub ShowHelper(ByVal Report As CodeReason.Reports.ReportDocument, ByVal Data As CodeReason.Reports.ReportData)

        ''' <summary>
        ''' A Dictionary(Of Section Header, Data for the Table)
        ''' </summary>
        ''' <remarks></remarks>
        Public Property DataTables As New Dictionary(Of String, DataTable)

        ''' <summary>
        ''' A List of Document Values to use in the Report
        ''' </summary>
        ''' <value></value>
        ''' <remarks></remarks>
        Public Property ReportDocumentValues As New Dictionary(Of String, Object)

        ''' <summary>
        ''' Either A Path to an XAML file of the Text from that File to use in the Path
        ''' </summary>
        ''' <value></value>
        ''' <remarks></remarks>
        Public Property XamlImagePath As String

        ''' <summary>
        ''' The Report Header/Title
        ''' </summary>
        ''' <value></value>
        ''' <remarks></remarks>
        Public Property Title As String


        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="Title">The Title of the Report</param>
        ''' <param name="XamlImagePath">Either A Path to an XAML file of the Text from that File to use in the Path</param>
        ''' <remarks></remarks>
        Sub New(ByVal Title As String, Optional ByVal XamlImagePath As String = "")
            Me.Title = Title
            Me.XamlImagePath = XamlImagePath
        End Sub


        ''' <summary>
        ''' 
        ''' </summary>
        ''' <remarks></remarks>
        ''' <stepthrough></stepthrough>
        Public Overridable Sub Show()
            Dim Report As New CodeReason.Reports.ReportDocument With {.ReportTitle = Title}
            Dim Data As New CodeReason.Reports.ReportData
            Data.ReportDocumentValues.Add("PrintDate", DateTime.Now)

            DataTables.ForEach(Sub(x) Data.DataTables.Add(x.Value))
            For Each Item In ReportDocumentValues
                If Not Data.ReportDocumentValues.ContainsKey(Item.Key) Then
                    Data.ReportDocumentValues.Add(Item.Key, Item.Value)
                End If
            Next

            If XamlImagePath.Contains("C:\") Then
                Dim reader As New IO.StreamReader(New IO.FileStream(XamlImagePath, IO.FileMode.Open, IO.FileAccess.Read))
                Report.XamlData = reader.ReadToEnd()
                reader.Close()
            Else
                Report.XamlData = XamlImagePath
            End If

            ShowHelper(Report, Data)
        End Sub

    End Class


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <features></features>
    ''' <stepthrough></stepthrough>
    <Obsolete("Test", True)>
    Public Class ChartReport
        Inherits Report
        Private View3D As Boolean
        Public Property Type As ChartType

        Public Enum ChartType
            Area
            Bar
            Bubble
            CandleStick
            Column
            Doughnut
            Line
            Pie
            Point
            SectionFunnel
            StackedArea100
            StackedArea
            StackedBar100
            StackedBar
            StackedColumn100
            StackedColumn
            Stock
            StreamLineFunnel
        End Enum






        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="Title"></param>
        ''' <param name="XamlImagePath"></param>
        ''' <remarks></remarks>
        ''' <stepthrough></stepthrough>
        Sub New(ByVal Title As String, Optional ByVal XamlImagePath As String = "")
            MyBase.new(Title, XamlImagePath)
        End Sub

        Private Function GetGroup(ByVal TableName As String, ByVal FirstColumn As String, ByVal LastColumn As String, ByVal Width As Integer, _
                                  ByVal Height As Integer, ByVal View3D As Boolean) As String

            Dim Node =
                <Paragraph>
                    <CH TableName="{0}" TableColumns="{1},{2}" Width="{3}cm" Height="{4}cm" View3D="{5}" Title="{6}"></CH>
                </Paragraph>
            Dim NewName = [Enum].GetName(GetType(ChartType), Me.Type) & "Chart"
            Node.<CH>(0).Name = NewName


            Dim Text As String = Node.ToString.Replace(NewName, "crcv:" & NewName)

            Text = Text.ToString.Formatter(TableName, FirstColumn, LastColumn, Width, Height, View3D, Me.Title)


            Return Text
        End Function


        Protected Overrides Sub ShowHelper(ByVal Report As CodeReason.Reports.ReportDocument, ByVal Data As CodeReason.Reports.ReportData)
            Dim Groups As String = ""
            For Each Item In Me.DataTables
                Groups += GetGroup(Item.Value.TableName, Item.Value.Columns(0).ColumnName, Item.Value.Columns(1).ColumnName, 16, 5, True)
            Next

            Dim Text = Report.XamlData.Replace("{0}", Groups)

            Report.XamlData = Text





            Dim IsForm As New Forms.Form With {.WindowState = Forms.FormWindowState.Maximized}
            Dim this As New Windows.Controls.DocumentViewer With {.Document = Report.CreateXpsDocument(Data).GetFixedDocumentSequence}
            IsForm.Controls.Add(New Forms.Integration.ElementHost With {.Dock = Forms.DockStyle.Fill, .Child = this})
            IsForm.ShowDialog()
        End Sub

    End Class


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <features></features>
    ''' <stepthrough></stepthrough>
    <Obsolete("Test", True)>
    Public Class DynamicReportWPF
        Inherits Report

        ''' <summary>
        ''' A List(Of String) that Adds in the Column Headers.
        ''' Add in some Way to Allow each Section to have its own Column Headers [I'm sure you can do this]
        ''' 
        ''' Try Using the Columns in Each DataTable
        ''' </summary>
        ''' <value></value>
        ''' <remarks></remarks>
        Public Property Header As New List(Of String)

        Public Property Details As String



        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="XamlImagePath">Either A Path to an XAML file of the Text from that File to use in the Path</param>
        ''' <param name="Header">A List(Of String) that Adds in the Column Headers.</param>
        ''' <remarks></remarks>
        Sub New(ByVal Title As String, ByVal Details As String, Optional ByVal XamlImagePath As String = "", Optional ByVal Header As List(Of String) = Nothing)
            MyBase.New(Title, XamlImagePath)
            Me.Details = Details
            Me.Header = Header
        End Sub


        ''' <summary>
        ''' Returns The Text used in A Table in A Report
        ''' </summary>
        ''' <param name="TableName"></param>
        ''' <param name="Title"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetGroup(ByVal TableName As String, ByVal HeaderTableName As String, ByVal Title As String) As String
            Return My.Resources.MyText.Replace("{0}", TableName).Replace("{1}", Title).Replace("{2}", HeaderTableName)
        End Function


        Protected Overrides Sub ShowHelper(ByVal Report As CodeReason.Reports.ReportDocument, ByVal Data As CodeReason.Reports.ReportData)
            Data.ReportDocumentValues.Add("Paragraph", Details)
            Dim TextHolder As String = "", I As Integer = 0
            For Each Item In DataTables
                Dim HeaderTable As New DataTable("Headers" & I)
                HeaderTable.Columns.Add()
                For Each Column As DataColumn In Item.Value.Columns
                    HeaderTable.Rows.Add(Column.ColumnName)
                Next

                Data.DataTables.Add(HeaderTable)
                TextHolder += C2 & GetGroup(Item.Value.TableName, HeaderTable.TableName, Item.Key)
                I += 1
            Next



            Report.XamlData = Report.XamlData.Replace("<!--{0}-->", TextHolder)
            Dim IsForm As New Forms.Form With {.WindowState = Forms.FormWindowState.Maximized}
            Dim this As New Windows.Controls.DocumentViewer With {.Document = Report.CreateXpsDocument(Data).GetFixedDocumentSequence}
            IsForm.Controls.Add(New Forms.Integration.ElementHost With {.Dock = Forms.DockStyle.Fill, .Child = this})
            IsForm.ShowDialog()
        End Sub
    End Class


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <features></features>
    ''' <stepthrough></stepthrough>
    <Obsolete("Test", True)>
    Public Class DynamicReportWPF1


        ''' <summary>
        ''' A List of Document Values to use in the Report
        ''' </summary>
        ''' <value></value>
        ''' <remarks></remarks>
        Public Property ReportDocumentValues As New Dictionary(Of String, Object)

        ''' <summary>
        ''' A Dictionary(Of Section Header, Data for the Table)
        ''' </summary>
        ''' <remarks></remarks>
        Public Property DataTables As New Dictionary(Of String, DataTable)

        ''' <summary>
        ''' A List(Of String) that Adds in the Column Headers.
        ''' Add in some Way to Allow each Section to have its own Column Headers [I'm sure you can do this]
        ''' 
        ''' Try Using the Columns in Each DataTable
        ''' </summary>
        ''' <value></value>
        ''' <remarks></remarks>
        Public Property Header As New List(Of String)

        ''' <summary>
        ''' Either A Path to an XAML file of the Text from that File to use in the Path
        ''' </summary>
        ''' <value></value>
        ''' <remarks></remarks>
        Public Property XamlImagePath As String
        ''' <summary>
        ''' The Report Header/Title
        ''' </summary>
        ''' <value></value>
        ''' <remarks></remarks>
        Public Property Title As String
        Public Property Details As String



        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="XamlImagePath">Either A Path to an XAML file of the Text from that File to use in the Path</param>
        ''' <param name="Header">A List(Of String) that Adds in the Column Headers.</param>
        ''' <remarks></remarks>
        Sub New(ByVal Title As String, ByVal Details As String, Optional ByVal XamlImagePath As String = "", Optional ByVal Header As List(Of String) = Nothing)
            Me.Title = Title
            Me.Details = Details
            Me.XamlImagePath = XamlImagePath
            Me.Header = Header
        End Sub


        ''' <summary>
        ''' Returns The Text used in A Table in A Report
        ''' </summary>
        ''' <param name="TableName"></param>
        ''' <param name="Title"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function GetGroup(ByVal TableName As String, ByVal HeaderTableName As String, ByVal Title As String) As String
            Return My.Resources.MyText.Replace("{0}", TableName).Replace("{1}", Title).Replace("{2}", HeaderTableName)
        End Function




        ''' <summary>
        ''' A List(Of String) that Adds in the Column Headers.
        ''' Add in some Way to Allow each Section to have its own Column Headers [I'm sure you can do this]
        ''' 
        ''' Try Using the Columns in Each DataTable
        ''' </summary>
        ''' <remarks></remarks>
        Sub Show()
            Dim Report As New CodeReason.Reports.ReportDocument With {.ReportTitle = Title}
            Dim Data As New CodeReason.Reports.ReportData
            DataTables.ForEach(Sub(x) Data.DataTables.Add(x.Value))
            For Each Item In ReportDocumentValues
                Data.ReportDocumentValues.Add(Item.Key, Item.Value)
            Next


            Data.ReportDocumentValues.Add("Paragraph", Details)
            Data.ReportDocumentValues.Add("PrintDate", DateTime.Now)
            If XamlImagePath.Contains("C:\") Then
                Dim reader As New IO.StreamReader(New IO.FileStream(XamlImagePath, IO.FileMode.Open, IO.FileAccess.Read))
                Report.XamlData = reader.ReadToEnd()
                reader.Close()
            Else
                Report.XamlData = XamlImagePath
            End If

            Dim TextHolder As String = ""
            Dim I As Integer = 0
            For Each Item In DataTables
                Dim HeaderTable As New DataTable("Headers" & I)
                HeaderTable.Columns.Add()
                For Each Column As DataColumn In Item.Value.Columns
                    HeaderTable.Rows.Add(Column.ColumnName)
                Next

                Data.DataTables.Add(HeaderTable)
                TextHolder += C2 & GetGroup(Item.Value.TableName, HeaderTable.TableName, Item.Key)
                I += 1
            Next
            Report.XamlData = Report.XamlData.Replace("<!--{0}-->", TextHolder)

            Dim IsForm As New Forms.Form With {.WindowState = Forms.FormWindowState.Maximized}
            Dim this As New Windows.Controls.DocumentViewer With {.Document = Report.CreateXpsDocument(Data).GetFixedDocumentSequence}
            IsForm.Controls.Add(New Forms.Integration.ElementHost With {.Dock = Forms.DockStyle.Fill, .Child = this})
            IsForm.ShowDialog()
        End Sub
    End Class


End Class



''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
''' <features></features>
''' <stepthrough></stepthrough>
<Obsolete("Test", True)>
Public Class XCReport
    Inherits WPFReports.Report
    Private View3D As Boolean
    Public Property Chart As Chart





    Private Function ChartXML() As XElement
        Dim XE =
            <FlowDocument xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                xmlns:xrd="clr-namespace:CodeReason.Reports.Document;assembly=CodeReason.Reports"
                xmlns:crcv="clr-namespace:CodeReason.Reports.Charts.Visifire;assembly=CodeReason.Reports.Charts.Visifire"
                PageHeight="29.7cm" PageWidth="21cm" ColumnWidth="21cm">
                <xrd:ReportProperties>
                    <xrd:ReportProperties.ReportName>ChartReport</xrd:ReportProperties.ReportName>
                    <xrd:ReportProperties.ReportTitle>Chart Report</xrd:ReportProperties.ReportTitle>
                </xrd:ReportProperties>
                <xrd:SectionReportHeader PageHeaderHeight="2" Padding="10,10,10,0" FontSize="12">
                    <Table CellSpacing="0">
                        <Table.Columns>
                            <TableColumn Width="*"/>
                            <TableColumn Width="*"/>
                        </Table.Columns>
                        <TableRowGroup>
                            <TableRow>
                                <TableCell>
                                    <Paragraph>
                                        <xrd:InlineContextValue PropertyName="ReportTitle"/>
                                    </Paragraph>
                                </TableCell>
                                <TableCell>
                                    <Paragraph TextAlignment="Right">
                                        <xrd:InlineDocumentValue PropertyName="PrintDate" Format="dd.MM.yyyy HH:mm:ss"/>
                                    </Paragraph>
                                </TableCell>
                            </TableRow>
                        </TableRowGroup>
                    </Table>
                </xrd:SectionReportHeader>
                <xrd:SectionReportFooter PageFooterHeight="2" Padding="10,0,10,10" FontSize="12">
                    <Table CellSpacing="0">
                        <Table.Columns>
                            <TableColumn Width="*"/>
                            <TableColumn Width="*"/>
                        </Table.Columns>
                        <TableRowGroup>
                            <TableRow>
                                <TableCell>
                                    <Paragraph>
                                    </Paragraph>
                                </TableCell>
                                <TableCell>
                                    <Paragraph TextAlignment="Right">
                                        Page
                                        <xrd:InlineContextValue PropertyName="PageNumber" FontWeight="Bold"/> of
                                        <xrd:InlineContextValue PropertyName="PageCount" FontWeight="Bold"/>
                                    </Paragraph>
                                </TableCell>
                            </TableRow>
                        </TableRowGroup>
                    </Table>
                </xrd:SectionReportFooter>
                <Section Padding="80,10,40,10" FontSize="12">
                    <Paragraph FontSize="24" FontWeight="Bold">
                        Bar Charts
                    </Paragraph>
                    {0}
                </Section>
            </FlowDocument>




        Return XE
    End Function






    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Title"></param>
    ''' <param name="XamlImagePath"></param>
    ''' <remarks></remarks>
    ''' <stepthrough></stepthrough>
    Sub New(ByVal Title As String, Optional ByVal XamlImagePath As String = "")
        MyBase.new(Title, XamlImagePath)
    End Sub

    Private Function GetGroup(ByVal Chart As Chart) As String
        Dim XE = <Paragraph/>
        XE.Add(Chart.ToXML)
        Return XE.ToString
    End Function


    Protected Overrides Sub ShowHelper(ByVal Report As CodeReason.Reports.ReportDocument, ByVal Data As CodeReason.Reports.ReportData)
        Dim Groups As String = ""
        For Each Item In Me.DataTables
            Groups += GetGroup(Me.Chart)
        Next

        Dim Text = ChartXML.ToString.Replace("{0}", Groups)




        Report.XamlData = Text


        Dim IsForm As New Forms.Form With {.WindowState = Forms.FormWindowState.Maximized}
        Dim this As New Windows.Controls.DocumentViewer With {.Document = Report.CreateXpsDocument(Data).GetFixedDocumentSequence}
        IsForm.Controls.Add(New Forms.Integration.ElementHost With {.Dock = Forms.DockStyle.Fill, .Child = this})
        IsForm.ShowDialog()
    End Sub

End Class