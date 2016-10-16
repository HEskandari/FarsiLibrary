#r "./lib/FAKE/FakeLib.dll"
open Fake 
open AssemblyInfoFile

let OutputDir          = "./binaries/"
let PublishDir         = "./publish/"
let Solution           = "FarsiLibrary.sln"
let ToolsFolder        = "./lib/"
let PackagesFolder     = "./packages"
let TestAssembly       = "FarsiLibrary.UnitTest.dll"
let TestRunnerPath     = "./packages/NUnit.Runners.2.6.4/tools"
let SolutionItems      = "./Solution Items/"

let GeneratePackage packageName = 
    let assemblyFile = OutputDir + packageName + ".dll"
    let nugetSpec = SolutionItems + packageName + ".nuspec"
    let version = GetAssemblyVersionString assemblyFile
    tracefn "Generating nuget package for %s version %s ..." packageName version
    
    NuGet (fun p -> 
        {p with
            Version = version
            OutputPath = PublishDir
            WorkingDir = OutputDir
            Publish = false }) 
            nugetSpec

Target "EnsureDir" (fun _ -> 
    trace "Ensuring directories exists..."
    ensureDirectory OutputDir
    ensureDirectory PublishDir
)

Target "Clean" (fun _ ->
    trace "Cleaning the output folder..."
    CleanDirs [ OutputDir; PublishDir ]
)

Target "RestorePackages" (fun _ -> 
     Solution 
       |> RestoreMSSolutionPackages  (fun p ->
         { p with
             ToolPath = findNuget ToolsFolder;
             OutputPath = PackagesFolder })
)

Target "Build" (fun _ ->
    !! Solution
    |> MSBuildRelease OutputDir "Build"
    |> Log "BuildSource output: "
)

Target "Test" (fun _ -> 
    !! (OutputDir + TestAssembly)
        |> NUnit (fun p -> 
            {p with
                ToolPath = TestRunnerPath;
                DisableShadowCopy = true; 
                OutputFile = OutputDir + "TestResults.xml"})
)

Target "Package" (fun _ ->
    GeneratePackage "FarsiLibrary.Utils"
    GeneratePackage "FarsiLibrary.WPF"
    GeneratePackage "FarsiLibrary.Win"
    GeneratePackage "FarsiLibrary.Win.DevExpress.15.2"
    GeneratePackage "FarsiLibrary.Win.DevExpress.16.1"
)

"EnsureDir"
   ==> "Clean"
   ==> "Restorepackages"
   ==> "Build"

"Build"
   ==> "Package"

RunTargetOrDefault "Build"

