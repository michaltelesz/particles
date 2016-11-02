using Particles.Engine.Emitters;
using Particles.Engine.Forces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Particles.Engine.Controls
{
    public class ParticleSystem : ItemsControl
    {
        #region Private Fields

        private bool mIsRunning = false;
        private double mThreshold = 1.0d; // the time the system will not run above
        private double mTime; // the total time ellapsed
        private TimeSpan mPreviousTimeSpan = TimeSpan.Zero; // the previous timespan to render a frame

        #endregion

        #region Properties

        public ObservableCollection<Emitter> Emitters
        {
            get { return (ObservableCollection<Emitter>)GetValue(EmittersProperty); }
            set { SetValue(EmittersProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Emmiters.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EmittersProperty =
            DependencyProperty.Register("Emitters", typeof(ObservableCollection<Emitter>), typeof(ParticleSystem));



        public ObservableCollection<Force> Forces
        {
            get { return (ObservableCollection<Force>)GetValue(ForcesProperty); }
            set { SetValue(ForcesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ForcesProperty =
            DependencyProperty.Register("Forces", typeof(ObservableCollection<Force>), typeof(ParticleSystem));



        public ObservableCollection<Particle> Particles
        {
            get { return (ObservableCollection<Particle>)GetValue(ParticlesProperty); }
            set { SetValue(ParticlesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Particles.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ParticlesProperty =
            DependencyProperty.Register("Particles", typeof(ObservableCollection<Particle>), typeof(ParticleSystem));


        public Vector Gravity
        {
            get { return (Vector)GetValue(GravityProperty); }
            set { SetValue(GravityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Gravity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GravityProperty =
            DependencyProperty.Register("Gravity", typeof(Vector), typeof(ParticleSystem));



        public Vector Drag
        {
            get { return (Vector)GetValue(DragProperty); }
            set { SetValue(DragProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DragProperty =
            DependencyProperty.Register("Drag", typeof(Vector), typeof(ParticleSystem));

        #endregion

        #region Constructors

        static ParticleSystem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ParticleSystem), new FrameworkPropertyMetadata(typeof(ParticleSystem)));
        }
        #endregion

        #region Overrides
        public override void BeginInit()
        {
            base.BeginInit();

            // Init the ObservableCollections, if this is done when declaring the depenncy property
            // an error will occur when creating multiple particle systems in an application.
            SetValue(EmittersProperty, new ObservableCollection<Emitter>());
            SetValue(ForcesProperty, new ObservableCollection<Force>());
            SetValue(ParticlesProperty, new ObservableCollection<Particle>());

            // render every frame
            CompositionTarget.Rendering += UpdateRender;
        }
        #endregion

        #region Public Methods

        // <summary>
        /// Update the particles in the particle system
        /// </summary>
        /// <param name="time"></param>
        public void Update(double time)
        {
            ScaleVectors(time);
            mTime += time; // update the time
        }

        /// <summary>
        /// Start the particle system
        /// </summary>
        public void Start()
        {
            Particles.Clear(); // clear all the particles 
            // Used to remove all forces that where Springs 
            //List<Force> lForce = new List<Force>(Forces); 
            //Forces = new ObservableCollection<Force>(lForce.FindAll(
            //    delegate(Force f) { return !f.GetType().Equals(typeof(Spring)); }));

            // for each emitter in the system generate particles
            foreach (Emitter emitter in Emitters)
            {
                emitter.GenerateParticles(this);
            }
            // start the system running
            mIsRunning = true; //mStopWatch.Start();
        }

        /// <summary>
        /// Stop the particle system
        /// </summary>
        public void Stop()
        {
            // stop the system running
            mIsRunning = false; //mStopWatch.Stop();
        }

        #endregion

        #region Protected Methods

        protected void UpdateRender(object sender, EventArgs e)
        {
            // Wait until the system is loaded
            if (this.IsLoaded)
            {
                if (mIsRunning)
                {
                    // Update the time based on the current rendering time subtrated from the previous rendering time and 
                    // returned in seconds
                    double time = ((RenderingEventArgs)e).RenderingTime.Subtract(mPreviousTimeSpan).TotalSeconds; //(float)mStopWatch.Elapsed.Subtract(mPreviousTimeSpan).TotalSeconds;
                    if (time <= mThreshold)
                    {
                        Update(time); // update the system
                    }
                    else
                    {
                        Console.Out.WriteLine(String.Format("ParticleSystem is running above the threshold of {0} second{1}", mThreshold, mThreshold == 1 ? "" : "s"));
                    }


                    // save the rendering time
                    mPreviousTimeSpan = ((RenderingEventArgs)e).RenderingTime; //mStopWatch.Elapsed;
                }
            }
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Scale the particle vectors for this system.
        /// </summary>
        /// <param name="time"></param>
        private void ScaleVectors(double time)
        {
            foreach (Particle particle in Particles)
            {
                // If the particle is alive
                if (particle.IsAlive)
                {
                    // calculate the forces acting onthe particle at the current time
                    particle.Force = ComputeForces(particle, time);

                    // Update the velocity vectors for this particle based on the time
                    Vector v = particle.Velocity;
                    double vx = v.X + time * (particle.Force.X / particle.Mass);
                    double vy = v.Y + time * (particle.Force.Y / particle.Mass);
                    particle.Velocity = new Vector(vx, vy);

                    // If the particle is not an anchor update its position based on the time
                    if (!particle.IsAnchor)
                    {
                        double px = particle.Position.X + time * particle.Velocity.X;
                        double py = particle.Position.Y + time * particle.Velocity.Y;
                        particle.Position = new Point(px, py);
                    }
                    particle.LifeSpan -= time; // update the particles lifespan
                }
            }
        }

        /// <summary>
        /// Calculate all the forces acting on a particle at a given time
        /// </summary>
        /// <param name="particle"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        private Vector ComputeForces(Particle particle, double time)
        {
            // set all the forces for a particle at a time and position
            double forceX = (particle.Mass * Gravity.X) - (particle.Velocity.X * Drag.X);
            double forceY = (particle.Mass * Gravity.Y) - (particle.Velocity.Y * Drag.Y);

            // If there are spring force connections on this particle then for every connection update
            // the forces acting upon the particle
            if (particle.Connections.Count > 0)
            {
                foreach (Spring s in particle.Connections)
                {
                    // apply the spring force   
                    Vector force = s.ApplyForce(particle);
                    forceX += force.X;
                    forceY += force.Y;
                }
            }
            // for every other force in the global list of forces, apply and update the force.
            foreach (Force f in Forces)
            {
                Vector force = f.ApplyForce(particle);
                forceX += force.X;
                forceY += force.Y;
            }
            return new Vector(forceX, forceY); // return the force vector.
        }

        #endregion
    }
}
