using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoDatPraktikum
{
    abstract class Tree
    {

        protected bool elementOperationNotNecessary = false;  // flag for AVL and Treap, intended for special case in Insert- and Delete-function   ---   mfg Madini

        protected class Element
        {
            public int wert;
            public Element right;
            public Element left;

            public Element(int wert) => this.wert = wert;
        }

        public Tree() => wurzel = null;

        protected Element wurzel;

        public void Print()
        {
            Print(wurzel, "");
        }

        void Print(Element elem, string s)
        {
            if (elem != null)
            {
                Print(elem.right, s + "   ");
                Console.WriteLine(s + elem.wert);
                Print(elem.left, s + "   ");
            }
        }


        // hinzugefügt von Madini für AVL
        protected Element RotateLeft(Element parent)
        {
            Element child = parent.left;
            parent.left = child.right;
            child.right = parent;
            return child;
        }

        protected Element RotateRight(Element parent)
        {
            Element child = parent.right;
            parent.right = child.left;
            child.left = parent;
            return child;
        }

        protected Element RotateLeftRight(Element parent)
        {
            Element child = parent.left;
            parent.left = RotateRight(child);
            return RotateLeft(parent);
        }

        protected Element RotateRightLeft(Element parent)
        {
            Element child = parent.right;
            parent.right = RotateLeft(child);
            return RotateRight(parent);
        }

        // Ende hinzugefügt von Madini
    }


    // Ab hier Madini

    class AVLTree : Tree, ISetSorted
    {
        public AVLTree() { }


        // liefert die Höhe des (Teil-)Baums rekursiv als Integer
        //  ----   Bestimmung durch Rekursion ist sehr rechenintensiv, bessere Idee:  Höhe als Variable in der Node speichern
        //  ----   --->  noch zu erledigen, mfg Madini

        int GetHeight(Element node)
        {
            int treeHeight = 0;
            
            if (node != null)
            {
                int leftSide = GetHeight(node.left);
                int rightSide = GetHeight(node.right);

                treeHeight = ((leftSide > rightSide) ? leftSide : rightSide) + 1;
            }

            return treeHeight;
        }

        // Liefert Hilfsgröße BalanceFaktor, zur Einschätzung wie ausgeglichen Baum ist
        // BFaktor=0 : Ausgeglichen, B>1 : links überwiegt, B<1, rechts überwiegt
        int BalanceFactor(Element node)
        {
            int leftSide = GetHeight(node.left);
            int rightSide = GetHeight(node.right);
            return leftSide - rightSide;
        }

        // Gleicht den Baum durch Rotation aus
        Element BalanceTree(Element node)
        {
            int bFactor = BalanceFactor(node);

            if (bFactor > 1)
            {
                if (BalanceFactor(node.left) > 0)
                    node = RotateLeft(node);
                else
                    node = RotateLeftRight(node);
            }

            else if (bFactor < -1)
            {
                if (BalanceFactor(node.right) > 0)
                    node = RotateRightLeft(node);
                else
                    node = RotateRight(node);
            }

            return node;
        }

        public bool Search(int key)
        {
            if (RecursiveSearch(key, wurzel) == null)
                return false;

            return true;
        }

        Element RecursiveSearch(int key, Element node)
        {
            if (node == null)
                return node;

            else if (key == node.wert)
                return node;

            else if (key < node.wert)
                return RecursiveSearch(key, node.left);

            else

                return RecursiveSearch(key, node.right);              
        }

        public bool Insert(int elem)
        {
            Element newNode = new Element(elem);

            if (wurzel == null)
            {
                wurzel = newNode;
                return true;
            }

            Element tempWurzel = wurzel;

            tempWurzel = RecursiveInsert(tempWurzel, newNode);


            if(elementOperationNotNecessary)  // Element ist schon im Baum
                return false;


            wurzel = tempWurzel;
            return true;
        }


        Element RecursiveInsert(Element node, Element newNode)
        {
            if (node == null)  // Blatt an das das Element hin müsste
            {
                node = newNode;
                return node;
            }

            else if (node.wert == newNode.wert)  // Element ist schon in Liste
            {
                elementOperationNotNecessary = true;
                return node;
            }


            else if (newNode.wert < node.wert)   // linker Teilbaum
            {
                node.left = RecursiveInsert(node.left, newNode);
                node = BalanceTree(node);
            }

            else if (newNode.wert > node.wert)   // rechter Teilbaum
            {
                node.right = RecursiveInsert(node.right, newNode);
                node = BalanceTree(node);
            }

            return node;
        }

        public bool Delete(int elem)
        {

            // -----------------  Baum wird unnötog zweimal durchlaufen - hier bessere Lösung finden wie bei Insert
            if (!Search(elem))
                return false;
            // -----------------


            if (wurzel == null)
                return false;

            wurzel = RecursiveDelete(wurzel, elem);

            return true;
        }


        Element RecursiveDelete(Element node, int elem)
        {
            Element predecessor;

            if (node == null)  
            {
                return null;
            }

            if (elem < node.wert)  // Suche linker Teilbaum
            {
                node.left = RecursiveDelete(node.left, elem);

                if (BalanceFactor(node) == -2)
                {
                    if (BalanceFactor(node.right) <= 0)
                        node = RotateRight(node);
                    else
                        node = RotateRightLeft(node);
                }
            }

            else if (elem > node.wert)  // Suche rechter Teilbaum
            {
                //if (node.rechts == null)
                //    return node; 

                node.right = RecursiveDelete(node.right, elem);

                if (BalanceFactor(node) == 2)
                {
                    if (BalanceFactor(node.left) >= 0)
                        node = RotateLeft(node);

                    else
                        node = RotateLeftRight(node);

                }
            }

            else  // das gesuchte Element wurde gefunden (node.wert == elem)
            {
                if (node.right != null)
                {
                    predecessor = node.right;  // eigentlicher Löschvorgang

                    while (predecessor.left != null)
                        predecessor = predecessor.left;


                    node.wert = predecessor.wert;
                    node.right = RecursiveDelete(node.right, predecessor.wert);

                    if (BalanceFactor(node) == 2)
                    {
                        if (BalanceFactor(node.left) >= 0)
                            node = RotateLeft(node);

                        else
                            node = RotateLeftRight(node);
                    }

                }

                else  // Fall, node.links != null
                {
                    return node.left;
                }
        
            }

            return node;
        }
    }
}
