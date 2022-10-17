using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    internal static class NumberSystemsConverter
    {
        private static string alpfabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static string ToBaseFromDecimal(string src, int to)
        {
            if (src == null)
                return "";
            if (to == 10)
                return src;
            var TwoStrings = src.Split('.', ',');
            string res = "";
            int number = int.Parse(TwoStrings[0]);
            while (number >= to)
            {
                res = res.Insert(0, $"{(number % to).NumberToLetter()}");
                number /= to;
            }
            res = res.Insert(0, $"{number.NumberToLetter()}");
            if (TwoStrings.Length >= 2)
            {
                res += ',';
                double dNumber = double.Parse($"0,{TwoStrings[1]}");
                int count = 0;
                while((dNumber != 0 ||  TwoStrings.Length != 1) && count != 16)
                {
                    dNumber *= to;
                    TwoStrings = dNumber.ToString().Split('.', ',');
                    
                    res += TwoStrings[0].NumberToLetter();
                      
                    dNumber = double.Parse($"0,{TwoStrings[1]}");
                    count++;
                }
            }
            return res;
        }
        public static string toDecimalFromBase(string src, int from)
        {
            if (src == null)
                return "";
            if (from == 10)
                return src;
            var TwoStrings = src.Split('.', ',');
            double res = 0;
            res += CalctoDecimalNumber(TwoStrings[0], from, false);
            if (TwoStrings.Length >= 2)
            {
                res += CalctoDecimalNumber(TwoStrings[1], from, true);
            }
            //Console.WriteLine(res)
            return res.ToString();
        }
        private static double CalctoDecimalNumber(string src, int from, bool isAfterDot)
        {
            double res = 0;
            for (int i = src.Length-1; i >= 0; i--)
            {
                if (isAfterDot)
                {
                    res += src[i].CharToDoubleInPow(from, -i - 1);
                }
                else
                {
                    res += src[i].CharToDoubleInPow(from, -(i - src.Length + 1) );
                }
            }
            return res;
        }
        private static double CharToDoubleInPow(this char ch, int from , int pow)
        {
            int numder = ch.LetterToNumber(); 
            return ((double)numder * Math.Pow(from, pow));
        }
        private static int LetterToNumber(this char ch)
        {
            
            int ret = alpfabet.IndexOf(ch.ToString().ToUpper());
            if (ret == -1)
                return int.Parse(ch.ToString());
            return ret+10;
        }
        private static string NumberToLetter(this int num)
        {
            if (num < 10)
                return num.ToString();
            return alpfabet[num - 10].ToString();
        }
        private static string NumberToLetter(this char num)
        {
            int tmp = int.Parse(num.ToString());
            return tmp.NumberToLetter();
        }
        private static string NumberToLetter(this string num)
        {
            int tmp = int.Parse(num);
            return tmp.NumberToLetter();
        }

        public static string toNumberBase(string src, int from, int to)
        {
            if (src == null)
                return "";
            if (from == to)
                return src;
            return ToBaseFromDecimal(toDecimalFromBase(src, from), to);
        }
    }
}
