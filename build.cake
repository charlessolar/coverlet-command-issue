#addin nuget:?package=Cake.Coverlet

Task("Restore-NuGet-Packages")
    .Does(() =>
{
    DotNetCoreRestore("./XUnit.Coverage.sln", new DotNetCoreRestoreSettings());

});


Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{   

    DotNetCoreBuild("./XUnit.Coverage.sln",
    new DotNetCoreBuildSettings {
        Configuration = "Release",
        ArgumentCustomization = aggs => aggs
                .Append("/p:SourceLinkEnabled=true")
                .Append("/p:SourceLinkCreate=true")
    });


});

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
{
    var coverletSettings = new CoverletSettings {
        CollectCoverage = true,
        CoverletOutputFormat = CoverletOutputFormat.opencover,
        CoverletOutputDirectory = Directory(@".\coverage-results\"),
        CoverletOutputName = $"results-{DateTime.UtcNow:dd-MM-yyyy-HH-mm-ss-FFF}"
    };

    // I want to specify the specific dll file and the project exactly.
    Coverlet(
        "./XUnit.Coverlet.Collector/bin/Release/net5.0/XUnit.Coverlet.Collector.dll", 
        "./XUnit.Coverlet.Collector/XUnit.Coverlet.Collector.csproj", 
        coverletSettings);

});

Task("Default")
    .IsDependentOn("Test");

RunTarget("Default");