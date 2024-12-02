module fsharp.FileUtility

open System.IO

let getFileLines filePath =
    let fullPath = Path.Combine(Directory.GetCurrentDirectory(), filePath)
    File.ReadLines(fullPath)
