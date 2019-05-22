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
            public Element rechts;
            public Element links;

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
                Print(elem.rechts, s + "   ");
                Console.WriteLine(s + elem.wert);
                Print(elem.links, s + "   ");
            }
        }


        // hinzugefügt von Madini für AVL + Treap
        protected Element RotateLeft(Element parent)
        {
            Element child = parent.rechts;
            parent.rechts = child.links;
            child.links = parent;
            return child;
        }

        protected Element RotateRight(Element parent)
        {
            Element child = parent.rechts;
            parent.rechts = child.links;
            child.links = parent;
            return child;
        }

        protected Element RotateLeftRight(Element parent)
        {
            Element child = parent.links;
            parent.links = RotateRight(child);
            return RotateLeft(parent);
        }

        protected Element RotateRightLeft(Element parent)
        {
            Element child = parent.rechts;
            parent.rechts = RotateLeft(child);
            return RotateRight(parent);
        }

        // Ende hinzugefügt von Madini
    }


    enum Richtung { rechts, links, keine }


    class BinSearchTree : Tree, ISetSorted
    {
        public BinSearchTree() : base() { }

        public virtual bool Search(int elem)
        {
            Search(elem,out _, out bool gefunden, out _);
            return gefunden;
        }

        public virtual bool Insert(int elem)
        {
            Element neu = new Element(elem);

            Element E = Search(elem, out Element vorgänger, out bool gefunden, out Richtung R);
            if (gefunden)
                return false;
            if (vorgänger == null)
            {
                wurzel = neu;
                return true;
            }
            if (R == Richtung.rechts)
                vorgänger.rechts = neu;
            if (R == Richtung.links)
                vorgänger.links = neu;
            return true;
        }

        public virtual bool Delete(int elem)
        {
            Element E = Search(elem,out Element vorgänger, out bool gefunden, out Richtung R);
            // 1. Fall Es gibt keinen Knoten mit element e
            if (!gefunden)
                return false;

            // 2. fall Knoten mit element ist ein Blatt.

            Element tmp;
            if (R == Richtung.rechts)
                tmp = vorgänger.rechts;
            else if (R == Richtung.links)
                tmp = vorgänger.links;
            else
                tmp = vorgänger;
            if (tmp.rechts == null && tmp.links == null)
            {
                if (R == Richtung.rechts)
                    vorgänger.rechts = null;
                else
                    vorgänger.links = null;
                return true;
            }

            //3. Fall Knoten mit element besitzt einen nachfolger

            if (tmp.rechts == null)
            {
                if (R == Richtung.links)
                    vorgänger.links = vorgänger.links.links;
                else
                    vorgänger.rechts = vorgänger.rechts.links;
                return true;
            }
            if (tmp.links == null)
            {
                if (R == Richtung.links)
                    vorgänger.links = vorgänger.links.rechts;
                else
                    vorgänger.rechts = vorgänger.rechts.rechts;
                return true;
            }

            // 4. Fall Knoten mit e hat 2 Nachfolger

            Element tmp2 = tmp.links;
            while (tmp2.rechts != null)
                tmp2 = tmp2.rechts;
            int symvorg = tmp2.wert;
            Delete(symvorg);
            tmp.wert = symvorg;
            return true;
        }

        protected Element Search(int wert, out Element vorgänger, out bool ElemGefunden, out Richtung R)
        {
            ElemGefunden = false;
            vorgänger = null;
            R = Richtung.keine;

            //kein Baum vorhanden
            if (wurzel==null)
                return null;

            //wurzel ist wert
            if(wurzel.wert == wert)
            {
                ElemGefunden = true;
                return wurzel;
            }

            //alle anderen fälle
            Element tmp = wurzel;
            ElemGefunden = true;
            while (tmp.links != null || tmp.rechts != null)
            {
                if (tmp.links != null && tmp.links.wert == wert)
                {
                    vorgänger = tmp;
                    R = Richtung.links;
                    return tmp.links;
                }
                if (tmp.rechts != null && tmp.rechts.wert == wert)
                {
                    vorgänger = tmp;
                    R = Richtung.rechts;
                    return tmp.rechts;
                }
                if (tmp.wert < wert)
                {
                    if (tmp.rechts != null)
                        tmp = tmp.rechts;
                    else
                    {
                        vorgänger = tmp;
                        ElemGefunden = false;
                        R = Richtung.rechts;
                        return null;
                    }
                }
                if (tmp.wert > wert)
                {
                    if (tmp.links != null)
                        tmp = tmp.links;
                    else
                    {
                        vorgänger = tmp;
                        ElemGefunden = false;
                        R = Richtung.links;
                        return null;
                    }
                }
            }
            vorgänger = tmp;
            ElemGefunden = false;
            if (tmp.wert < wert)
                R = Richtung.rechts;
            if (tmp.wert > wert)
                R = Richtung.links;
            return null;
        }
    }

    // Ab hier Madini

    class AVLTree : BinSearchTree, ISetSorted
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
                int leftSide = GetHeight(node.links);
                int rightSide = GetHeight(node.rechts);

                treeHeight = ((leftSide > rightSide) ? leftSide : rightSide) + 1;
            }

            return treeHeight;
        }

        // Liefert Hilfsgröße BalanceFaktor, zur Einschätzung wie ausgeglichen Baum ist
        // BFaktor=0 : Ausgeglichen, B>1 : links überwiegt, B<1, rechts überwiegt
        int BalanceFactor(Element node)
        {
            int leftSide = GetHeight(node.links);
            int rightSide = GetHeight(node.rechts);
            return leftSide - rightSide;
        }

        // Gleicht den Baum durch Rotation aus
        Element BalanceTree(Element node)
        {
            int bFactor = BalanceFactor(node);

            if (bFactor > 1)
            {
                if (BalanceFactor(node.links) > 0)
                    node = RotateLeft(node);
                else
                    node = RotateLeftRight(node);
            }

            else if (bFactor < -1)
            {
                if (BalanceFactor(node.rechts) > 0)
                    node = RotateRightLeft(node);
                else
                    node = RotateRight(node);
            }

            return node;
        }

        override public bool Search(int key)
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
                return RecursiveSearch(key, node.links);

            else

                return RecursiveSearch(key, node.rechts);              
        }

        public override bool Insert(int elem)
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
                node.links = RecursiveInsert(node.links, newNode);
                node = BalanceTree(node);
            }

            else if (newNode.wert > node.wert)   // rechter Teilbaum
            {
                node.rechts = RecursiveInsert(node.rechts, newNode);
                node = BalanceTree(node);
            }

            return node;
        }

        public override bool Delete(int elem)
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
                node.links = RecursiveDelete(node.links, elem);

                if (BalanceFactor(node) == -2)
                {
                    if (BalanceFactor(node.rechts) <= 0)
                        node = RotateRight(node);
                    else
                        node = RotateRightLeft(node);
                }
            }

            else if (elem > node.wert)  // Suche rechter Teilbaum
            {
                //if (node.rechts == null)
                //    return node; 

                node.rechts = RecursiveDelete(node.rechts, elem);

                if (BalanceFactor(node) == 2)
                {
                    if (BalanceFactor(node.links) >= 0)
                        node = RotateLeft(node);

                    else
                        node = RotateLeftRight(node);

                }
            }

            else  // das gesuchte Element wurde gefunden (node.wert == elem)
            {
                if (node.rechts != null)
                {
                    predecessor = node.rechts;  // eigentlicher Löschvorgang

                    while (predecessor.links != null)
                        predecessor = predecessor.links;


                    node.wert = predecessor.wert;
                    node.rechts = RecursiveDelete(node.rechts, predecessor.wert);

                    if (BalanceFactor(node) == 2)
                    {
                        if (BalanceFactor(node.links) >= 0)
                            node = RotateLeft(node);

                        else
                            node = RotateLeftRight(node);
                    }

                }

                else  // Fall, node.links != null
                {
                    return node.links;
                }
        
            }

            return node;
        }
    }


    class Treap : BinSearchTree, ISetSorted
    {

        static Random randomPriority = new Random();  

        protected class TreapElement : Element
        {
            public int priority;

            public TreapElement(int wert) : base(wert)
            {
                priority = randomPriority.Next(1,11);
            }
        }

        public Treap() { }


        public override bool Search(int elem)
        {
            return base.Search(elem);
        }

        public override bool Insert(int elem)
        {
            return false;
        }

        public override bool Delete(int elem)
        {
            return base.Delete(elem);
        }
    }
}
