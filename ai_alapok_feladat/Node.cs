using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ai_alapok_feladat
{
    internal class Node
    {
        #region Fields
        /// <summary>
        /// A csucshoz tartozo allapot
        /// </summary>
        private LabirintusÁllapot allapot;
        /// <summary>
        /// Ebbol a csucsbol az osszes operator aktivalasa utan letrejovo csucsok.
        /// </summary>
        private List<Node> gyerekCsucsok;
        /// <summary>
        /// Ennek a csucsnak a szuloje, ha ez a kezdo csucs, akkor ez null.
        /// </summary>
        private Node szulo;
        #endregion // Fields

        #region Constructor
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Node()
            : this(default(LabirintusÁllapot))
        { }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="allapot">A kiindulo allapot.</param>
        public Node(LabirintusÁllapot kezdoAllapot)
        {
            allapot = kezdoAllapot;
            szulo = null;
            gyerekCsucsok = new List<Node>();
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="node">Masik csucs alapjan hozzuk letre ezt a csucsot.</param>
        public Node(Node node)
        {
            szulo = node;
            (int, int) koord = node.allapot.Koordinatak;
            allapot = new LabirintusÁllapot(koord);
            gyerekCsucsok = new List<Node>();
        }
        #endregion // Constructor

        #region Public methods
        /// <summary>
        /// Kiterjesztjuk ebbol a csucsbol, az osszes olyan csucsot, amit kapunk, ha az összes operátort alkalmazzuk erre a csúcsra.
        /// Aztan felvesszuk ezeket a csucsokat ennek a csucsnak a gyerekeikent.
        /// </summary>
        /// <returns>Csucsok listaja, amit az operatorok alkalmazasabol kaptunk..</returns>
        public List<Node> Kiterjesztes()
        {
            List<Node> újCsúcsok = new List<Node>();
            for (int i = 0; i < allapot.OperátorokSzáma(); i++)
            {
                Node újCsúcs = new Node(this);
                if (újCsúcs.allapot.SzuperOperátor(i))
                {
                    újCsúcs.allapot.Mozog((Operatorok)i);
                    újCsúcsok.Add(újCsúcs);
                }
            }
            gyerekCsucsok = újCsúcsok;
            return újCsúcsok;
        }
        #endregion

        #region Public properties
        public Node Szulo { get => szulo; }
        /// <summary>
        /// Manhattan tavolsag a celtol, ez a heurisztika.
        /// </summary>
        public double h
        {
            get
            {
                double helper = Math.Abs(allapot.Koordinatak.Item1 - 8);
                double helper2 = Math.Abs(allapot.Koordinatak.Item2 - 8);
                return helper + helper2;
            }
        }
        /// <summary>
        /// Valos utkoltseg eddig a csucsig.
        /// </summary>
        public double g
        {
            get
            {
                double value = 0;
                Node n = new Node();
                n = this;
                while (n.szulo != default(Node))
                {
                    value += n.h;
                    n = n.szulo;
                }
                return value;
            }
        }
        /// <summary>
        /// Becsult utkoltseg a teljes utra.
        /// </summary>
        public double BecsultUtKoltseg
        {
            get => h + g;
        }
        /// <summary>
        /// Jelzi, hogy ez a csucs celcsucs-e.
        /// </summary>
        public bool CelCsucs { get => allapot.CélÁllapotE(); }
        #endregion

        #region Overrides
        public override bool Equals(object? obj)
        {
            bool res = false;
            if (obj is Node)
            {
                res = (obj as Node).allapot.Koordinatak == this.allapot.Koordinatak;
            }
            return res;
        }
        public override string ToString()
        {
            return $"{allapot.Koordinatak.Item1}:{allapot.Koordinatak.Item2}";
        }
        #endregion
    }
}
