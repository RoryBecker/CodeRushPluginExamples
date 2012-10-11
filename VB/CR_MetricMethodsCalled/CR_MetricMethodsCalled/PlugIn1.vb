Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports DevExpress.CodeRush
Imports System.Linq
Imports System.Xml
Imports DevExpress.CodeRush.Extensions


Public Class PlugIn1

#Region "Generated + Registration"
    Public Overrides Sub InitializePlugIn()
        MyBase.InitializePlugIn()
        RegisterMetricMethodsCalled()
    End Sub
    Public Overrides Sub FinalizePlugIn()
        MyBase.FinalizePlugIn()
    End Sub
#End Region

    Public Sub RegisterMetricMethodsCalled()
        Try
            Dim MetricMethodsCalled As New CodeMetricProvider(components)
            CType(MetricMethodsCalled, System.ComponentModel.ISupportInitialize).BeginInit()
            MetricMethodsCalled.ProviderName = "MetricMethodsCalled"
            MetricMethodsCalled.DisplayName = "Methods Called"
            MetricMethodsCalled.Description = "Counts the number of method calls which are made from within each member."
            AddHandler MetricMethodsCalled.GetMetricValue, AddressOf MetricMethodsCalled_GetMetricValue
            CType(MetricMethodsCalled, System.ComponentModel.ISupportInitialize).EndInit()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub
    Private Sub MetricMethodsCalled_GetMetricValue(ByVal sender As Object, ByVal e As Extensions.GetMetricValueEventArgs)
        e.Value = GetMethodCallCount(e.LanguageElement)
    End Sub

    Private Function GetMethodCallCount(LanguageElement As LanguageElement) As Integer
        Dim Types = New Type() {GetType(MethodCall), _
                                GetType(MethodCallExpression), _
                                GetType(ElementReferenceExpression)}
        ' Create Enumerator of certain types of LanguageElement.
        Dim Enumerator As New ElementEnumerable(LanguageElement, Types, True)
        ' Count elements which qualify as calls to methods.
        Return (From Item As LanguageElement In Enumerator Where IsMethodCall(Item)).Count
    End Function

    Private Function IsMethodCall(ByVal Item As LanguageElement) As Boolean
        If TypeOf Item Is MethodCall Then
            ' Calls which discard any returned value.
            Return True
        End If
        If TypeOf Item Is MethodCallExpression Then
            ' Method calls which are themselves passed to other methods.
            Return True
        End If

        ' C# requires parenthesis for it's method calls. This makes identifying method calls very easy.
        ' Other languages (VB.Net for example) do not share this requirement. 
        ' References to Methods and Variables end up looking exactly the same to the parser.

        ' ElementReferenceExpressions may potentially refer to Methods.
        If Not (TypeOf Item Is ElementReferenceExpression) Then
            Return False
        End If

        ' This forces us to locate the declaration of the item the reference points at. 
        ' Once there we can confirm if the Item in question is a Method.
        If Not (TypeOf Item.GetDeclaration Is IMethodElement) Then
            Return False
        End If

        ' Finally we need to confirm that the method reference is in fact a call.
        ' We do this by eliminating the other purpose of a method reference: That of a Method pointer.

        ' No parent AddressOf operator. Therefore not a method pointer.
        If Not TypeOf Item.Parent Is AddressOfExpression Then
            Return True
        End If
        Return False
    End Function

End Class