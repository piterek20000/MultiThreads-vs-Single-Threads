using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadSimulation_
{
    class Program
    {
        //Lista naszych wątków
        static List<Thread> Threads = new List<Thread>();

        //Symulowana baza danych
        static long[] Box = new long[100000000];

        //Ile mamy dostępnych wątkó w naszym komputerze
        static int n = Environment.ProcessorCount;

        //Ile elementów przypada na pojedyńczy wątek w naszym środowisku
        static int items = Box.Length / n;

        /// <summary>
        /// Metoda tworząca nam nowe obiekty w określonych przedziałach naszej symulowanej bazie danych
        /// </summary>
        /// <param name="A">Numer sekcji naszej bazy danych</param>
        static void Create_Items(object A)
        {
            for (int i = items * (int)A; i < items; i++)
            {
                Box[i] = i;
            }
        }

        static void Main(string[] args)
        {
            //Do pomiaru czasu
            Stopwatch A = new Stopwatch();

            MultiThread(A);

            //Reset czasu
            A.Reset();

            //Reset tablicy dla bardziej miarodajnych wyników 
            Reset_Box();

            Single_Thread(A);

            Console.ReadLine();
        }

        /// <summary>
        /// Metoda resetuje wszystkie elementy tablicy (0)
        /// </summary>
        private static void Reset_Box()
        {
            for (int i = 0; i < Box.Length; i++)
            {
                Box[i] = 0;
            }
        }

        /// <summary>
        /// Metoda pokazuje w jakim czasie zostanie zmodyfikowana lista za pomocą pojedynczego wątka
        /// </summary>
        /// <param name="A"></param>
        private static void Single_Thread(Stopwatch A)
        {
            A.Start();

            for (int i = 0; i < Box.Length; i++)
            {
                Box[i] = i;
            }

            A.Stop();

            Console.WriteLine("\n\n\t Całkowity czas pracy z pojedyńczym wątkiem: {0:N0} ms", A.ElapsedMilliseconds);
        }

        /// <summary>
        /// Metoda pokazująca w jakim czasie (ms) zostana wykonane przykładowe liczby dla wielu wątków
        /// </summary>
        /// <param name="A"></param>
        private static void MultiThread(Stopwatch A)
        {
            A.Start();

            //Symulacja wielowątkowa tworzenia nowej listy
            for (int i = 0; i < Environment.ProcessorCount; i++)
            {
                var thread = new Thread(Create_Items);
                Threads.Add(thread);
                thread.Start(i);
            }

            //Poczekanie na wszystkie aktywowane wątki
            foreach (var item in Threads)
            {
                item.Join();
            }
            A.Stop();

            Console.WriteLine("\n\n\t Całkowity czas pracy z wieloma wątkami: {0:N0} ms", A.ElapsedMilliseconds);
        }

    }
}
