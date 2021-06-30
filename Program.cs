using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Lab1.Disk
{
    class Program
    {
        //static long count;
        static int seed = (int)DateTime.Now.Ticks & 0x0000FFFF;


        static void Main(string[] args)
        {
            int seed = (int)DateTime.Now.Ticks & 0x0000FFFF;
            Test_File_Array_List(seed);
        }

        public static void Test_File_Array_List(int seed)
        {
            int n = 10;
            string filename = @"mydataarray.dat";
            MyFileArray myfilearray = new MyFileArray(filename, n, seed);
            using (myfilearray.fs = new FileStream(filename, FileMode.Open,
           FileAccess.ReadWrite))
            {
                Console.WriteLine("\n ARRAY DISKINĖJE ATMINTYJE");
                Console.WriteLine("\nPradinis masyvas: \n");
                myfilearray.Print(n);
                myfilearray.BucketSort1();
                Console.WriteLine("Surikiuotas masyvas: \n");
                myfilearray.Print(n);

            }

            //------------------------------------
            string filename4 = @"test66.dat";
            int n3 = 0000;
            Console.WriteLine("Laiko testavimas masyve:");
            Console.WriteLine("Elementų skaičius                   Laikas                 Operacijos");

            for (int i = 0; i < 7; i++)
            {
                MyFileArray myfilearray4 = new MyFileArray(filename4, n3, seed);
                using (myfilearray4.fs = new FileStream(filename4, FileMode.Open,
                   FileAccess.ReadWrite))
                {

                    long count = 0;

                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    myfilearray4.BucketSort_time(ref count);
                    stopwatch.Stop();

                    Console.WriteLine("{0,15} {1,30} {2,20}", n3, stopwatch.Elapsed, count);
                    n3 *= 2;

                }

            }

        }
      

    }

    abstract class DataArray
    {
        protected int length;
        public int Length { get { return length; } }
        public abstract int this[int index] { get; set; }
        public void Print(int n)
        {
            for (int i = 0; i < n; i++)
                Console.Write(" {0} \n", this[i]);
            Console.WriteLine();

        }
     }

}

