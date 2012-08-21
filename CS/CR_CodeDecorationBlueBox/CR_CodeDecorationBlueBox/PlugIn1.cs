using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace CR_CodeDecorationBlueBox
{
    public partial class PlugIn1 : StandardPlugIn
    {
        // DXCore-generated code...
        #region InitializePlugIn
        public override void InitializePlugIn()
        {
            base.InitializePlugIn();

            //
            // TODO: Add your initialization code here.
            //
        }
        #endregion
        #region FinalizePlugIn
        public override void FinalizePlugIn()
        {
            //
            // TODO: Add your finalization code here.
            //

            base.FinalizePlugIn();
        }
        #endregion

        private void PlugIn1_DecorateLanguageElement(object sender, DecorateLanguageElementEventArgs args)
        {

        // You must decide if it//s appropriate to decorate the LanguageElement in question.
            if (args.LanguageElement ==  null)
                return;
        // You must select a suitable TextDocumentAdornment derived class and pass it to the AddAdornment method.
            if (args.LanguageElement.ElementType != LanguageElementType.Method) 
                return;
            var Method = (Method) args.LanguageElement;
            SourceRange range = GetMethodBoxRange(args, Method);
            args.AddAdornment(new BlueBoxAdornmentFactory(range));
        }
        private SourceRange GetMethodBoxRange(DecorateLanguageElementEventArgs args, Method Method)
        {        	
            // Get all the lines of the method
            var Lines = args.TextDocument.GetLines(Method.StartLine, Method.EndLine - Method.StartLine);
            // determine the longest of these
            string Longest = (from line in Lines orderby line.Length descending select line).First();
            // Build and return a SourceRange around the method
            SourcePoint EndPoint = new SourcePoint(Method.EndLine, Longest.Length);
            SourcePoint StartPoint = new SourcePoint(Method.StartLine, Method.StartOffset);
            return new SourceRange(StartPoint, EndPoint);
        }
    }
}