using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ASD.Lab6
{
    class Student
    {
        public String Name;
        public int BornYear;
        public int Grade;
        public Student(String name, int bornYear, int grade)
        {
            Name = name;
            BornYear = bornYear;
            Grade = grade;
        }
        public override string ToString()
        {
            return $"|{Name}, {BornYear}, {Grade}|";
        }
    }
    enum Treetype
    {
        ByGrade,
        ByYear
    }
    class m_BinnaryTree
    {
        class Node
        {
            public Student Value;
            public Node LeftTree;
            public Node RightTree;

            public Node(Student value = null, Node leftTree = null, Node rightTree = null)
            {
                Value = value;
                LeftTree = leftTree;
                RightTree = rightTree;
            }
        }

        private Node root;
        private Treetype type;  
        public m_BinnaryTree(){}
        public m_BinnaryTree(Student student, Treetype type = Treetype.ByGrade)
        {
            root = new Node(student);
        }
        public m_BinnaryTree(m_BinnaryTree tree)
        {
            root = Restruct(tree);
        }
        public void Add(Student student)
        {
            if (type == Treetype.ByGrade)
            {
                Add(student, (m) =>
                {
                    return m.Grade > student.Grade;
                });
            }
            if (type == Treetype.ByYear)
            {
                Add(student, m =>
                {
                    return m.BornYear > student.BornYear;
                });
            }
        }

        private void Add(Student student, Predicate<Student> predicate)
        {
            if (root == null)                          // Found a leaf?    
            {
                root = new Node(student);                          // Found it! Add the new node as the new leaf.
                return;
            }
            Node tmp = root;
            while(tmp.LeftTree != null && tmp.RightTree != null)
            {
                if (predicate.Invoke(tmp.Value))
                {
                    tmp = tmp.LeftTree;
                }
                else
                {
                    tmp = tmp.RightTree;
                }
            }
            if (predicate.Invoke(tmp.Value))
            {
                tmp.LeftTree = new Node(student);
            }
            else
            {
                tmp.RightTree = new Node(student);
            }
        }
        
        private Node Restruct(m_BinnaryTree tree)
        {
            return null;
        }

        public void Print()
        {
            Print(root);
        }
        private void Print(Node node)
        {
            if (node == null)
            {
                return;
            }
            Console.WriteLine(node.Value.ToString());
            Print(node.LeftTree);
            Print(node.RightTree);
        }
    }

    internal class Lab6
    {
        static m_BinnaryTree tree = new m_BinnaryTree();
        public static void Main6()
        {
            tree = RandomFill(20);
            tree.Print();
        }
        public static m_BinnaryTree RandomFill(int count)
        {
            Random random = new Random();
            m_BinnaryTree ret = new m_BinnaryTree();
            for (int i = count; i > 0; i--)
            {
                ret.Add(new Student($"name{i}", 2000 + i, random.Next(100)));
            }
            return ret;
        }
    }
}
