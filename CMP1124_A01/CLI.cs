namespace CMP1124_A01;

// This will be the main class that will handle the user interface and call the other classes

public class CLI
{
    private FileHandler fh;
    public CLI()
    {
        fh = new FileHandler();
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

    private void ReadFiles()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Files found: ");

            var allFiles = fh.AllFIles();
            var files = allFiles.Aggregate("", (current, file) => current + $"{Path.GetFileName(file)}\n");
            Console.WriteLine(files);

            var options = "[1] All Files\n" + "[2] Select Files\n" + "[3] Back\n";
            Console.WriteLine(options);

            try
            {
                var choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        // Read all Files
                        break;
                    case 2:
                        // Provide option to read select files
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

            if (ExitMenuOption()) return;
        }
    }

    private bool ExitMenuOption()
    {
        Console.WriteLine("Do you wish to go back to Menu?");
        Console.WriteLine(  "[1] Yes\n" 
                          + "[0 or Any] No");
        try
        {
            var choice = Convert.ToInt32(Console.ReadLine());
            return choice == 1;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    
    private void MergeData()
    {
        throw new NotImplementedException();
    }
    
    private void SeeData()
    {
        throw new NotImplementedException();
    }
    
    private void Search()
    {
            throw new NotImplementedException();
    }
    
    private void SortData()
    {
        throw new NotImplementedException();
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