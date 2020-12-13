namespace Recognition.Core
{
    /// <summary>
    /// MatchResult represents the result of matching a <code>VoicePrint</code> against a given voice sample. 
    /// It holds the user defined key of the voice print and a likelihood ratio expressed as a percentage.
    /// </summary>
    /// <typeparam name="T">The type of the key selected by the user</typeparam>
    public class MatchResult<T>
    {
        readonly T key;
        readonly int likelihoodRatio;
        readonly double distance;

        /**
         * Default constructor
         * @param key the user defined key for the corresponding VoicePrint
         * @param likelihoodRatio the likelihood ratio expressed as a percentage
         */
        public MatchResult(T key, int likelihoodRatio, double distance)
        {
            this.key = key;
            this.likelihoodRatio = likelihoodRatio;
            this.distance = distance;
        }

        /**
         * Get the matched key
         * @return the key
         */
        public T Key
        {
            get
            {
                return key;
            }
        }

        /**
         * Get the likelihoodRatio level
         * @return the likelihoodRatio ratio expressed as a percentage
         */
        public int LikelihoodRatio
        {
            get
            {
                return likelihoodRatio;
            }
        }

        /**
         * Get the raw distance between the <code>VoicePrint</code> idenntified by K 
         * and the given voice sample
         * @return the distance
         */
        public double Distance
        {
            get
            {
                return distance;
            }
        }

    }
}
