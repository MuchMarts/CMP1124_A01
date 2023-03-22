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
                    case 1:
                        fh.ReadFiles();
                        break;
                    case 2:
                        fh.MergeData();
                        break;
                    case 3:
                        fh.SeeData();
                        break;
                    case 4:
                        fh.Search();
                        break;
                    case 5:
                        fh.SortData();
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
    
    private void Menu()
    {
        string choices = "Options\n" +
                         "[1] Read Data from Files\n" +
                         "[2] Merge Data\n" +
                         "[3] See Data\n" +
                         "[4] Search\n" +
                         "[5] Sort Data\n";
        Console.WriteLine(choices);
    }
    
}