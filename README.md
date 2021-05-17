# coverlet-command-issue

reproduce:

- `dotnet build --configuration Release`
- `cd .\XUnit.Coverlet.Collector\`
- `coverlet .\bin\Release\net5.0\XUnit.Coverlet.Collector.dll --target dotnet --targetargs "test --no-build"`

error

`The test source file "C:\Projects\Numbers\XUnit.Coverlet.Collector\bin\Debug\net5.0\XUnit.Coverlet.Collector.dll" provided was not found.`
