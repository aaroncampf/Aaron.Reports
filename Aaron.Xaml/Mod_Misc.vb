Imports System.Windows.Documents
Imports System.Windows

'Public Module Mod_Misc


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

	'Public Property PageHeaderHeight As Double
	'	Get
	'		Return CDbl(GetValue(PageHeaderHeightProperty))
	'	End Get
	'	Set(value As Double)
	'		SetValue(PageHeaderHeightProperty, value)
	'	End Set
	'End Property

	'' Using a DependencyProperty as the backing store for PageHeaderHeight.  This enables animation, styling, binding, etc...
	'Public Shared ReadOnly PageHeaderHeightProperty As DependencyProperty =
	'						DependencyProperty.Register("PageHeaderHeight", GetType(Double), GetType(ReportProperties1), New UIPropertyMetadata(2.0))
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

	''Public Property PageFooterHeight As Double
	''	Get
	''		Return CDbl(GetValue(PageFooterHeightProperty))
	''	End Get
	''	Set(value As Double)
	''		SetValue(PageFooterHeightProperty, value)
	''	End Set
	''End Property

	' '' Using a DependencyProperty as the backing store for PageFooterHeight.  This enables animation, styling, binding, etc...
	''Public Shared ReadOnly PageFooterHeightProperty As DependencyProperty =
	''						DependencyProperty.Register("PageFooterHeight", GetType(Double), GetType(ReportProperties1), New UIPropertyMetadata(2.0))
End Class


' ''' <summary>
' ''' Specifies properties for report
' ''' </summary>
'<Obsolete("Removing", True)>
'Public Class ReportProperties1
'	Inherits Section
'	''' <summary>
'	''' Gets or sets the report name
'	''' </summary>
'	Public Property ReportName As String
'		Get
'			Return DirectCast(GetValue(ReportNameProperty), String)
'		End Get
'		Set(value As String)
'			SetValue(ReportNameProperty, value)
'		End Set
'	End Property

'	' Using a DependencyProperty as the backing store for ReportName.  This enables animation, styling, binding, etc...
'	Public Shared ReadOnly ReportNameProperty As DependencyProperty =
'								DependencyProperty.Register("ReportName", GetType(String), GetType(ReportProperties1), New UIPropertyMetadata(Nothing))

'	''' <summary>
'	''' Gets or sets the report title
'	''' </summary>
'	Public Property ReportTitle As String
'		Get
'			Return DirectCast(GetValue(ReportTitleProperty), String)
'		End Get
'		Set(value As String)
'			SetValue(ReportTitleProperty, value)
'		End Set
'	End Property

'	' Using a DependencyProperty as the backing store for ReportTitle.  This enables animation, styling, binding, etc...
'	Public Shared ReadOnly ReportTitleProperty As DependencyProperty =
'								DependencyProperty.Register("ReportTitle", GetType(String), GetType(ReportProperties1), New UIPropertyMetadata(Nothing))
'End Class

' ''' <summary>
' ''' Interface for inline context values
' ''' </summary>
'<Obsolete("Removing", True)>
'Public Interface IInlineContextValue1
'	Inherits IPropertyValue1
'	'Inherits IAggregateValue1
'End Interface

' ''' <summary>
' ''' Interface for property values
' ''' </summary>
'<Obsolete("Removing", True)>
'Public Interface IPropertyValue1
'	Inherits IHasValue1
'	''' <summary>
'	''' Gets or sets the property name
'	''' </summary>
'	Property PropertyName() As String
'End Interface

' ''' <summary>
' ''' Interface for values to be used as aggregate values
' ''' </summary>
'<Obsolete("Being Removed", True)>
'Public Interface IAggregateValue1
'	''' <summary>
'	''' Gets or sets the aggregate group
'	''' </summary>
'	Property AggregateGroup() As String
'End Interface

' ''' <summary>
' ''' Interface for values
' ''' </summary>
'<Obsolete("Removing", True)>
'Public Interface IHasValue1
'	''' <summary>
'	''' Gets or sets the value format
'	''' </summary>
'	Property Format() As String

'	''' <summary>
'	''' Gets or sets the object value
'	''' </summary>
'	Property Value() As Object
'End Interface


' ''' <summary>
' ''' Interface for inline property values
' ''' </summary>
'<Obsolete("Removing", True)>
'Public Interface IInlineDocumentValue1
'	Inherits IPropertyValue1
'	'Inherits IAggregateValue1
'End Interface

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

