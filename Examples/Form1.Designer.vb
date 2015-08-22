<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.btnReport1 = New System.Windows.Forms.Button()
        Me.btnComplexReports = New System.Windows.Forms.Button()
        Me.btnReportTesting = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnReport1
        '
        Me.btnReport1.Location = New System.Drawing.Point(13, 13)
        Me.btnReport1.Name = "btnReport1"
        Me.btnReport1.Size = New System.Drawing.Size(100, 23)
        Me.btnReport1.TabIndex = 0
        Me.btnReport1.Text = "Sample Report 1"
        Me.btnReport1.UseVisualStyleBackColor = True
        '
        'btnComplexReports
        '
        Me.btnComplexReports.Location = New System.Drawing.Point(119, 13)
        Me.btnComplexReports.Name = "btnComplexReports"
        Me.btnComplexReports.Size = New System.Drawing.Size(100, 23)
        Me.btnComplexReports.TabIndex = 1
        Me.btnComplexReports.Text = "Complex Reports"
        Me.btnComplexReports.UseVisualStyleBackColor = True
        '
        'btnReportTesting
        '
        Me.btnReportTesting.Location = New System.Drawing.Point(13, 84)
        Me.btnReportTesting.Name = "btnReportTesting"
        Me.btnReportTesting.Size = New System.Drawing.Size(100, 23)
        Me.btnReportTesting.TabIndex = 2
        Me.btnReportTesting.Text = "Reports Testing"
        Me.btnReportTesting.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(970, 557)
        Me.Controls.Add(Me.btnReportTesting)
        Me.Controls.Add(Me.btnComplexReports)
        Me.Controls.Add(Me.btnReport1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnReport1 As Button
    Friend WithEvents btnComplexReports As Button
    Friend WithEvents btnReportTesting As Button
End Class
