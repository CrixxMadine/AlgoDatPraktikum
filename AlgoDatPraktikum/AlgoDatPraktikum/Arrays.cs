using System;

namespace AlgoDatPraktikum
{
    /// <summary>
    /// abstrakte Oberklasse Array 
    /// </summary>
    abstract class Array
    {
        public Array()
        {
            array = new int[100];
            anz = 0;
        }

        protected int[] array;
        protected int anz;
        /// <summary>
        /// Print Funktion ist für alle Arrayarten gleich
        /// </summary>
        public void Print()
        {
            foreach (int i in array)
                if (i != 0)
                    Console.WriteLine(i);
        }

        /// <summary>
        /// search die auf indivuellen search mit spezifizierteren rückgabenwerten zurückgreift
        /// </summary>
        /// <param name="elem"></param>
        /// <returns></returns>
        public bool Search(int elem)
        {
            Search(elem, out bool b);
            return b;
        }


        protected int Search(int elem, out bool b)
        {
            b=false;
            int i;
            for (i = 0; i < array.Length; i++)
                if (elem == array[i])
                    return i;
            return i - 1;
        }
    }

    /// <summary>
    /// Mehrfache gleiche Elemente erlaubt und unsortiert
    /// </summary>
    class MultiSetUnsortedArray : Array, IMultiSet
    {
        public MultiSetUnsortedArray() : base() { }

        public bool Insert(int elem)
        {
            if (anz == array.Length)
                return false;
            array[anz] = elem;
            anz++;
            return true;
        }
        public bool Delete(int elem)
        {
            int pos = Search(elem, out bool b);
            if (b)
            {
                array[pos] = array[anz];
                array[anz] = 0;
                anz--;
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Ein gleiches Element erlaubt aber unsortiert
    /// </summary>
    class SetUnsortedArray : MultiSetUnsortedArray, ISet
    {

        public SetUnsortedArray() : base() { }

        public new bool Insert(int elem)
        {
            if (Search(elem))
                return false;
            return base.Insert(elem);
        }
    }

    /// <summary>
    /// Mehrere gleiche Elemente erlaubt aber sortiert
    /// </summary>
    class MultiSetSortedArray : Array, IMultiSetSorted
    {
        public MultiSetSortedArray() : base() { }

        new protected int Search(int elem, out bool b)
        {
            if (array[0] == 0)
            {
                b = false;
                return 0;
            }
            int left = 0;
            int right = anz;
            int middle = 0;

            while (left <= right)
            {
                middle = (left + right) / 2;
                if (array[middle] == elem)
                {
                    b = true;
                    return middle;
                }
                if (array[middle] < elem)
                    left = middle + 1;
                if (array[middle] > elem)
                    right = middle - 1;
            }
            b = false;
            if (array[middle] != 0 && array[middle] < elem)
                return middle + 1;
            return middle;
        }

        public bool Insert(int elem)
        {
            if (anz == array.Length)
                return false;
            int pos = Search(elem, out bool b);
            Insert(elem, pos);
            return true;
        }

        protected void Insert(int elem, int pos)
        {
            for (int i = anz; i >= pos; i--)
                array[i + 1] = array[i];
            array[pos] = elem;
            anz++;
            return;
        }

        public bool Delete(int elem)
        {
            int pos = Search(elem, out bool b);
            if (!b)
            {
                return false;
            }
            for (int i = pos; i < anz; i++)
            {
                array[i] = array[i + 1];
            }
            array[anz] = 0;
            anz--;
            return true;
        }
    }

    /// <summary>
    /// Ein gleiches Element erlaubt und sortiert
    /// </summary>
    class SetSortedArray : MultiSetSortedArray, ISetSorted
    {
        public SetSortedArray() : base() { }

        new public bool Insert(int elem)
        {
            if (anz == array.Length)
                return false;
            int pos = Search(elem, out bool b);
            if (b)
                return false;
            Insert(elem, pos);
            return true;
        }
    }
}
