using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoDatPraktikum  // Achtung noch Bug !!!!!! Lösche knotem -> right -> left lösche Knoten, bug
{
    class BinSearchTree : ISetSorted
    {
        protected class Node
        {
            public Node left;
            public Node right;
            public int value;

            public Node(int wert)
            {
                this.value = wert;
            }
        }
        protected Node root;

        public BinSearchTree() => root = null;

        public bool Insert(int elem)
        {
            if (root == null)
            {
                root = new Node(elem);
                return true;
            }
            return Insert(root, elem);
        }

        protected bool Insert(Node parent, int elem)
        {
            if(parent.value > elem)
            {
                if (parent.left == null)
                {
                    parent.left = new Node(elem);
                }
                else
                    return Insert(parent.left, elem);
            }
            else if (parent.value < elem)
            {
                if (parent.right == null)
                {
                    parent.right = new Node(elem);
                }
                else
                    return Insert(parent.right, elem);
            }
            else
                return false;
            return true;
        }

        public bool Search(int elem)
        {
            return Search(root, elem);
        }

        protected bool Search(Node parent, int elem)
        {
            if (parent == null)
                return false;
            if (parent.value > elem)
                return Search(parent.left, elem);
            if (parent.value < elem)
                return Search(parent.right, elem);
            return false;
        }

        public bool Delete(int elem)
        {
            if (root == null)
                return false;
            if (root.value == elem)
            {
                if (root.left == null && root.right == null)
                    root = null;
                else if (root.right == null)
                    root = root.left;
                else if (root.left == null)
                    root = root.right;
                else
                {
                    Node temp = root;
                    temp = temp.left;
                    if (temp.right == null)
                    {
                        root.value = temp.value;
                        root.left = temp.left;
                    }
                    else
                    {
                        while (temp.right.right != null)
                            temp = temp.right;
                        root.value = temp.right.value;
                        temp.right = temp.right.left;
                    }
                }
                return true;
            }
            return Delete(root, elem);                
        }

        protected bool Delete(Node parent, int elem)
        {
            if (parent == null)
                return false;

            if (parent.value > elem)
            {
                if(parent.left.value == elem && parent.left.left == null && parent.left.right == null)
                    parent.left = null;
                else
                    return Delete(parent.left, elem);
                return true;
            }
            else if (parent.value < elem)
            {
                if (parent.right.value == elem && parent.right.left == null && parent.right.right == null)
                    parent.right = null;
                else
                    return Delete(parent.right, elem);
                return true;
            }
            else
            {
                if (parent.right == null)
                {
                    parent.value = parent.left.value;
                    parent.left = parent.left.left;
                    parent.right = parent.left.right;
                }
                else if (parent.left == null)
                {
                    parent.value = parent.right.value;
                    parent.left = parent.right.left;
                    parent.right = parent.right.right;
                }
                else
                {
                    Node temp = parent;
                    temp = temp.left;
                    if (temp.right == null)
                    {
                        parent.value = temp.value;
                        parent.left = temp.left;
                    }
                    else
                    {
                        while (temp.right.right != null)
                            temp = temp.right;
                        root.value = temp.right.value;
                        temp.right = temp.right.left;
                    }
                }
                return true;
            }
        }

        public void Print()
        {
            Console.WriteLine("______________________________________________________________________________________________________");
            if (root == null)
                Console.WriteLine("Baum ist leer");
            else
                Print(root, "");
            Console.WriteLine("______________________________________________________________________________________________________");
        }

        protected void Print(Node parent, string Absatz)
        {
            if(parent != null)
            {
                Print(parent.right, Absatz + "           ");
                Console.WriteLine(Absatz + parent.value);
                Print(parent.left, Absatz + "           ");
            }
        }
    }

    //class BinSearchTree : Baum
    //{
    //    public BinSearchTree() : base() { }
    //}    
}
