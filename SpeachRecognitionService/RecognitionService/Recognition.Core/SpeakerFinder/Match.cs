
namespace Recognition.Core.SpeakerFinder
{
    public class Match
    {
        readonly string audioFile;
        readonly double distance;

        public Match(string audioFile, double distance)
        {
            this.audioFile = audioFile;
            this.distance = distance;
        }
    }
}
