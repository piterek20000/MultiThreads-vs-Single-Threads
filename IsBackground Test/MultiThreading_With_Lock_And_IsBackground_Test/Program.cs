using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThreading_With_Lock_And_IsBackground_Test
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Run();

            Console.ReadLine();
            Console.WriteLine("\n\t\t\t Time: " + Time.Seconds);
            Console.ReadLine();
        }
    }
}
