using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.IsolatedStorage;
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
            return $"{Name}, {BornYear}, {Grade}";
        }
    }
    enum Treetype
    {
        ByGrade,
        ByYear
    }
    class m_BinnaryTree
    {
        public class Node
        {
            public Student Value;
            public Node SorceNode;
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
        public m_BinnaryTree(){ type = Treetype.ByGrade; }
        public m_BinnaryTree(Student student, Treetype type = Treetype.ByGrade)
        {
            root = new Node(student);
        }
        public m_BinnaryTree(m_BinnaryTree tree)
        {
            List<Student> students = tree.ToArray();
            if (tree.type == Treetype.ByGrade)
                type = Treetype.ByYear;
            else if (tree.type == Treetype.ByYear)
                type = Treetype.ByGrade;
            foreach (var item in students)
            {
                this.Add(item);
            }
        }
        public void Add(Student student)
        {
            if (type == Treetype.ByGrade)
            {
                root = Add(root, student, (m) =>
                {
                    return m.Grade > student.Grade;
                });
            }
            if (type == Treetype.ByYear)
            {
                root = Add(root, student, m =>
                {
                    return m.BornYear > student.BornYear;
                });
            }
        }

        private Node Add(Node node, Student student, Predicate<Student> predicate)
        {
            if (node == null)
            {
                node = new Node(student);
                return node;
            }
            if (predicate.Invoke(node.Value))
                node.LeftTree = Add(node.LeftTree, student, predicate);
            else
                node.RightTree = Add(node.RightTree, student, predicate);
            return node;
        }
        
        public List<Student> ToArray()
        {
            List<Student> list = new List<Student>();
            ToArray(list, root);
            return list;
        }
        private void ToArray(List<Student> list, Node node)
        {
            if (node == null)
            {
                return;
            }
            ToArray(list, node.LeftTree);
            list.Add(node.Value);
            ToArray(list, node.RightTree);
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
        
        public void traverseNodes(StringBuilder sb, String padding, String pointer, Node node, bool hasRightSibling)
        {
            if (node != null)
            {
                sb.Append("\n");
                sb.Append(padding);
                sb.Append(pointer);
                sb.Append(node.Value);

                StringBuilder paddingBuilder = new StringBuilder(padding);
                if (hasRightSibling)
                {
                    paddingBuilder.Append("│  ");
                }
                else
                {
                    paddingBuilder.Append("   ");
                }

                String paddingForBoth = paddingBuilder.ToString();
                String pointerRight = "└──";
                String pointerLeft = (node.RightTree != null) ? "├──" : "└──";

                traverseNodes(sb, paddingForBoth, pointerLeft, node.LeftTree, node.RightTree != null);
                traverseNodes(sb, paddingForBoth, pointerRight, node.RightTree, false);
            }
        }
        public override string ToString()
        {

            if (root == null)
            {
                return "";
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(root.Value);

            String pointerRight = "└──";
            String pointerLeft = (root.RightTree != null) ? "├──" : "└──";

            traverseNodes(sb, "", pointerLeft, root.LeftTree, root.RightTree != null);
            traverseNodes(sb, "", pointerRight, root.RightTree, false);

            return sb.ToString();
        }

    }

    internal class Lab6
    {
        static m_BinnaryTree tree = new m_BinnaryTree();
        public static void Main6()
        {
            RandomFill(20);
            Console.WriteLine(tree.ToString());
            Console.WriteLine("###################");
            tree = new m_BinnaryTree(tree);
            Console.WriteLine("Пересипання дерева");
            Console.WriteLine("###################");
            Console.WriteLine(tree.ToString());
        }
        public static void RandomFill(int count)
       {
            Random random = new Random();
            for (int i = count; i > 0; i--)
            {
                tree.Add(new Student($"name{i}", 2000 + random.Next(30), random.Next(100)));
            }
        }
    }
}
