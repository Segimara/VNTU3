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
        String Name;
        int BornYear;
        int Grade;
        Student(String name, int bornYear, int grade)
        {

        }
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
        public m_BinnaryTree(){}
        public m_BinnaryTree(Student student)
        {
            root = new Node(student);
        }
        public m_BinnaryTree(m_BinnaryTree tree)
        {
            root = Restruct(tree);
        }
        public string ToStringWide()
        {
            return null;
        }
        public string ToStringMud()
        {
            return null;
        }
        public void Add(Student student)
        {

        }
        private Node Restruct(m_BinnaryTree tree)
        {
            return null;
        }
        public override string ToString()
        {
            return ToStringMud();
        }
    }
















    internal class Lab6
    {
    }
}
