using System.Collections.Generic;
using System.IO;

namespace Recognition.Core.SpeakerFinder
{
    public interface ISpeakerFinderAlgorithm
    {
        List<Match> FindAudioFilesContainingSpeaker(Stream speakerAudioFile, string toBeScreenedForAudioFilesWithSpeakerFolder);
    }
}
