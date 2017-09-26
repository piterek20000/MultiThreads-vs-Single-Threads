using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading_With_Lock_And_IsBackground_Test
{
    partial class Program
    {
        //Threats count
        static List<int> Thread_Count = new List<int>();

        static int count = 0;

        //How long our program is running
        static TimeSpan Time;

        /// <summary>
        /// List of our processor threads
        /// </summary>
        static List<Thread> List_Of_Threads = new List<Thread>(); 

        /// <summary>
        /// Method start our test
        /// </summary>
        public static void Run()
        {
            //Onely one program running at the same time (so static)
            Thread A = new Thread(Time_Spand);

            //Force our thread to close with our main thread
            A.IsBackground = true;

            //Thread is background, despite that method stopped working, this thread will still working in background, till program stopped working
            A.Start();

            Create_List_Of_Threads();

            Threads_Start();

            //Main Thread wait for others
            Thread_Join();

            Show_Results();
        }

        /// <summary>
        /// Method show method results
        /// </summary>
        private static void Show_Results()
        {
            Console.WriteLine("\n\n\t\t END\n");
            Console.WriteLine("\t Proces time: {0} s", Time.Seconds);
            Console.WriteLine("\t Count: " + count);

            int k = 1;
            foreach (var item in Thread_Count)
            {
                Console.WriteLine("\t Thread nr: {0}, score: {1}", k, item);
            }
        }

        /// <summary>
        /// Method create new Threads
        /// </summary>
        private static void Create_List_Of_Threads()
        {
            //Create new threads, depending on processor
            for (int i = 0; i < Environment.ProcessorCount - 1; i++)    // 1 thread reserved for our static method (Time_Spand)
            {
                //Create new thread 
                Thread Threads = new Thread(Thread_Method);

                //Add new thread to our list
                List_Of_Threads.Add(Threads);

                //Add new count
                Thread_Count.Add(0);
            }
        }


        /// <summary>
        /// Method update each 0.1s Time
        /// </summary>
        private static void Time_Spand()
        {
            //Starting time
            DateTime A = DateTime.Now;

            while (true)
            {
                Time = DateTime.Now - A;
                Thread.Sleep(100);
                Console.WriteLine("Time ++");
            }
        }

        /// <summary>
        /// Method:
        /// + start new Threads
        /// + force to join
        /// </summary>
        private static void Threads_Start()
        {
            //Start all threads working
            Start_Wotking_Threads();
        }

        private static void Start_Wotking_Threads()
        {
            int x = 0;
            foreach (var item in List_Of_Threads)
            {
                item.Start(x);
                x++;
            }
        }

        /// <summary>
        /// Method force our main Thread to wait for others Threads
        /// </summary>
        private static void Thread_Join()
        {
            foreach (var item in List_Of_Threads)
            {
                    item.Join();
            }
        }

        /// <summary>
        /// Working simulation method
        /// </summary>
        /// <param name="X">Nr of thread</param>
        private static void Thread_Method(object X)
        {
            while (Time.Seconds < 6)
            {
                Console.WriteLine("\t Thread nr: {0}", (int)X+1 + " count++" );

                //Add point to count
                Thread_Count[(int)X] += 1;

                Thread.Sleep(1000);

            }
        }
    }
}
