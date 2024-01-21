using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text;

namespace InteractMergeEngine.MergeStore
{
    public class ColumnData
    {
        public ConcurrentDictionary<string, string> columnData = new ConcurrentDictionary<string, string>();

        private string _id = string.Empty;

        public ColumnData(string id)
        {
            _id = id;
        }

        public void UpdateProperties(string[] columns, string[] fields)
        {
            lock (columnData)
            {
                for (var counter = 0; counter < columns.Length; counter++)
                {
                    var column = columns[counter];
                    var value = getField(fields, counter);
                    columnData.AddOrUpdate(column, value, (key, value) => value);
                }
            }
        }

        private string getField(string[] fields, int counter)
        {
            try
            {
                return fields[counter];
            }
            catch
            {
                return string.Empty;
            }
        }

        public string ToCSVString(ColumnNames columns)
        {
            var dataColumns = columns.GetColumns().Skip(1);

            var result = new StringBuilder();

            foreach (var column in dataColumns)
            {
                var propertyValue = string.Empty;
                if (columnData.TryGetValue(column, out propertyValue))
                {
                    result.Append($"{propertyValue.ToString()},");
                }
                else
                    result.Append($",");
            }

            return result.ToString().Substring(0, result.Length - 1);
        }
    }
}