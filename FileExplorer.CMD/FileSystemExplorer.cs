using System.Collections;

namespace FileExplorer.CMD;

public class FileSystemExplorer : IFileSystemExplorer, IEnumerable<string>
{
    private readonly string _rootFolder;
    private readonly Func<string, bool> _filter;

    // Events for start and finish
    public event EventHandler Start;
    public event EventHandler Finish;

    public event EventHandler<FileSystemEventArgs> FileFound;
    public event EventHandler<FileSystemEventArgs> DirectoryFound;
    public event EventHandler<FileSystemEventArgs> FilteredFileFound;
    public event EventHandler<FileSystemEventArgs> FilteredDirectoryFound;

    public FileSystemExplorer(string rootFolder)
    {
        _rootFolder = rootFolder;
    }

    public FileSystemExplorer(string rootFolder, Func<string, bool> filter)
    {
        _rootFolder = rootFolder;
        _filter = filter ?? (path => true);
    }

    public IEnumerator<string> GetEnumerator()
    {
        // Notify subscribers that the traversal is starting
        OnStart();

        List<string> paths = new List<string>();
        Traverse(_rootFolder, paths);
        foreach (var path in paths)
        {
            yield return path;
        }

        // Notify subscribers that the traversal has finished
        OnFinish();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    // Event methods begin
    protected virtual void OnStart()
    {
        Start?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void OnFinish()
    {
        Finish?.Invoke(this, EventArgs.Empty);
    }
    // Event methods end

    public void Traverse(string folderPath, List<string> paths)
    {
        try
        {
            foreach (string file in Directory.GetFiles(folderPath))
            {
                FileSystemEventArgs args = new FileSystemEventArgs(file);
                FileFound?.Invoke(this, args);

                if (!args.ShouldExclude)
                {
                    paths.Add($"- File: {file}");
                    if (_filter == null || _filter(file))
                    {
                        FilteredFileFound?.Invoke(this, args);
                        if (!args.ShouldExclude)
                        {
                            paths.Add($"- Filtered File: {file}");
                        }
                    }
                }
            }

            foreach (string subFolder in Directory.GetDirectories(folderPath))
            {
                FileSystemEventArgs args = new FileSystemEventArgs(subFolder);
                DirectoryFound?.Invoke(this, args);

                if (!args.ShouldExclude)
                {
                    paths.Add($"Folder: {subFolder}");
                    if (_filter == null || _filter(subFolder))
                    {
                        FilteredDirectoryFound?.Invoke(this, args);
                        if (!args.ShouldExclude)
                        {
                            Traverse(subFolder, paths);
                        }
                    }
                    else
                    {
                        Traverse(subFolder, paths); // Continue traversal regardless of filtering
                    }
                }
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
