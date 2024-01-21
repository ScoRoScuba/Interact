using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractMergeEngine.MergeStore
{
    public interface IMergeStoreWriter
    {
        void ToFile(string fileName, ColumnNames columnNames, IEnumerable<KeyValuePair<string, ColumnData>> values);
    }
}
