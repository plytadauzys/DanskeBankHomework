using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Transactions;

namespace DanskeBankHomework
{
    class Program
    {
        static void Main(string[] args)
        {
            // Reading from console
            //----------------------
            Console.WriteLine("Instructions.");
            Console.WriteLine("Write a line of numbers with 1 space between them to make an array.");
            Console.WriteLine("Enter an array:");
            string[] arrayChars = Console.ReadLine().Split(' ');
            int[] arr = new int[arrayChars.Length];
            for (int i = 0; i < arrayChars.Length; i++)
            {
                arr[i] = int.Parse(arrayChars[i]);
            }
            //-----------------------------
            // Calculations
            //-----------------------------
            int index = 0;
            List<int> steps = new List<int>();
            while(index < arr.Length-1)
            {
                steps.Add(index);
                index = GetNextStep(index, arr);
                if (index == -2147483648)
                    break;
            }
            Console.WriteLine();
            if (index == arr.Length-1)
            {
                Console.WriteLine("Success!");
                Console.WriteLine("Steps:");
                for(int i = 0; i < steps.Count; i++)
                {
                    Console.WriteLine("Step "+ (i + 1) + ": index=" + steps[i] + " ");
                }
                Console.WriteLine();
            } 
            else if (index == -2147483648)
            {
                Console.WriteLine("Game over. Steps you can make: ");
                for (int i = 0; i < steps.Count; i++)
                {
                    Console.WriteLine("Step " + (i + 1) + ": index=" + steps[i] + " ");
                }
                Console.WriteLine();
            }
        }
        static int GetNextStep(int currentIndex, int[] array)
        {
            if (array[currentIndex] == 0)
            {
                if (IsItTheEnd(currentIndex, array))
                    return array.Length - 1;
                else
                    return -2147483648;
            } 
            else if (array[currentIndex] == 1)
            {
                if (IsItTheEnd(currentIndex, array))
                    return array.Length - 1;
                else if (!IsTheNextValueNegative(currentIndex, array))
                    return 1 + currentIndex;
                else if (IsTheNextValueNegative(currentIndex, array))
                    return -2147483648;
            }
            else if (array[currentIndex] > 1)
            {
                if (IsItTheEnd(currentIndex, array))
                    return array.Length - 1;
                int max = -2147483648;
                int value = array[currentIndex];
                int stepValue = 0;
                while(value > 0)
                {
                    if(array[currentIndex + value] > max)
                    {
                        max = array[currentIndex + value];
                        stepValue = value;
                    }
                    value--;
                }
                if (max == 0)
                {
                    return array[currentIndex] + currentIndex;
                }
                if (max + currentIndex - 1 == 0)
                    return currentIndex + 1;
                return stepValue + currentIndex;
            }
            else if (array[currentIndex] == -1)
            {
                return currentIndex - 1;
            }
            return 0;
        }
        static bool IsItTheEnd(int currentIndex, int[] array)
        {
            int value = array[currentIndex];
            if (currentIndex + value >= array.Length - 1)
                return true;
            return false;
        }
        static bool IsTheNextValueNegative(int currentIndex, int[] array)
        {
            int value = array[currentIndex];
            if (array[currentIndex + 1] < 0)
                return true;
            return false;
        }
    }
}