open System
open System.IO
open System.Text.RegularExpressions

#if !DEBUG
let baseFolder : string = Directory.GetCurrentDirectory()
let fileName = "Properties\AssemblyInfo.cs"
#else
let baseFolder :string = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName
let fileName = "TestFile.txt"
#endif
    
let getFullPath = Path.Combine(baseFolder, fileName)

let regexPattern = "(?<=(AssemblyVersion(\(\"))(((\d)\.){2}))" + "(.*)" + "(?=(.(\d)\"\)]))"
let fullVersionRegexPattern = "(?<=(AssemblyVersion(\(\")))" + "(.*)" + "(?=(\"\)]))"

let getFile =
    File.ReadAllText(getFullPath)
    
let replaceBuildVersion (fileContent : string) : string =
    let value = Int32.Parse(Regex.Match(fileContent, regexPattern).Value)
    Regex.Replace(fileContent, regexPattern, (value + 1).ToString())

let saveFile (fileContent : string) =
    File.WriteAllText(getFullPath, fileContent)
    
let printBuildVersion (fileContent : string) =
    Console.WriteLine "Build version has been updated"
    Console.WriteLine("New version: " + Regex.Match(fileContent, fullVersionRegexPattern).Value)
    

[<EntryPoint>]
let main argv =
    let newContent = replaceBuildVersion (getFile)
    saveFile (newContent)
    printBuildVersion (getFile)
    0
