namespace CMP1124_A01;

// This will be the main class that will handle the user interface and call the other classes

public class Cli
{
    private FileHandler _fh;
    private bool ascending = true;
    public Cli()
    {
        _fh = new FileHandler();
    }
    
    public void Run()
    {

        while (true)
        {
            Console.Clear();
            Menu();
            Console.Write("Enter your choice: ");
            try
            {
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 0:
                        Console.WriteLine("Exiting application!");
                        return;
                    case 1:
                        ReadFiles();
                        break;
                    case 2:
                        MergeData();
                        break;
                    case 3:
                        SeeData();
                        break;
                    case 4:
                        Search();
                        break;
                    case 5:
                        SortData();
                        break;
                    case 6:
                        TestAlgos();
                        break;
                    default:
                        Console.WriteLine("Choice does not exists");
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }

    private void TestAlgos()
    {
        throw new NotImplementedException();
    }

    // Used to Read files into Arrays
    private void ReadFiles()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Files found: ");

            var allFiles = _fh.AllFiles();
            var files = allFiles.Aggregate("", (current, file) => current + $"{Path.GetFileName(file)}\n");
            Console.WriteLine(files);

            var options = "[0] Back\n" + "[1] All Files\n" + "[2] Select Files\n";
            Console.WriteLine(options);

            void SelectFiles()
            {
                var fileOptions = new List<string>(allFiles);
                if (fileOptions.Count < 1)
                {
                    Console.WriteLine("No Files Found!");
                    return;
                }

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Choose Files to be Read: "); 
                    var opt = 1; 
                    var fOptions = fileOptions.Aggregate("", (current, file) => current + $"[{opt++}] {Path.GetFileName(file)}\n"); 
                    fOptions += "[0] Back\n";
                    Console.WriteLine(fOptions);

                    try
                    {
                        var choice = Convert.ToInt32(Console.ReadLine());
                        if (choice == 0) return;
                        if (choice > fileOptions.Count) return;
                        
                        _fh.ReadFiles(new []{fileOptions[choice - 1]});
                        fileOptions.RemoveAt(choice - 1);
                        
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }
            }
            
            try
            {
                var choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        // Read all Files
                        _fh.ReadFiles(allFiles);
                        break;
                    case 2:
                        // Provide option to read select files
                        SelectFiles();
                        break;
                    case 0:
                        // Back goes back to Menu
                        return;
                    default:
                        Console.WriteLine("Option does not exists");
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            TempStop();
            return;
        }
    }
    
    private void TempStop()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();    
    }
    
