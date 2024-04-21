namespace FileExplorer.CMD;

public class FileSystemExplorer
{
    private readonly string _rootFolder;

    public FileSystemExplorer(string rootFolder)
    {
        _rootFolder = rootFolder;
    }

    public void Traverse()
    {
        TraverseFolder(_rootFolder);
    }

    private void TraverseFolder(string folderPath)
    {
        Console.WriteLine($"Folder: {folderPath}");

        try
        {
            string[] files = Directory.GetFiles(folderPath);

            foreach (string file in files)
            {
                Console.WriteLine($"File: {file}");
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