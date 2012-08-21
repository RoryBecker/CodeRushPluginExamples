Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.DXCore.Adornments
Imports DevExpress.DXCore.Platform.Drawing

Public Class BlueBoxAdornment
    Inherits VisualObjectAdornment

    Public Sub New(ByVal feature As String, ByVal frame As IElementFrame)
        MyBase.New(feature, frame)
    End Sub

    Public Overrides Sub Render(ByVal context As IDrawingSurface, ByVal geometry As ElementFrameGeometry)
        context.DrawRectangle(Nothing, Colors.Blue, geometry.Bounds)
    End Sub
End Class