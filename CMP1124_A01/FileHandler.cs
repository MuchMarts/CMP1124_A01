namespace CMP1124_A01;

// This will be responsible for handling all of the file IO and merging the data

public class FileHandler
{
    
    private Dictionary<string, int[]> _storedRoads;
    private Dictionary<string, int[]> _combined;
    private Dictionary<string, int[]> _sorted;
    
    public FileHandler()
    {
        _storedRoads = new Dictionary<string, int[]>();
        _combined = new Dictionary<string, int[]>();
        _sorted = new Dictionary<string, int[]>();
    }


    public void ReadFiles(string[] files)
    {
        foreach (var file in files)
        {
            var fileName = Path.GetFileName(file);
            
            if (_storedRoads.ContainsKey(file)) continue;
            
            var fileData = File.ReadAllLines(file);
            _storedRoads[fileName] = fileData.Select(n => Convert.ToInt32(n)).ToArray();
        }
    }

    public void MergeData()
    {
        throw new NotImplementedException();
    }

    public int[] SeeData(string file)
    {
        if(_sorted.ContainsKey(file)) return _sorted[file];
        Console.WriteLine("File has not been Sorted!");
        
        if(_storedRoads.ContainsKey(file)) return _storedRoads[file];
        if(_combined.ContainsKey(file)) return _combined[file];
        
        throw new Exception("Stored file not found in either _storedRoads or _combined");
    }

    public int[] Search(string file, int key)
    {
        var result = Algorithms.SequentialSearch(SeeData(file), key);
        return result;
    }

    public void SortData(string[] files)
    {
        foreach (var file in files)
        {
            var name = Path.GetFileName(file);
            if (_storedRoads.ContainsKey(name))
            {
                _sorted[name] = Algorithms.BubbleSort(_storedRoads[name]);
            }
            else if (_combined.ContainsKey(name))
            {
                _sorted[name] = Algorithms.BubbleSort(_combined[name]);
            }
            else
            {
                throw new Exception("File not found in either _storedRoads or _combined");
            }
        }
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