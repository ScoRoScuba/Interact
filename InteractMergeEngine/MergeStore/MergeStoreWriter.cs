using System.Diagnostics;

namespace InteractMergeEngine.MergeStore
{
    public class MergeStoreWriter : IMergeStoreWriter
    {
        public void ToFile(string fileName, ColumnNames columnNames, IEnumerable<KeyValuePair<string, ColumnData>> columnData)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                // Write header
                writer.WriteLine(columnNames.GetCSVFormatted());

                // Write merged data
                foreach (var item in columnData)
                {
                    var values = item.Value.ToCSVString(columnNames);

                    string line = $"{item.Key},{values}";
                    writer.WriteLine(line);
                }
            }
        }
    }
}
