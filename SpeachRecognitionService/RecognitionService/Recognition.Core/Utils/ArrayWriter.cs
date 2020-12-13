using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Recognition.Core.Utils
{
    public class ArrayWriter
    {
        //This will keep numbers format to dotz
        static NumberFormatInfo numberFormat = new CultureInfo("en-US", false).NumberFormat;

        public static void WriteToFile(int[] array, string fileName)
        {
            ToFile(ToStringArray(array), fileName);
        }

        public static void WriteToFile(double[] array, string fileName)
        {
            ToFile(ToStringArray(array), fileName);
        }

        public static void WriteToFile(float[] array, string fileName)
        {
            ToFile(ToStringArray(array), fileName);
        }

        public static void WriteToFile(double[][] array, string fileName)
        {
            ToFile(ToStringArray(array), fileName);
        }

        public static void WriteToFile(string[] array, string fileName)
        {
            ToFile(array, fileName);
        }

        public static void PrintToConsole(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine(array[i]);
            }
        }

        public static void PrintToConsole(double[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine(array[i]);
            }
        }

        public static void PrintToConsole(string[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine(array[i]);
            }
        }

        public static void PrintToConsole(float[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine(array[i]);
            }
        }

        public static void PrintToConsole(double[][] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array[i].Length; j++)
                {
                    Console.WriteLine(array[i][j]);
                }
                Console.WriteLine();
            }
        }

        public static void PrintTabbedToConsole(double[][] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array[i].Length; j++)
                {
                    Console.Write(array[i][j] + "\t");
                }
                Console.WriteLine();
            }

        }

        public static void PrintFrameWiseToConsole(double[][] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array[i].Length; j++)
                {
                    Console.WriteLine(array[j][i]);
                }
                Console.WriteLine();
            }
        }

        public static string[] ToStringArray<T>(T[] array) where T : IConvertible
        {
            if (array == null)
                return new string[0];

            return Array.ConvertAll(array, i => i.ToString(numberFormat));
        }

        public static string[] ToStringArray<T>(T[][] array) where T : IConvertible
        {
            if (array == null)
                return new string[0];

            var result = new string[] { };

            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array[i].Length; j++)
                {
                    result.Add(array[i][j].ToString(numberFormat));
                }
            }

            return result;
        }

        static void ToFile(string[] array, string filename)
        {
            File.AppendAllLines(filename, array);
        }
    }
}
