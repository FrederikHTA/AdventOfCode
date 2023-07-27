using System.Text.RegularExpressions;
using _2022_csharp.Lib;
using FluentAssertions;

namespace _2022_csharp.Day7;

public record Directory(string Name, Directory? Parent)
{
    public string Name { get; } = Name;
    public Directory? Parent { get; } = Parent;
    public List<Directory> SubDirectories { get; } = new();
    public List<SystemFile> Files { get; } = new();
    public int Size { get; set; }
}

public record SystemFile(string Name, long Size);

static class Day7
{
    private static Regex dir = new(@"dir (\w+)");
    private static Regex file = new(@"(\d+) (.+)");
    private static Regex cd = new(@"\$ cd (\w+|\d+)");
    private static Regex cdSlash = new(@"\$ cd \/");
    private static Regex cdOut = new(@"\$ cd \.\.");

    // private static Regex ls = new(@"\$ ls");

    public static void Part1()
    {
        // var lines = Utilities.GetLines("/Day7/Test2.txt");
        var lines = Utilities.GetLines("/Day7/Data.txt");

        var directories = BuildTree(lines);

        var sizes = directories
            .Select(CalculateDirectorySize)
            .Where(dirSize => dirSize <= 100000);

        var res = sizes.Sum();

        res.Should().Be(1297683);
        Console.WriteLine(res);
    }

    public static void Part2()
    {
        Console.WriteLine("Test");
    }

    private static int CalculateDirectorySize(Directory directory)
    {
        if (!directory.SubDirectories.Any()) return directory.Size;
        return directory.Size + directory.SubDirectories.Sum(CalculateDirectorySize);
    }

    private static List<Directory> BuildTree(IEnumerable<string> lines)
    {
        var rootDirectory = new Directory("root", null);
        var currentDirectory = rootDirectory;
        var directories = new List<Directory> { rootDirectory }; // list of all directories

        lines.ForEach(line =>
        {
            if (dir.IsMatch(line))
            {
                var split = line.Split(" ");
                var subDirectory = new Directory(split[1], currentDirectory);
                currentDirectory.SubDirectories.Add(subDirectory);
                directories.Add(subDirectory);
            }
            else if (file.IsMatch(line))
            {
                var split = line.Split(" ");

                var size = int.Parse(split[0]);
                var name = split[1];

                currentDirectory.Files.Add(new SystemFile(name, size));
                currentDirectory.Size += size;
            }
            else if (cd.IsMatch(line))
            {
                var folderName = line.Split(" ")[2];
                currentDirectory = currentDirectory.SubDirectories.First(x => x.Name == folderName);
            }
            else if (cdOut.IsMatch(line))
            {
                currentDirectory = currentDirectory.Parent ?? rootDirectory;
            }
            else if (cdSlash.IsMatch(line))
            {
                currentDirectory = rootDirectory;
            }
        });

        return directories;
    }
}