using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.Intrinsics;
using System.Text;
using System.Threading.Tasks;
using static ASD.Lab6.m_BinnaryTree;

namespace ASD.CreativeTask
{


    internal class HafmanCode
    {
        public static Dictionary<char, string> Encode(string text)
        {
            Dictionary<char, int> freq = new Dictionary<char, int>();
            Dictionary<char, string> codes = new Dictionary<char, string>();
            foreach (char c in text)
            {
                if (freq.ContainsKey(c))
                {
                    freq[c]++;
                }
                else
                {
                    freq[c] = 1;
                }
            }
            List<Node> nodes = new List<Node>();
            foreach (char c in freq.Keys)
            {
                nodes.Add(new Node(c, freq[c]));
            }
            while (nodes.Count > 1)
            {
                nodes.Sort((x, y) => x.Freq.CompareTo(y.Freq));
                Node n1 = nodes[0];
                Node n2 = nodes[1];
                nodes.Remove(n1);
                nodes.Remove(n2);
                Node n = new Node(n1, n2);
                nodes.Add(n);
            }
            Node root = nodes[0];
            root.SetCode("");
            Console.WriteLine("Коди-----");
            foreach (char c in freq.Keys)
            {
                string tmp = root.GetCode(c);
                codes[c] = tmp.Remove(tmp.Length-1);
                Console.WriteLine(c + " | " + codes[c]);
            }
            return codes;
        }
        public static string Decode(string text, Dictionary<char, string> codes)
        {
            string result = "";
            string code = "";
            foreach (char c in text)
            {
                code += c;
                foreach (char d in codes.Keys)
                {
                    if (codes[d] == code)
                    {
                        result += d;
                        code = "";
                    }
                }
            }
            return result;
        }
        public static void Main()
        {
            string text = " Ми стаємо тим, про що думаємо";
            Dictionary<char, string> codes = Encode(text);
            string coded = "";
            foreach (char c in text)
            {
                coded += codes[c];
            }
            string decoded = Decode(coded, codes);
            Console.WriteLine("Закодована стрiчка: \n" + coded);
            Console.WriteLine("Розкодована стрiчка: \n" + decoded);
        }
    }
    class Node : IComparable<Node>
    {
        public char Char;
        public int Freq;
        public Node Left;
        public Node Right;

        public Node(Node Left, Node Right)
        {
            this.Left = Left;
            this.Right = Right;
            this.Freq = Left.Freq + Right.Freq;
        }
        public Node(char Char, int Freq)
        {
            this.Char = Char;
            this.Freq = Freq;
        }
        public int CompareTo(Node other)
        {
            return Freq - other.Freq;
        }
        public string GetCode(char c)
        {
            if (Left == null && Right == null)
            {
                if (Char == c)
                {
                    return "0";
                }
                else
                {
                    return null;
                }
            }
            else
            {
                string code = null;
                if (Left != null)
                {
                    string tmp = Left.GetCode(c);
                    if (tmp != null)
                    {
                        code = "0" + tmp;
                    }
                }
                if (code == null && Right != null)
                {
                    string tmp = Right.GetCode(c);
                    if (tmp != null)
                    {
                        code = "1" + tmp;
                    }
                }
                if (code != null)
                {
                    return code;
                }
                else
                {
                    return null;
                }
            }
        }
        public void SetCode(string code)
        {
            if (Left == null && Right == null)
            {
                Char = Char;
            }
            else
            {
                if (Left != null)
                {
                    Left.SetCode(code + "0");
                }
                if (Right != null)
                {
                    Right.SetCode(code + "1");
                }
            }
        }
    }
}
