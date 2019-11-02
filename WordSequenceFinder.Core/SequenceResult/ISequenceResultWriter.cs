namespace WordSequenceFinder.Core.Sequence
{
    public interface ISequenceResultWriter
    {
        void Write(SequenceResult result, string resultLocation);
    }
}
