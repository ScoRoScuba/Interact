namespace InteractMergeEngine.MergeStore
{
    public interface IMergeEngineStore : IEnumerable<KeyValuePair<string, ColumnData>>
    {
        void UpdateStore(string id, string[] propertyColumnNames, string[] values);
    }
}