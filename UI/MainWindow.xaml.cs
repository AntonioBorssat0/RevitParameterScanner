using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Autodesk.Revit.DB.SpecTypeId;

namespace RevitParameterScanner.UI
{
    //This will manage what happens when user interact with WPF window
    public partial class MainWindow : Window
    {
        private UIDocument _uidoc; //to get uidoc from command.cs

        public MainWindow(UIDocument uidoc)
        {
            InitializeComponent();
            _uidoc = uidoc;
        }

        //Getting all elements of the model
        private ICollection<Element> GetAllElements(Document doc)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            return collector.WhereElementIsNotElementType().ToElements();
        }

        //Getting filtered elements of the model by paramName and paramValue
        private (List<ElementId> filteredElements, bool paramExists) GetFilteredElements(ICollection<Element> allElements, string paramName, string paramValue)
        {
            Document doc = _uidoc.Document;

            bool paramExists = false;

            List<ElementId> filteredElements = new List<ElementId>();

            foreach (Element element in allElements)
            {
                Parameter parameter = element.LookupParameter(paramName);
                if (parameter != null) //if we find the parameter within the element
                {
                    paramExists = true;

                    //Comparing paramValue with element parameter value
                    if (paramValue.Equals(parameter.AsValueString())) //if element parameter value is equal to paramValue add to the list
                    {
                        filteredElements.Add(element.Id);
                    }
                }
            }
            return (filteredElements, paramExists);
        }

        //Using this to show element count message
        private void ElementCountMessage(List<ElementId> filteredElements)
        {
            int elementCount = filteredElements.Count;
            MessageBox.Show(elementCount + " element(s) found for the specified parameter value.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        //Warning message if paramName is null
        private bool ValidateParameterName()
        {
            if (string.IsNullOrWhiteSpace(txtParameterName.Text))
            {
                MessageBox.Show("Parameter name input cannot be left empty.", "Input Required", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        //Warning message if parameter is not found within elements
        private bool ParamExistenseWarning ()
        {
            MessageBox.Show("Parameter not found within the elements.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }

        //This one is gonna check if the view is valid
        private bool IsValidView(View view)
        {
            if (view.ViewType != ViewType.FloorPlan && view.ViewType != ViewType.CeilingPlan && view.ViewType != ViewType.ThreeD)
            {
                MessageBox.Show("This functionality only works within Floor Plans, Reflected Ceiling Plans and 3D Views.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        private void btnIsolate_Click(object sender, RoutedEventArgs e)
        {
            //Getting doc info
            View view = _uidoc.ActiveView;
            Document doc = _uidoc.Document;

            //Checking if it is a valid view
            if (!IsValidView(view))
            {
                return;
            }

            //Checks if parameter name is not null in WPF
            if (!ValidateParameterName())
            {
                return;
            }

            //Getting parameter name and value from WPF
            string paramName = txtParameterName.Text;
            string paramValue = txtParameterValue.Text;

            //Getting all elements
            ICollection<Element> allElements = GetAllElements(doc);

            //Checking if parameter exists within elements
            var result = GetFilteredElements(allElements, paramName, paramValue);

            if (!result.paramExists)
            {
                ParamExistenseWarning();
                return;
            }
            else
            {
                //Filtering elements
                List<ElementId> filteredElements = result.filteredElements;
            
                // Isolating elements on the view
                using (Transaction trans = new Transaction(_uidoc.Document, "Isolate Elements"))
                {
                    trans.Start();
                    _uidoc.ActiveView.IsolateElementsTemporary(filteredElements);
                    trans.Commit();
                }

                //Showing the total of elements filtered
                ElementCountMessage(filteredElements);
            }

        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            //Getting doc info
            View view = _uidoc.ActiveView;
            Document doc = _uidoc.Document;

            //Checking if it is a valid view
            if (!IsValidView(view))
            {
                return;
            }

            //Checks if parameter name is not null in WPF
            if (!ValidateParameterName())
            {
                return;
            }

            //Getting parameter name and value from WPF
            string paramName = txtParameterName.Text;
            string paramValue = txtParameterValue.Text;

            //Getting all elements
            ICollection<Element> allElements = GetAllElements(doc);

            //Checking if parameter exists within elements
            var result = GetFilteredElements(allElements, paramName, paramValue);

            if (!result.paramExists)
            {
                ParamExistenseWarning();
                return;
            }
            else
            {
                //Filtering elements
                List<ElementId> filteredElements = result.filteredElements;
                //Selecting elements
                _uidoc.Selection.SetElementIds(filteredElements);

                //Printing the total of elements filtered
                ElementCountMessage(filteredElements);
            }

        }
    }
}
