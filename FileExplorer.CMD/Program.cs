using FileExplorer.CMD;

Console.Write("Enter path to folder: ");

string userInput = Console.ReadLine().Trim();

string rootFolder = @$"{userInput}";

FileSystemExplorer fileSysteExplorer = new FileSystemExplorer(rootFolder);

fileSysteExplorer.Traverse();
