WinCreator - Windows Service scaffolding tool (work in progress)

Usage (after building src/WinSvcScaffold):
  WinSvcScaffold.exe --names ServiceA,ServiceB --out C:\projects

Templates live in templates\service-template. Edit them to match your standard project layout and packages.

See scripts\ci-test.ps1 for a sample acceptance test that runs the scaffold and verifies output.
