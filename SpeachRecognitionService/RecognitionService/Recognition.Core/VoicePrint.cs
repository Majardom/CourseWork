using Recognition.Core.Distances;
using Recognition.Core.Utils;
using System;
using System.IO;
using System.Threading;

namespace Recognition.Core
{
    [Serializable]
    public sealed class VoicePrint
    {
        readonly ReaderWriterLockSlim rwl = new ReaderWriterLockSlim();
        double[] features;
        int meanCount;


        /**
         * Package visible constructor for Hibernate and the likes
         */
        public VoicePrint() { }

        /**
         * Contructor for a voice print
         * @param features the features
         */
        public VoicePrint(double[] features)
        {
            this.features = features;
            meanCount = 1;
        }

        /**
         * Copy constructor
         * @param print the VoicePrint to copy
         */
        public VoicePrint(VoicePrint print) : this(ArrayHelper.Copy(print.features, print.features.Length))
        { }

        /**
         * Returns the distance between this voice print and the given one using the calculator.
         * Threading : it is safe to call this method while other threads may merge this voice print instance
         * with another one in the sense that the distance calculation will not happen on half merged voice print.
         * Since this method is read only, it is safe to call it from multiple threads for a single instance
         * @param calculator the distance calculator
         * @param voicePrint the voice print
         * @return the distance
         */
        public double GetDistance(DistanceCalculator calculator, VoicePrint voicePrint)
        {
            rwl.EnterReadLock();

            try
            {
                return calculator.GetDistance(features, voicePrint.features);
            }
            finally
            {
                rwl.ExitReadLock();
            }
        }

        /**
         * Merges this voice print features with the given one.
         * Threading : it is safe to call this method while other threads may request the distance of this voice 
         * regarding another one in the sense that the distance calculation will not happen on half merged voice print
         * @param features the features to merge
         */
        public void Merge(double[] features)
        {
            if (this.features.Length != features.Length)
            {
                throw new ArgumentException($"Features of new VoicePrint is of different size : [{features.Length}] expected [{this.features.Length}]");
            }


            rwl.EnterWriteLock();
            try
            {
                Merge(this.features, features);
                meanCount++;
            }
            finally
            {
                rwl.ExitWriteLock();
            }
        }

        /**
         * Convenience method to merge voice prints
         * @param print the voice print to merge
         * @see VoicePrint#merge(double[])
         */
        public void Merge(VoicePrint print)
        {
            Merge(print.features);
        }

        /**
         * Recomputes the mean values for the inner features when adding the outer features
         * @param inner the inner features
         * @param outer the outer features
         */
        private void Merge(double[] inner, double[] outer)
        {
            for (int i = 0; i < inner.Length; i++)
            {
                inner[i] = (inner[i] * meanCount + outer[i]) / (meanCount + 1);
            }
        }

        public override string ToString()
        {
            return ArrayHelper.ToString(features);
        }


        public static VoicePrint FromFeatures(double[] features)
        {
            return new VoicePrint(features);
        }

    }
}
