Public Class Design

    Public Class Report_Table

        Dim _Columns As New List(Of Report_Column)
        Public ReadOnly Property Columns As List(Of Report_Column)
            Get
                Return _Columns
            End Get
        End Property


        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="Header"></param>
        ''' <remarks></remarks>
        ''' <stepthrough></stepthrough>
        Public Sub Add(ByVal Header As String)

        End Sub

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="Index"></param>
        ''' <param name="Header"></param>
        ''' <remarks></remarks>
        ''' <stepthrough></stepthrough>
        Public Sub Add(ByVal Index As Integer, ByVal Header As String)

        End Sub



    End Class



    Public Class Report_Column
        Public Property Index As Integer
        Public Property Header As String

        Friend Sub New(ByVal Header As String)
            Me.Header = Header
        End Sub


        Sub New(ByVal Idex As Integer, ByVal Header As String)
            Me.Index = Index
            Me.Header = Header
        End Sub


    End Class


    Public Class BaseReport



    End Class

End Class