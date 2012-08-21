using System;
using System.Collections.Generic;

using DevExpress.DXCore.Adornments;
using DevExpress.DXCore.Platform.Drawing;

using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.StructuralParser;

namespace CR_CodeDecorationBlueBox
{
    class BlueBoxAdornmentFactory : TextDocumentAdornment
    {
        public BlueBoxAdornmentFactory(SourceRange range)
            : base(range)
        {
        }

        protected override TextViewAdornment NewAdornment(string feature, IElementFrame frame)
        {
            return new BlueBoxAdornment(feature, frame);
        }
    }
}