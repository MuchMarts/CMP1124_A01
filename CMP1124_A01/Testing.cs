using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace CMP1124_A01;

public class Testing
{
    public Testing()
    {
        FileHandler fh = new FileHandler();
        // Test for algo speeds
        fh.cycleAlgo(); // resets to algo type 0 aka Bubble sort
        
        // Reads all 6 files into arrays
        fh.ReadFiles(fh.AllFiles());

        var data256 = new List<string>();
        var data2048 = new List<string>();
        
        // Divide files into 2 groups based on size
        foreach (var file in fh.AllFiles())
        {
            if (fh.SeeData(Path.GetFileName(file)).Length < 300) data256.Add(Path.GetFileName(file));
            else data2048.Add(Path.GetFileName(file));
        }
        
        // loop over all 3 algo types
        
        Console.WriteLine("256\n");
        
        for (int i = 0; i < fh.AlgoAmmount(); i++)
        {
            foreach (var ascend in new[] { true, false })
            {
                foreach (var data in data256)
                {
                    var watch = new Stopwatch();
                    watch.Start();
                    fh.SortData(new[] { data }, ascend);
                    watch.Stop();
                    Console.WriteLine(watch.ElapsedTicks / 10000);
                }
            }
            fh.cycleAlgo();
        }
        
        
        Console.WriteLine("2048\n");
        
        for (int i = 0; i < fh.AlgoAmmount(); i++)
        {
            foreach (var ascend in new[] { true, false })
            {
                foreach (var data in data2048)
                {
                    var watch = new Stopwatch();
                    watch.Start();
                    fh.SortData(new[] { data }, ascend);
                    watch.Stop();
                    Console.WriteLine(watch.ElapsedTicks / 1000);
                }
            }
            fh.cycleAlgo();
        }
        
        
    }
}