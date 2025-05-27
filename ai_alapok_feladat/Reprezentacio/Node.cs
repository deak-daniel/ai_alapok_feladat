using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ai_alapok_feladat
{
    internal class Node : IComparable<Node>
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
        /// Az osszes lehetseges uj allapotot kiterjesztjuk, ugy, hogy az osszes operatort alkalmazzuk erre az allapotra (erre a csucsra),
        /// az igy kapott csucsokat eltaroljuk, es visszaadjuk.
        /// </summary>
        /// <returns>Csucsok listaja, amit az operatorok alkalmazasabol kaptunk.</returns>
        public List<Node> Kiterjesztes()
        {
            List<Node> újCsúcsok = new List<Node>();
            for (int i = 0; i < allapot.OperátorokSzáma(); i++)
            {
                Node újCsúcs = new Node(this);
                if (újCsúcs.allapot.SzuperOperátor(i))
                {
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
        /// Euklideszi tavolsag a celtol, ez a heurisztika.
        /// </summary>
        public double h
        {
            get
            {
                double helper = Math.Pow(allapot.Koordinatak.Item1 - 8, 2);
                double helper2 = Math.Pow(allapot.Koordinatak.Item2 - 8, 2);
                return Math.Sqrt(helper + helper2);
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
        public bool IsCelCsucs { get => allapot.CélÁllapotE(); }
        #endregion

        #region Overrides
        public override bool Equals(object? obj)
        {
            bool res = false;
            if (obj is Node)
            {
                res = this.allapot.Equals((obj as Node).allapot);
            }
            return res;
        }
        public override string ToString()
        {
            return allapot.ToString();
        }
        #endregion

        #region IComparable interface implementation
        public int CompareTo(Node? node)
        {
            if (node == null) throw new ArgumentNullException("Argument cannot be null.");
            if (node.BecsultUtKoltseg == this.BecsultUtKoltseg) return 0;
            else if (node.BecsultUtKoltseg > this.BecsultUtKoltseg) return -1;
            else if (node.BecsultUtKoltseg < this.BecsultUtKoltseg) return 1;
            throw new Exception("Nem sikerult az osszehasonlitas.");
        }
        #endregion
    }
}
