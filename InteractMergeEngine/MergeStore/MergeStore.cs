using System.Collections;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace InteractMergeEngine.MergeStore
{
    public class MergeStore : IMergeEngineStore
    {
        private ConcurrentDictionary<string, ColumnData> mergedData = new ConcurrentDictionary<string, ColumnData>();

        public void UpdateStore(string id, string[] propertyColumnNames, string[] values)
        {
            lock (mergedData)
            {
                ColumnData properties;
                if (!mergedData.TryGetValue(id, out properties))
                {
                    //                    Debug.WriteLine($"New Property ID: {id}");
                    properties = new ColumnData(id);
                }

                properties.UpdateProperties(propertyColumnNames, values);

                mergedData.AddOrUpdate(id, properties, (key, properties) => properties);
            }
        }

        public IEnumerator<KeyValuePair<string, ColumnData>> GetEnumerator()
        {
            foreach (KeyValuePair<string, ColumnData> guest in mergedData)
            {
                yield return guest;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
