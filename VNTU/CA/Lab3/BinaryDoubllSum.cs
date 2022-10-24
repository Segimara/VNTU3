using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Lab2;

namespace Lab3
{
    public class BinumDoblSum
    {
        private BinaryNum mantica;
        private BinaryNum power;

        public static BinumDoblSum Add(BinumDoblSum num1, BinumDoblSum num2)
        {
            throw new Exception();
        }

        public static void MainProg()
        {
            while (Console.ReadKey(true).Key != ConsoleKey.Escape)
            {
                Console.WriteLine("Enter expresion like: {num1} {+|-} {num2}");
                try
                {
                    var SplitedExpresion = Console.ReadLine().Split(" ");
                    int num1 = int.Parse(SplitedExpresion[0]);
                    int num2 = int.Parse(SplitedExpresion[2]);
                    switch (SplitedExpresion[1])
                    {
                        case "+":
                            {
                                Console.WriteLine(Add(new BinumDoblSum(num1), new BinumDoblSum(num2)));
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Unsupported operation");
                                break;
                            }
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid Data");
                }
            }
        }

    }


    public static class ExtentionBinumNum
    {
        public static BinaryNum ShiftByNum(this BinaryNum num, int count)
        {
            BinaryNum res = new BinaryNum(num.ToString());
            for (int i = 0; i < count; i++)
            {
                res.value.Insert(0, 0);
                res.value.Remove(res.value.Count-1);
            }
            return res;
        }
    }
}
