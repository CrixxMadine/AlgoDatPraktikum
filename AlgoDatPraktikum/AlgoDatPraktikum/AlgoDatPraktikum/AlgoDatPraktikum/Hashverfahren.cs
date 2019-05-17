using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoDatPraktikum
{
    abstract class Hashverfahren 
    {
        protected const int tableSize = 11;  // m == 3 mod 4 && m == prime number

        protected abstract int HashAlgorithm(int elem);

        protected abstract bool Search(int elem, out int tablePosition);


        public bool search(int elem)
        {
            if (Search(elem, out int tablePosition))
                return true;

            return false;
        }

    }


    class HashTabSepChain : Hashverfahren, ISet
    {
        // every position in the hash-table is a pointer to a list with collided hashcodes

        List<int>[] hashChains = new List<int>[tableSize];


        protected override bool Search(int elem, out int tablePosition)
        {
            tablePosition = HashAlgorithm(elem);

            if (hashChains[tablePosition] == null)
                return false;

            List<int> hashChain = hashChains[tablePosition];

            if (hashChain.Contains(elem))
                return true;

            return false;
        }

        public bool insert(int elem)
        {
            if (Search(elem, out int tablePosition))
            {
                hashChains[tablePosition].Add(elem);
                return true;
            }

            return false;
        }

        public bool delete(int elem)
        {
            if (Search(elem, out int tablePosition))
            {
                hashChains[tablePosition].Remove(elem);
                return true;
            }

            return false;
        }

        public void print()
        {
            for(int i = 0; i < tableSize; i++)
            {
                if (hashChains[i] != null)
                    foreach (int val in hashChains[i])
                        Console.WriteLine($"Elemente an Tabellenplatz {0} : {1}", i, val);
            }
        }


        protected override int HashAlgorithm(int elem) => (((int)(0.618 * elem)) % tableSize);


    }

    class HashTabQuadProb : Hashverfahren, ISet
    {
        // quadratic probing: in case of collision, the interval is increased/decreased by adding the successive outputs of a quadratic polynomial

        static int[] hashTable = new int[tableSize];
        static int quadProbCount = 0;

        public bool insert(int elem)
        {
            if (Search(elem, out int tablePosition))
                return false;

            hashTable[tablePosition] = elem;

            return true;
        }

        public bool delete(int elem)
        {
            if (Search(elem, out int tablePosition))
            {
                hashTable[tablePosition] = 0;
                return true;
            }

            return false;
        }

        public void print()
        {
            for (int i = 0; i < tableSize; i++)
                if (hashTable[i] != 0)
                    Console.WriteLine($"Elemente an Tabellenplatz {0} : {1}", i, hashTable[i]);
        }


        // this does not work so far!!!
        protected override bool Search(int elem, out int tablePosition)
        {
            tablePosition = HashAlgorithm(elem);

            if (hashTable[tablePosition] == 0)
                return false;

            return true;
        }

        protected override int HashAlgorithm(int elem)
        {
            int tablePosition = (elem + quadProbCount * Math.Abs(quadProbCount)) % tableSize;

            if (hashTable[tablePosition] != elem || hashTable[tablePosition] != 0)
            {
                quadProbCount = -quadProbCount;
                if (quadProbCount >= 0)
                    quadProbCount++;

                HashAlgorithm(elem);
            }

            quadProbCount = 0;
            return tablePosition;
        }
    }
}
