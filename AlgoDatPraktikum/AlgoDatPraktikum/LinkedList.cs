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

        public bool Search(int elem)
        {
            return Search(elem, out _);
        }

        protected bool Search(int wert, out Element vorgänger)
        {
            vorgänger = wurzel;
            if (wurzel != null)
            {
                for (; vorgänger.next != null; vorgänger = vorgänger.next)
                {
                    if (vorgänger.next.wert == wert)
                        return true;
                }
                if (wurzel.wert == wert)
                {
                    vorgänger = null;
                    return true;
                }
            }
            return false;
        }


        public bool Delete(int elem)
        {
            bool gefunden = Search(elem, out Element vorgänger);
            if (!gefunden)
                return false;
            if (vorgänger == null)
            {
                wurzel = wurzel.next;
                return true;
            }
            vorgänger.next = vorgänger.next.next;
            return true;
        }

        public void Print()
        {
            for (Element tmp = wurzel; tmp != null; tmp = tmp.next)
                Console.Write($"-{tmp.wert}- ");     // Console.WriteLine temporär verändert  ---   mfg Madini
            Console.WriteLine();
        }
    }

    class MultiSetUnsortedLinkedList : LinkedList, IMultiSet
    {
        public MultiSetUnsortedLinkedList() : base() { }

        public bool Insert(int elem)
        {
            Element neu = new Element(elem);
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
            bool gefunden = Search(elem, out _);
            if (gefunden)                           // Wahrheitswert in Bedingung umgedreht (davor mit !, jetzt ohne)  ---  mfg Madini
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
            bool gefunden = Search(elem, out Element vorgänger);
            Insert(elem, vorgänger);
            return true;
        }

        new protected bool Search(int wert, out Element vorgänger)
        {
            bool gefunden = false;
            if (wurzel == null || wert < wurzel.wert)
            {
                vorgänger = null;
                return false;
            }
            vorgänger = wurzel;
            for (Element tmp = wurzel; tmp != null; tmp = tmp.next)
            {
                if (tmp.wert == wert)
                    gefunden = true;
                if (vorgänger.wert < tmp.wert && tmp.wert < wert)
                    vorgänger = tmp;
            }
            return gefunden;
        }

        protected void Insert(int elem, Element vorgänger)
        {
            Element neu = new Element(elem);
            if (vorgänger == null)
            {
                neu.next = wurzel;
                wurzel = neu;
                return;
            }
            neu.next = vorgänger.next;
            vorgänger.next = neu;
        }
    }

    class SetSortedLinkedList : MultiSetSortedLinkedList, ISetSorted
    {
        public SetSortedLinkedList() : base() { }

        new public bool Insert(int elem)
        {
            bool gefunden = Search(elem, out Element vorgänger);
            if (gefunden)
                return false;
            base.Insert(elem, vorgänger);
            return true;

        }
    }
}
