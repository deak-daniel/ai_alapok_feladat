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
    abstract class Kereso
    {
        #region Fields
        /// <summary>
        /// prioritási sor, hogy ne kelljen ujra rendezni minden beszurasnal.
        /// </summary>
        protected PriorityQueue<Node, double> nyiltCsucsok;
        /// <summary>
        /// A zart csucsok listaja.
        /// </summary>
        protected List<Node> zartCsucsok;
        #endregion

        #region Constructor
        protected Kereso()
        {
            nyiltCsucsok = new PriorityQueue<Node, double>();
            zartCsucsok = new List<Node>();
        }
        #endregion

        #region Abstract methods
        public abstract Node Kereses(Node kezdoNode);
        #endregion
    }
}
