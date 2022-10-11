using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ASD.Lab3
{
    class FLinkedList<T> : IEnumerable<T>
    {
        //List<T> list;

        private class Node<T>
        {
            public T value;
            public Node<T> next;
            public Node() { }
            public Node(T value, Node<T> next = null)
            {
                this.value = value;
                this.next = next;
            }

        }
        public struct Enumerator : IEnumerator<T>, IDisposable
        {
            private int index;
            public T Current => throw new NotImplementedException();

            object IEnumerator.Current => throw new NotImplementedException();

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public bool MoveNext()
            {
                throw new NotImplementedException();
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }
        }
        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private Node<T> head;
        private int count = 0;
        public int Count { get { return count; } }
        public FLinkedList()
        {
            head = new Node<T>();
        }
        public FLinkedList(T val)
        {
            head = new Node<T> { value = val };
        }
        public void Add(T val)
        {
            if (head.value == null)
            {
                head.value = val;
                count++;
                return;
            }
            Node<T> tmpHead = head;
            while(tmpHead.next != null)
            {
                tmpHead = tmpHead.next;
            }
            tmpHead.next = new Node<T>(val);
            count++;    
        }
        public void RemoveEnd()
        {
            Node<T> tmpHead = head;
            while (tmpHead.next.next != null)
            {
                tmpHead = tmpHead.next;
            }
            tmpHead.next = tmpHead.next.next;
            count--;
        }
        public FLinkedList<T> Split(Func<T, bool> func)
        {
            FLinkedList<T> ret = new FLinkedList<T>();
            Node<T> tmpHead = head;
            int tmpC = 0;
            while (tmpHead.next != null)
            {
                if (func.Invoke(tmpHead.next.value))
                {
                    break;
                }
                tmpHead = tmpHead.next;
                tmpC++;
            }
            ret.head = tmpHead.next;
            tmpHead.next = null;
            ret.count = this.count - tmpC;
            this.count = tmpC;
            return ret;
        }
        public int CountWithFunc(Func<T, bool> func, Func<T, int> func2)
        {
            Node<T> tmpHead = head;
            int tmpC = 0;
            int ret = 0;
            while (tmpHead.next != null)
            {
                if (func.Invoke(tmpHead.value))
                {
                    ret += func2.Invoke(tmpHead.value);
                }
                tmpHead = tmpHead.next;
                tmpC++;
            }
            return ret;
        }
        public void Print()
        {
            Node<T> walkpointer = head;
            Console.WriteLine("Список:");
            if (walkpointer.value != null)
                Console.WriteLine(walkpointer.value.ToString());
            while (walkpointer.next != null)
            {
                walkpointer = walkpointer.next;
                Console.WriteLine(walkpointer.value.ToString());
            }
        }
    }
    class Country
    {
        public string Name
        {
            get;
            set;    
        }
        public string City
        {
            get;
            set;
        }
        public int Number
        {
            get;
            set;
        }
        static int count = 0;

        public Country(String name, string price, int number)
        {
            this.Name = name;
            this.City = price;
            this.Number = number;
            count++;
        }

        public Country()
        {
            count++;
            this.Name = "Країна" + count;
            this.City = "Мiсто" + count;
            this.Number = count;
        }
        public override string ToString()
        {
            return $"Назва Країни: {Name},Назва Мiста: {City}, Кiлькiсть населення: {Number}";
        }
    }
    class Lab3
    {
        static FLinkedList<Country> list = new FLinkedList<Country>();
        static FLinkedList<Country> list2 = new FLinkedList<Country>();
        private static void Print()
        {
            Console.WriteLine("1. Додавання елементу\n" + 
                "2. Видалення елементу з кiнця\n" + 
                "3. розділення списку( <= k)\n" +
                "4. розділення списку( > k)\n" +
                "5. Пiдрахунок числа мешканцiв Країни\n" + 
                "6. Виведення списку\n" + 
                "0. Вихід з програми");
        }
        
        public static void Main3()
        {
            bool programwork = true;
            while (programwork)
            {
                testAdd();
                Print();
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1: { add(); break; };
                    case 2: { list.RemoveEnd(); break; };
                    case 3: { split1(); break; };
                    case 4: { split2(); break; };
                    case 5: { count(); break; };
                    case 6: { list.Print(); break; };
                    case 0: {programwork = false; break; };
                }
            }
        }
        public static void testAdd()
        {
            list.Add(new Country("1", "1", 100));
            list.Add(new Country("1", "2", 200));
            list.Add(new Country("1", "3", 300));
            list.Add(new Country("2", "4", 400));
            list.Add(new Country("1", "5", 500));
            list.Add(new Country("2", "6", 600));
            list.Add(new Country("1", "7", 700));
            list.Add(new Country("2", "8", 800));
        }
        public static void add()
        {
            Country tmp = new Country();
            Console.WriteLine("Введiть назву країни");
            tmp.Name = Console.ReadLine();
            Console.WriteLine("Введiть назву мiста");
            tmp.City = Console.ReadLine();
            Console.WriteLine("Введiть кiлькiсть населення");
            tmp.Number = int.Parse(Console.ReadLine());
            list.Add(tmp);
        }
        public static void split1()
        {
            Console.WriteLine("Введiть назву країни");
            string TmpCont = Console.ReadLine();
            Console.WriteLine("Введiть кiлькiсть населення");
            int tmpNum = int.Parse(Console.ReadLine());
            list2 = list.Split((d) => d.Name == TmpCont && d.Number >= tmpNum);
            list.Print();
            list2.Print();
        }
        public static void split2()
        {
            Console.WriteLine("Введiть назву країни");
            string TmpCont = Console.ReadLine();
            Console.WriteLine("Введiть кiлькiсть населення");
            int tmpNum = int.Parse(Console.ReadLine());
            list2 = list.Split((d) => d.Name == TmpCont && d.Number < tmpNum);
            list.Print();
            list2.Print();
        }
        public static void count()
        {
            Console.WriteLine("Введiть назву країни");
            //string TmpCont = Console.ReadLine();
            //list2 = list.CountWithFunc(
            //    ((d) => d.City == TmpCont),
            //    (d2) => tmpDel(d2)
            //    );
        }

        public int tmpDel(Country t)
        {
            int ret = t.Number;
            return ret;
        }

    }

    /*
     public int find(T val)
        {
            Node<T> tmpHead = head;
            int count = -1;
            while (tmpHead.next != null)
            {
                if (tmpHead.value == val)
                {
                    return count+1;
                }
                tmpHead = tmpHead.next;
                count++;
            }
            return count;
        }
        public List<int> findAll(T val)
        {
            Node<T> tmpHead = head;
            List<int> list = new List<int>();
            int count = -1;
            while (tmpHead.next != null)
            {
                if (tmpHead.value == val)
                {
                    list.Add(count + 1);
                }
                tmpHead = tmpHead.next;
                count++;
            }
            return list;
        }
     */
}