' ''' <summary>
' ''' Interface for inline property values
' ''' </summary>
'<Obsolete("Removing", True)>
'Public Interface IInlinePropertyValue1
'	Inherits IPropertyValue1
'End Interface

' ''' <summary>
' ''' Interface for table cell values
' ''' </summary>
'Public Interface ITableCellValue1
'	Inherits IHasValue1
'	'Inherits IAggregateValue1
'End Interface

' ''' <summary>
' ''' Interface for special table rows
' ''' </summary>
'Public Interface ITableRowForDataTable1
'	''' <summary>
'	''' Gets or sets the table name
'	''' </summary>
'	Property TableName() As String
'End Interface

' ''' <summary>
' ''' Interface for special table rows
' ''' </summary>
'Public Interface ITableRowForDynamicDataTable1
'	''' <summary>
'	''' Gets or sets the table name
'	''' </summary>
'	Property TableName() As String
'End Interface

' ''' <summary>
' ''' Interface for special table rows
' ''' </summary>
'Public Interface ITableRowForDynamicHeader1
'	''' <summary>
'	''' Gets or sets the table name
'	''' </summary>
'	Property TableName() As String
'End Interface


' ''' <summary>
' ''' Computes a single aggregate report value that is to be displayed on the report (e.g. report title)
' ''' </summary>
' ''' 
'<Obsolete("Removing", True)>
'Public Class InlineAggregateValue1
'	Inherits InlineHasValue1
'	Private _aggregateGroup As String = Nothing
'	''' <summary>
'	''' Gets or sets the aggregate group
'	''' </summary>
'	Public Property AggregateGroup() As String
'		Get
'			Return _aggregateGroup
'		End Get
'		Set(value As String)
'			_aggregateGroup = value
'		End Set
'	End Property

'	Private _aggregateValueType As ReportAggregateValueType1 = ReportAggregateValueType1.Count
'	''' <summary>
'	''' Gets or sets the report value aggregate type
'	''' </summary>
'	Public Property AggregateValueType() As ReportAggregateValueType1
'		Get
'			Return _aggregateValueType
'		End Get
'		Set(value As ReportAggregateValueType1)
'			_aggregateValueType = value
'		End Set
'	End Property

'	Private _emptyValue As String = ""
'	''' <summary>
'	''' Gets or sets the value which is shown if the computation has no result
'	''' </summary>
'	Public Property EmptyValue() As String
'		Get
'			Return _emptyValue
'		End Get
'		Set(value As String)
'			_emptyValue = value
'		End Set
'	End Property

'	Private _errorValue As String = "!ERROR!"
'	''' <summary>
'	''' Gets or sets the value which is shown if the computation fails
'	''' </summary>
'	Public Property ErrorValue() As String
'		Get
'			Return _errorValue
'		End Get
'		Set(value As String)
'			_errorValue = value
'		End Set
'	End Property

'	''' <summary>
'	''' Computes an aggregate value and formats it
'	''' </summary>
'	''' <param name="values">list of values</param>
'	''' <returns>calculated and formatted value</returns>
'	''' <exception cref="NotSupportedException">The aggregate value type {0} is not supported yet!</exception>
'	Public Function ComputeAndFormat(values As Dictionary(Of String, List(Of Object))) As String
'		If (values Is Nothing) OrElse (values.Count <= 0) Then
'			Return _emptyValue
'		End If
'		If Not values.ContainsKey(_aggregateGroup) Then
'			Return _emptyValue
'		End If

