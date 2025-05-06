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
            nyiltCsucsok.Enqueue(kezdoNode, kezdoNode.BecsultUtKoltseg); // Betesszuk a sorba, az adott becsult utkoltseggel.
            while (nyiltCsucsok.Count != 0)
            {
                Node node = nyiltCsucsok.Dequeue(); // Kivesszuk a legkisebb utkoltsegu csucsot, majd kiterjesztjuk.
                List<Node> newNodes = node.Kiterjesztes();
                foreach (Node item in newNodes)
                {
                    if (!zartCsucsok.Contains(item)) // Megnezzuk, hogy a kiterjesztett csucsok valamelyike benne van-e mar a zart csucsokban, 
                    {                                // ha igen, akkor mar nem kell vele foglalkozni, ha nincs akkor nyitott csucskent felvesszuk a sorba
                        nyiltCsucsok.Enqueue(item, item.BecsultUtKoltseg);
                    }
                    if (item.CelCsucs) // Megnezzuk, hogy az adott csucs celcsucs-e.
                    {
                        cel = item;
                    }
                }
                zartCsucsok.Add(node); // A legutobb kiterjesztett csucsot betesszuk a zart csucsokba.
            }
            return cel;
        }
        #endregion

        #region Static methods
        /// <summary>
        /// Utvonal visszafejtese, az adott csucsbol.
        /// </summary>
        /// <param name="cel">Az adott csucs, amibol visszafejtjuk az utvonalat a start csucsig.</param>
        /// <returns>A koordinatak sorban a starttol az adott csucsig.</returns>
        public static string UtKiir(Node cel)
        {
            StringBuilder sb = new StringBuilder();
            while (cel.Szulo != null)
            {
                sb.Append(cel.ToString() + ";");
                cel = cel.Szulo;
            }
            string[] reverse = sb.ToString().Split(";", StringSplitOptions.RemoveEmptyEntries).Reverse().ToArray();
            return string.Join(" ", reverse);
        }
        #endregion
    }
}
