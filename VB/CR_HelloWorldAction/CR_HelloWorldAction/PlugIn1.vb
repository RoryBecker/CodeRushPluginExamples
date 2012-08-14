Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core

Public Class PlugIn1

    ' This is a code-only PlugIn.
    ' It does not rely on anything being dropped on the plugin design surface.

    ' This plugin creates single action, which is then given a name and registered with the DXCore.
    ' When executed, this action will display a messagebox with tht phrase "Hello World".

    ' Usage: 
    '  - Configure a shortcut to execute the 'SayHelloWorld' action using 'Options - IDE\Shortcuts'
    '  - Invoke the shortcut as defined.

    Public Overrides Sub InitializePlugIn()
        MyBase.InitializePlugIn()
        Dim SayHelloWorld As New DevExpress.CodeRush.Core.Action(components)
        CType(SayHelloWorld, System.ComponentModel.ISupportInitialize).BeginInit()

        SayHelloWorld.ActionName = "SayHelloWorld"
        SayHelloWorld.RegisterInCR = True
        AddHandler SayHelloWorld.Execute, AddressOf SayHelloWorld_Execute

        CType(SayHelloWorld, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub

    Private Sub SayHelloWorld_Execute(ByVal ea As ExecuteEventArgs)
        MessageBox.Show("Hello World")
    End Sub

End Class
