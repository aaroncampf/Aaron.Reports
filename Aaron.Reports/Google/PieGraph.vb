Imports System.Text

Namespace Google

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <features></features>
    ''' <stepthrough></stepthrough>
    Public Structure ChartItem
        Property Value As Double
        Property Lable As String
        Property Legend As String

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="Value"></param>
        ''' <param name="Lable"></param>
        ''' <param name="Legend"></param>
        ''' <remarks></remarks>
        ''' <stepthrough></stepthrough>
        Sub New(Value As Double, Optional Lable As String = Nothing, Optional Legend As String = Nothing)
            Me.Value = Value
            Me.Lable = Lable
            Me.Legend = Legend
        End Sub
    End Structure


    ''' <summary>
    ''' Summary description for PieGraph
    ''' </summary>
    Public Class Chart
        '*****************************************************
        'Add Support for Legends and (Showlegend + ShowLables)
        '*****************************************************




        Property Showlegend As Boolean
        Property ShowLables As Boolean

        ''' <summary>
        ''' Graph Title to be displayed above the Graph
        ''' </summary> 
        Property GraphTitle As String

        ' ''' <summary>
        ' ''' Colors to be displayed in the graph one or n number of colors eg: A333EE or A333EE,A311EE,1133EE 
        ' ''' </summary> 
        'Property GraphColors As String

        ''' <summary>
        ''' Graph Dimensions width (2:1 -|- width:height)
        ''' </summary> 
        Property GraphWidth As Single = 1000

        ''' <summary>
        ''' Graph Dimensions Height (2:1 -|- width:height)
        ''' </summary> 
        Property GraphHeight As Single = 300

        ''' <summary>
        ''' Graph Title Color (Title Font Color eg:5533AA)
        ''' </summary> 
        Property GraphTitleColor As String

        ''' <summary>
        ''' Graph Title Size (Title Font Size in pixcels 1-20)
        ''' </summary>  
        Property GraphTitleSize As Single



        Dim _Scale As String = "a"
        ReadOnly Property Scale As String
            Get
                Return _Scale
            End Get
        End Property



        'Dim _Data As New Dictionary(Of String, Decimal)
        ' ''' <summary>
        ' ''' DataSet for Drawing the Graph Column:2 for Labels and Column:3 for Column Data
        ' ''' </summary> 
        'ReadOnly Property Data As Dictionary(Of String, Decimal)
        '    Get
        '        Return _Data
        '    End Get
        'End Property


        Dim _Data As New List(Of ChartItem)
        ''' <summary>
        ''' DataSet for Drawing the Graph Column:2 for Labels and Column:3 for Column Data
        ''' </summary> 
        ReadOnly Property Data As List(Of ChartItem)
            Get
                Return _Data
            End Get
        End Property





        ''' <summary>
        ''' Method which returns the Url be placed in the image src tag
        ''' </summary>
        ''' <returns>URL String </returns>
        Function GenerateGraph() As String
            Dim maxval As Decimal = Me.Data.Max(Function(x) x.Value)


            'SAMPLE: http://chart.apis.google.com/chart?cht=p3&chs=400x200&chd=s:asR&chl=A|B|C

            'Dim Format = "http://chart.apis.google.com/chart?chts={0},{1}&chtt={2}&chco={3}&cht=p3&chs={4}x{5}"
            'Dim GReqURL = String.Format(Format, Me.GraphTitleColor, GraphTitleSize, GraphTitle, GraphColors, GraphWidth, GraphHeight)

            Dim fScale = If(String.IsNullOrWhiteSpace(_Scale), "a", _Scale)

            Dim Format = "http://chart.apis.google.com/chart?chts={0},{1}&chtt={2}&cht=p3&chs={3}x{4}&chds=" & fScale

            Dim Width = Me.GraphWidth, Height = Me.GraphHeight
            If Width * Height > 300000 Then
                Width = 1000
                Height = 300
            End If


            Dim GReqURL = String.Format(Format, Me.GraphTitleColor, GraphTitleSize, GraphTitle, Width, Height)


            Dim ChartData As New StringBuilder("&chd=t:"), ChartLabels As New StringBuilder("&chl=")

            'For Each Item In Me.Data
            '    ChartData.Append(Item.Value & ",")
            '    ChartLabels.Append(Item.Key & "|")
            'Next

            For Each Item In Me.Data
                ChartData.Append(Item.Value & ",")
                ChartLabels.Append(Item.Lable & "|")
            Next


            'Return GReqURL & ChartData.ToString.Shorten(1) & ChartLabels.ToString.Shorten(1)
            Return GReqURL &
                ChartData.ToString.Remove(ChartData.ToString - 1, 1) &
                ChartLabels.ToString.Remove(ChartLabels.ToString - 1, 1)
        End Function
    End Class

    Partial Class Chart
        Public Enum ChatType
            Pie2D
            Pie3D
            PieConcentric

            Graph

            BarHorizontal
            BarVertical
            BarStacked
            BarGrouped

            Line
            SparkLine

            GoogleOMeter

        End Enum

        Private Function ChatTypeToString(Type As ChatType) As String
            Select Case Type
                Case ChatType.Line
                    Return "&cht=lc"
                Case ChatType.SparkLine
                    Return "&cht=ls"


                Case ChatType.BarVertical
                    Return "&cht=bvg"
                Case ChatType.BarHorizontal
                    Return "&cht=bhg"
                Case ChatType.BarStacked
                    Return "&cht=bhs"
                Case ChatType.BarGrouped
                    Return "&cht=bvg"


                Case ChatType.GoogleOMeter
                    Return "&cht=gm"



                Case ChatType.Pie3D
                    Return "&cht=p3"
                Case ChatType.PieConcentric
                    Return "&cht=pc"
                Case Else
                    Return "&cht=p"
            End Select

        End Function


    End Class


    Public Class GoogleOMeter
        Inherits Chart


    End Class

End Namespace