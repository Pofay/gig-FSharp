module GitignoreGenerator.App 

open Argu
open GitignoreGenerator.Logic


[<EntryPoint>]
let main argv =

    let parser = ArgumentParser.Create<CLIArgs.CLIArguments>(programName = "gig.exe")
    let usage = parser.PrintUsage()
    printfn "%s" usage
    0 // return an integer exit code
