## Prerequisites:
- Install the nuget.exe CLI by downloading it from [nuget.org](https://dist.nuget.org/win-x86-commandline/latest/nuget.exe) , 
 saving that .exe file to a suitable folder, and adding that folder to your PATH environment variable.

## Build a Nuget Package:
 - Edit culqi.net.nuspec file with proper values
 - From a command prompt in the folder containing your .nuspec file, run the command nuget pack.
 - NuGet generates a .nupkg file in the form of identifier-version.nupkg, which you'll find in the current folder.

## More info:
 - https://docs.microsoft.com/en-us/nuget/quickstart/create-and-publish-a-package-using-visual-studio-net-framework