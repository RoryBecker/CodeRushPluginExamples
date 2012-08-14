using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;

namespace CR_HelloWorldAction
{
    public partial class PlugIn1 : StandardPlugIn
    { 
        // This is a code-only PlugIn.
        // It does not rely on anything being dropped on the plugin design surface.

        // This plugin creates single action, which is then given a name and registered with the DXCore.
        // When executed, this action will display a messagebox with tht phrase "Hello World".

        // Usage: 
        //  - Configure a shortcut to execute the 'SayHelloWorld' action using 'Options - IDE\Shortcuts' 
        //  - Invoke the shortcut as defined.
        
        public override void InitializePlugIn()
        {
            base.InitializePlugIn();
            DevExpress.CodeRush.Core.Action SayHelloWorld = new DevExpress.CodeRush.Core.Action(components);
            ((System.ComponentModel.ISupportInitialize)(SayHelloWorld)).BeginInit();

            SayHelloWorld.ActionName = "SayHelloWorld";
            SayHelloWorld.RegisterInCR = true;
            SayHelloWorld.Execute += SayHelloWorld_Execute;
            
            ((System.ComponentModel.ISupportInitialize)(SayHelloWorld)).EndInit();
        }
        private void SayHelloWorld_Execute(ExecuteEventArgs ea)
        {
            MessageBox.Show("Hello World");
        }
    }
}