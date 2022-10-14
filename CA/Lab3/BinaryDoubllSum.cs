using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public enum State
    {
        straight,
        conversed,
        complimentary
    }
    public class BinaryNum
    {
        private State state;
        private List<int> reg;
        private List<int> value;
        public BinaryNum(string number, State state = State.straight)
        {
            this.reg = new List<int>();
            this.value = new List<int>();
            var Strings = number.Split('.');
            foreach (char ch in Strings[0])
            {
                reg.Add(int.Parse(ch.ToString()));
            }
            foreach (char ch in Strings[1])
            {
                value.Add(int.Parse(ch.ToString()));
            }
            this.state = state;
        }
        private static string StringAdd(BinaryNum left, BinaryNum right)
        {
            StringBuilder res = new StringBuilder();
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
            res.Insert(0, ".");
            for (int i = left.reg.Count - 1; i >= 0; i--)
            {
                int number = left.reg[i] + right.reg[i] + tmp;
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
        public static string toString(this List<int> src)
        {
            StringBuilder ret = new StringBuilder();
            foreach (var obj in src)
            {
                ret.Append(obj.ToString());
            }
            return ret.ToString();
        }
        public static BinaryNum toBinaryNum(this string src)
        {
            return new BinaryNum(src);
        }
    }
}
