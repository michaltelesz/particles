﻿using Particles.Engine.Emitters;
using Particles.Engine.Forces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Particles.Engine.Controls
{
    public class Particle : Control
    {
        #region Private Fields

        private Storyboard mStoryboard;
        private SolidColorBrush particleSolidColorBrush;

        // Fields for properties
        private bool mIsAlive;
        private Emitter mOwner;
        private List<Spring> mConnections = new List<Spring>();
        private double mMass;
        private Vector mVelocity;
        private Vector mForce;
        private bool mIsAnchor = false;
        private double mStartOpacity = 0.0;
        private double mEndOpacity = 0.0;
        private ColorKeyFrameCollection mBackgroundColors = new ColorKeyFrameCollection();

        #endregion

        #region Properties

        /// <summary>
        /// Used to create a unique name for this particle. Used in storyboards.
        /// </summary>
        public static int ParticleNo = 0;

        /// <summary>
        /// A flag denoted whether or not this particle is alive
        /// </summary>
        public bool IsAlive
        {
            get { return mIsAlive; }
            set
            {
                mIsAlive = value;
                // if the paritlce is no longer alive then remove the storyboard and call the updateparticle
                // method on the emitter that owns this particle
                if (!mIsAlive)
                {
                    mStoryboard.Remove(this);
                    this.Owner.UpdateParticle(this);

                    // rerun the particle
                    Run(true);

                    // Start the rerun storyboard.
                    mStoryboard.Begin(this, true);
                }
            }
        }

        /// <summary>
        /// A property which holds the emitter that created this particle.
        /// </summary>
        public Emitter Owner
        {
            get { return mOwner; }
            set { mOwner = value; }
        }

        /// <summary>
        /// A collection of Spring forces which connect this particle to others.
        /// </summary>
        public List<Spring> Connections
        {
            get { return mConnections; }
            set { mConnections = value; }
        }

        /// <summary>
        /// The mass of the particle
        /// </summary>        

        public double Mass
        {
            get { return mMass; }
            set { mMass = value; }
        }

        /// <summary>
        /// The velocity vector for this partilce
        /// </summary>
        public Vector Velocity
        {
            get { return mVelocity; }
            set { mVelocity = value; }
        }

        /// <summary>
        /// The cumulative force vector applied to this particle
        /// </summary>
        public Vector Force
        {
            get { return mForce; }
            set { mForce = value; }
        }

        /// <summary>
        /// A flag which denotes if this particles position should be updated or not.
        /// </summary>
        public bool IsAnchor
        {
            get { return mIsAnchor; }
            set { mIsAnchor = value; }
        }

        /// <summary>
        /// The starting opacity for this particle. Must be a value greater than zero.
        /// </summary>
        public double StartOpacity
        {
            get { return mStartOpacity; }
            set
            {
                if (value < 0.0)
                    mStartOpacity = 0.0;
                else
                    mStartOpacity = value;
            }
        }

        /// <summary>
        /// The ending opacity for this particle. Must be a value greater than zero.
        /// </summary>
        public double EndOpacity
        {
            get { return mEndOpacity; }
            set
            {
                if (value < 0.0)
                    mEndOpacity = 0.0;
                else
                    mEndOpacity = value;
            }
        }

        /// <summary>
        /// A property which holds the background colors for this particle as a ColorKeyFrameCollection
        /// </summary>
        public ColorKeyFrameCollection BackgroundColors
        {
            get { return mBackgroundColors; }
            set { mBackgroundColors = value; }
        }


        /// <summary>
        /// Dependency Property which maintains the life span of this particle
        /// </summary>
        public double LifeSpan
        {
            get { return (double)GetValue(LifeSpanProperty); }
            set { SetValue(LifeSpanProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LifeSpan.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LifeSpanProperty =
            DependencyProperty.Register("LifeSpan", typeof(double), typeof(Particle), new PropertyMetadata(0d));

        /// <summary>
        /// Dependency Property which maintains the position for the particle
        /// </summary>
        public Point Position
        {
            get { return (Point)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Position.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register("Position", typeof(Point), typeof(Particle), new PropertyMetadata(new Point()));

        #endregion

        #region Constructors

        static Particle()
        {
            //This OverrideMetadata call tells the system that this element wants to provide a style that is different than its base class.
            //This style is defined in themes\generic.xaml
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Particle), new FrameworkPropertyMetadata(typeof(Particle)));
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Init the particle.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            // Run the particle for the first time
            Run(false);
        }

        /// <summary>
        /// Capture Position, Width, Height and LifeSpan changes for the paritcle
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            // if the Position, Width or Height have changed then update the position of the paritcle on the canvas
            if (e.Property.Name.Equals("Position") || e.Property.Name.Equals("Width") || e.Property.Name.Equals("Height"))
            {
                Canvas.SetLeft(this, Position.X - this.Width / 2d);
                Canvas.SetTop(this, Position.Y - this.Height / 2d);
            }

            // if the lifespan has expired then set the isalive flag to false along with any particle that 
            // is connected to it.
            if (e.Property.Name.Equals("LifeSpan"))
            {
                if (this.LifeSpan <= 0)
                {
                    this.IsAlive = false;

                    foreach (Spring s in this.Connections)
                    {
                        if (s.ThisParticle.Equals(this))
                            s.ConnectedParticle.IsAlive = false;
                        else
                            s.ThisParticle.IsAlive = false;
                    }
                }
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Starts the particle running or restarts the particle if it has expired
        /// </summary>
        /// <param name="rerun"></param>
        private void Run(bool rerun)
        {
            // only init the particle if it hasn't been run yet
            if (!rerun)
            {
                NameScope.SetNameScope(this, new NameScope());
                this.Name = String.Format("p{0}", Particle.ParticleNo++);
                this.RegisterName(this.Name, this);
                this.Width = Helpers.RandomNumberGenerator.Instance.NextDouble(Owner.MinParticleWidth, Owner.MaxParticleWidth);
                this.Height = Helpers.RandomNumberGenerator.Instance.NextDouble(Owner.MinParticleHeight, Owner.MaxParticleHeight);
                this.particleSolidColorBrush = new SolidColorBrush(Colors.White);
                this.RegisterName(String.Format("{0}Brush", this.Name), particleSolidColorBrush);
                this.Background = particleSolidColorBrush;
            }

            // create the storyboard for this particle and hold its end behavior
            mStoryboard = new Storyboard();
            mStoryboard.FillBehavior = FillBehavior.HoldEnd;

            // create a parallel timeline for the the opacity and background changes.
            ParallelTimeline pt = new ParallelTimeline(TimeSpan.FromSeconds(0));

            // the timeline for the opacity change using the particle Start and End Opacity
            DoubleAnimation daOpacity = new DoubleAnimation(StartOpacity, EndOpacity,
                new Duration(TimeSpan.FromSeconds(this.LifeSpan)));
            Storyboard.SetTargetName(daOpacity, this.Name);
            Storyboard.SetTargetProperty(daOpacity, new PropertyPath(Particle.OpacityProperty));

            // the timeline for the background color change using a colorkeyframecollection
            ColorAnimationUsingKeyFrames daBackground = new ColorAnimationUsingKeyFrames();
            daBackground.Duration = new Duration(TimeSpan.FromSeconds(this.LifeSpan));
            daBackground.KeyFrames = BackgroundColors;
            Storyboard.SetTargetName(daBackground, String.Format("{0}Brush", this.Name));
            Storyboard.SetTargetProperty(daBackground, new PropertyPath(SolidColorBrush.ColorProperty));
            pt.Children.Add(daOpacity);
            pt.Children.Add(daBackground);
            mStoryboard.Children.Add(pt);

            // the first time this is run begin the storyboard on load.
            if (!rerun)
            {
                this.Loaded += delegate (object sender, RoutedEventArgs args)
                {
                    mStoryboard.Begin(this, true);
                };
            }

            mIsAlive = true;
        }

        #endregion
    }
}
