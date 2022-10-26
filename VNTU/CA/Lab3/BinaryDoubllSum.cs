using System;
using System.Collections.Generic;
using System.Data;
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
        public BinumDoblSum(string str)
        {
            var strs = str.Split("^");
            mantica = new BinaryNum(strs[0]);
            power = new BinaryNum("00."+strs[1]);
        }
        public BinumDoblSum(BinaryNum mantica, BinaryNum power)
        {
            this.mantica = mantica;
            this.power = power;
        }
    
        public static BinumDoblSum Add(BinumDoblSum num1, BinumDoblSum num2)
        {
            BinaryNum Mant;
            BinaryNum Pow;
            int powOdds = BinaryNum.Minus(num1.power, num2.power).ToNum();
            if (powOdds >= 0)
            {
                Mant = BinaryNum.Add(num1.mantica, num2.mantica.ShiftByNum(Math.Abs(powOdds)));
            }
            else
            {
                Mant = BinaryNum.Add(num1.mantica.ShiftByNum(Math.Abs(powOdds)), num2.mantica);
            }
            Pow = new BinaryNum(Math.Abs(powOdds));
            return new BinumDoblSum(Mant, Pow);
        }
 
        public override string ToString()
        {
            return mantica.ToString() + "^" + power.value.ToRawString();
        }

        public static void MainProg()
        {
            while (Console.ReadKey(true).Key != ConsoleKey.Escape)
            {
                Console.WriteLine("Enter expresion like: {num1} {+} {num2}");
                try
                {
                    var SplitedExpresion = Console.ReadLine().Split(" ");
                    string num1 = SplitedExpresion[0];
                    string num2 = SplitedExpresion[2];
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
