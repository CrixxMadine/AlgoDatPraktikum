using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoDatPraktikum
{
    class Program
    {
        public static string CheckInput(string[] input)
        {
            while (true)
            {
                string sexystring = Console.ReadLine().ToUpper();
                if (input.Contains(sexystring))
                    return sexystring;
                Console.WriteLine("Junge lern schreiben roflmao");
            }
        }

        static void Main(string[] args)
        {
            IDictionary Element;

            Console.WriteLine("Abstrakten Datentyp auswählen");
            Console.WriteLine("Array/Hash/LinkedList/Tree");

            string[] DataStructure = { "ARRAY", "HASH", "LINKEDLIST", "TREE" };
            string coolstring = CheckInput(DataStructure);

            if (coolstring == "ARRAY")
            {
                Console.WriteLine("MultiSet oder Set?");
                string nicestring = CheckInput(new string[] { "MULTISET", "SET" });
                Console.WriteLine("Sorted oder Unsorted?");
                string stringtheory = CheckInput(new string[] { "SORTED", "UNSORTED" });
                if ( nicestring == "MULTISET")
                {
                    if (stringtheory == "SORTED")
                        Element = new MultiSetSortedArray();
                    else
                        Element = new MultiSetUnsortedArray();
                }
                else
                {
                    if (stringtheory == "SORTED")
                        Element = new SetSortedArray();
                    else
                        Element = new SetUnsortedArray();
                }
            }
            else if (coolstring == "HASH")
            {
                Console.WriteLine("HTSC oder HTQP?");
                string nicestring = CheckInput(new string[] { "HTSC", "HTQP" });
                if (nicestring == "HTSC")
                    Element = new HashTabSepChain();
                else
                    Element = new HashTabQuadProb();
                
            }
            else if (coolstring == "LINKEDLIST")
            {
                Console.WriteLine("MultiSet oder Set?");
                string nicestring = CheckInput(new string[] { "MULTISET", "SET" });
                Console.WriteLine("Sorted oder Unsorted?");
                string stringtheory = CheckInput(new string[] { "SORTED", "UNSORTED" });
                if (nicestring == "MULTISET")
                {
                    if (stringtheory == "SORTED")
                    {
                        Element = new MultiSetSortedLinkedList();
                    }
                    else
                        Element = new MultiSetUnsortedLinkedList();
                }
                else
                {
                    if (stringtheory == "SORTED")
                        Element = new SetSortedLinkedList();
                    else
                        Element = new SetUnsortedLinkedList();
                }
            }
            else
            {
                Console.WriteLine("AVL oder normal");
                string nicestring = CheckInput(new string[] { "AVL", "NORMAL" });
                if (nicestring == "AVL")
                {
                    Element = new AVLTree();
                }
                else
                    Element = new BinSearchTree();
            }
            while (true)
            {
                Console.WriteLine("Search oder insert oder delete oder print oder cancel");
                string StringyMcString = CheckInput(new string[] { "SEARCH", "INSERT", "DELETE", "PRINT", "CANCEL" });
                if (StringyMcString == "CANCEL")
                    break;
                else if (StringyMcString == "SEARCH")
                {
                    while (true)
                    {
                        Console.WriteLine("Bitte die gesuchte Zahl eingeben oder x für Abbruch");
                        string Input = Console.ReadLine();
                        if (Input == "x")
                            break;
                        else if (checknumber(Input))
                            Element.Search(Convert.ToInt32(Input));
                        else
                            Console.WriteLine("geh sterben maan");
                    }
                }
                else if (StringyMcString == "INSERT")   // !!! Achtung, bei Enter ohne Eingabe gibt es Fehler!!!!!!!! -> noch auszubessern
                {
                    while (true)
                    {
                        Console.WriteLine("Bitte die eizufügende Zahl eingeben oder x für Abbruch");
                        string Input = Console.ReadLine();
                        if (Input == "x")
                            break;
                        else if (checknumber(Input))
                            Element.Insert(Convert.ToInt32(Input));
                        else
                            Console.WriteLine("geh sterben maan");
                    }
                }
                else if (StringyMcString == "DELETE")
                {
                    while (true)
                    {
                        Console.WriteLine("Bitte die zu löschende Zahl eingeben oder x für Abbruch");
                        string Input = Console.ReadLine();
                        if (Input == "x")
                            break;
                        else if (checknumber(Input))
                            Element.Delete(Convert.ToInt32(Input));
                        else
                            Console.WriteLine("geh sterben maan");
                    }
                }
                else
                {
                    Element.Print();
                }
            }
        }
        public static bool checknumber(string checkymcstring)
        {
            if (checkymcstring == "0")
                return false;
            bool b = true;
            foreach (char c in checkymcstring)
                if (c < '0' || c > '9')
                    b = false;
            return b;
        }
    }
}
