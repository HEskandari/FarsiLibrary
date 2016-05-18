#r "./lib/FAKE/FakeLib.dll"
open Fake 


let OutputDir      = "./binaries/"
let DeployDir      = "./publish/"
let Solution       = "FarsiLibrary.sln"
let ToolsFolder    = "./lib/"
let PackagesFolder = "./packages"
let TestAssembly   = "FarsiLibrary.UnitTest.dll"
let TestRunnerPath = "./packages/NUnit.Runners.2.6.4/tools"

Target "EnsureDir" (fun _ -> 
    trace "Ensuring directories exists..."
    ensureDirectory OutputDir
    ensureDirectory DeployDir
)

Target "Clean" (fun _ ->
    trace "Cleaning the output folder..."
    CleanDirs [ OutputDir; DeployDir ]
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

Target "Deploy" (fun _ ->
    trace "Generating nuget package..."
)

"EnsureDir"
   ==> "Clean"
   ==> "Restorepackages"
   ==> "Build"
   ==> "Test"
   ==> "Deploy"

Run "Test"