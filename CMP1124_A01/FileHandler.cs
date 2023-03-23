using System.Runtime.Versioning;

namespace CMP1124_A01;

// This will be responsible for handling all of the file IO and merging the data

public class FileHandler
{
    // maps file names to arrays of data
    private Dictionary<string, int[]> _storedRoads;
    private Dictionary<string, int[]> _combined;
    private Dictionary<string, int[]> _sorted;

    // maps file names to type of sort order and type of sort algorithm
    private Dictionary<string, bool> _sortType;
    private Dictionary<string, int> _sortAlgo;

    // List of Sorting algorithms
    private string[] _algoTypes = {"Bubble Sort", "Insertion Sort", "Merge Sort"};
    
    // Current active sorting algorithm
    private int _currAlgoType = 2;
    
    // translation layer for combined files for easy access
    private Dictionary<string, int> _nameToKey;
    
    // keyIndex used to distinguish between files used in combinations
    private int _keyIndex = 1;

    public FileHandler()
    {
        // Initializes all of the Dictionaries
        _storedRoads = new Dictionary<string, int[]>();
        _combined = new Dictionary<string, int[]>();
        _sorted = new Dictionary<string, int[]>();
        
        _sortType = new Dictionary<string, bool>();
        _sortAlgo = new Dictionary<string, int>();
        
        _nameToKey = new Dictionary<string, int>();
    }

    // Returns Sorting Algorithm name
    public string getAlgoName()
    {
        return _algoTypes[_currAlgoType];
    }

    // Returns size of algorithm array
    public int AlgoAmmount()
    {
        return _algoTypes.Length;
    }
    
    // Cycles active algorithm by one
    public void cycleAlgo()
    {
        if(_currAlgoType == _algoTypes.Length - 1) _currAlgoType = 0;
        else _currAlgoType++;
    }
    
    // Read all files and stores them into _storedRoads
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

    // Handles Merge logic and storage
    public void MergeData(string[] files)
    {
        var keys = new List<int>();
        var value = new List<int>();

        // Add new files to translation layer and sort keys
        foreach (var file in files)
        {
            if (_combined.ContainsKey(file)) value.AddRange(_combined[file]);
            if (_storedRoads.ContainsKey(file)) value.AddRange(_storedRoads[file]);
            
            // Ads new files to key translation layer for combination creation
            // key is used to create a unique name for each combination so it can be distinguished in storage
            if (!_nameToKey.ContainsKey(file))
            {
                _nameToKey[file] = _keyIndex++;
            }
            keys.Add(_nameToKey[file]);
            
        }
        // Create key and array of values an store it into _combined
        
        var sKeys = Algorithms.BubbleSort(keys.ToArray());
        var combinedKey = string.Join("-", sKeys);
        
        if (_combined.ContainsKey(combinedKey)) return;
        
        _combined[combinedKey] = value.ToArray();
    }

    // returns stored data
    public int[] SeeData(string file)
    {
        if(_sorted.ContainsKey(file)) return _sorted[file];
        if(_storedRoads.ContainsKey(file)) return _storedRoads[file];
        if(_combined.ContainsKey(file)) return _combined[file];
        
        throw new Exception("Stored file not found in either _storedRoads or _combined");
    }

    // Uses Binary or Sequential search to search for data 
    public int[] Search(string file, int key)
    {
        int[] result;

        if (_sorted.ContainsKey(file))
        {
            Console.WriteLine("Using Binary Search");
            result = Algorithms.BinarySearch(SeeData(file), key);
        }
        else
        {
            Console.WriteLine("File has not been Sorted!");
            Console.WriteLine("Using Sequential Search");
            result = Algorithms.SequentialSearch(SeeData(file), key);
        }

        return result;
    }

    // Handles all sorting logic, Based on _currAlgoType determines which sorting algorithm has to be used
    public void SortData(string[] files, bool ascending)
    {
        foreach (var file in files)
        {
            var name = file;
            
            if (_sorted.ContainsKey(name) && _sortType[name] == ascending && _sortAlgo[name] == _currAlgoType) continue;
            
            if (_storedRoads.ContainsKey(name))
            {
               // 2 switch cases based on where the target file is stored
                switch (_currAlgoType)
                {
                    case 0:
                        _sorted[name] = Algorithms.BubbleSort(_storedRoads[name], ascending);
                        break;
                    case 1:
                        _sorted[name] = Algorithms.InsertionSort(_storedRoads[name], ascending);
                        break;
                    case 2:
                        _sorted[name] = Algorithms.MergeSort(_storedRoads[name], ascending);
                        break;
                    default:
                        Console.WriteLine("This sorting type does not exists!");
                        break;
                }
                
                _sortType[name] = ascending;
                _sortAlgo[name] = _currAlgoType;
            }
            else if (_combined.ContainsKey(name))
            {
                switch (_currAlgoType)
                {
                    case 0:
                        _sorted[name] = Algorithms.BubbleSort(_combined[name], ascending);
                        break;
                    case 1:
                        _sorted[name] = Algorithms.InsertionSort(_combined[name], ascending);
                        break;
                    case 2:
                        _sorted[name] = Algorithms.MergeSort(_combined[name], ascending);
                        break;
                    default:
                        Console.WriteLine("This sorting type does not exists!");
                        break;
                }
                _sortType[name] = ascending;
                _sortAlgo[name] = _currAlgoType;
            }
            else
            {
                throw new Exception("File not found in either _storedRoads or _combined");
            }
        }
    }

    // Get all files that have not been sorted
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
    // Gets all files that have been stored _storedRoads and _combined
    public string[] AllLoadedFiles()
    {
        var files = _storedRoads.Keys.ToList();
        files.AddRange(_combined.Keys.ToList());
        
        return files.ToArray();
    }
    
    // Gets all *.txt files in memory
    public string[] AllFiles()
    {
        string dir = Directory.GetCurrentDirectory();
        string[] files = Directory.GetFiles(dir, "Road_Data/Road_*.txt");
        return files;
    }
}