﻿Public NotInheritable Class WPFReport_XML
    Private Sub New()
    End Sub

    Private Const strFileName As String = "XMLFile.config"
    Public Shared Function GetXMLDocument() As System.Xml.XmlDocument
        '
        Dim assembly__1 = Reflection.Assembly.GetExecutingAssembly()
        Dim stream = assembly__1.GetManifestResourceStream("Aaron.Reports.DynamicReport.xml")

        Dim Reader As New IO.StreamReader(stream)

        Dim XE = XElement.Load(stream)

    End Function



    Public Shared Function BaseReport() As XElement
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

    Public Shared Function Document() As XElement
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

            </FlowDocument>



        Return XE
    End Function





    Public Shared Function Table() As XElement
        'I Manually added in xmlns:xrd="clr-namespace:CodeReason.Reports.Document;assembly=CodeReason.Reports"
        'This prevents a Really stupid Warning

        Dim XE =
            <Table CellSpacing="0" xmlns:xrd="clr-namespace:CodeReason.Reports.Document;assembly=CodeReason.Reports">
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



        Return XE
    End Function




End Class
