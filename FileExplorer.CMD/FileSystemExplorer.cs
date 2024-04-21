﻿namespace FileExplorer.CMD;

public class FileSystemExplorer : IFileSystemExplorer
{
    private readonly string _rootFolder;

    public FileSystemExplorer(string rootFolder)
    {
        _rootFolder = rootFolder;
    }

    public void Traverse()
    {
        if (Directory.Exists(_rootFolder))
        {
            TraverseFolder(_rootFolder);
        }
        else
        {
            Console.WriteLine("There is no such directory.");
        }
    }

    private void TraverseFolder(string folderPath)
    {
        Console.WriteLine($"Folder: {folderPath}");

        try
        {
            string[] files = Directory.GetFiles(folderPath);

            foreach (string file in files)
            {
                Console.WriteLine($"- File: {file}");
            }

            string[] subFolders = Directory.GetDirectories(folderPath);

            foreach (string subFolder in subFolders)
            {
                TraverseFolder(subFolder);
            }
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine($"Access to folder '{folderPath}' is denied.");
        }
        catch (DirectoryNotFoundException)
        {
            Console.WriteLine($"Folder '{folderPath}' not found.");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error accessing folder '{folderPath}': {ex.Message}");
        }
    }

}