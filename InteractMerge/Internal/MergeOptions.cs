using System.Collections;

namespace InteractMerge.Internal
{
    internal  class MergeOptions
    {
        public MergeOptions() 
        {
            SourceFiles = new List<string>();
        }

        public IList SourceFiles{ get; set; }
        public string? OutputFile { get; set; }
    }
}
