using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ai_alapok_feladat
{
    /// <summary>
    /// Absztrakt ososztalya az osszes keresonek.
    /// </summary>
    internal abstract class Kereso
    {
        #region Fields
        /// <summary>
        /// Sajat lista objektum.
        /// </summary>
        protected KulonlegesLista nyiltCsucsok;
        /// <summary>
        /// A zart csucsok listaja.
        /// </summary>
        protected List<Node> zartCsucsok;
        #endregion

        #region Constructor
        protected Kereso()
        {
            nyiltCsucsok = new KulonlegesLista();
            zartCsucsok = new List<Node>();
        }
        #endregion

        #region Abstract methods
        public abstract Node Kereses(Node kezdoNode);
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
            sb.Append("1:1");
            string[] reverse = sb.ToString().Split(";", StringSplitOptions.RemoveEmptyEntries).Reverse().ToArray();
            return string.Join(" ", reverse);
        }
        #endregion
    }
}
