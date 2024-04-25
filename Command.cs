using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitParameterScanner.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace RevitParameterScanner
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]

    //This class will be responsible for executing the logic when the command is triggered from the Ribbon.
    public class Command : IExternalCommand
    {
        public Result Execute(
            ExternalCommandData commandData,
            ref string message,
            ElementSet elements)
        {
            //Getting current user interface and Revit document
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;

            // Iniciar a interface do usuário
            MainWindow mainWindow = new MainWindow(uidoc);
            mainWindow.ShowDialog();

            return Result.Succeeded;
        }
    }
}
