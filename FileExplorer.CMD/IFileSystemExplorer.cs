namespace FileExplorer.CMD;

public interface IFileSystemExplorer
{
    void Traverse(string folderPath, List<string> paths);
}