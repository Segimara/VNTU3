using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASD.Lab5
{
    internal class StatichashTable<Key, T>
    {
        private List<List<Key>> Keys = new List<List<Key>>(13);
        private List<List<T>> Values = new List<List<T>>(13);

        public StatichashTable()
        {
            for (int i = 0; i < 13; i++)
            {
                Keys.Add(null);
                Values.Add(null);
            }
        }

        public bool Add((Key, T) elem)
        {
            int index = getHash(elem.Item1);
            if (Values[index] == null)
            {
                Values[index] = new List<T>();
            }
            if (Keys[index] == null)
            {
                Keys[index] = new List<Key>();
            }
            if (!(Keys[index].Select(m => m.GetHashCode()).Contains(elem.Item1.GetHashCode())))
            {
                Keys[index].Add(elem.Item1);
                Values[index].Add(elem.Item2);
                return true;    
            }
            return false;
        }
        public bool Remove(Key key)
        {
            int index = getHash(key);
            int secIndex = Keys[index].FindIndex(0, m => m.GetHashCode() == key.GetHashCode());
            if (secIndex == -1)
                return false;
            Values[index].RemoveAt(secIndex);
            Keys[index].RemoveAt(secIndex);
            return true;
        }
        public T this[Key key]
        {
            get
            {
                int secIndex = -1;
                int index = getHash(key);
                if (Keys[index] == null)
                    throw new IndexOutOfRangeException();
                for (int i = 0; i < Keys[index].Count; i++)
                {
                    if (key.GetHashCode() == Keys[index].ElementAt(i).GetHashCode())
                    {
                        secIndex = i;
                    }    
                }   
                if (secIndex == -1)
                    throw new IndexOutOfRangeException();
                return Values[index].ElementAt(secIndex);
            }
        }
        private static int getHash(Key key)
        {
            return key.GetHashCode() % 13;
        }

        public override string? ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("{\n");
            for (int i = 0; i < Keys.Count; i++)
            {
                
                stringBuilder.Append($"{i} - ");
                if (Keys[i] != null)
                {
                    stringBuilder.Append("{");
                    for (int j = 0; j < Keys[i].Count; j++)
                    {
                        stringBuilder.Append(Keys[i][j]);
                        stringBuilder.Append(" | ");
                        stringBuilder.Append(Values[i][j]);
                        if (j != Keys[i].Count - 1)
                            stringBuilder.Append(", ");
                    }
                    stringBuilder.Append("}");
                }
                else
                {
                    stringBuilder.Append("null");
                }
                stringBuilder.Append("\n");
            }
            stringBuilder.Append("}\n");
            return stringBuilder.ToString();
        }
    }

    class Lab5
    {
        static StatichashTable<int, int> table = new StatichashTable<int, int>();
        public static void FillRandom(int count = 8)
        {
            Random r = new Random();

            for (int i = 0; i < count; i++)
            {
                table.Add((i, r.Next(1000)));
            }
        }
        public static void Main5()
        {
            bool work = false;
            while(!work)
            {
                PrintMenu();
                work = Actions(Console.ReadKey().Key);
            }
        }
        public static void PrintMenu()
        {
            Console.WriteLine("Print Table - 1");
            Console.WriteLine("Fill table with random values - 2");
            Console.WriteLine("Add to table - 3");
            Console.WriteLine("Remove from table - 4");
            Console.WriteLine("Get from table - 5");
            Console.WriteLine("Clear table - 6");
            Console.WriteLine("End program - ESC");
        }
        public static bool Actions(ConsoleKey id)
        {
            switch (id)
            {
                case (ConsoleKey.D1) :
                    {
                        Console.WriteLine("");
                        Console.WriteLine(table.ToString());
                        break;
                    }
                case (ConsoleKey.D2):
                    {
                        Console.WriteLine("");
                        try
                        {
                            
                            Console.WriteLine("Enter count of random");
                            FillRandom(int.Parse(Console.ReadLine()));
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("You enter not int number!");
                        }
                        break;
                    }
                case (ConsoleKey.D3):
                    {
                        Console.WriteLine("");
                        try
                        {
                            Console.WriteLine("Enter Key");
                            int key = int.Parse(Console.ReadLine());
                            Console.WriteLine("Enter Value");
                            int value = int.Parse(Console.ReadLine());
                            table.Add((key, value));
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("You enter not int number!");
                        }
                        break;
                    }
                case (ConsoleKey.D4):
                    {
                        Console.WriteLine("");
                        try
                        {
                            Console.WriteLine("Enter Key");
                            int key = int.Parse(Console.ReadLine());
                            table.Remove(key);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("You enter not int number!");
                        }
                        break;
                    }
                case (ConsoleKey.D5):
                    {
                        Console.WriteLine("");
                        try
                        {
                            Console.WriteLine("Enter Key");
                            int key = int.Parse(Console.ReadLine());
                            Console.WriteLine(table[key]);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("You enter not int number!");
                        }
                        break;
                    }
                case (ConsoleKey.D6):
                    {
                        Console.WriteLine("");
                        table = new StatichashTable<int, int>();
                        break;
                    }
                case (ConsoleKey.Escape):
                    {
                        return true;
                    }
                default:
                    {
                        Console.WriteLine("\n Invalid Key");
                        break;
                    }
            }
            return false;
        }
    }
}
