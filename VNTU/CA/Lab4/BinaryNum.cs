
using CA.Lab2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.Lab4
{
    public class BinaryMult
    {
        public List<int> reg;
        public List<int> value;
        public BinaryMult(string number)
        {
            this.reg = new List<int>();
            this.value = new List<int>();
            var TwoStrings = number.Split('.');
            foreach (char ch in TwoStrings[0])
            {
                reg.Add(int.Parse(ch.ToString()));
            }
            foreach (char ch in TwoStrings[1])
            {
                value.Add(int.Parse(ch.ToString()));
            }
        }
        public BinaryMult(int number) : this("" + ((number >= 0) ? "0" : "1") + "." + Lab1.NumberSystemsConverter.toNumberBase(Math.Abs(number).ToString(), 10, 2))
        {

        }
        private static string StringAdd(BinaryMult left, BinaryMult right)
        {
            StringBuilder res = new StringBuilder();
            int r = left.value.Count - right.value.Count;
            if (r < 0)
                left.value.AddZerows(Math.Abs(r), false);
            else
                right.value.AddZerows(Math.Abs(r), false);
            int tmp = 0;
            for (int i = left.value.Count - 1; i >= 0; i--)
            {
                int number = left.value[i] + right.value[i] + tmp;
                if (number == 0 || number == 1)
                {
                    tmp = 0;
                }
                if (number == 2)
                {
                    number = 0;
                    tmp = 1;
                }
                if (number == 3)
                {
                    number = 1;
                    tmp = 1;
                }
                res = res.Insert(0, number.ToString());
            }
            
            if (tmp == 1)
            {
                res = res.Insert(0, tmp);
            }
            return res.ToString();
        }
        public override string? ToString()
        {
            return $"{reg.ToRawString()}.{value.ToRawString()}";
        }
        public static BinaryMult Multiply(BinaryMult left, BinaryMult right)
        {
            string zerows = "";
            for (int i = 0; i < left.value.Count+right.value.Count; i++)
            {
                zerows += "0";
            }
            BinaryMult sum;
            string tmpSingn;
            if (left.reg[0] == right.reg[0])
            {
                sum = new BinaryMult($"0.{zerows}");
                tmpSingn = "0.";
            }
            else
            {
                sum = new BinaryMult($"1.{zerows}");
                tmpSingn = "1.";
            }
                 
            int r = Math.Abs(left.ToNum()) - Math.Abs(right.ToNum());
            if (r <= 0)
            {
                for (int i = left.value.Count - 1; i >= 0; i--)
                {
                    int olsSumCount = sum.value.Count;
                    if (left.value[i] == 1)
                    {
                        sum = new BinaryMult(tmpSingn + StringAdd(sum, right));
                    }
                    if (olsSumCount == sum.value.Count)
                        sum = sum.ShiftByNum(1);
                    else
                        sum.value.RemoveAt(sum.value.Count - 1);
                    Console.WriteLine($"{sum} | {left.value[i]} | {i}");
                }
            }
            else
            {
                for (int i = right.value.Count - 1; i >= 0 ; i--)
                {
                    int olsSumCount = sum.value.Count;
                    if (right.value[i] == 1)
                    {
                        sum = new BinaryMult(tmpSingn + StringAdd(sum, left));
                    }
                    if (olsSumCount == sum.value.Count)
                        sum = sum.ShiftByNum(1);
                    else
                        sum.value.RemoveAt(sum.value.Count-1);
                    Console.WriteLine($"{sum} | {right.value[i]} | {i}");
                }
            }
            return sum;
        }
        public int ToNum()
        {
            int num = int.Parse(Lab1.NumberSystemsConverter.toNumberBase(value.ToRawString(), 2, 10));
            if (reg[0] == 1)
            {
                return -num; 
            }
            return num;
        }
        public static void Main4()
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
                        case "*":
                            {
                                Console.WriteLine(Multiply(new BinaryMult(num1), new BinaryMult(num2)));
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
    public static class HelpExtensions
    {
        public static int Invert(this int num)
        {
            if (num == 1)
            {
                return 0;
            }
            if (num == 0)
            {
                return 1;
            }
            throw new Exception("Not binary");
        }
        public static List<int> Invert(this List<int> num)
        {
            for (int i = 0; i < num.Count; i++)
            {
                num[i] = num[i].Invert();
            }
            return num;
        }
        public static void AddZerows(this List<int> src, int count, bool isStart)
        {
            for (int i = 0; i < count; i++)
            {
                src.Insert((isStart) ? 0 : src.Count, 0);
            }
        }
        public static string ToRawString(this List<int> src)
        {
            StringBuilder ret = new StringBuilder();
            foreach (var obj in src)
            {
                ret.Append(obj.ToString());
            }
            return ret.ToString();
        }
        public static BinaryMult toBinaryNum(this string src)
        {
            return new BinaryMult(src);
        }
    }
    public static class ExtentionBinumNum
    {
        public static BinaryMult ShiftByNum(this BinaryMult num, int count)
        {
            BinaryMult res = new BinaryMult(num.ToString());
            for (int i = 0; i < count; i++)
            {
                res.value.Insert(0, 0);
                res.value.RemoveAt(res.value.Count - 1);
            }
            return res;
        }
    }

}
