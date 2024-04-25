using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace RevitParameterScanner
{
    //This class will be responsible for initializing and terminating the add-in, as well as configure addin ribbon properties
    public class AddInApp : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            //I'm gonna create the ribbon tab for the scanner
            application.CreateRibbonTab("Parameters");

            //Here I'm gonna create the ribbon panel
            RibbonPanel ribbonPanel = application.CreateRibbonPanel("Parameters", "Tool");

            //Getting the assembly location path
            string assemblyPathLoc = Assembly.GetExecutingAssembly().Location;

            //Creating the button for the parameter scanner
            PushButtonData buttonData = new PushButtonData(
                "ParameterScanner",
                "Parameter Scanner",
                assemblyPathLoc,
                "RevitParameterScanner.Command"); //calling the external command class

            PushButton pushButton = ribbonPanel.AddItem(buttonData) as PushButton;
            pushButton.ToolTip = "Scan model elements for specific parameter name and values.";

            //setting button icon
            Uri uriImage = new Uri("pack://application:,,,/RevitParameterScanner;component/Resources/iconSmall.png");
            BitmapImage bitmapImage = new BitmapImage(uriImage);
            pushButton.LargeImage = bitmapImage;

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
