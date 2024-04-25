
# RevitParameterScanner Add-In for Revit 2023

## Overview

This Add-In was developed for Autodesk Revit 2023 using Microsoft Visual Studio 2022 and targets the .NET Framework 4.8. This tool allows users to isolate or select elements in a model based on parameter name and value.

## Development Environment Setup

### Prerequisites

- Microsoft Visual Studio 2022
- .NET Framework 4.8
- Autodesk Revit 2023
- Autodesk Revit 2023 .NET SDK 

### Initial Setup

To start working with the Add-In, make sure you have `.NET Framework 4.8` and `Revit 2023 .NET SDK` installed. They can be downloaded here:
- SDK: https://damassets.autodesk.net/content/dam/autodesk/files/REVIT_2023_1_SDK.msi
- .NET: https://dotnet.microsoft.com/pt-br/download/dotnet-framework/net48

After that, you need to clone the repository from GitHub.

### Setting up the project in Visual Studio 2022

After cloning the repository, you need to set up the project in Visual Studio 2022:

1. Open Visual Studio 2022.
2. Select 'Open a project or solution'.
3. Navigate to the directory where you cloned the repository and open the solution file.

Make sure the solution has required References to communicate with Revit API. To do that, in the `Solution Explorer` window on the right-hand side of the `Visual Studio` window, click `References` and look for `RevitAPI` and `RevitAPIUI` on the list. If they are there, you are good to go. If they are not, you can right-click `References` and click `Add Reference`. The two dll files can be found in Revit installation sub-folder, usually in the path:

```
C:\Program Files\Autodesk\Revit 2023
```

Note: if you needed to add the two references, set the referenced file property `Copy Local` to False.

Now your project is properly configured.

### Project structure

- AddInApp.cs: It is responsible for initializing and terminating the add-in, as well as configure addin ribbon properties.
- Command.cs: It is responsible for executing the logic when the command is triggered from the ribbon.
- UI folder: It contains files for the WPF window the user sees in Revit.
- Resources folder: It contains the icons used by the Add-in and its manifest (.addin)

## Using the Add-In in Revit

### Configuring the Add-In

The DLL and addin files are available in the `RevitParameterScanner.zip` file that can be downloaded here: https://drive.google.com/file/d/1B1MXl9ofpkX2MLiR112gJRqB5_1dzw5t/view?usp=sharing

To properly set up in Autodesk Revit 2023 you need to:

1. Unzip the `.zip` file containing `RevitParameterScanner.dll` and `RevitParameterScanner.addin`.
2. Copy the `RevitParameterScanner.addin` file to the Revit add-ins folder, typically located at:
   
   ```
   C:\ProgramData\Autodesk\Revit\Addins\2023\
   ```
   
   Note: The `ProgramData` folder is hidden by default in Windows 10.
   
3. Edit the `RevitParameterScanner.addin` file you just moved to set the correct location of `RevitParameterScanner.dll`. To do that, right-click `RevitParameterScanner.dll` and click `Copy as path`. Open `RevitParameterScanner.addin` using Notepad and update the `<Assembly>` tag to point to the correct path. Save the file and you are good to go.

### Usage

When starting Revit 2023, agree with any message shown regarding the new add-in, and you may see a Ribbon tab named `Parameters`. Click on it and will show the button for the `Parameter Scanner`. Click on it again and a window will pop up. You may enter the parameter name and value, and then click the button for isolating or selecting the elements.

Note: the add-in has some restrictions:
- It only works within Floor Plans, Reflected Ceiling Plans and 3D Views.
- Parameter name input cannot be left empty.
- The parameter name should be written exactly as you see in Revit. It is case sensitive. For example: There is a parameter called "Base Offset", if you write "base offset" the add-in won't find the parameter.

## Contributors

- Antonio Borssato - Initial developer of the RevitParameterScanner Add-In.
