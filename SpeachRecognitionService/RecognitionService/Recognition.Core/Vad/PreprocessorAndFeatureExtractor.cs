using Recognition.Core.Enchancements;
using Recognition.Core.Features;
using Recognition.Core.Utils;
using System.IO;

namespace Recognition.Core.Vad
{
    public class PreprocessorAndFeatureExtractor
    {
        readonly IFeaturesExtractor<double[]> extractor;
        readonly Normalizer normalizer;
        readonly AutocorrellatedVoiceActivityDetector voiceDetector;
        readonly float sampleRate;

        public PreprocessorAndFeatureExtractor(float sampleRate)
        {
            extractor = new LpcFeaturesExtractor(sampleRate, 20);
            voiceDetector = new AutocorrellatedVoiceActivityDetector();
            normalizer = new Normalizer();
            this.sampleRate = sampleRate;
        }


        public double[] ProcessAndExtract(double[] voiceSample)
        {
            var processed = voiceSample;

            processed = voiceDetector.RemoveSilence(processed, sampleRate);
            normalizer.Normalize(processed, sampleRate);
            processed = extractor.ExtractFeatures(processed);

            return processed;
        }


        public double[] ProcessAndExtract(Stream voiceSample)
        {
            var processed = AudioConverter.ConvertAudioToDoubleArray(voiceSample, sampleRate);

            processed = voiceDetector.RemoveSilence(processed, sampleRate);
            normalizer.Normalize(processed, sampleRate);
            processed = extractor.ExtractFeatures(processed);

            return processed;
        }
    }
}
