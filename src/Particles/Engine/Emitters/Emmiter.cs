using Particles.Engine.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

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


        public double MaxMass
        {
            get { return (double)GetValue(MaxMassProperty); }
            set { SetValue(MaxMassProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxMass.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxMassProperty =
            DependencyProperty.Register("MaxMass", typeof(double), typeof(Emitter), new PropertyMetadata(0d));


        public double MinMass
        {
            get { return (double)GetValue(MinMassProperty); }
            set { SetValue(MinMassProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MinMass.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinMassProperty =
            DependencyProperty.Register("MinMass", typeof(double), typeof(Emitter), new PropertyMetadata(0d));


        public double MaxParticleWidth
        {
            get { return (double)GetValue(MaxParticleWidthProperty); }
            set { SetValue(MaxParticleWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxParticleWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxParticleWidthProperty =
            DependencyProperty.Register("MaxParticleWidth", typeof(double), typeof(Emitter), new PropertyMetadata(0d));


        public double MinParticleWidth
        {
            get { return (double)GetValue(MinParticleWidthProperty); }
            set { SetValue(MinParticleWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MinParticleWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinParticleWidthProperty =
            DependencyProperty.Register("MinParticleWidth", typeof(double), typeof(Emitter), new PropertyMetadata(0d));


        public double MaxParticleHeight
        {
            get { return (double)GetValue(MaxParticleHeightProperty); }
            set { SetValue(MaxParticleHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxParticleHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxParticleHeightProperty =
            DependencyProperty.Register("MaxParticleHeight", typeof(double), typeof(Emitter), new PropertyMetadata(0d));


        public double MinParticleHeight
        {
            get { return (double)GetValue(MinParticleHeightProperty); }
            set { SetValue(MinParticleHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MinParticleHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinParticleHeightProperty =
            DependencyProperty.Register("MinParticleHeight", typeof(double), typeof(Emitter), new PropertyMetadata(0d));


        public double MaxPositionOffset
        {
            get { return (double)GetValue(MaxPositionOffsetProperty); }
            set { SetValue(MaxPositionOffsetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxPositionOffset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxPositionOffsetProperty =
            DependencyProperty.Register("MaxPositionOffset", typeof(double), typeof(Emitter), new PropertyMetadata(0d));


        public double MinPositionOffset
        {
            get { return (double)GetValue(MinPositionOffsetProperty); }
            set { SetValue(MinPositionOffsetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MinPositionOffset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinPositionOffsetProperty =
            DependencyProperty.Register("MinPositionOffset", typeof(double), typeof(Emitter), new PropertyMetadata(0d));


        public double StartOpacity
        {
            get { return (double)GetValue(StartOpacityProperty); }
            set { SetValue(StartOpacityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StartOpacity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartOpacityProperty =
            DependencyProperty.Register("StartOpacity", typeof(double), typeof(Emitter), new PropertyMetadata(0d));


        public double EndOpacity
        {
            get { return (double)GetValue(EndOpacityProperty); }
            set { SetValue(EndOpacityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EndOpacity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EndOpacityProperty =
            DependencyProperty.Register("EndOpacity", typeof(double), typeof(Emitter), new PropertyMetadata(0d));

        public ColorKeyFrameCollection ColorKeyFrames
        {
            get { return (ColorKeyFrameCollection)GetValue(ColorKeyFramesProperty); }
            set { SetValue(ColorKeyFramesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ColorKeyFrames.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColorKeyFramesProperty =
            DependencyProperty.Register("ColorKeyFrames", typeof(ColorKeyFrameCollection), typeof(Emitter), new PropertyMetadata(new ColorKeyFrameCollection()));

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
            particle.Owner = this;
            particle.Mass = Helpers.RandomNumberGenerator.Instance.NextDouble(MinMass, MaxMass);
            particle.StartOpacity = this.StartOpacity;
            particle.EndOpacity = this.EndOpacity;
            particle.Force = new Vector(0, 0);
            particle.Velocity = new Vector(
                Helpers.RandomNumberGenerator.Instance.NextDouble(MinHorizontalVelocity, MaxHorizontalVelocity),
                Helpers.RandomNumberGenerator.Instance.NextDouble(MinVerticalVelocity, MaxVerticalVelocity));
            particle.LifeSpan = Helpers.RandomNumberGenerator.Instance.NextDouble(MinLifeSpan, MaxLifeSpan);
            particle.BackgroundColors = this.ColorKeyFrames;
        }

        #endregion
    }
}
