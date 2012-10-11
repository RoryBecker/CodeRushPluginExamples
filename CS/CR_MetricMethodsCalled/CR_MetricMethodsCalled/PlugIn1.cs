using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace CR_MetricMethodsCalled
{
    public partial class PlugIn1 : StandardPlugIn
    {
        // DXCore-generated code...
        #region InitializePlugIn
        public override void InitializePlugIn()
        {
            base.InitializePlugIn();
            RegisterMetricMethodsCalled();
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
        private void RegisterMetricMethodsCalled()
        {
            var MetricMethodsCalled = new DevExpress.CodeRush.Extensions.CodeMetricProvider(components);
            ((System.ComponentModel.ISupportInitialize)(MetricMethodsCalled)).BeginInit();
            MetricMethodsCalled.ProviderName = "MetricMethodsCalled"; // Should be Unique
            MetricMethodsCalled.DisplayName = "Methods Called";
            MetricMethodsCalled.GetMetricValue += MetricMethodsCalled_GetMetricValue;
            ((System.ComponentModel.ISupportInitialize)(MetricMethodsCalled)).EndInit();
        }
        private void MetricMethodsCalled_GetMetricValue(Object sender, DevExpress.CodeRush.Extensions.GetMetricValueEventArgs e)
        {
            e.Value = GetMethodCallCount(e.LanguageElement);
        }
        private int GetMethodCallCount(LanguageElement LanguageElement)
        {
            var Types = new Type[] {
                typeof(MethodCall),
                typeof(MethodCallExpression),
                typeof(ElementReferenceExpression)
            };
            // Create Enumerator of certain types of LanguageElement.
            var Enumerator = new ElementEnumerable(LanguageElement, Types, true);
            // Count elements which qualify as calls to methods.
            return (from LanguageElement Item in Enumerator
                    where IsMethodCall(Item)
                    select Item).Count();
        }
        private bool IsMethodCall(LanguageElement Item)
        {
            if (Item is MethodCall)
            {
                // Calls which discard any returned value.
                return true;
            }
            if (Item is MethodCallExpression)
            {
                // Method calls which are themselves passed to other methods.
                return true;
            }
            // C# requires parenthesis for it's method calls. This makes identifying method calls very easy.
            // Other languages (VB.Net for example) do not share this requirement. 
            // References to Methods and Variables end up looking exactly the same to the parser.
            // ElementReferenceExpressions may potentially refer to Methods.
            if (!(Item is ElementReferenceExpression))
                return false;
            
            // This forces us to locate the declaration of the item the reference points at. 
            // Once there we can confirm if the Item in question is a Method.
            if (!(Item.GetDeclaration() is IMethodElement))
                return false;

            // Finally we need to confirm that the method reference is in fact a call.
            // We do this by eliminating the other purpose of a method reference: That of a Method pointer.
            // No parent AddressOf operator. Therefore not a method pointer.
            if (!(Item.Parent is AddressOfExpression))
                return true;
            return false;
        }
    }
}