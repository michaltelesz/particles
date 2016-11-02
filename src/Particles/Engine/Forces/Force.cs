using Particles.Engine.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Particles.Engine.Forces
{
    public abstract class Force : Control
    {
        #region Virtual Methods

        /// <summary>
        /// Used by the Particle System to apply a force vector to a particle.
        /// </summary>
        /// <param name="particle"></param>
        /// <returns></returns>
        virtual public Vector ApplyForce(Particle particle) { return new Vector(); }

        #endregion
    }
}
