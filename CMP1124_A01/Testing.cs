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
        
        // loop over all 3 algo types and ascending/descending types
        
        
        // Do this for 256 large arrays
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
        
        
        // Do this for 2048 large arrays
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
        
        // Testing algos with bigger files. *Using merge functionality to merge to 2048 arrays into 1*
        fh.MergeData(new []{data2048[0], data2048[1]});
        var allFiles = fh.AllLoadedFiles();
        var listFiles = allFiles.ToList();
        
        // Remove all regular files will leave only the merged file
        foreach(var df in data256) listFiles.Remove(df);
        foreach(var df in data2048) listFiles.Remove(df);
        
            
        Console.WriteLine("4096 Merged file test\n");
        for (int i = 0; i < fh.AlgoAmmount(); i++)
        {
            foreach (var ascend in new[] { true, false })
            {
                foreach (var data in listFiles)
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