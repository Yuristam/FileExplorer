public class FileSystemEventArgs : EventArgs
{
    public string Path { get; }
    public bool ShouldExclude { get; set; }

    public FileSystemEventArgs(string path)
    {
        Path = path;
        ShouldExclude = false;
    }
}
