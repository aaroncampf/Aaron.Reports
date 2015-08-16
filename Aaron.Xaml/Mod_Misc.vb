Imports System.Windows.Documents
Imports System.Windows

''' <summary>
''' Section representing the report header
''' </summary>
<DebuggerStepThrough>
Public Class SectionReportHeader
    Inherits Section
    ''' <summary>
    ''' Gets or sets the page header height in percent
    ''' </summary>
    Public Property PageHeaderHeight As Double = 2.0

End Class

''' <summary>
''' Section representing the report footer
''' </summary>
<DebuggerStepThrough>
Public Class SectionReportFooter
    Inherits Section
    ''' <summary>
    ''' Gets or sets the page footer height in percent
    ''' </summary>
    Public Property PageFooterHeight As Double = 2.0
End Class


''' <summary>
''' Interface for a chart object
''' </summary>
Public Interface IChart1
    Inherits ICloneable
    ''' <summary>
    ''' Gets or sets the table columns which are used to draw the chart
    ''' </summary>
    Property TableColumns() As String

    ''' <summary>
    ''' Gets or sets the table name containing the data to be drawn
    ''' </summary>
    Property TableName() As String

    ''' <summary>
    ''' Gets or sets the data columns which are used to draw the chart
    ''' </summary>
    Property DataColumns() As String()

    ''' <summary>
    ''' Data view to be used to draw the data
    ''' </summary>
    Property DataView() As DataView

    ''' <summary>
    ''' Updates the chart to use the chart data
    ''' </summary>
    Sub UpdateChart()
End Interface

''' <summary>
''' Abstract class for fillable run values
''' </summary>
<Obsolete("Removing", True)>
Public MustInherit Class InlineHasValue1
    Inherits Run
    'Implements IHasValue1
    ''' <summary>
    ''' Gets or sets the value format
    ''' </summary>
    Public Overridable Property Format() As String ' Implements IHasValue1.Format
        Get
            Return DirectCast(GetValue(FormatProperty), String)
        End Get
        Set(value As String)
            SetValue(FormatProperty, value)
        End Set
    End Property

    ' Using a DependencyProperty as the backing store for Format.  This enables animation, styling, binding, etc...
    Public Shared ReadOnly FormatProperty As DependencyProperty = DependencyProperty.Register("Format", GetType(String), GetType(InlineHasValue1), New UIPropertyMetadata(Nothing))

    ''' <summary>
    ''' Gets or sets the object value
    ''' </summary>
    Public Overridable Property Value() As Object ' Implements IHasValue1.Value
        Get
            Return DirectCast(GetValue(ValueProperty), Object)
        End Get
        Set(value As Object)
            SetValue(ValueProperty, value)
            Text = FormatValue(value, Format)
        End Set
    End Property

    ' Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
    Public Shared ReadOnly ValueProperty As DependencyProperty = DependencyProperty.Register("Value", GetType(Object), GetType(InlineHasValue1), New UIPropertyMetadata(Nothing))

    ''' <summary>
    ''' Identifies the ValueChanged routed event.
    ''' </summary>
    Public Shared ReadOnly ValueChangedEvent As RoutedEvent = EventManager.RegisterRoutedEvent("ValueChanged", RoutingStrategy.Bubble, GetType(RoutedPropertyChangedEventHandler(Of Decimal)), GetType(InlineHasValue1))

    ''' <summary>
    ''' Raises the ValueChanged event.
    ''' </summary>
    ''' <param name="args">Arguments associated with the ValueChanged event.</param>
    Protected Overridable Sub OnValueChanged(args As RoutedPropertyChangedEventArgs(Of Decimal))
        [RaiseEvent](args)
    End Sub

    ''' <summary>
    ''' Formats a value for output
    ''' </summary>
    ''' <param name="value">value</param>
    ''' <param name="format">format</param>
    ''' <returns></returns>
    Public Shared Function FormatValue(value As Object, format As String) As String
        If value Is Nothing Then
            Return ""
        ElseIf String.IsNullOrEmpty(format) Then
            Return value.ToString()
        End If

        Select Case value.[GetType]()
            Case GetType(DateTime)
                Return DirectCast(value, DateTime).ToString(format)
            Case GetType(Decimal)
                Return CDec(value).ToString(format)
            Case GetType(Double)
                Return CDbl(value).ToString(format)
            Case GetType(Single)
                Return CSng(value).ToString(format)
            Case GetType(Integer)
                Return CInt(value).ToString(format)
            Case GetType(Long)
                Return CLng(value).ToString(format)
            Case GetType(Short)
                Return CShort(value).ToString(format)
            Case GetType(UInteger)
                Return CUInt(value).ToString(format)
            Case GetType(ULong)
                Return CULng(value).ToString(format)
            Case GetType(UShort)
                Return CUShort(value).ToString(format)
            Case Else
                Return value.ToString()
        End Select
    End Function
