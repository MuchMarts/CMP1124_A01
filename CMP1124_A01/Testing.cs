namespace CMP1124_A01;

public class Testing
{
    public Testing()
    {
        FileHandler fh = new FileHandler();
        
        fh.ReadFiles(fh.AllFiles());
        var file = Path.GetFileName(fh.AllFiles()[0]);
        fh.SortData(new []{file});
        var output = fh.Search(file, 104);
        
        Console.WriteLine(file);
        
        foreach (var VARIABLE in output)
        {
            Console.WriteLine(VARIABLE);
        }
    }
}