namespace GitignoreGenerator.Logic

open Argu

module CLIArgs = 

  type CLIArguments =
    | [<Mandatory>] Contents of tags:string
    | Directory of path:string
    | Mode of mode:string
  with
     interface IArgParserTemplate with
       member s.Usage =
          match s with
            | Contents _ -> "This is a mandatory option since the tags are needed for gitignore content, Example: linux,java || CSharp,Windows"
            | Directory _ -> "The directory you wish to create the .gitignore file"
            | Mode _ -> "Example: append || overwrite"