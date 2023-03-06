using System.Globalization;

namespace SimplexMethod
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите количество условий:");
            SimplexMethod SM = new SimplexMethod(Convert.ToInt32(Console.ReadLine()));
            SM.VivodMatr();
            Console.WriteLine();
            SM.SimplexMeth();
        }
    }
}