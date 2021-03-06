// include Fake lib
#r "./packages/FAKE/tools/FakeLib.dll"

open Fake
open Fake.Testing

// Directories
let appDir = "build/a/"
let testDir  = "build/t/"
let deployDir = "deploy/"

// info
let applicationName = "LoonieTrader.Server"
let version = "0.1"  // or retrieve from CI server

MSBuildDefaults <- {
    MSBuildDefaults with
        Verbosity = Some MSBuildVerbosity.Minimal
}

// Targets
Target "Clean" (fun f ->
  CleanDirs [appDir; testDir; deployDir]
)

Target "BuildApp" (fun f ->
  !! "app/*/*.xproj"
    |> MSBuildRelease appDir "Build"
    |> ignore
)

Target "BuildTest" (fun f ->
  !! "test/*/*.xproj"
    |> MSBuildDebug testDir "Build"
    |> ignore
)

Target "UnitTest" (fun f ->
 !! (testDir + "**/*.Tests.dll")
   |> NUnit3 (fun p -> {p with ErrorLevel = Error; ResultSpecs = [testDir + "UnitTestResults.xml"]; Where = "cat == Unit" })
)

Target "IntegrationTest" (fun f ->
 !! (testDir + "**/*.Tests.dll")
   |> NUnit3 (fun p -> {p with ErrorLevel = Error; ResultSpecs = [testDir + "IntegrationTestResults.xml"]; Where = "cat == Integration" })
)

Target "Deploy" (fun f ->
  !! (appDir + "/**/*.*")
  -- "*.zip"
  |> Zip appDir (deployDir + applicationName + "." + version + ".zip")
)

// Dependencies
"Clean"
  ==> "BuildApp"
    ==> "BuildTest"
      ==> "UnitTest"
        ==> "IntegrationTest"
          ==> "Deploy"

// Start default build if nothing explicitly specified
RunTargetOrDefault "UnitTest"