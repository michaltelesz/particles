using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Particles.Helpers
{
    public sealed class RandomNumberGenerator
    {
        private static RandomNumberGenerator m_oInstance = null;
        private static readonly object m_oPadLock = new object();

        private Random mRandom; // store the random object

        public static RandomNumberGenerator Instance
        {
            get
            {
                lock (m_oPadLock)
                {
                    if (m_oInstance == null)
                    {
                        m_oInstance = new RandomNumberGenerator();
                    }
                    return m_oInstance;
                }
            }
        }

        private RandomNumberGenerator()
        {
            mRandom = new Random(DateTime.Now.Millisecond);
        }

        /// <summary>
        /// Returns the next double no greater than max
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public double NextDouble(double max)
        {
            return mRandom.NextDouble() * max;
        }

        /// <summary>
        /// Returns the next double between min and max
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public double NextDouble(double min, double max)
        {
            if (min > max)
                return mRandom.NextDouble() * (min - max) + max;
            else
                return mRandom.NextDouble() * (max - min) + min;
        }
    }
}
