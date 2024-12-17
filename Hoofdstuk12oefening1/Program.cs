using System.Reflection.Metadata.Ecma335;

internal class Program
{
    private static string[] namen = new string[10];
    private static void Main(string[] args)
    {        
        for (int i = 0; i < namen.Length; i++)
        {
            Console.WriteLine($"Naam {i+1}");
            namen[i] = Console.ReadLine();
        }

        Array.Sort(namen);

        foreach (string naam in namen)
        {
            Console.WriteLine(naam);              
        }
        Console.ReadKey();
    }
}