End Class

''' <summary>
''' Contains a single report context value that is to be displayed on the report
''' </summary>
<DebuggerDisplay("InlineContextValue: {PropertyName}")>
Public Class InlineContextValue
    Inherits Run 'Inherits InlinePropertyValue
    '' ''' <summary>
    '' ''' Gets or sets the aggregate group
    '' ''' </summary>
    ''Public Property AggregateGroup() As String 'Implements IAggregateValue1.AggregateGroup

    '' Using a DependencyProperty as the backing store for PropertyName.  This enables animation, styling, binding, etc...
    'Public Shared ReadOnly PropertyNameProperty As DependencyProperty =
    '	DependencyProperty.Register("PropertyName", GetType(String), GetType(InlinePropertyValue), New UIPropertyMetadata(Nothing))


    ' Using a DependencyProperty as the backing store for PropertyName.  This enables animation, styling, binding, etc...
    Public Shared ReadOnly PropertyNameProperty As DependencyProperty =
        DependencyProperty.Register("PropertyName", GetType(String), GetType(InlineContextValue), New UIPropertyMetadata(Nothing))

    ' Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
    Public Shared ReadOnly ValueProperty As DependencyProperty =
        DependencyProperty.Register("Value", GetType(Object), GetType(InlineContextValue), New UIPropertyMetadata(Nothing))


    ' Using a DependencyProperty as the backing store for Format.  This enables animation, styling, binding, etc...
    Public Shared ReadOnly FormatProperty As DependencyProperty =
        DependencyProperty.Register("Format", GetType(String), GetType(InlineContextValue), New UIPropertyMetadata(Nothing))

    ''' <summary>
    ''' Gets or sets the value format
    ''' </summary>
    Public Overridable Property Format As String 'Implements IHasValue1.Format
        Get
            Return DirectCast(GetValue(FormatProperty), String)
        End Get
        Set(value As String)
            SetValue(FormatProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the property name
    ''' </summary>
    Public Overridable Property PropertyName() As String
        Get
            Return DirectCast(GetValue(PropertyNameProperty), String)
        End Get
        Set(value As String)
            SetValue(PropertyNameProperty, value)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the object value
    ''' </summary>
    Public Overridable Property Value() As Object ' Implements IHasValue1.Value
        Get
            Return DirectCast(GetValue(ValueProperty), Object)
        End Get
        Set(value As Object)
            SetValue(ValueProperty, value)
            Text = FormatValue(value, Format)
        End Set
    End Property



    ''' <summary>
    ''' Formats a value for output
    ''' </summary>
    ''' <param name="value">value</param>
    ''' <param name="format">format</param>
    ''' <returns></returns>
    Public Shared Function FormatValue(value As Object, format As String) As String
        If value Is Nothing Then
            Return ""
        ElseIf [String].IsNullOrEmpty(format) Then
            Return value.ToString()
        End If

        Select Case value.[GetType]()
            Case GetType(DateTime)
                Return DirectCast(value, DateTime).ToString(format)
            Case GetType(Decimal)
                Return CDec(value).ToString(format)
            Case GetType(Double)
                Return CDbl(value).ToString(format)
            Case GetType(Single)
                Return CSng(value).ToString(format)
            Case GetType(Integer)
                Return CInt(value).ToString(format)
            Case GetType(Long)
                Return CLng(value).ToString(format)
            Case GetType(Short)
                Return CShort(value).ToString(format)
            Case GetType(UInteger)
                Return CUInt(value).ToString(format)
            Case GetType(ULong)
                Return CULng(value).ToString(format)
            Case GetType(UShort)
                Return CUShort(value).ToString(format)
        End Select

        Return value.ToString()
    End Function


    ''' <summary>
    ''' Raises the ValueChanged event.
    ''' </summary>
    ''' <param name="args">Arguments associated with the ValueChanged event.</param>
    Protected Overridable Sub OnValueChanged(args As RoutedPropertyChangedEventArgs(Of Decimal))
        [RaiseEvent](args)
    End Sub

End Class


''' <summary>
''' Contains a single report value that is to be displayed on the report (e.g. report title)
''' </summary>
<DebuggerStepThrough>
Public Class InlineDocumentValue
    Inherits Run

    ''' <summary>
    ''' Gets or sets the value format
    ''' </summary>
    Public Overridable Property Format As String 'Implements IHasValue1.Format
        Get
            Return DirectCast(GetValue(FormatProperty), String)
        End Get
        Set(value As String)
            SetValue(FormatProperty, value)
        End Set
    End Property


    ' Using a DependencyProperty as the backing store for Format.  This enables animation, styling, binding, etc...
    Public Shared ReadOnly FormatProperty As DependencyProperty =
        DependencyProperty.Register("Format", GetType(String), GetType(InlineDocumentValue), New UIPropertyMetadata(Nothing))

    ''' <summary>
    ''' Gets or sets the object value
    ''' </summary>
    Public Overridable Property Value As Object
        Get
            Return DirectCast(GetValue(ValueProperty), Object)
        End Get
        Set(value As Object)
            SetValue(ValueProperty, value)
            Text = FormatValue(value, Format)
        End Set
    End Property

    ' Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
    Public Shared ReadOnly ValueProperty As DependencyProperty =
        DependencyProperty.Register("Value", GetType(Object), GetType(InlineDocumentValue), New UIPropertyMetadata(Nothing))

    ''' <summary>
    ''' Identifies the ValueChanged routed event.
    ''' </summary>
    Public Shared ReadOnly ValueChangedEvent As RoutedEvent =
        EventManager.RegisterRoutedEvent("ValueChanged", RoutingStrategy.Bubble, GetType(RoutedPropertyChangedEventHandler(Of Decimal)), GetType(InlineDocumentValue))

    ''' <summary>
    ''' Raises the ValueChanged event.
    ''' </summary>
    ''' <param name="args">Arguments associated with the ValueChanged event.</param>
    Protected Overridable Sub OnValueChanged(args As RoutedPropertyChangedEventArgs(Of Decimal))
        [RaiseEvent](args)
    End Sub

    ''' <summary>
    ''' Formats a value for output
    ''' </summary>
    ''' <param name="value">value</param>
    ''' <param name="format">format</param>
    ''' <returns></returns>
    Public Shared Function FormatValue(value As Object, format As String) As String
        If value Is Nothing Then
            Return ""
        ElseIf [String].IsNullOrEmpty(format) Then
            Return value.ToString()
        End If

        Select Case value.[GetType]()
            Case GetType(DateTime)
                Return DirectCast(value, DateTime).ToString(format)
            Case GetType(Decimal)
                Return CDec(value).ToString(format)
            Case GetType(Double)
                Return CDbl(value).ToString(format)
            Case GetType(Single)
                Return CSng(value).ToString(format)
            Case GetType(Integer)
                Return CInt(value).ToString(format)
            Case GetType(Long)
                Return CLng(value).ToString(format)
            Case GetType(Short)
                Return CShort(value).ToString(format)
            Case GetType(UInteger)
                Return CUInt(value).ToString(format)
            Case GetType(ULong)
                Return CULng(value).ToString(format)
            Case GetType(UShort)
                Return CUShort(value).ToString(format)
        End Select

        Return value.ToString()
    End Function

    ''' <summary>Gets or sets the property name</summary>
    Public Overridable Property PropertyName As String
        Get
            Return DirectCast(GetValue(PropertyNameProperty), String)
        End Get
        Set(value As String)
            SetValue(PropertyNameProperty, value)
        End Set
    End Property

    ' Using a DependencyProperty as the backing store for PropertyName.  This enables animation, styling, binding, etc...
    Public Shared ReadOnly PropertyNameProperty As DependencyProperty =
        DependencyProperty.Register("PropertyName", GetType(String), GetType(InlineDocumentValue), New UIPropertyMetadata(Nothing))
End Class