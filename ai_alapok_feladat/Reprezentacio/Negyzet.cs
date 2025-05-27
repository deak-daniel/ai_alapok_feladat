using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ai_alapok_feladat
{
    /// <summary>
    /// Representation of a single tile in the labyrinth.
    /// </summary>
    internal class Negyzet : ICloneable
    {
        #region Public properties
        public double Bal { get; private set; }
        public double Felso { get; private set; }
        public double Jobb { get; private set; }
        public double Also { get; private set; }
        public double Ertek { get; set; }
        public int x { get; set; } = -1; // To be set later
        public int y { get; set; } = -1; // To be set later
        #endregion // Public properties

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Negyzet()
        { }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="bal">The represented square's left wall.</param>
        /// <param name="felso">The represented square's upper wall.</param>
        /// <param name="jobb">The represented square's right wall.</param>
        /// <param name="also">The represented square's lower wall.</param>
        /// <param name="ertek">The value associated with the current tile. (Should only be used if the given tile is an EndState, or StartState)</param>
        public Negyzet(double bal, double felso, double jobb, double also, double ertek = 0)
        {
            Ertek = ertek;
            Bal = bal;
            Felso = felso;
            Jobb = jobb;
            Also = also;
        }
        #endregion // Constructors

        #region Override
        public override string ToString()
        {
            return $"{x}:{y}";
        }

        public object Clone()
        {
            return new Negyzet() 
            {
                Bal = this.Bal,
                Felso = this.Felso,
                Jobb = this.Jobb,
                Also = this.Also,
                Ertek = this.Ertek,
                x = this.x,
                y = this.y
            };
        }

        #endregion
    }
}
