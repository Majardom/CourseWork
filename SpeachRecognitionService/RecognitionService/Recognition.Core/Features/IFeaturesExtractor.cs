namespace Recognito.Features
{
    public interface IFeaturesExtractor<T>
    {
        T ExtractFeatures(double[] voiceSample);
    }
}
