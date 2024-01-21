namespace InteractMergeEngine.MergeStore
{
    public class ColumnNames
    {
        private List<string> columnNames = new List<string>();

        public void AddUpdateColumnNames(string[] names)
        {
            lock (columnNames)
            {
                var currentNames = columnNames.ToArray();
                columnNames.AddRange(names.Except(currentNames));
            }
        }

        public List<string> GetColumns()
        {
            return columnNames.OrderBy(a => a).ToList();
        }

        public string GetCSVFormatted()
        {
            var columnNames = GetColumns();
            string result = string.Join(",", columnNames.Select(name => $"\"{name.Replace("\"", "\"\"")}\""));
            return result;
        }
    }
}