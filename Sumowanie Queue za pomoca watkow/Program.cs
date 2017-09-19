using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zabawa_z__Queue
{
    class Program
    {
        //Utworzenie kolejki naszych losowych liczb
        static Queue<int> Liczby = new Queue<int>();

        //Utworzenie tablicy watkow (ilosc zalezy od procesora)
        static Thread[] threads = new Thread[Environment.ProcessorCount];

        //Tablica sum z poszczegulnych watkow
        static int[] Thread_Suma = new int[Environment.ProcessorCount];

        static int Suma;

        static void Main(string[] args)
        {
            DateTime A = DateTime.Now;

            Add_Random_Nr_To_Queue(ref Liczby);

            //Dopuki nie uplynie 6 s program bedzie dzialal 
            while ((DateTime.Now.Second - A.Second) < 6 )
            {
                if (Liczby.Count == 0)
                {
                    Console.WriteLine("\t Brak elementów w kolejce!");
                    Thread.Sleep(1000);
                }
                else
                {
                    Sum_Random_Numbers();
                }
                

            }

            foreach (var item in threads)
            {
                item.Join();
            }

            Sum_Results();

            Console.WriteLine("\n==========================================\n\n\t\tSuma: " + Suma);

            Console.ReadLine();

        }

        /// <summary>
        /// Metoda sumuje wyniki z poszczegulnych watkow
        /// </summary>
        private static void Sum_Results()
        {
            for (int i = 0; i < Environment.ProcessorCount; i++)
            {
                Suma += Thread_Suma[i];
            }
        }

        /// <summary>
        /// Metoda sumuje losowe liczby za pomoca dostepnych watkow (ilosc watkow zalezy od procesora)
        /// </summary>
        private static void Sum_Random_Numbers()
        {

                for (int i = 0; i < Environment.ProcessorCount; i++)
                {
                    threads[i] = new Thread(Sum_Numbers);
                }

                for (int i = 0; i < threads.Length; i++)
                {
                    threads[i].Start(i);
                }
            
        }

        /// <summary>
        /// Metoda tworzy losowe liczby i dodaje je na poczatek Queue "Liczby"
        /// </summary>
        /// <param name="Liczby">Referencja do Queue</param>
        private static void Add_Random_Nr_To_Queue(ref Queue<int> Liczby)
        {
            Random rand = new Random();

            for (int i = 0; i < 100; i++)
            {
                int losowa = rand.Next(1,50);

                //  !!!   Zabezpieczenie naszej kolejki przed MultiThread !!!
                //  !!!   Samo zabezpieczenie Queue w metodzie obslugujacej Liczby (wywoływanej przez inne watki) nie wystarczy !!!
                //  !!!   Kolejka jest powiazana (kazdy objekt ma kordy nastepnego obiektu) !!!
                lock (Liczby)
                    Liczby.Enqueue(losowa);
            }
        }

        /// <summary>
        /// Metoda wywoływana przez watki
        /// </summary>
        /// <param name="A"></param>
        private static void Sum_Numbers(object A)
        {
            //To sie chyba nazywa flaga + pomocnicza
            int liczba = -1;

            //!!!   Metoda jest wywoływana przez wiele watkow wiec trzeba zabespieczyc przed nadmiernym wczytaniem  !!!
            //!!!   Zabespieczenie kolekcji "Liczby" przed dublowaniem  !!!
            lock (Liczby)
            {
                if (Liczby.Count != 0)
                {
                    //Usuniecie elementu z Queue Liczby 
                    liczba = Liczby.Dequeue();
                }
            }

            if (liczba != -1)
            {
                //Dodanie wartosci do sumy danego watka
                Thread_Suma[(int)A] += liczba;

                Console.WriteLine("\t Thread #{0} just sum {1}.", (int)A+1, liczba);
                Thread.Sleep(250);
            }
                
        }
    }
}
