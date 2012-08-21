using System;
using System.Collections.Generic;
using DevExpress.DXCore.Adornments;
using DevExpress.DXCore.Platform.Drawing;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.StructuralParser;

namespace CR_CodeDecorationBlueBox
{
    class BlueBoxAdornment : VisualObjectAdornment
    {
        public BlueBoxAdornment(string feature, IElementFrame frame)
            : base(feature, frame)
        {
        }
        public override void Render(IDrawingSurface context, ElementFrameGeometry geometry)
        {                
            // TODO: Add adornment painting logic
            context.DrawRectangle(null, Colors.Blue, geometry.Bounds);
        }
    }
}
