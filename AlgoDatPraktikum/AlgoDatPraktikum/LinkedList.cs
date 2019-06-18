using System;

namespace AlgoDatPraktikum
{
    abstract class LinkedList
    {
        protected class Element
        {
            public Element next;
            public int wert;

            public Element(int wert) => this.wert = wert;
        }

        protected Element wurzel;
        public LinkedList() => wurzel = null;

        public bool ListIsEmpty() => wurzel == null; 

        public bool Search(int elem)
        {
            if (wurzel == null)
                return false;
            Element test = Search(elem, wurzel);
            if (test == null || test.wert != elem)
                return false;
            return true;
        }

        //Entweder Fundort oder Vorgänger (wenn null = Anfang der liste)
        protected Element Search(int wert, Element E)
        {
            if (E.next == null || E.wert == wert)
                return E;
            return Search(wert, E.next);
        }


        public bool Delete(int elem)
        {
            if (wurzel == null)
                return false;
            Element E = Search(elem, wurzel);
            if (E == null || E.wert != elem)
                return false;
            if (E == wurzel)
            {
                wurzel = wurzel.next;
            }
            if (E.next == null)
                E = null;
            else
            {
                E.wert = E.next.wert;
                E.next = E.next.next;
            }
            return true;
        }

        public void Print()
        {
            for (Element tmp = wurzel; tmp != null; tmp = tmp.next)
                Console.WriteLine(tmp.wert);
        }
    }

    class MultiSetUnsortedLinkedList : LinkedList, IMultiSet
    {
        public MultiSetUnsortedLinkedList() : base() { }

        public bool Insert(int elem)
        {
            Element neu = new Element(elem);
            if (wurzel == null)
            {
                wurzel = neu;
                return true;
            }
            neu.next = wurzel;
            wurzel = neu;
            return true;
        }
    }

    class SetUnsortedLinkedList : MultiSetUnsortedLinkedList
    {
        public SetUnsortedLinkedList() : base() { }

        new public bool Insert(int elem)
        {
            if (wurzel == null)
            {
                wurzel = new Element(elem);
                return true;
            }
            Element E = Search(elem, wurzel);
            if (E.wert == elem)
                return false;
            base.Insert(elem);
            return true;
        }
    }

    class MultiSetSortedLinkedList : LinkedList, IMultiSetSorted
    {
        public MultiSetSortedLinkedList() : base() { }

        public bool Insert(int elem)
        {
            Element neu = new Element(elem);
            if (wurzel == null)
            {
                wurzel = neu;
                return true;
            }
            if (wurzel.wert > elem)
            {
                neu.next = wurzel;
                wurzel = neu;
                return true;
            }
            Element test = Search(elem, wurzel);
            if(test == null)
            {
                neu.next = wurzel;
                wurzel = neu;
            }
            neu.next = test.next;
            test.next = neu;
            return true;
        }

        new protected Element Search(int wert, Element E)
        {
            if (E.next == null || E.next.wert >= wert)
                return E;
            return Search(wert, E.next);
        }
    }

    class SetSortedLinkedList : MultiSetSortedLinkedList, ISetSorted
    {
        public SetSortedLinkedList() : base() { }

        new public bool Insert(int elem)
        {
            Element neu = new Element(elem);
            if (wurzel == null)
            {
                wurzel = neu;
                return true;
            }
            if (wurzel.wert > elem)
            {
                neu.next = wurzel;
                wurzel = neu;
                return true;
            }
            Element test = Search(elem, wurzel);
            if (test == null)
            {
                neu.next = wurzel;
                wurzel = neu;
                return true;
            }
            if (test.wert == elem)
                return false;
            neu.next = test.next;
            test.next = neu;
            return true;
        }
    }
}
