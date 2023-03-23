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


    public void ReadFiles(string[] files)
    {
        foreach (var file in files)
        {
            var fileName = Path.GetFileName(file);
            
            if (_storedRoads.ContainsKey(file)) continue;
            
            var fileData = File.ReadAllLines(file);
            _storedRoads[fileName] = fileData;
        }
    }

    public void MergeData()
    {
        throw new NotImplementedException();
    }

    public string[] SeeData(string file)
    {
        if(_storedRoads.ContainsKey(file)) return _storedRoads[file];
        if(_combined.ContainsKey(file)) return _combined[file];
        throw new Exception("Stored file not found in either _storedRoads or _combined");
    }

    public void Search()
    {
        throw new NotImplementedException();
    }

    public void SortData()
    {
        throw new NotImplementedException();
    }

    public string[] AllLoadedFiles()
    {
        var files = _storedRoads.Keys.ToList();
        files.AddRange(_combined.Keys.ToList());
        
        return files.ToArray();
    }
    
    public string[] AllFiles()
    {
        string dir = Directory.GetCurrentDirectory();
        string[] files = Directory.GetFiles(dir, "Road_Data/Road_*.txt");
        return files;
    }
}