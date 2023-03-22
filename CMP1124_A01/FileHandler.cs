namespace CMP1124_A01;

// This will be responsible for handling all of the file IO and merging the data

public class FileHandler
{
    
    private Dictionary<string, string[]> _storedRoads;
    private Dictionary<string, string[]> _combined;
    
    public FileHandler()
    {
        _storedRoads = new Dictionary<string, string[]>();
        _combined = new Dictionary<string, string[]>();
    }


    public void ReadFiles()
    {
        throw new NotImplementedException();
    }

    public void MergeData()
    {
        throw new NotImplementedException();
    }

    public void SeeData()
    {
        throw new NotImplementedException();
    }

    public void Search()
    {
        throw new NotImplementedException();
    }

    public void SortData()
    {
        throw new NotImplementedException();
    }

    public string[] AllFIles()
    {
        string dir = Directory.GetCurrentDirectory();
        string[] files = Directory.GetFiles(dir, "Road_*.txt");
        return files;
    }
}