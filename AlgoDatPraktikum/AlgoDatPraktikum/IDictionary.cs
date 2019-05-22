using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoDatPraktikum
{
    interface IDictionary
    {
        bool Search(int elem);   // true = gefunden

        bool Insert(int elem);   // true = hinzugefügt

        bool Delete(int elem);   // true = gelöscht

        void Print();            // Einfache Ausgabe der Elemente des Wörterbuchs auf der Console,
                                 // so dass Inhalt und Struktur daraus eindeutig erkennbar
                                 // Für die Ausgabe von Bäumen wird das in der Übung behandelte Verfahren verwendet
    }

    interface IMultiSet : IDictionary { }

    interface ISet : IMultiSet { }

    interface IMultiSetSorted : IDictionary { }

    interface ISetSorted : IMultiSetSorted { }
}
