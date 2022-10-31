using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ASD.Lab6
{
    class Student
    {
        public String Name;
        public int BornYear;
        public int Grade;
        Student(String name, int bornYear, int grade)
        {

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
                    return m.Grade < student.Grade;
                });
            }
            if (type == Treetype.ByYear)
            {
                Add(student, m =>
                {
                    return m.BornYear < student.BornYear;
                });
            }
        }
        private void Add(Student student, Predicate<Student> predicate)
        {
            Node tmproot = root;
            while (tmproot != null)
            {
                if (predicate.Invoke(tmproot.Value))
                {
                    tmproot = tmproot.LeftTree;
                }
                else
                {
                    tmproot = tmproot.RightTree;
                }
            }
            tmproot = new Node(student);
        }
        private Node Restruct(m_BinnaryTree tree)
        {
            return null;
        }
        public override string ToString()
        {
            return RecursToString(root, new StringBuilder()).ToString();
        }

        private StringBuilder RecursToString(Node node, StringBuilder stringBuilder)
        {
            if (node == null)
            {
                return stringBuilder;
            }
            stringBuilder.Append(node.Value);
            stringBuilder.Append( RecursToString(node.LeftTree, stringBuilder).ToString());
            stringBuilder.Append( RecursToString(node.RightTree, stringBuilder).ToString());
            return stringBuilder;
        }
    }
















    internal class Lab6
    {
    }
}
