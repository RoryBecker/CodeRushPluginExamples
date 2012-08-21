Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Linq

Public Class PlugIn1
    'This method is called whenever the DXCore sees the opportunity to decorate a LanguageElement
    Private Sub PlugIn1_DecorateLanguageElement(sender As Object, args As DecorateLanguageElementEventArgs) Handles Me.DecorateLanguageElement
        ' You must decide if it's appropriate to decorate the LanguageElement in question.
        If args.LanguageElement Is Nothing Then
            Exit Sub
        End If
        ' You must select a suitable TextDocumentAdornment derived class and pass it to the AddAdornment method.
        If args.LanguageElement.ElementType <> LanguageElementType.Method Then
            Exit Sub
        End If
        Dim Method = TryCast(args.LanguageElement, Method)
        Dim range As SourceRange = GetMethodBoxRange(args, Method)
        args.AddAdornment(New BlueBoxAdornmentFactory(range))
    End Sub
    Private Shared Function GetMethodBoxRange(ByVal args As DecorateLanguageElementEventArgs, ByVal Method As Method) As SourceRange
        ' Get all the lines of the method
        Dim Lines As String() = args.TextDocument.GetLines(Method.StartLine, Method.EndLine - Method.StartLine)
        ' determine the longest of these
        Dim Longest = (From line In Lines Order By line.Length Descending).First
        ' Build and return a SourceRange around the method
        Dim EndPoint As SourcePoint = New SourcePoint(Method.EndLine, Longest.Length)
        Dim StartPoint As SourcePoint = New SourcePoint(Method.StartLine, Method.StartOffset)
        Return New SourceRange(StartPoint, EndPoint)
    End Function
End Class
