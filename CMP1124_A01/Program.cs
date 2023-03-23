namespace CMP1124_A01;

// This will be the main program class that will initialize CLI.cs

public class Program
{
    public static void Main(string[] args)
    {
        // Initializes and runs CLI
        Cli cli = new Cli();
        cli.Run();
        
        // Manually testing without CLI running
        // Testing t = new Testing();
        
    }
}