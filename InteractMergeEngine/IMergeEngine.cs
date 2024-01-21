namespace InteractMergeEngine
{
    public interface IMergeEngine
    {
        public Task<MergeResult> MergeFilesAsync(string[] files, string outputFile);
    }
}