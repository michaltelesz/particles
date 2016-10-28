using Particles.Engine.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Particles.Engine.Emitters
{
    public abstract class Emitter : Control
    {
        #region DependencyProperties

        public int MaxParticles
        {
            get { return (int)GetValue(MaxParticlesProperty); }
            set { SetValue(MaxParticlesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxParticles.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxParticlesProperty =
            DependencyProperty.Register("MaxParticles", typeof(int), typeof(Emitter), new PropertyMetadata(0));


        public int MinParticles
        {
            get { return (int)GetValue(MinParticlesProperty); }
            set { SetValue(MinParticlesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MinParticles.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinParticlesProperty =
            DependencyProperty.Register("MinParticles", typeof(int), typeof(Emitter), new PropertyMetadata(0));


        public double MaxHorizontalVelocity
        {
            get { return (double)GetValue(MaxHorizontalVelocityProperty); }
            set { SetValue(MaxHorizontalVelocityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxHorizontalVelocity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxHorizontalVelocityProperty =
            DependencyProperty.Register("MaxHorizontalVelocity", typeof(double), typeof(Emitter), new PropertyMetadata(0d));


        public double MinHorizontalVelocity
        {
            get { return (double)GetValue(MinHorizontalVelocityProperty); }
            set { SetValue(MinHorizontalVelocityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MinHorizontalVelocity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinHorizontalVelocityProperty =
            DependencyProperty.Register("MinHorizontalVelocity", typeof(double), typeof(Emitter), new PropertyMetadata(0d));


        public double MaxVerticalVelocity
        {
            get { return (double)GetValue(MaxVerticalVelocityProperty); }
            set { SetValue(MaxVerticalVelocityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxVerticalVelocity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxVerticalVelocityProperty =
            DependencyProperty.Register("MaxVerticalVelocity", typeof(double), typeof(Emitter), new PropertyMetadata(0d));


        public double MinVerticalVelocity
        {
            get { return (double)GetValue(MinVerticalVelocityProperty); }
            set { SetValue(MinVerticalVelocityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MinVerticalVelocity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinVerticalVelocityProperty =
            DependencyProperty.Register("MinVerticalVelocity", typeof(double), typeof(Emitter), new PropertyMetadata(0d));


        public double MaxLifeSpan
        {
            get { return (double)GetValue(MaxLifeSpanProperty); }
            set { SetValue(MaxLifeSpanProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxLifeSpan.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxLifeSpanProperty =
            DependencyProperty.Register("MaxLifeSpan", typeof(double), typeof(Emitter), new PropertyMetadata(0d));


        public double MinLifeSpan
        {
            get { return (double)GetValue(MinLifeSpanProperty); }
            set { SetValue(MinLifeSpanProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MinLifeSpan.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinLifeSpanProperty =
            DependencyProperty.Register("MinLifeSpan", typeof(double), typeof(Emitter), new PropertyMetadata(0d));

        #endregion

        #region Virtual Methods

        /// <summary>
        /// Generates the particles for an emitter, called once at creation by the Particle System.
        /// </summary>
        /// <param name="system"></param>
        virtual public void GenerateParticles(ParticleSystem system)
        {
            // Init the particles for the emitter
            for (int i = 0; i < MaxParticles; i++)
            {
                Particle mParticle = new Particle();
                UpdateParticle(mParticle);
                this.AddParticle(system, mParticle);
                system.Particles.Add(mParticle);
            }
        }

        /// <summary>
        /// Method which is used to perform additional processing when adding a particle to the system for the
        /// first time.
        /// </summary>
        /// <param name="system"></param>
        /// <param name="particle"></param>
        virtual public void AddParticle(ParticleSystem system, Particle particle) { }

        /// <summary>
        /// Updates the particle by reinitializing its parameters. Used when a particle is first created and 
        /// when a particle is resurrected.
        /// </summary>
        /// <param name="particle"></param>
        virtual public void UpdateParticle(Particle particle)
        {
            //particle.Owner = this;
            //particle.Mass = ParticleSystem.random.NextDouble(MinMass, MaxMass);
            //particle.StartOpacity = this.StartOpacity;
            //particle.EndOpacity = this.EndOpacity;
            //particle.Force = new Vector(0, 0);
            //particle.Velocity = new Vector(
            //    ParticleSystem.random.NextDouble(MinHorizontalVelocity, MaxHorizontalVelocity),
            //    ParticleSystem.random.NextDouble(MinVerticalVelocity, MaxVerticalVelocity));
            //particle.LifeSpan = ParticleSystem.random.NextDouble(MinLifeSpan, MaxLifeSpan);
            //particle.BackgroundColors = this.ColorKeyFrames;
        }

        #endregion
    }
}
