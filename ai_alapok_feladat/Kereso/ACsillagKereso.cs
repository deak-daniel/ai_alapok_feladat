using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ai_alapok_feladat
{
    /// <summary>
    /// Az A* kereso algoritmus implementacioja.
    /// </summary>
    internal class ACsillagKereso : Kereso
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ACsillagKereso()
            : base()
        { }
        #endregion

        #region Public methods
        /// <summary>
        /// A keresesi algoritmus.
        /// </summary>
        /// <param name="kezdoNode">Csucs amitol kezdunk.</param>
        /// <returns>A cel csucs.</returns>
        public override Node Kereses(Node kezdoNode) // Elindulunk a kiindulo csucsbol (1,1) koordinata
        {
            Node cel = new Node();
            nyiltCsucsok.Enqueue(kezdoNode); // Betesszuk a sorba, az adott becsult utkoltseggel.
            while (nyiltCsucsok.Count != 0) // Addig megyunk, amig van nyitott csucs.
            {
                Node node = nyiltCsucsok.Dequeue(); // Kivesszuk a legkisebb utkoltsegu csucsot, 
                List<Node> newNodes = node.Kiterjesztes(); // majd kiterjesztjuk.
                foreach (Node item in newNodes)
                {
                    bool canEnqueue = false;
                    if (nyiltCsucsok.Contains(item)) // Korfigyeles, ha benne van a nyitott csucsokban,
                    {
                        Node node1 = nyiltCsucsok.Find(item); // Akkor megkeressuk
                        if (node1.CompareTo(item) == -1) canEnqueue = true; // Es ha jobb a vizsgalt csucsnak az f erteke, akkor betesszuk ujra a nyiltcsucsokba.
                    }
                    if (!zartCsucsok.Contains(item)) // Megnezzuk, hogy a kiterjesztett csucsok valamelyike benne van-e mar a zart csucsokban, 
                    {                                // ha igen, akkor mar nem kell vele foglalkozni, ha nincs akkor nyitott csucskent felvesszuk a sorba
                        canEnqueue = true;
                    }
                    if (item.IsCelCsucs) // Megnezzuk, hogy az adott csucs celcsucs-e.
                    {
                        cel = item;
                        return cel; // Ha megvan a celcsucs, akkor mar keszen is vagyunk a keresessel.
                    }
                    if (canEnqueue) nyiltCsucsok.Enqueue(item);
                }
                zartCsucsok.Add(node); // A legutobb kiterjesztett csucsot betesszuk a zart csucsokba.
            }
            return cel;
        }
        #endregion // Public methods
    }
}
