Partial Class Sections
    'Public Class Chart
    '    Inherits Basic
    '    Public Property View3D As Boolean = False

    '    ''' <summary>The DataSource for the Chart.</summary>
    '    ''' <remarks>Column 1 = X_Axis Column 2 = Y_Axis</remarks> 
    '    Public Property DataSource As DataTable



    '    ''' <summary>
    '    ''' 
    '    ''' </summary>
    '    ''' <param name="Title">The Optional Title of the Report. If Empty then Appears as A Blank Spot in the Report</param>
    '    ''' <param name="Details">The Optional Details of the Report. If Empty then Appears as A Blank Spot in the Report</param>
    '    ''' <remarks></remarks>
    '    ''' <stepthrough>Enabled</stepthrough>
    '    <DebuggerNonUserCode()>
    '    Sub New(Optional Title As String = "", Optional Details As String = "")
    '        MyBase.New(Title, Details)
    '    End Sub


    '    'Private Function GetGroup(ByVal TableName As String, ByVal FirstColumn As String, ByVal LastColumn As String, ByVal Width As Integer, _
    '    '              ByVal Height As Integer) As String


    '    '    Dim Text As String =
    '    '        <Paragraph>
    '    '            <ColumnChart TableName="{0}" TableColumns="{1},{2}" Width="{3}cm" Height="{4}cm" View3D=<%= Me.View3D %> Title="{6}"></ColumnChart>
    '    '        </Paragraph>.ToString

    '    '    Return Text.Replace("ColumnChart", "crcv:ColumnChart").Formatter(TableName, FirstColumn, LastColumn, Width, Height, View3D, Me.Title)
    '    'End Function




    '    Public Overrides Function ToXML() As XElement
    '        Dim Code = MyBase.ToXML()





    '        Return Code
    '    End Function


    'End Class
End Class


Public Class Chart
    Inherits CodeReason.Reports.Charts.Visifire.ChartBase


    Public Overrides Function Clone() As Object
        Dim MyClone As Chart = MyBase.Clone
        MyClone.Width = Me.Width
        MyClone.Height = Me.Height
        MyClone.TableName = Me.TableName

		Return MyClone
    End Function


    ''' <summary>Data view to be used to draw the data</summary>
    Public Overrides Property DataView As DataView
        Get
            Return MyBase.DataView
        End Get
        Set(value As DataView)
            MyBase.DataView = value
        End Set
    End Property


    '***********************************************************************************************************************************************
    'It seems that the Items for the Chart are stored in mybase.DataView 
    'Where Each Row Contains Column 1 for X-Axis and Column 2 for the Y-Axis
    '
    'To Add Items to the Chart you Use MyBase.DataView.Table.Rows.Add("Aaron",5)) 
    '
    'The Actual Chart Items Are Located in MyBase._chart.Series(0).DataPoints.ToList()
    'The Actual Chart has 1 Data Series Located at MyBase._chart.Series(0)
    'The Chart's Items Are Located at MyBase._chart.Series(0).DataPoints
    'Chart Items have an AxisXLable(Name on Botton) and A Yvalue(Value of Item)
    '
    'MyBase._chart.Series(0).DataPoints.Add(New Visifire.Charts.DataPoint With {.AxisXLabel = "Aaron",.YValue =5})
    '***********************************************************************************************************************************************


	Public Property ChartType() As Visifire.Charts.RenderAs
		Get
			Return MyBase._renderAs
		End Get
		Set(value As Visifire.Charts.RenderAs)
			MyBase._renderAs = value
		End Set
	End Property

    '<DebuggerNonUserCode()>
    Sub New()
        If Me.Height < 1 Then Me.Height = 150
		If Me.Width < 1 Then Me.Width = 300
	End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="ChartType"></param>
    ''' <remarks></remarks>
    ''' <stepthrough>Enabled</stepthrough>
	<DebuggerNonUserCode()>
	Sub New(TableName As String, Columns As String, ChartType As Visifire.Charts.RenderAs)
		Me.New(Nothing, TableName, Columns, ChartType)
		'Me.TableName = TableName
		'Me.TableColumns = Columns
		'Me.ChartType = ChartType
		'Me.Height = 150
		'Me.Width = 300
	End Sub

	''' <summary>
	''' 
	''' </summary>
	''' <param name="ChartType"></param>
	''' <remarks></remarks>
	''' <stepthrough>Enabled</stepthrough>
	<DebuggerNonUserCode()>
	Sub New(Title As String, TableName As String, Columns As String, ChartType As Visifire.Charts.RenderAs)
		Me.Title = Title
		Me.TableName = TableName
		Me.TableColumns = Columns
		Me.ChartType = ChartType
		Me.Height = 150
		Me.Width = 300
	End Sub



    'Protected Overrides Sub PrepareChart()
    '    MyBase.PrepareChart()



    '    'MyBase._chart.Series(0).DataPoints.Add(New Visifire.Charts.DataPoint With {.AxisXLabel = "Aaron", .YValue = 5})

    '    'MyBase._chart = New Visifire.Charts.Chart With {.Height = 150, .Width = 300}

    'End Sub

    'Public Overrides Sub UpdateChart()
    '    MyBase.UpdateChart()

    '    'MyBase._chart = New Visifire.Charts.Chart With {.Height = 150, .Width = 300}

    '    'MyBase._chart.Series.Add(New Visifire.Charts.DataSeries With {.RenderAs = Visifire.Charts.RenderAs.Column})
    '    'MyBase._chart.Series(0).DataPoints.Add(New Visifire.Charts.DataPoint With {.AxisXLabel = "Aaron", .YValue = 5})





    '    'Dim Title1 As New Visifire.Charts.Title
    '    'Title1.Text = "Hello Title1"

    '    'MyBase._chart.Titles.Add(Title1)
    'End Sub


    Public Function ToXML() As XElement
        Dim Node = Extensions.ToXML(Me)


        Return Node
    End Function




End Class