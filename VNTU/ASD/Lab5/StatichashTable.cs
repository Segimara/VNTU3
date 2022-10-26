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
        public void FillRandom(int count = 8)
        {
            Random r = new Random();

        }

    }



    class Lab5
    {
        public static void Main5()
        {
            while(true)
            {
                PrintMenu();
            }
        }
        public static void PrintMenu()
        {
            Console.WriteLine();
        }
    }
}
