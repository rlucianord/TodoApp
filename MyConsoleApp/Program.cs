using System;
namespace MyConsoleApp
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            
        string? name;
            Console.WriteLine("Escribe tu Edad:");
            int.TryParse(Console.ReadLine(),out int edad);
             Console.WriteLine("Escribe tu nombre:");
            name=Console.ReadLine();
        
            Console.WriteLine("Hello my name  is: "+name+ ", my age is :"+edad.ToString()+ ", This is my World!");
        }
    }
}