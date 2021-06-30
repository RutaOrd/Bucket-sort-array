using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Disk
{
    class MyFileArray : DataArray
    {

        int[] data;
        public MyFileArray(string filename, int n, int seed)
        {
            data = new int[n];
            length = n;
            Random rand = new Random(seed);
            int min = 10000;
            int max = 10000000;

            for (int i = 0; i < length; i++)
            {
                data[i] = rand.Next(min, max);
            }
            if (File.Exists(filename)) File.Delete(filename);
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(filename,
               FileMode.Create)))
                {
                    for (int j = 0; j < length; j++)
                        writer.Write(data[j]);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public FileStream fs { get; set; }

        public override int this[int index]
        {
            get
            {
                Byte[] data = new Byte[4];
                fs.Seek(4 * index, SeekOrigin.Begin);
                fs.Read(data, 0, 4);
                int result = BitConverter.ToInt32(data, 0);
                return result;
            }
            set
            {
                Byte[] data = new Byte[4];
                BitConverter.GetBytes(value).CopyTo(data, 0);
                fs.Seek(4 * index, SeekOrigin.Begin);
                fs.Write(data, 0, 4);
            }
        }


        public void BucketSort1()
        {

            int minValue = this[0];
            int maxValue = this[0];
          

            for (int i = 1; i < data.Length; i++)
            {
                if (this[i] > maxValue)
                    maxValue = data[i];
                if (this[i] < minValue)
                    minValue = data[i];
            }

            List<int>[] bucket = new List<int>[maxValue - minValue + 1];
            for (int i = 0; i < bucket.Length; i++)
            {
                bucket[i] = new List<int>();
            }

            for (int i = 0; i < data.Length; i++)
            {
                bucket[data[i] - minValue].Add(data[i]);
            }

            int k = 0;
            for (int i = 0; i < bucket.Length; i++)
            {
                if (bucket[i].Count > 0)
                {
                    for (int j = 0; j < bucket[i].Count; j++)
                    {
                        this[k] = bucket[i][j];
                        k++;
                    }
                }
            }
        }

    //--------------------------------------------
        public void BucketSort_time(ref long count)
        {

            int minValue = this[0];
            int maxValue = this[0];
            count += 2;

            for (int i = 1; i < data.Length; i++)
            {
                if (this[i] > maxValue)
                    maxValue = data[i];
                if (this[i] < minValue)
                    minValue = data[i];
                count += 4;
            }

            List<int>[] bucket = new List<int>[maxValue - minValue + 1];
            for (int i = 0; i < bucket.Length; i++)
            {
                bucket[i] = new List<int>();
            }
            count += 2;
            for (int i = 0; i < data.Length; i++)
            {
                bucket[data[i] - minValue].Add(data[i]);
            }
            count += 2;
            int k = 0;
            for (int i = 0; i < bucket.Length; i++)
            {
                if (bucket[i].Count > 0)
                {
                    for (int j = 0; j < bucket[i].Count; j++)
                    {
                        this[k] = bucket[i][j];
                        k++;
                        count += 3;
                    }
                    
                }
                count += 5;
            }
            count += 8;
        }


    }


    }


