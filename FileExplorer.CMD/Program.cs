using FileExplorer.CMD;

string rootFolder = @"D:\YourFilePath";
FileSystemExplorer fileSystemExplorer = new FileSystemExplorer(rootFolder);
fileSystemExplorer.Traverse();
