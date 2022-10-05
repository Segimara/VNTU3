using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
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
        public List<int> RawNumber
        {
            get
            {
                List<int> tmp = new List<int>(reg);
                tmp.AddRange(value);
                return tmp;
            }
        }
        //public BinaryNum(string number) : this(number, State.straight) { }
        //"" + ((right >= 0) ? "00" : "11") + "." + Lab1.NumberSystemsConverter.toNumberBase(Math.Abs(right).ToString(), 10, 2)
        public BinaryNum(string number, State state = State.straight)
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
            this.state = state;
            this.CheckAndRollover();
            Simplify();
        }
        public BinaryNum(int number, State state = State.straight) : this("" + ((number >= 0) ? "00" : "11") + "." + Lab1.NumberSystemsConverter.toNumberBase(Math.Abs(number).ToString(), 10, 2), state)
        {

        }
        private void Simplify()
        {
            if (reg.Count > 2)
            {
                SimplifyTherdReg();
            }
        }
        private void SimplifyTherdReg()
        {
            State tmp = AskReg();
            if (tmp == State.conversed)
            {
                SimplifyTherdRegConversed();
            }
            if (tmp == State.complimentary)
            {
                SimplifyTherdRegComplimentary();
            }
        }
        private static State AskReg()
        {
            Console.WriteLine("1 - Обернений, 2 - Доповняльний");
            string tmp = Console.ReadLine();
            return (tmp.Equals("1") ? State.conversed : State.complimentary);
        }
        private void SimplifyTherdRegConversed()
        {
            reg.RemoveAt(0);
            BinaryNum tmp = Increment(this);
            value = tmp.value;
            reg = tmp.reg;
            state = tmp.state;
        }
        private void SimplifyTherdRegComplimentary()
        {
            reg.RemoveAt(0);
        }
        public BinaryNum Convert(State st)
        {
            if (st == State.conversed)
                return this.ToConversed();
            if (st == State.complimentary)
                return this.ToComplimentary_BiDirect();
            return this;
        }
        public BinaryNum ToConversed()
        {
            if (!this.isNegative())
            {
                return this;
            }
            value = value.Invert();
            state = State.conversed;
            return this;
        }
        public BinaryNum ToComplimentary_BiDirect()
        {
            if (!this.isNegative())
            {
                return this;
            }
            if (state == State.conversed)
            {
                BinaryNum tmp = Increment(this);
                reg = tmp.reg;
                value = tmp.value;
                state = State.complimentary;
                return this;
            }
            else
            {
                if (!this.isNegative())
                {
                    return this;
                }
                ToConversed();
                BinaryNum tmp = Increment(this);
                reg = tmp.reg;
                value = tmp.value;
                state = (state == State.complimentary) ? State.straight : State.complimentary;
                return this;
            }
        }

        public bool CheckAndRollover()
        {
            if (CheckToRollover())
            {
                Console.WriteLine("#---Переповнення розрядної сiтки---#");
                Console.WriteLine($"\t{this}");
                value.Insert(0, reg[reg.Count - 1]);
                reg.RemoveAt(reg.Count - 1);
                reg.Insert(reg.Count, reg[reg.Count - 1]);

                Console.WriteLine($"\t{this}");
                return true;
            }
            return false;
        }
        private bool CheckToRollover()
        {
            return reg.Contains(1) && reg.Contains(0);
        }
        public bool isNegative()
        {
            return reg.Contains(1);
        }
        public void AddZerows(int count)
        {
            for (int i = 0; i < count; i++)
            {
                this.value.Insert(0, 0);
            }
        }

        public static BinaryNum operator +(BinaryNum left, BinaryNum right)
        {
            return Add(left, right);
        }
        public static BinaryNum Add(BinaryNum left, BinaryNum right)
        {
            if (left.value.Count != right.value.Count)
            {
                if (left.value.Count > right.value.Count)
                    right.AddZerows(left.value.Count - right.value.Count);
                else
                    left.AddZerows(right.value.Count - left.value.Count);
            }

            if (left.isNegative() || right.isNegative())
            {
                return NegativeAdd(left, right);
            }
            else
            {
                return OnlyPosAdd(left, right);
            }
        }
        public static BinaryNum NegativeAdd(BinaryNum left, BinaryNum right)
        {
            State tmpSt = AskReg();
            BinaryNum ret = new BinaryNum(StringAdd(left.Convert(tmpSt), right.Convert(tmpSt)), tmpSt);
            if (ret.isNegative())
            {
                ret.Convert(tmpSt);
            }
            return ret;
        }
        public static BinaryNum OnlyPosAdd(BinaryNum left, BinaryNum right)
        {
            return new BinaryNum(StringAdd(left, right));
        }
        public static BinaryNum operator++(BinaryNum num)
        {
            return Increment(num); 
        }
        public static BinaryNum Increment(BinaryNum num)
        {
            string tmp = "00.";
            for (int i = 0; i < num.value.Count - 1; i++)
            {
                tmp += "0";
            }
            tmp += "1";
            BinaryNum obj = new BinaryNum(StringAdd(num, tmp.toBinaryNum()));
            return obj;
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
        public override string? ToString()
        {
            return $"{reg.toString()}.{value.toString()}";
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