'		Dim result As System.Nullable(Of Decimal) = Nothing
'		Dim isTimeSpan As Boolean = False
'		Dim count As Long = 0
'		For Each value As Object In values(_aggregateGroup)
'			count += 1
'			If _aggregateValueType = ReportAggregateValueType1.Count Then
'				Continue For
'			End If
'			' count needs no real calculation
'			Dim thisValue As Decimal
'			If value Is Nothing Then
'				Return _errorValue
'			End If
'			If TypeOf value Is TimeSpan Then
'				thisValue = Convert.ToDecimal(DirectCast(value, TimeSpan).Ticks)
'				isTimeSpan = True
'			Else
'				If Not [Decimal].TryParse(value.ToString(), thisValue) Then
'					Return _errorValue
'				End If
'			End If
'			Select Case _aggregateValueType
'				Case ReportAggregateValueType1.Average, ReportAggregateValueType1.Sum
'					If result Is Nothing Then
'						result = thisValue
'						Exit Select
'					End If
'					result += thisValue
'					Exit Select
'				Case ReportAggregateValueType1.Maximum
'					If result Is Nothing Then
'						result = thisValue
'						Exit Select
'					End If
'					If thisValue > result Then
'						result = thisValue
'					End If
'					Exit Select
'				Case ReportAggregateValueType1.Minimum
'					If result Is Nothing Then
'						result = thisValue
'						Exit Select
'					End If
'					If thisValue < result Then
'						result = thisValue
'					End If
'					Exit Select
'				Case Else
'					Throw New NotSupportedException([String].Format("The aggregate value type {0} is not supported yet!", _aggregateValueType.ToString()))
'			End Select
'		Next
'		If _aggregateValueType = ReportAggregateValueType1.Count Then
'			result = count
'		End If
'		If result Is Nothing Then
'			Return _emptyValue
'		End If

'		If _aggregateValueType = ReportAggregateValueType1.Average Then
'			result /= count
'		End If
'		' calculate average
'		If isTimeSpan Then
'			Return TimeSpan.FromTicks(Convert.ToInt64(result)).ToString()
'		End If
'		'for timespans
'		Return InlineHasValue1.FormatValue(result, Format)
'	End Function
'End Class

' ''' <summary>
' ''' Enumeration of available aggregate types
' ''' </summary>
'Public Enum ReportAggregateValueType1
'	''' <summary>
'	''' Computes the average value
'	''' </summary>
'	Average
'	''' <summary>
'	''' Gets the values count
'	''' </summary>
'	Count
'	''' <summary>
'	''' Determines the maximum value
'	''' </summary>
'	Maximum
'	''' <summary>
'	''' Determines the minimum value
'	''' </summary>
'	Minimum
'	''' <summary>
'	''' Computes the sum over all values
'	''' </summary>
'	Sum
'End Enum


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
		End If
		If [String].IsNullOrEmpty(format) Then
			Return value.ToString()
		End If

		Dim type As Type = value.[GetType]()
		If type = GetType(DateTime) Then
			Return DirectCast(value, DateTime).ToString(format)
		End If
		If type = GetType(Decimal) Then
			Return CDec(value).ToString(format)
		End If
		If type = GetType(Double) Then
			Return CDbl(value).ToString(format)
		End If
		If type = GetType(Single) Then
			Return CSng(value).ToString(format)
		End If
		If type = GetType(Integer) Then
			Return CInt(value).ToString(format)
		End If
		If type = GetType(Long) Then
			Return CLng(value).ToString(format)
		End If
		If type = GetType(Short) Then
			Return CShort(value).ToString(format)
		End If
		If type = GetType(UInteger) Then
			Return CUInt(value).ToString(format)
		End If
		If type = GetType(ULong) Then
			Return CULng(value).ToString(format)
		End If
		If type = GetType(UShort) Then
			Return CUShort(value).ToString(format)
		End If

		Return value.ToString()
	End Function
End Class



' ''' <summary>
' ''' Class for fillable table row values
' ''' </summary>
'Public Class TableRowForDataTable1
'	Inherits TableRow
'	Implements ITableRowForDataTable1

'	''' <summary>
'	''' Gets or sets the table name
'	''' </summary>
'	Public Property TableName() As String Implements ITableRowForDataTable1.TableName


'	''' <summary>
'	''' Constructor
'	''' </summary>
'	Public Sub New()
'	End Sub
'End Class







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



' ''' <summary>
' ''' Abstract class for fillable run values
' ''' </summary>
'<Obsolete("Removing", True)>
'Public MustInherit Class InlinePropertyValue
'	Inherits Run
'	'Implements IHasValue1
'	'Implements IPropertyValue1

'	''' <summary>
'	''' Gets or sets the value format
'	''' </summary>
'	Public Overridable Property Format As String 'Implements IHasValue1.Format
'		Get
'			Return DirectCast(GetValue(FormatProperty), String)
'		End Get
'		Set(value As String)
'			SetValue(FormatProperty, value)
'		End Set
'	End Property



