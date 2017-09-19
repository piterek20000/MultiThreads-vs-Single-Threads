using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThread_vs_Single
{
    partial class Program
    {
        #region Datas

        /// <summary>
        /// Inform how may tests will be created
        /// </summary>
        public static int Repeats = 10;

        /// <summary>
        /// Inform how many objects will be created dooring single test
        /// </summary>
        public static long Numbers = 1000000;

        /// <summary>
        /// Object for created random numbers
        /// </summary>
        static Random Rand = new Random();

        #endregion

        /// <summary>
        /// Method Show Main Panel
        /// </summary>
        public static void Main_Panel()
        {
            Console.Clear();

            Console.WriteLine("\n\n\t\t MAIN PANEL");
            Console.WriteLine("\n\n\t 1) Single thread speed test");
            Console.WriteLine("\n\n\t 2) Multithread speed test");
            Console.WriteLine("\n\n\t 3) Single vs Multithread");
            Console.WriteLine("\n\n\t 4) Settings");
            Console.WriteLine("\n\n\t 5) Exit");

            M_P_Switch();
        }

        /// <summary>
        /// Method execution options from Main Panel
        /// </summary>
        private static void M_P_Switch()
        {
            switch (Your_Choice())
            {
                case "1":
                    Single_Thread_Test();
                    break;

                case "2":
                    MultiThread_Test();
                    break;

                case "3":
                    Console.Clear();
                    Show_Settings();
                    Results();
                    Main_Panel();
                    break;

                case "4":
                    Settings();
                    break;

                case "5":
                    Exit_Program_Method();
                    break;

                default:
                    Out_Of_Scope();
                    Main_Panel();
                    break;
            }
        }



        /// <summary>
        /// Method return Your choice
        /// </summary>
        /// <returns>Your choice</returns>
        public static string Your_Choice()
        {
            string choice;
            Console.Write("\n\n\t\t Your choise: ");

            choice = Console.ReadLine();

            return choice.ToUpper();
        }

        /// <summary>
        /// Single Thread Executed Method
        /// </summary>
        private static void Single_Thread_Test()
        {
            Console.Clear();

            Single_Thread_Test_Start();

            Console.ReadLine();

            Main_Panel();

        }

        /// <summary>
        /// Single Threat Test Run
        /// </summary>
        private static void Single_Thread_Test_Start()
        {
            Console.WriteLine("\n\n\t\t SINGLE THREAD TESTS:\n");

            DateTime A = DateTime.Now;

            for (int i = 0; i < Repeats; i++)
            {
                Create_Objects_Method(i + 1);
            }

            DateTime B = DateTime.Now;

            TimeSpan C = B - A;

            Console.WriteLine("\n\n\t\t Full time: {0} s", C.TotalSeconds);
        }

        //TODO: Fix problem with nr test in multithread version tests
        /// <summary>
        /// Method Creating New Objects
        /// </summary>
        /// <param name="i">Test nr</param>
        private static void Create_Objects_Method(int i)
        {
            DateTime A = DateTime.Now;

            for (int j = 0; j < Numbers; j++)
            {
                Data1 AS = new Data1(Rand.Next());
            }

            DateTime B = DateTime.Now;

            TimeSpan C = B - A;

            Console.WriteLine("\tTest nr: {0}.\t Time: {1} ms.", i, C.TotalMilliseconds);
        }

        /// <summary>
        /// MultiThread Executed Method
        /// </summary>
        private static void MultiThread_Test()
        {
            Console.Clear();

            MultiThread_Test_Start();

            Console.ReadLine();

            Main_Panel();
        }

        /// <summary>
        /// MultiThread Test Run
        /// </summary>
        private static void MultiThread_Test_Start()
        {
            Console.WriteLine("\n\n\t\t MULTITHREAD TESTS:\n");

            DateTime A = DateTime.Now;

            Chreate_New_Threads();

            DateTime B = DateTime.Now;

            TimeSpan C = B - A;

            Console.WriteLine("\n\n\t\t Full time: {0} s", C.TotalSeconds);
        }

        /// <summary>
        /// Method Create new threads (depend's on Repeats)
        /// </summary>
        private static void Chreate_New_Threads()
        {
            var Thread_List = new List<Thread>();

            for (int i = 0; i < Repeats; i++)
            {
                Thread_List.Add(new Thread(() => Create_Objects_Method(i + 1)));
            }

            //Activate threads from Thread_List
            foreach (var item in Thread_List)
            {
                item.Start();
            }

            //Wait for each of threads form Thread_List
            foreach (var item in Thread_List)
            {
                item.Join();
            }
        }

        /// <summary>
        /// Method to change value of Repeats & Numbers
        /// </summary>
        private static void Settings()
        {
            Console.Clear();
            Number_Change();
            Repeats_Change();
            Main_Panel();
        }

        /// <summary>
        /// Method change Numbers value
        /// </summary>
        private static void Number_Change()
        {
            Console.Clear();
            Console.WriteLine("\n\t{0:n0}", Numbers);
            Console.WriteLine("\t{0:n0}", Repeats);

            Console.Write("\n\t Numbers of items: ");

            try
            {
                Numbers = long.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                Number_Change();
            }
        }

        /// <summary>
        /// Method change Repeats value
        /// </summary>
        private static void Repeats_Change()
        {
            Console.Write("\n\n\t Loops repeats: ");

            try
            {
                Repeats = Int32.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();

                Repeats_Change();
            }
        }

        /// <summary>
        /// Method force our application to cancel
        /// </summary>
        private static void Exit_Program_Method()
        {
            Console.Clear();
            Console.WriteLine("\n\n\t\t Exit ");
            Thread.Sleep(1000);
            Environment.Exit(0);
        }

        /// <summary>
        /// If choice is out of scope that method tell aboit it
        /// </summary>
        public static void Out_Of_Scope()
        {
            Console.Clear();
            Console.WriteLine("\n\n\t\t !!!Choice out of scope!!!\a");
            Console.ReadLine();
        }

        /// <summary>
        /// Method show settings 
        /// </summary>
        private static void Show_Settings()
        {
            Console.WriteLine("\n\n\t Tests: {0:N0}\n\t Objects: {1:N0}\n",Repeats,Numbers);
        }

        /// <summary>
        /// Method return time of single threat stests
        /// </summary>
        /// <returns>Full time of single thread tests</returns>
        private static TimeSpan Single_Thread_Test_Time()
        {
            Console.WriteLine("\n\n\t\t SINGLE THREAD TESTS:\n");

            DateTime A = DateTime.Now;

            for (int i = 0; i < Repeats; i++)
            {
                Create_Objects_Method(i + 1);
            }

            DateTime B = DateTime.Now;

            TimeSpan C = B - A;

            Console.WriteLine("\n\n\t\t Full time: {0} s", C.TotalSeconds);

            return C;
        }

        /// <summary>
        /// Method return time of multithread tests
        /// </summary>
        /// <returns>Full time of multithread tests</returns>
        private static TimeSpan MultiThread_Test_Time()
        {
            Console.WriteLine("\n\n\t\t MULTITHREAD TESTS:\n");

            DateTime A = DateTime.Now;

            Chreate_New_Threads();

            DateTime B = DateTime.Now;

            TimeSpan C = B - A;

            Console.WriteLine("\n\n\t\t Full time: {0} s", C.TotalSeconds);

            return C;
        }

        /// <summary>
        /// Method confront Single Thread test with MultiThread test
        /// </summary>
        private static void Results()
        {
            TimeSpan A = Single_Thread_Test_Time();
            TimeSpan B = MultiThread_Test_Time();

            if (A.TotalMilliseconds > B.TotalMilliseconds)
            {
                Console.WriteLine("\n\t Single Thread test works faster: {0} ms", (B-A).TotalMilliseconds );
            }
            else
                Console.WriteLine("\n\t MultiThread test works faster: {0} ms", (B - A).TotalMilliseconds);

            Console.ReadLine();
        }
    }
}
