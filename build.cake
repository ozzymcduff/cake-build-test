var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

// Define directories.
var buildDir = Directory("./test_cake/bin") + Directory(configuration);

Task("Clean")
    .Does(() =>
{
    CleanDirectory(buildDir);
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    NuGetRestore("./test_cake.sln");
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    MSBuild("./test_cake.sln", new MSBuildSettings()
        .UseToolVersion(MSBuildToolVersion.NET45)
        .SetVerbosity(Verbosity.Minimal)
        .SetConfiguration(configuration));
});

Task("Default")
    .IsDependentOn("Build");

RunTarget(target);
