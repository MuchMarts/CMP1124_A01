namespace CMP1124_A01;

// This will be the main class that will handle the user interface and call the other classes
public class Cli
{
    // Atributes used inside the class
    private FileHandler _fh;
    
    // This determines whether the sorting output order is ascending or descending
    private bool _ascending = true;

    public Cli()
    {
        _fh = new FileHandler();
    }
    
    // This is the main Menu loop
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
                
                // Contains all possible choices
                switch (choice)
                {
                    case 0:
                        Console.WriteLine("Exiting application!");
                        return;
                    case 1:
                        // You can choose which files to read into arrays
                        ReadFiles();
                        break;
                    case 2:
                        // You can choose which files to merge
                        MergeData();
                        break;
                    case 3:
                        // You can choose which file to display in the console
                        SeeData();
                        break;
                    case 4:
                        // You can pick a file and value to search in it
                        Search();
                        break;
                    case 5:
                        // You can choose which file in what order and with what sorting algorithm to sort
                        SortData();
                        break;
                    case 6:
                        // Will run through all algorithms with both ascending options and display step count and time in ms
                        Testing t = new Testing();
                        TempStop();
                        break;
                    default:
                        // Handle error if choice does not exists
                        Console.WriteLine("Choice does not exists");
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Invalid input try again!");
                TempStop();
            }
        }
    }

    // Used to Read files into Arrays
    private void ReadFiles()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Files found: ");
            
            // Gets all file paths and transforms it into a list of file names
            var allFiles = _fh.AllFiles();
            
            var files = allFiles.Aggregate("", (current, file) => current + $"{Path.GetFileName(file)}\n");
            Console.WriteLine(files);
            
            // Options for the user
            var options = "[0] Back\n" + "[1] All Files\n" + "[2] Select Files\n";
            Console.WriteLine(options);
            
            // Helper function to handle selecting specific files to Read. Required as to have switch statement more readable
            void SelectFiles()
            {
                var fileOptions = new List<string>(allFiles);
                if (fileOptions.Count < 1)
                {
                    Console.WriteLine("No Files Found!");
                    return;
                }
                
                // Loop that allows user to pick multiple files that get read into arrays
                // fOptions stores all non picked files
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Choose Files to be Read: "); 
                    var opt = 1; 
                    var fOptions = fileOptions.Aggregate("", (current, file) => current + $"[{opt++}] {Path.GetFileName(file)}\n"); 
                    fOptions += "[0] Back\n";
                    Console.WriteLine(fOptions);
                    
                    // Try catch to ensure valid input from user and to handle errors like out of range
                    try
                    {
                        var choice = Convert.ToInt32(Console.ReadLine());
                        if (choice == 0) return;
                        if (choice > fileOptions.Count) return;
                        
                        // Read file into array and remove from fileOptions so user cant double read file
                        _fh.ReadFiles(new []{fileOptions[choice - 1]});
                        fileOptions.RemoveAt(choice - 1);
                        
                    }
                    catch
                    {
                        Console.WriteLine("Invalid input try again!");
                    }
                }
            }
            
            // Main try catch for the ReadFiles menu
            // You can choose to read all files or select specific files
            // Back returns to main menu
            try
            {
                var choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("here");
                        // Read all Files
                        _fh.ReadFiles(allFiles);
                        return;
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
            catch
            {
                Console.WriteLine("Invalid input. Try again!");
                TempStop();
            }
        }
    }
    
    // Used to create a small pause inside the program loop. So the user can read the output and decide to go further
    private void TempStop()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();    
    }
    
    // Handles interface logic to merge files
    private void MergeData()
    {    
        // Gets all files and creates a list of chosen files
        var allFiles = _fh.AllLoadedFiles().ToList();
        var chosenFiles = new List<string>();
        
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Choose files to merge: ");
            
            // Creates a list of not chosen files which user can pick
            var opt = 1;
            var notChosenFiles = allFiles.Aggregate("", (current, file) => current + $"[{opt++}] {file}\n");
            notChosenFiles += "[0] Merge\n" + "[-1] Back\n";
            
            // cFiles is string that has chosen files in it
            string cFiles;
            if (chosenFiles.Count > 0)
            {
                opt = 1;
                cFiles = chosenFiles.Aggregate("", (current, file) => current + $"[{opt++}] {file}\n");
            } else cFiles = "No Files Chosen\n";
            
            
            // Display chosen and not chosen files
            // not chosen files have buttons as they can be picked
            // 0 : Merge (takes chosen files and merges them)
            // -1 : Back (goes back to main menu)
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
                
                // Add to chosen remove from not chosen
                chosenFiles.Add(allFiles[choice - 1]);
                allFiles.RemoveAt(choice - 1);
            }
            catch
            {
                Console.WriteLine("Invalid input. Try again!");
                TempStop();
            }

        }
    }

    // Interface function to display certain file data
    private void SeeData()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Files stored: ");
            
            // Creates a list of option for the user to choose from
            // options are all stored files (read files, combined files)
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
                // if lengths is less than 2048 display every 10th value
                // if not the nevery 50th value
                var step = (data.Length < 2048) ? 1 : 50;

                var dataString = "";

                for (int i = 0; i < data.Length; i += step)
                {
                    dataString += $"{data[i]}\n";
                }

                Console.WriteLine(dataString);
                TempStop();
            }
            catch
            {
                Console.WriteLine("Invalid input. Try again!");
            }
        }
    }

    // Function to hold all interface search logic
    private void Search()
    {
        Console.Clear();
        Console.WriteLine("Choose a file to search: ");
        
        // Create a list of options of all the available files
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
            // after file chosen user enters a value they are searching for
            Console.WriteLine("Choose a value to search: ");
            
            var value = Convert.ToInt32(Console.ReadLine());
            var file = allFiles[choice - 1];
            
            // Get results and formats them into a readable string
            var result = _fh.Search(file, value);
            Console.WriteLine((result[^1] == value) ? "Entered value found!" : "Entered value not found!");
            Console.WriteLine((result[^1] == value) ? "All found values: " : "All closest values: ");
            
            // Shows all found values
            for (int i = 0; i < result.Length - 1; i++)
            {
                Console.WriteLine("Line: " + result[i+1] + " Value: " + result[^1]);
            }
            TempStop();
        }
        catch
        {
            Console.WriteLine("Invalid input. Try Again!");
            TempStop();
        }
    }

    // Main interface function that has all data sorting logic
    private void SortData()
    {
        while (true)
        {   
            // Get all files and stored options 
            var allFiles = _fh.AllLoadedFiles();
            var direction = (_ascending ? " Ascending" : " Descending");
            var options = "[0] Back\n" + "[1] Sort All Files\n" + "[2] Sort Select Files\n" +
                          "[3] Change Sort Direction\n" + "[4] Change Sorting Algorithm\n";
            
            // Display UI elements and options 
            Console.Clear();
            Console.WriteLine("Algorithms files Options: ");
            Console.WriteLine("Current Sort Direction: " + direction);
            Console.WriteLine("Current sorting algorithm: " + _fh.getAlgoName());
            Console.WriteLine(options);

            try
            {
                var choice = Convert.ToInt32(Console.ReadLine());
                // Main logic of the function
                // Gived you diferent option on how to interact with the files
                switch (choice)
                {
                    case 1:
                        // Sort all Files
                        _fh.SortData(allFiles, _ascending);
                        TempStop();
                        return;
                    case 2:
                        // Provide option to sort select files
                        SelectFiles();
                        break;
                    case 3:
                        // Change sort type
                        // Asceding oreder or Descending order
                        _ascending = !_ascending;
                        break;
                    case 4:
                        // Cycles to the next sorting algorithm
                        // if algo index is max then goes to 0
                        // it cycles in a circle Bubble > Insertion > Merge > Bubble
                        _fh.cycleAlgo();
                        break;
                    case 0:
                        // Back goes back to Menu
                        return;
                    default:
                        Console.WriteLine("Option does not exists");
                        break;
                }
                
                // Helper function for selection of specific files
                void SelectFiles()
                {
                    // Gets all non sorted files so user doesnt try to sort a sorted file allready
                    // TODO: Check if new options have been chosen for sorting
                    var notSorted = _fh.getNotSorted().ToList();
                    
                    if(notSorted.Count < 1)
                    {
                        Console.WriteLine("No Files Found!");
                        return;
                    }
                    
                
                    while (true)
                    {
                        // Create option menu for all non sorted files
                        var sOpt = 1;
                        var fileOptions = notSorted.Aggregate("[0] Back\n", (current, file) => current + $"[{sOpt++}] {file}\n");
                        Console.Write(fileOptions);
                        try
                        {
                            var choice = Convert.ToInt32(Console.ReadLine());
                        
                            if (choice == 0) return;
                            if (choice > notSorted.Count) return;
                        
                            _fh.SortData(new []{notSorted[choice - 1]}, _ascending);
                            notSorted.RemoveAt(choice - 1);
                        }
                        catch
                        {
                            Console.WriteLine("Invalid input. Try again!");
                            TempStop();
                        }
                    }
                }
                
            }
            catch
            {
                Console.WriteLine("Invalid input. Try again!");
                TempStop();
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