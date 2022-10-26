using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASD.Lab5
{
    internal class StatichashTable<Key, T>
    {
        private List<LinkedList<Key>> Keys = new List<LinkedList<Key>>(13);
        private List<LinkedList<T>> Values = new List<LinkedList<T>>(13);

        public bool Add((Key, T) elem)
        {
            int index = elem.Item1.GetHashCode() % 13;
            if (Values[index] == null)
            {
                Values[index] = new LinkedList<T>();
            }
            if (Keys[index] == null)
            {
                Keys[index] = new LinkedList<Key>();
            }
            if (!(Keys[index].Select(m => m.GetHashCode()).Contains(elem.Item1.GetHashCode())))
            {
                Keys[index].AddLast(elem.Item1);
                Values[index].AddLast(elem.Item2);
                return true;    
            }
            return false;
        }
        //public bool Remove()
        public T this[Key key]
        {
            get
            {
                int secIndex = -1;
                int index = key.GetHashCode() % 13;
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

        public void FillRandom(int count = 8)
        {
            Random r = new Random();

        }

    }
}