    private void MergeData()
    {
        var allFiles = _fh.AllLoadedFiles().ToList();
        var chosenFiles = new List<string>();
        
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Choose files to merge: ");

            var opt = 1;
            var notChosenFiles = allFiles.Aggregate("", (current, file) => current + $"[{opt++}] {file}\n");
            notChosenFiles += "[0] Merge\n" + "[-1] Back\n";

            string cFiles;
            if (chosenFiles.Count > 0)
            {
                opt = 1;
                cFiles = chosenFiles.Aggregate("", (current, file) => current + $"[{opt++}] {file}\n");
            } else cFiles = "No Files Chosen\n";

            Console.WriteLine("Chosen Files: ");
            Console.WriteLine(cFiles);
            Console.WriteLine("Not Chosen Files: ");
            Console.WriteLine(notChosenFiles);

            try
            {
                var choice = Convert.ToInt32(Console.ReadLine());
                if (choice == 0)
                {
                    _fh.MergeData(chosenFiles.ToArray());
                    TempStop();
                    return;
                }

                if (choice == -1) return;
                if (choice > allFiles.Count) return;

                chosenFiles.Add(allFiles[choice - 1]);
                allFiles.RemoveAt(choice - 1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }

    private void SeeData()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Files stored: ");

            var allFiles = _fh.AllLoadedFiles();
            var opt = 1;
            var options = allFiles.Aggregate("", (current, file) => current + $"[{opt++}] {file}\n");
            options += "[0] Back\n";

            Console.WriteLine(options);

            try
            {
                var choice = Convert.ToInt32(Console.ReadLine());
                if (choice == 0) return;
                if (choice > allFiles.Length)
                {
                    Console.WriteLine("Choice does not exists");
                    TempStop();
                    return;
                }

                

                var file = allFiles[choice - 1];
                var data = _fh.SeeData(file);
                var step = (data.Length < 2048) ? 10 : 50;

                var dataString = "";

                for (int i = 0; i < data.Length; i += step)
                {
                    dataString += $"{data[i]}\n";
                }

                Console.WriteLine(dataString);
                TempStop();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }

    private void Search()
    {
        Console.Clear();
        Console.WriteLine("Choose a file to search: ");
        var allFiles = _fh.AllLoadedFiles();
        var opt = 1;
        var options = allFiles.Aggregate("", (current, file) => current + $"[{opt++}] {file}\n");
        options += "[0] Back\n";
        
        Console.WriteLine(options);

        try
        {
            var choice = Convert.ToInt32(Console.ReadLine());
            if (choice == 0) return;
            if (choice > allFiles.Length)
            {
                Console.WriteLine("Choice does not exists");
                TempStop();
                return;
            }
            
            Console.WriteLine("Choose a value to search: ");
            
            var value = Convert.ToInt32(Console.ReadLine());
            var file = allFiles[choice - 1];

            var result = _fh.Search(file, value);
            Console.WriteLine((result[^1] == value) ? "Entered value found!" : "Entered value not found!");
            Console.WriteLine((result[^1] == value) ? "All found values: " : "All closest values: ");

            for (int i = 0; i < result.Length - 1; i++)
            {
                Console.WriteLine("Line: " + result[i+1] + " Value: " + result[^1]);
            }
            TempStop();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private void SortData()
    {
        while (true)
        {
            var allFiles = _fh.AllLoadedFiles();
            var type = (ascending ? " Ascending" : " Descending");
            var options = "[0] Back\n" + "[1] Sort All Files\n" + "[2] Sort Select Files\n" + "[3] Change Sort Type";
            Console.Clear();
            Console.WriteLine("Algorithms files Options: ");
            Console.WriteLine("Current Sort Type: " + type);
            Console.WriteLine(options);

            try
            {
                var choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        // Sort all Files
                        _fh.SortData(allFiles, ascending);
                        TempStop();
                        return;
                    case 2:
                        // Provide option to sort select files
                        SelectFiles();
                        break;
                    case 3:
                        // Change sort type
                        ascending = !ascending;
                        break;
                    case 0:
                        // Back goes back to Menu
                        return;
                    default:
                        Console.WriteLine("Option does not exists");
                        break;
                }
                
                void SelectFiles()
                {
                    var notSorted = _fh.getNotSorted().ToList();
                    
                    if(notSorted.Count < 1)
                    {
                        Console.WriteLine("No Files Found!");
                        return;
                    }
                    
                
                    while (true)
                    {
                        var sOpt = 1;
                        var fileOptions = notSorted.Aggregate("[0] Back\n", (current, file) => current + $"[{sOpt++}] {file}\n");
                        Console.Write(fileOptions);
                        try
                        {
                            var choice = Convert.ToInt32(Console.ReadLine());
                        
                            if (choice == 0) return;
                            if (choice > notSorted.Count) return;
                        
                            _fh.SortData(new []{notSorted[choice - 1]}, true);
                            notSorted.RemoveAt(choice - 1);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            throw;
                        }
                    }
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }

    private void Menu()
    {
        string choices = "Options\n" +
                         "[1] Read Data from Files\n" +
                         "[2] Merge Data\n" +
                         "[3] See Data\n" +
                         "[4] Search\n" +
                         "[5] Sort Data\n" +
                         "[0] Exit";

    Console.WriteLine(choices);
    }
    
}