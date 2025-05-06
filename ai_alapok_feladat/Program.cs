namespace ai_alapok_feladat
{
    internal class Program
    {
                    public static int KezdoallapotErtek = 1;
   /* y tengely*/   public static int CelallapotErtek = 10;
   /* | */          public readonly static Negyzet[,] labirintus = new Negyzet[,]
   /* v */          {
   /*  x -> | 1                                     | 2                   | 3                   | 4                   | 5                   | 6                   | 7                   | 8                     |*/  
   /*1*/    { new Negyzet(1,1,1,0,KezdoallapotErtek), new Negyzet(1,1,0,0), new Negyzet(0,1,0,1), new Negyzet(0,1,1,0), new Negyzet(1,1,0,0), new Negyzet(0,1,0,1), new Negyzet(0,1,1,0), new Negyzet(1,1,1,0)},
   /*2*/    { new Negyzet(1,0,0,0),                   new Negyzet(0,0,1,1), new Negyzet(1,1,1,0), new Negyzet(1,0,0,0), new Negyzet(0,0,1,1), new Negyzet(1,1,0,0), new Negyzet(0,0,1,0), new Negyzet(1,0,1,0)},
   /*3*/    { new Negyzet(1,0,0,1),                   new Negyzet(0,1,1,0), new Negyzet(1,0,1,0), new Negyzet(1,0,1,0), new Negyzet(1,1,0,0), new Negyzet(0,0,1,1), new Negyzet(1,0,0,0), new Negyzet(0,0,1,1)},
   /*4*/    { new Negyzet(1,1,1,0),                   new Negyzet(1,0,0,1), new Negyzet(0,0,1,0), new Negyzet(1,0,1,0), new Negyzet(1,0,0,1), new Negyzet(0,1,1,0), new Negyzet(1,0,0,1), new Negyzet(0,1,1,0)},
   /*5*/    { new Negyzet(1,0,0,0),                   new Negyzet(0,1,1,1), new Negyzet(1,0,1,0), new Negyzet(1,0,0,0), new Negyzet(0,1,1,1), new Negyzet(1,0,0,1), new Negyzet(0,1,0,1), new Negyzet(0,0,1,0)},
   /*6*/    { new Negyzet(1,0,1,0),                   new Negyzet(1,1,0,0), new Negyzet(0,0,1,1), new Negyzet(1,0,0,1), new Negyzet(0,1,0,1), new Negyzet(0,1,0,0), new Negyzet(0,1,1,0), new Negyzet(1,0,1,0)},
   /*7*/    { new Negyzet(1,0,1,0),                   new Negyzet(1,0,0,1), new Negyzet(0,1,1,0), new Negyzet(1,1,0,0), new Negyzet(0,1,1,0), new Negyzet(1,0,1,0), new Negyzet(1,0,1,1), new Negyzet(1,0,1,0)},
   /*8*/    { new Negyzet(1,0,0,1),                   new Negyzet(0,1,0,1), new Negyzet(0,0,0,1), new Negyzet(0,0,1,1), new Negyzet(1,0,1,1), new Negyzet(1,0,0,1), new Negyzet(0,1,0,1), new Negyzet(0,0,1,1, CelallapotErtek)}
        };
        static void Main(string[] args)
        {
            LabirintusInit(); // Labirintus inicializalasa
            Node kezdoallapot = new Node(new LabirintusÁllapot(labirintus[0,0])); // Uj csucs letrehozasa, ami a kezdoallapotot tartalmazza
            ACsillagKereso kereso = new ACsillagKereso(); // A kereso algoritmus
            Node cel = kereso.Kereses(kezdoallapot); // A cel csucs, aminek a szuleibol lesz a helyes ut.
            string path = ACsillagKereso.UtKiir(cel); // Az ut visszafejtese
            List<(int,int)> ut = StringUtvonaltListava(path); // Konvertalas
            KirajzolLabirintus(labirintus, ut); // Kiiras
        }
        /// <summary>
        /// Koordinatak ad a labirintus negyzeteinek.
        /// </summary>
        private static void LabirintusInit()
        {
            for (int i = 0; i < labirintus.GetLength(0); i++) 
            {
                for(int j = 0; j < labirintus.GetLength(1); j++) 
                {
                    labirintus[i, j].x = j+1;
                    labirintus[i, j].y = i+1;
                }
            }
        }
        /// <summary>
        /// A kesz utvonalt koordinata parok listajava alakitja
        /// </summary>
        /// <param name="szoveg">A kesz utvonal.</param>
        /// <returns>A koordinata parok listaja.</returns>
        private static List<(int x, int y)> StringUtvonaltListava(string szoveg)
        {
            return szoveg
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(coord =>
                {
                    string[] resz = coord.Split(':');
                    int x = int.Parse(resz[0]) - 1;
                    int y = int.Parse(resz[1]) - 1;
                    return (x, y);
                })
                .ToList();
        }
        /// <summary>
        /// Labirintus kirajzolasa, ha van ut, akkor azt is berajzolja
        /// </summary>
        /// <param name="labirintus">Az egesz labirintus.</param>
        /// <param name="ut">A kesz ut.</param>
        private static void KirajzolLabirintus(Negyzet[,] labirintus, List<(int x, int y)> ut = null)
        {
            int sorok = labirintus.GetLength(0);
            int oszlopok = labirintus.GetLength(1);

            for (int y = 0; y < sorok; y++)
            {
                for (int x = 0; x < oszlopok; x++)
                {
                    Console.Write(labirintus[y, x].Felso == 1 ? "+---" : "+   ");
                }
                Console.WriteLine("+");
                for (int x = 0; x < oszlopok; x++)
                {
                    string belso = "   ";
                    if (ut != null && ut.Contains((x, y)))
                        belso = " * ";

                    Console.Write(labirintus[y, x].Bal == 1 ? "|" : " ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(belso);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine("|");
            }
            for (int x = 0; x < oszlopok; x++)
            {
                Console.Write("+---");
            }
            Console.WriteLine("+");
        }

    }
}
