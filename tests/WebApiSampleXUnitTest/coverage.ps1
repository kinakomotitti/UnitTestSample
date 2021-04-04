//
// This script will execute xUnit Test with code coverage.
// After the test, this program generate Test report into TestResult\html dir.
//

$id = [Guid]::NewGuid().ToString()
if (Test-Path .\TestResults\coverage.cobertura.xml ){
    Rename-Item .\TestResults\coverage.cobertura.xml -NewName coverage.cobertura.$id.xml
}
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:Exclude="[xunit*]\*" /p:CoverletOutput="./TestResults/"
reportgenerator "-reports:TestResults\coverage.cobertura.xml" "-targetdir:TestResults\html" -reporttypes:HTML