'	' Using a DependencyProperty as the backing store for Format.  This enables animation, styling, binding, etc...
'	Public Shared ReadOnly FormatProperty As DependencyProperty =
'		DependencyProperty.Register("Format", GetType(String), GetType(InlineHasValue1), New UIPropertyMetadata(Nothing))

'	''' <summary>
'	''' Gets or sets the object value
'	''' </summary>
'	Public Overridable Property Value() As Object ' Implements IHasValue1.Value
'		Get
'			Return DirectCast(GetValue(ValueProperty), Object)
'		End Get
'		Set(value As Object)
'			SetValue(ValueProperty, value)
'			Text = FormatValue(value, Format)
'		End Set
'	End Property

'	' Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
'	Public Shared ReadOnly ValueProperty As DependencyProperty =
'		DependencyProperty.Register("Value", GetType(Object), GetType(InlineHasValue1), New UIPropertyMetadata(Nothing))

'	''' <summary>
'	''' Identifies the ValueChanged routed event.
'	''' </summary>
'	Public Shared ReadOnly ValueChangedEvent As RoutedEvent =
'		EventManager.RegisterRoutedEvent("ValueChanged", RoutingStrategy.Bubble, GetType(RoutedPropertyChangedEventHandler(Of Decimal)), GetType(InlineHasValue1))

'	''' <summary>
'	''' Raises the ValueChanged event.
'	''' </summary>
'	''' <param name="args">Arguments associated with the ValueChanged event.</param>
'	Protected Overridable Sub OnValueChanged(args As RoutedPropertyChangedEventArgs(Of Decimal))
'		[RaiseEvent](args)
'	End Sub

'	''' <summary>
'	''' Formats a value for output
'	''' </summary>
'	''' <param name="value">value</param>
'	''' <param name="format">format</param>
'	''' <returns></returns>
'	Public Shared Function FormatValue(value As Object, format As String) As String
'		If value Is Nothing Then
'			Return ""
'		ElseIf [String].IsNullOrEmpty(format) Then
'			Return value.ToString()
'		End If

'		Select Case value.[GetType]()
'			Case GetType(DateTime)
'				Return DirectCast(value, DateTime).ToString(format)
'			Case GetType(Decimal)
'				Return CDec(value).ToString(format)
'			Case GetType(Double)
'				Return CDbl(value).ToString(format)
'			Case GetType(Single)
'				Return CSng(value).ToString(format)
'			Case GetType(Integer)
'				Return CInt(value).ToString(format)
'			Case GetType(Long)
'				Return CLng(value).ToString(format)
'			Case GetType(Short)
'				Return CShort(value).ToString(format)
'			Case GetType(UInteger)
'				Return CUInt(value).ToString(format)
'			Case GetType(ULong)
'				Return CULng(value).ToString(format)
'			Case GetType(UShort)
'				Return CUShort(value).ToString(format)
'		End Select

'		Return value.ToString()
'	End Function





'	''' <summary>
'	''' Gets or sets the property name
'	''' </summary>
'	Public Overridable Property PropertyName As String ' Implements IPropertyValue1.PropertyName
'		Get
'			Return DirectCast(GetValue(PropertyNameProperty), String)
'		End Get
'		Set(value As String)
'			SetValue(PropertyNameProperty, value)
'		End Set
'	End Property

'	' Using a DependencyProperty as the backing store for PropertyName.  This enables animation, styling, binding, etc...
'	Public Shared ReadOnly PropertyNameProperty As DependencyProperty =
'		DependencyProperty.Register("PropertyName", GetType(String), GetType(InlinePropertyValue), New UIPropertyMetadata(Nothing))

'End Class





''' <summary>
''' Contains a single report value that is to be displayed on the report (e.g. report title)
''' </summary>
<DebuggerStepThrough>
Public Class InlineDocumentValue
	Inherits Run 'InlinePropertyValue
	'Implements IAggregateValue1
	'Implements IInlineDocumentValue1
	'Implements IInlinePropertyValue1

	' ''' <summary>
	' ''' Gets or sets the aggregate group
	' ''' </summary>
	'Public Property AggregateGroup() As String 'Implements IAggregateValue1.AggregateGroup



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
	Public Overridable Property Value As Object	' Implements IHasValue1.Value
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





	''' <summary>
	''' Gets or sets the property name
	''' </summary>
	Public Overridable Property PropertyName As String ' Implements IPropertyValue1.PropertyName
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






'End Module
