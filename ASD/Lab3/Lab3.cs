using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            count++;
        }
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
        public List<int> find(T val)
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



















    }
}
