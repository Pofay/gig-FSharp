### gig-F

**Reminder: This only serves as an F# Learning Exercise especially on its Monadic Libraries as well as app publishing and making console apps**

A CLI App targeting dotnet-core that generates a .gitignore file with appropriate contents based on the options given.

[Javascript Version][gig-js]

### Basic Usage

    OPTIONS:

    --ignore-tags <tags>  This is a mandatory option since the tags are needed for gitignore
                          content, Example: linux,java || CSharp,Windows
    --directory <path>    The directory you wish to create the .gitignore file
    --mode <mode>         Example: append || overwrite
    --help                display this list of options.

This is for Windows-x64:

    GitignoreGenerator.App.exe --ignore-tags linux,java --directory cd --mode overwrite

### Small Difference in Implementation

Compared to my original [gitignore-generator][gig-js] in Javascript that hits the Github API this one hits the [gitignore.io](https://gitignore.io/) API.

It also uses Async<'T> instead of RxObservables for dealing with asynchronous operations.

[gig-js]: https://github.com/Pofay/gitignore-generator
