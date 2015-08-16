Imports System.Windows.Documents

''' <summary>
''' Dynamic cache class for report paginator
''' </summary>
Public Class DocSuperWalker
	'Inherits DocumentWalker1

	Private _documentByType As New Dictionary(Of Type, ArrayList)()
	Private _documentByInterface As New Dictionary(Of Type, ArrayList)()
	Private _flowDocument As FlowDocument ' = Nothing

	''' <summary>
	''' Gets the associacted flow document
	''' </summary>
	Public ReadOnly Property FlowDocument As FlowDocument
		Get
			Return _flowDocument
		End Get
	End Property

	''' <summary>
	''' Constructor
	''' </summary>
	''' <param name="flowDocument">flow document</param>
	Public Sub New(flowDocument As FlowDocument)
		_flowDocument = flowDocument
		'BuildCache()
		'Me.Walk(_flowDocument)
		DocWalker.Walk(_flowDocument, AddressOf walker_VisualVisited)
	End Sub

	' ''' <summary>
	' ''' Build cache
	' ''' </summary>
	'Private Sub BuildCache()
	'	Dim walker As New DocumentWalker1
	'	'walker.VisualVisited += New DocumentVisitedEventHandler(AddressOf walker_VisualVisited)
	'	AddHandler walker.VisualVisited, New DocumentVisitedEventHandler(AddressOf walker_VisualVisited)
	'	walker.Walk(_flowDocument)
	'End Sub

	Private Sub walker_VisualVisited(visitedObject As Object, start As Boolean) ' Handles Me.VisualVisited
		If visitedObject Is Nothing Then Exit Sub

		Dim type As Type = visitedObject.[GetType]()
		If Not _documentByType.ContainsKey(type) Then
			_documentByType(type) = New ArrayList()
		End If
		_documentByType(type).Add(visitedObject)

		For Each interfaceType As Type In type.GetInterfaces()
			If Not _documentByInterface.ContainsKey(interfaceType) Then
				_documentByInterface(interfaceType) = New ArrayList()
			End If
			_documentByInterface(interfaceType).Add(visitedObject)
		Next
	End Sub

	''' <summary>
	''' Gets an ArrayList of all document visual object of a specific type
	''' </summary>
	''' <param name="type">type of document visual object</param>
	''' <returns>empty ArrayList, if type does not exist</returns>
	Public Function GetFlowDocumentVisualListByType(type As Type) As ArrayList
		If type Is Nothing Then
			Return New ArrayList()
		ElseIf Not _documentByType.ContainsKey(type) Then
			Return New ArrayList()
		Else
			Return _documentByType(type)
		End If
	End Function


End Class