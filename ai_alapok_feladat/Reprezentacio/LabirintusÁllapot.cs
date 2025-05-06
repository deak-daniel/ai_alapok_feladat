using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ai_alapok_feladat
{
    /// <summary>
    /// Enum ami a lehetseges lepeseket reprezentalja.
    /// </summary>
    public enum Operatorok
    {
        Balra = 0,
        Fel = 1,
        Jobbra = 2,
        Le = 3
    }
    internal class LabirintusÁllapot : AbsztraktÁllapot
    {
        public delegate Negyzet OperatorDelegate();

        #region Private fields
        /// <summary>
        /// Lehetseges operatorok, es a hozzajuk tartozo cselekves osszerendelese.
        /// </summary>
        private Dictionary<Operatorok, OperatorDelegate> OperatorOperationDict;
        /// <summary>
        /// Mivel 8x8-as labirintusról van szó.
        /// </summary>
        private static int N = 8;
        /// <summary>
        /// Az aktualis negyzet, ahol az agensunk van
        /// </summary>
        private Negyzet aktualisNegyzet;
        #endregion // Private fields

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public LabirintusÁllapot()
            : this(default(Negyzet))
        { }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="kezdoNegyzet">A kezdo allapot.</param>
        public LabirintusÁllapot(Negyzet kezdoNegyzet)
        {
            aktualisNegyzet = kezdoNegyzet;
            OperatorOperationDict = new Dictionary<Operatorok, OperatorDelegate>()
            {
                {Operatorok.Balra, new OperatorDelegate(Balra) },
                {Operatorok.Fel, new OperatorDelegate(Fel) },
                {Operatorok.Jobbra, new OperatorDelegate(Jobbra) },
                {Operatorok.Le, new OperatorDelegate(Le) },
            };
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="koordinata">Egy koordinata paros, ahol az adott allapot van.</param>
        public LabirintusÁllapot((int,int) koordinata)
        {
            aktualisNegyzet = Program.labirintus[koordinata.Item2 - 1, koordinata.Item1 - 1];
            OperatorOperationDict = new Dictionary<Operatorok, OperatorDelegate>()
            {
                {Operatorok.Balra, new OperatorDelegate(Balra) },
                {Operatorok.Fel, new OperatorDelegate(Fel) },
                {Operatorok.Jobbra, new OperatorDelegate(Jobbra) },
                {Operatorok.Le, new OperatorDelegate(Le) },
            };
        }
        #endregion // Constructor

        #region AbsztraktAllapot implementation
        /// <summary>
        /// Megadja, hogy az aktuális állapot cél állapot-e.
        /// </summary>
        /// <returns>Igaz, ha az állapot célállapot, különben hamis.</returns>
        public override bool CélÁllapotE()
        {
            return aktualisNegyzet.Ertek == Program.CelallapotErtek;
        }
        /// <summary>
        /// Alapból felhasználható operátorok száma.
        /// </summary>
        /// <returns>Visszatér az alapból felhasználható operátorok számával.</returns>
        public override int OperátorokSzáma()
        {
            return OperatorOperationDict.Count;
        }
        /// <summary>
        /// Visszaadja, hogy az operátor elvégezhető-e az aktuális állapotra.
        /// </summary>
        /// <param name="i">Az operátor indexe.</param>
        /// <returns>Az adott operátor alkalmazható-e az adott állapotra.</returns>
        public override bool SzuperOperátor(int i)
        {
            switch (i)
            {
                case 0:
                    if (aktualisNegyzet.Bal == 1) return false;
                    else 
                    {
                        if (PreMozog(Operatorok.Balra))
                        {
                            aktualisNegyzet = OperatorOperationDict[Operatorok.Balra]();
                            return true;
                        }
                        return false;
                    }
                case 1:
                    if (aktualisNegyzet.Felso == 1) return false;
                    else
                    {
                        if (PreMozog(Operatorok.Fel))
                        {
                            aktualisNegyzet = OperatorOperationDict[Operatorok.Fel]();
                            return true;
                        }
                        return false;
                    }
                case 2:
                    if (aktualisNegyzet.Jobb == 1) return false;
                    else
                    {
                        if (PreMozog(Operatorok.Jobbra))
                        {
                            aktualisNegyzet = OperatorOperationDict[Operatorok.Jobbra]();
                            return true;
                        }
                        return false;
                    }
                case 3:
                    if (aktualisNegyzet.Also == 1) return false;
                    else
                    {
                        if (PreMozog(Operatorok.Le))
                        {
                            aktualisNegyzet = OperatorOperationDict[Operatorok.Le]();
                            return true;
                        }
                        return false;
                    }
                default:
                    return false;
            }
        }
        /// <summary>
        /// Megnézi, hogy az aktuális állapot igazából állapot-e.
        /// </summary>
        /// <returns>Igaz, ha valóban állapot, különben hamis.</returns>
        public override bool ÁllapotE()
        {
            if (!(aktualisNegyzet.x > 0 || aktualisNegyzet.x <= N)) return false;
            else if (!(aktualisNegyzet.y > 0 || aktualisNegyzet.y <= N)) return false;
            return true;
        }
        #endregion // AbsztraktAllapot implementation

        #region Overrides
        public override bool Equals(object a)
        {
            if (a == null) return false;
            if (a is LabirintusÁllapot)
            {
                if (aktualisNegyzet.x == (a as LabirintusÁllapot).aktualisNegyzet.x && aktualisNegyzet.y == (a as LabirintusÁllapot).aktualisNegyzet.y)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        public override string ToString()
        {
            return aktualisNegyzet.ToString();
        }
        #endregion

        #region Private methods
        /// <summary>
        /// A balra mozgast reprezentalja az aktualis negyzetbol.
        /// </summary>
        /// <returns>Az aktualis negyzettol balra levo negyzetet adja vissza.</returns>
        private Negyzet Balra() => Program.labirintus[aktualisNegyzet.y - 1, aktualisNegyzet.x - 2];
        /// <summary>
        /// A felfele mozgast reprezentalja az aktualis negyzetbol.
        /// </summary>
        /// <returns>Az aktualis negyzettol felfele levo negyzetet adja vissza.</returns>
        private Negyzet Fel() => Program.labirintus[aktualisNegyzet.y - 2, aktualisNegyzet.x - 1];
        /// <summary>
        /// A jobbra mozgast reprezentalja az aktualis negyzetbol.
        /// </summary>
        /// <returns>Az aktualis negyzettol jobbra levo negyzetet adja vissza.</returns>
        private Negyzet Jobbra() => Program.labirintus[aktualisNegyzet.y - 1, aktualisNegyzet.x];
        /// <summary>
        /// A lefele mozgast reprezentalja az aktualis negyzetbol.
        /// </summary>
        /// <returns>Az aktualis negyzettol lefele levo negyzetet adja vissza.</returns>
        private Negyzet Le() => Program.labirintus[aktualisNegyzet.y, aktualisNegyzet.x - 1];
        /// <summary>
        /// Metodus ami megnezi, hogy az aktualis negyzetbol, az adott iranybol elmozdulas valos allapotot eredmenyez-e.
        /// </summary>
        /// <param name="op">Az operator amivel tesztelunk</param>
        /// <returns>Igaz, ha a lepes utan is valodi allapot lesz, kulonben hamis.</returns>
        private bool PreMozog(Operatorok op)
        {
            try
            {
                OperatorOperationDict[op]();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Az aktualis allapot koordinataja.
        /// </summary>
        public (int, int) Koordinatak => (aktualisNegyzet.x, aktualisNegyzet.y);
        #endregion
    }
}
