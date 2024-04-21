using FileExplorer.CMD;

Console.Write("Enter path to folder: ");

string userInput = Console.ReadLine().Trim();
string rootFolder = @$"{userInput}";

Func<string, bool> filter = path => path.EndsWith(".png", StringComparison.OrdinalIgnoreCase);

FileSystemExplorer fileSystemExplorer = new FileSystemExplorer(rootFolder, filter);
//FileSystemExplorer fileSystemExplorer = new FileSystemExplorer(rootFolder);


// Events
fileSystemExplorer.Start += (sender, eventArgs) =>
{
    Console.WriteLine("Traversal started...");
};

fileSystemExplorer.Finish += (sender, eventArgs) =>
{
    Console.WriteLine("Traversal finished!");
};

fileSystemExplorer.FileFound += (sender, eventArgs) =>
{
    Console.WriteLine($"- File found: {eventArgs.Path}");
};

fileSystemExplorer.DirectoryFound += (sender, eventArgs) =>
{
    Console.WriteLine($"Directory found: {eventArgs.Path}");
};

fileSystemExplorer.FilteredFileFound += (sender, eventArgs) =>
{
    Console.WriteLine($"-- Filtered file found: {eventArgs.Path}");
};

fileSystemExplorer.FilteredDirectoryFound += (sender, eventArgs) =>
{
    Console.WriteLine($"Filtered directory found: {eventArgs.Path}");
};


foreach (string item in fileSystemExplorer)
{
    //Console.WriteLine(item);
}
