SAMPLE CODE: READ ME

This sample uses MSBuild to build all the projects.

1. To build the projects, run the runner.bat command file.
2. The runner.bat calls MSBuild command-line, passing in the runner.msbuild filename.
3. MSBuild calls the default target, which is Build.
4. The Build target then calls MSBuild to build each of the csproj files defined under the ItemGroup.
