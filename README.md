Steps to run:
1. Open the Visual Studio Solution (.sln) in Visual Studio 2013 or higher.
2. In Visual Studio, go to Tools -> Extensions and Updates
3. In Extensions and Updates dialog, select Online. Search for and install SpecFlow.
4. Also, search for and install NUnit 3 Test Adapter.
5. Click on Build -> uild Solution.
6. Click on Test -> Windows -> Test Explorer.
7. The tests should be displayed in the  Test Explorer window.
8. Select and run any/all tests.

Alternatively, instead of Step 8 above.
9. If using nunit-console utility, open command prompt and type the following command:
		nunit-console <path of the solution>\bin\Debug\ComputerDatabase.UI.dll
		
NOTE: The ChromeDriver in Drivers folder works with Chrome version 79. Please replace with appropriate version of ChromeDriver according to local installation of Chrome browser.