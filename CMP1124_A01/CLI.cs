namespace CMP1124_A01;

// This will be the main class that will handle the user interface and call the other classes

public class Cli
{
    private FileHandler _fh;
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

            var options = "[1] All Files\n" + "[2] Select Files\n" + "[3] Back\n";
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
                    case 3:
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

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return;
        }
    }

    private void MergeData()
    {
        throw new NotImplementedException();
    }

    private void SeeData()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Files stored: ");

            var allFiles = _fh.AllLoadedFiles();
            var opt = 1;
            var options = allFiles.Aggregate("", (current, file) => current + $"[{opt++}] {Path.GetFileName(file)}\n");
            options += "[0] Back\n";

            Console.WriteLine(options);

            try
            {
                var choice = Convert.ToInt32(Console.ReadLine());
                if (choice == 0) return;
                if (choice > allFiles.Length)
                {
                    Console.WriteLine("Choice does not exists");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                

                var file = allFiles[choice - 1];
                var data = _fh.SeeData(Path.GetFileName(file));
                var step = (data.Length < 2048) ? 10 : 50;

                var dataString = "";

                for (int i = 0; i < data.Length; i += step)
                {
                    dataString += $"{data[i]}\n";
                }

                Console.WriteLine(dataString);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
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
            throw new NotImplementedException();
    }

    private void SortData()
    {
        while (true)
        {
            var allFiles = _fh.AllLoadedFiles();
            var options = "[1] Sort All Files\n" + "[2] Sort Select Files\n" + "[3] Back\n";
            Console.Clear();
            Console.WriteLine("Algorithms files Options: ");
            Console.WriteLine(options);

            void SelectFiles()
            {
                var _allFiles = allFiles.ToList();
                
                if(_allFiles.Count < 1)
                {
                    Console.WriteLine("No Files Found!");
                    return;
                }
                
                while (true)
                {
                    var sOpt = 1;
                    var fileOptions = _allFiles.Aggregate("[0] Back\n", (current, file) => current + $"[{sOpt++}] {Path.GetFileName(file)}\n");
                    Console.WriteLine(fileOptions);
                    try
                    {
                        var choice = Convert.ToInt32(Console.ReadLine());
                        
                        if (choice == 0) return;
                        if (choice > _allFiles.Count) return;
                        
                        _fh.SortData(new []{_allFiles[choice - 1]});
                        _allFiles.RemoveAt(choice - 1);
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
                        // Sort all Files
                        _fh.SortData(allFiles);
                        break;
                    case 2:
                        // Provide option to sort select files
                        SelectFiles();
                        break;
                    case 3:
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