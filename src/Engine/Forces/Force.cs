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
        virtual public Vector ApplyForce(Particle particle) { return new Vector(); }
    }
}
