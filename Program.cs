class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        
        using (broEngine.Engine engine = new broEngine.Engine(800, 600, "BroEngine") )
        {
            engine.Run(60.0);
        }
    }
}