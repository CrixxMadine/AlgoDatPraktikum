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
            int index = Search(elem,0);
            return array[index]==elem;
        }


        protected int Search(int elem,int index)
        {
            if (index == array.Length)
                return -1;
            if (array[index] == elem || array[index] == 0)
                return index;
            else
                return Search(elem, index + 1);
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
            int pos = Search(elem,0);
            if (pos < 0 || array[pos] != elem)
                return false;
            else
            {
                array[pos] = array[--anz];
                array[anz] = 0;
                return true;
            }
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
            int index = Search(elem, 0);
            if (index < 0 || array[index] == elem)
                return false;
            array[index] = elem;
            anz++;
            return true;
        }
    }

    /// <summary>
    /// Mehrere gleiche Elemente erlaubt aber sortiert
    /// </summary>
    class MultiSetSortedArray : Array, IMultiSetSorted
    {
        public MultiSetSortedArray() : base() { }


        new protected int Search(int elem, int index)
        {
            if (array[0] == 0)
                return 0;
            if (anz == array.Length)
                return -1;

            int left = 0;
            int right = anz;
            int middle = 0;

            while (left <= right)
            {
                middle = (left + right) / 2;
                if (array[middle] == elem)
                {
                    return middle;
                }
                if (array[middle] < elem)
                    left = middle + 1;
                if (array[middle] > elem)
                    right = middle - 1;
            }
            if (array[middle] != 0 && array[middle] < elem)
                return middle + 1;
            return middle;
        }

        public bool Insert(int elem)
        {
            if (anz == array.Length)
                return false;
            int pos = Search(elem, 0);
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
            int pos = Search(elem, 0);
            if (array[pos]!=elem)
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
            int pos = Search(elem,0);
            if (array[pos]==elem)
                return false;
            Insert(elem, pos);
            return true;
        }
    }
}
