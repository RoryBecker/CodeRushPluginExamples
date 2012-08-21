Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.DXCore.Adornments
Imports DevExpress.CodeRush.StructuralParser

' This class is instanciated by the DXCore per TextDocument.
' It's NewAdornment function is called whenever the DXCore needs to create an adornement
Class BlueBoxAdornmentFactory
    Inherits TextDocumentAdornment
    Public Sub New(ByVal range As SourceRange)
        MyBase.New(range)
    End Sub
    Protected Overrides Function NewAdornment(ByVal feature As String, ByVal frame As IElementFrame) As TextViewAdornment
        Return New BlueBoxAdornment(feature, frame)
    End Function
End Class

