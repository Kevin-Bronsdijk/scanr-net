// --------------------------------------------------------------------------------------
// FAKE build script 
// --------------------------------------------------------------------------------------

#r @"tools\FAKE\tools\FakeLib.dll"
open Fake
open Fake.AssemblyInfoFile

// --------------------------------------------------------------------------------------
// Information about the project to be used at NuGet and in AssemblyInfo files
// --------------------------------------------------------------------------------------

let project = "scanr-net"
let authors = ["Kevin Bronsdijk"]
let summary = "A .NET library for the ScanR REST API"
let version = "0.1.1.0"
let description = "This library interacts with the ScanR REST API allowing you to utilize ScanR features using a .NET interface."
let notes = "For more information and documentation, please visit the project site on GitHub."
let nugetVersion = "1.1.0"
let tags = "scanR C# API ORC image text scan"
let gitHome = "https://github.com/Kevin-Bronsdijk"
let gitName = "scanr-net"

// --------------------------------------------------------------------------------------
// Build script 
// --------------------------------------------------------------------------------------

let buildDir = "./output/"
let packagingOutputPath = "./nuGet/"
let packagingWorkingDir = "./inputNuget/"
let nugetDependencies = getDependencies "./src/scanr-net/packages.config"

// --------------------------------------------------------------------------------------

Target "Clean" (fun _ ->
 CleanDir buildDir
)

// --------------------------------------------------------------------------------------

Target "AssemblyInfo" (fun _ ->
    let attributes =
        [ 
            Attribute.Title project
            Attribute.Product project
            Attribute.Description summary
            Attribute.Version version
            Attribute.FileVersion version
        ]

    CreateCSharpAssemblyInfo "src/scanr-net/Properties/AssemblyInfo.cs" attributes
)

// --------------------------------------------------------------------------------------

Target "Build" (fun _ ->
 !! "src/*.sln"
 |> MSBuildRelease buildDir "Build"
 |> Log "AppBuild-Output: "
)

// --------------------------------------------------------------------------------------

Target "CreatePackage" (fun _ ->

    CreateDir packagingWorkingDir
    CleanDir packagingWorkingDir
    CopyFile packagingWorkingDir "./output/ScanR.dll"

    NuGet (fun p -> 
        {p with
            Authors = authors
            Dependencies = nugetDependencies
            Files = [@"ScanR.dll", Some @"lib/net452", None]
            Project = project
            Description = description
            OutputPath = packagingOutputPath
            Summary = summary
            WorkingDir = packagingWorkingDir
            Version = nugetVersion
            ReleaseNotes = notes
            Publish = false }) 
            "scanr.nuspec"
            
    DeleteDir packagingWorkingDir
)

// --------------------------------------------------------------------------------------

Target "All" DoNothing

"Clean"
  ==> "AssemblyInfo"
  ==> "Build"
  ==> "CreatePackage"
  ==> "All"

RunTargetOrDefault "All"