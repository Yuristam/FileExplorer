﻿using FileExplorer.CMD;

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


foreach (string item in fileSystemExplorer)
{
    Console.WriteLine(item);
}
