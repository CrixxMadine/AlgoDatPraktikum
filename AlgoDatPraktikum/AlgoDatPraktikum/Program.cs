using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoDatPraktikum
{
    class Program
    {
        static void Main(string[] args)
        {
            IDictionary testAVLTree = new AVLTree();

            testAVLTree.Insert(78);
            testAVLTree.Insert(79);
            testAVLTree.Insert(80);
            testAVLTree.Insert(81);
            testAVLTree.Insert(82);
            testAVLTree.Insert(83);
            testAVLTree.Insert(85);
            testAVLTree.Insert(84);
            testAVLTree.Insert(87);

            

            if (testAVLTree.Search(87))
                Console.WriteLine("Juhu");
            else
                Console.WriteLine("Dohhh");

            testAVLTree.Delete(81);

            if (testAVLTree.Insert(85))
                Console.WriteLine("falsche Ausgabe");
            else
                Console.WriteLine("richtige Ausgabe");


            if (testAVLTree.Delete(79))   /// !!!! Löschen von Element 87 erzeugt Fehler !!!!  
                Console.WriteLine("sehr schön");
            else
                Console.WriteLine("Passt noch nicht");


            testAVLTree.Print();

            Console.WriteLine("\n\n");





            IDictionary testHashSep = new HashTabSepChain();

            testHashSep.Insert(20);
            testHashSep.Insert(21);
            testHashSep.Insert(22);
            testHashSep.Insert(23);
            testHashSep.Insert(20);
            testHashSep.Insert(23);
            testHashSep.Insert(20);
            testHashSep.Insert(299);

            testHashSep.Print();

            Console.WriteLine("\n\n");




            IDictionary testSetUnsortedLinkedList = new SetUnsortedLinkedList();

            testSetUnsortedLinkedList.Insert(12);
            testSetUnsortedLinkedList.Insert(45);
            testSetUnsortedLinkedList.Insert(45);  // !!!! wird doppelt eingefügt -> noch auszubessern !!!!!   --- mfg Madini
            testSetUnsortedLinkedList.Insert(10);
            testSetUnsortedLinkedList.Insert(60);

            testSetUnsortedLinkedList.Delete(12);

            testSetUnsortedLinkedList.Print();

            Console.WriteLine("\n\n");


            IDictionary testHashQuadProb = new HashTabQuadProb();

            testHashQuadProb.Insert(20);          
            testHashQuadProb.Insert(21);
            testHashQuadProb.Insert(22);
            testHashQuadProb.Insert(23);
            testHashQuadProb.Insert(20);
            testHashQuadProb.Insert(23);
            testHashQuadProb.Insert(20);
            testHashQuadProb.Insert(31);
            testHashQuadProb.Insert(299);

            testHashQuadProb.Print();

            Console.WriteLine("\n\n");

        }
    }
}
