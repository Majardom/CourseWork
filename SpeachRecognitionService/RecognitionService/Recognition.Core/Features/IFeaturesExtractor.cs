namespace Recognition.Core.Features
{
    public interface IFeaturesExtractor<T>
    {
        T ExtractFeatures(double[] voiceSample);
    }
}
