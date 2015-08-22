Imports Aaron.Reports

Public Class Form1
    Private Sub btnReport1_Click(sender As Object, e As EventArgs) Handles btnReport1.Click
        '******************************************************************************************************
        'This example requires the following DLL: 
        '
        'Aaron.Xaml, Aaron.Reports, PresentationCore, PresentationFramework, WindowsBase
        '******************************************************************************************************

        Dim Report As New Basic("Hello World", "None")

        Report.Sections.Add(New Sections.List("List", "A", "B", "C"))
        Report.Sections.Add(New Sections.Basic("Header", "Body", "Footer"))

        Dim Table As New Sections.Table("Title", "Details")
        Table.Table.Columns.Add(New System.Windows.Documents.TableColumn() With {.Width = New System.Windows.GridLength(100), .Tag = "Name"})
        Table.Table.Columns.Add(New System.Windows.Documents.TableColumn() With {.Width = New System.Windows.GridLength(100), .Tag = "Value"})
        Table.Table.AddRow(0, System.Windows.TextAlignment.Left, "A", "B")
        Report.Sections.Add(Table)

        Report.Show()
    End Sub

    Private Sub btnComplexReports_Click(sender As Object, e As EventArgs) Handles btnComplexReports.Click
        '******************************************************************************************************
        'This example requires the following DLL: 
        '
        'Aaron.Xaml, Aaron.Reports, PresentationCore, PresentationFramework, WindowsBase
        'CodeReason.Reports.Charts.Visifire, CodeReason.Reports, itextsharp, WPFVisifire.Charts
        '******************************************************************************************************

        Dim Report As New Basic("Hello World", "None")
        Report.Sections.Add(New Sections.Basic("Header", "Body", "Footer"))

        If My.Computer.FileSystem.FileExists("PDFExample.pdf") Then My.Computer.FileSystem.DeleteFile("PDFExample.pdf")
        Dim File = Report.AsPDF
        My.Computer.FileSystem.MoveFile(File, "PDFExample.pdf")
        Process.Start("PDFExample.pdf")
    End Sub

    Private Sub btnReportTesting_Click(sender As Object, e As EventArgs) Handles btnReportTesting.Click
        Dim Report As New Basic("Hello World", "None")
        Report.Bottom_Left.Inlines.Add("Bottom_Left")

        Report.Sections.Add(New Sections.List("List", "A", "B", "C"))
        Report.Sections.Add(New Sections.Basic("Header", "Body", "Footer"))

        Dim Table As New Sections.Table("Title", "Details")
        Table.Table.Columns.Add(New System.Windows.Documents.TableColumn() With {.Width = New System.Windows.GridLength(100), .Tag = "Name"})
        Table.Table.Columns.Add(New System.Windows.Documents.TableColumn() With {.Width = New System.Windows.GridLength(100), .Tag = "Value"})
        Table.Table.AddRow(0, System.Windows.TextAlignment.Left, "A", "B")
        Report.Sections.Add(Table)

        Report.Show()
    End Sub

End Class
