using FileExplorer.CMD;

Console.Write("Enter path to folder: ");

string userInput = Console.ReadLine().Trim();

string rootFolder = @$"{userInput}";

Func<string, bool> filter = path => Path.GetExtension(path).Equals(".jpeg", StringComparison.OrdinalIgnoreCase);

FileSystemExplorer fileSystemExplorer = new FileSystemExplorer(rootFolder, filter);

//FileSystemExplorer fileSystemExplorer = new FileSystemExplorer(rootFolder);

fileSystemExplorer.Traverse();
