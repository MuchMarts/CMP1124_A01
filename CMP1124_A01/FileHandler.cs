using System.Runtime.Versioning;

namespace CMP1124_A01;

// This will be responsible for handling all of the file IO and merging the data

public class FileHandler
{
    // Store arrays of data with a key of the file name
    private Dictionary<string, int[]> _storedRoads;
    private Dictionary<string, int[]> _combined;
    private Dictionary<string, int[]> _sorted;
    
    // translation layer for combined files for easy access
    private Dictionary<string, int> _nameToKey;
    private Dictionary<int, string> _keyToName;
    // keyIndex used to distinguish between files used in combinations
    private int _keyIndex = 1;

    public FileHandler()
    {
        _storedRoads = new Dictionary<string, int[]>();
        _combined = new Dictionary<string, int[]>();
        _sorted = new Dictionary<string, int[]>();
        
        _nameToKey = new Dictionary<string, int>();
        _keyToName = new Dictionary<int, string>();
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

    public void MergeData(string[] files)
    {
        var keys = new List<int>();
        var value = new List<int>();

        // Add new files to translation layer and sort keys
        foreach (var file in files)
        {
            if (_combined.ContainsKey(file)) value.AddRange(_combined[file]);
            if (_storedRoads.ContainsKey(file)) value.AddRange(_storedRoads[file]);
            
            if (!_nameToKey.ContainsKey(file))
            {
                _nameToKey[file] = _keyIndex++;
                _keyToName[_nameToKey[file]] = file;
            }
            keys.Add(_nameToKey[file]);
            
        }
        
        var sKeys = Algorithms.BubbleSort(keys.ToArray());
        var combinedKey = string.Join("-", sKeys);
        
        if (_combined.ContainsKey(combinedKey)) return;
        
        _combined[combinedKey] = value.ToArray();
    }

    public int[] SeeData(string file)
    {
        if(_sorted.ContainsKey(file)) return _sorted[file];
        if(_storedRoads.ContainsKey(file)) return _storedRoads[file];
        if(_combined.ContainsKey(file)) return _combined[file];
        
        throw new Exception("Stored file not found in either _storedRoads or _combined");
    }

    public int[] Search(string file, int key)
    {
        int[] result;
        
        if (_sorted.ContainsKey(file)) result = Algorithms.BinarySearch(SeeData(file), key);
        else
        {
            Console.WriteLine("File has not been Sorted!");
            result = Algorithms.SequentialSearch(SeeData(file), key);
        }

        return result;
    }

    public void SortData(string[] files)
    {
        foreach (var file in files)
        {
            var name = file;
            if (_sorted.ContainsKey(name)) continue;
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

    public string[] getNotSorted()
    {
        var all = AllLoadedFiles();
        var notSorted = new List<string>();
        foreach (var file in all)
        {
            if (!_sorted.ContainsKey(file)) notSorted.Add(file);
        }
        
        return notSorted.ToArray();
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