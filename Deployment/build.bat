Task("Build")
    .IsDependentOn("Clean")
    .IsDependentOn("PackageRestore")
    .Does(() =>
{
    MSBuild(solution, new MSBuildSettings()
        .SetConfiguration(configuration)
        .WithProperty("Platform", platform)
        .UseToolVersion(MSBuildToolVersion.VS2015)
        .SetVerbosity(Verbosity.Minimal)
        .SetNodeReuse(false)
        .WithProperty("Windows", "True")
        .WithProperty("TreatWarningsAsErrors", "False")
        .WithProperty("OutputPath", "./bin/Build/")
        .WithTarget("Build"));     
});