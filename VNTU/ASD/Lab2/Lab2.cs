﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    internal class Lab2
    {
        static Random random = new Random();
        public static void Main3()
        {
            MainQueue();
            MainDeQueue();
            MainStack();
        }
        public static int[] RandomFill(int Count, int from, int to)
        {

            int[] ret = new int[Count];
            for (int i = 0; i < Count; i++)
            {
                ret[i] = random.Next(from, to);
            }
            return ret;
        }
        public static void MainQueue()
        {
            Queue<int> src = new Queue<int>(RandomFill(100, -10, 10));
            int sum = 0;
            for (int i = 0; i < src.Count; i++)
            {
                sum += src.Dequeue();
            }
            Console.WriteLine("Середнє арифметичне чисел з черги: \t\t" + ((double)sum / (double)src.Count).ToString());
        }
        public static void MainDeQueue()
        {
            LinkedList<int> src = new LinkedList<int>(RandomFill(100, -10, 10));
            int sum = 0;
            for (int i = 0; i < src.Count; i++)
            {
                if (random.Next() % 2 == 0)
                {
                    sum += src.Last();
                    src.RemoveLast();
                }
                else
                {
                    sum += src.First();
                    src.RemoveFirst();
                }
            }
            Console.WriteLine("Сумма елементiв подвiйної черги: \t\t" + ((double)sum / (double)src.Count).ToString());
        }
        public static void MainStack()
        {
            Stack<int> src = new Stack<int>(RandomFill(100, -10, 10));
            int sum = 0;
            for (int i = 0; i < src.Count; i++)
            {
                sum += Math.Abs(src.Pop());
            }
            Console.WriteLine("Середнє арифметичне чисел за модулем з стеку:  \t" + ((double)sum / (double)src.Count).ToString());
        }

    }
}
