module GitignoreGenerator.App 

open Argu
open GitignoreGenerator.Logic
open FSharpPlus
open FSharp.Data
open System.IO
open System


let fetchGitIgnoreOrDoNothing api (ignoreTagsOrNone: string option) = async {
    return! 
     match ignoreTagsOrNone with
        | Some v -> Http.AsyncRequestString(api + "/" + v)
        | None _ -> async {  let empty = "" 
                             return empty }
}

let asyncWriteFile fileName contents = async {
    File.WriteAllText(fileName, contents)
}

let asyncAppendFile fileName contents = async {
    File.AppendAllText(fileName, contents)
}

let asyncDoNothing fileName contents = async { ignore() }

let determineMode fileName mode = 
   match mode with
   | "append" -> asyncAppendFile fileName
   | "overwrite" -> asyncWriteFile fileName
   | _ -> asyncDoNothing fileName


let unsafeGetCurrentDirectory = Environment.CurrentDirectory + "/.gitignore"


[<EntryPoint>]
let main argv =
    let parser = ArgumentParser.Create<CLIArgs.CLIArguments>(programName = "gig.exe")
    let api = "https://www.gitignore.io/api/"

    let results = parser.Parse(argv)

    let ignoreTagsOrNone = results.TryGetResult CLIArgs.CLIArguments.Ignore_Tags
    let directory = results.TryGetResult CLIArgs.CLIArguments.Directory 
                  |> map(fun dir -> dir + "/.gitignore") 
                  |> Option.defaultValue unsafeGetCurrentDirectory

    let operation = results.TryGetResult CLIArgs.CLIArguments.Mode 
                 |> Option.defaultValue "overwrite"
                 |> determineMode directory
    
    
    ignoreTagsOrNone |> (fetchGitIgnoreOrDoNothing api >=> operation) |> Async.RunSynchronously

    0


