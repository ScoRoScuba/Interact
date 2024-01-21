using InteractMergeEngine.MergeStore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace InteractMergeEngine
{
    public class MergeEngine : IMergeEngine
    {
        private readonly ILogger<MergeEngine> _logger;
        private readonly IMergeEngineStore _mergeStore;
        private readonly IMergeStoreWriter _mergeStoreWriter;

        public MergeEngine (ILogger<MergeEngine> logger, IMergeEngineStore mergeStore, IMergeStoreWriter mergeStoreWriter)
        {
            _logger = logger;
            _mergeStore = mergeStore;
            _mergeStoreWriter = mergeStoreWriter;
        }

        public async Task<MergeResult> MergeFilesAsync(string[] files, string outputFile) 
        {
            var stopWatch = Stopwatch.StartNew();
            stopWatch.Start();

            ParallelOptions parallelOptions = new()
            {
                MaxDegreeOfParallelism = 5
            };

            ColumnNames columns = new ColumnNames();           

            await Parallel.ForEachAsync(files, parallelOptions, (file, token) => {
                _logger.LogInformation($"Proicessing File {file}");
                try
                {
                    using (StreamReader reader = new StreamReader(file))
                    {
                        var headerLine = reader.ReadLine();
                        var columnNames = headerLine.Split(',');
                        var countOfColumns = columnNames.Length;

                        string[] propertyColumnNames = columnNames.Skip(1).ToArray();

                        columns.AddUpdateColumnNames(columnNames);

                        string dataLine = string.Empty;
                        while ((dataLine = reader.ReadLine()) != null)
                        {
                            var fields = dataLine.Split(',');
                            string id = fields[0];

                            if (fields.Length != countOfColumns)
                            {
                                _logger.LogWarning($"File {file}, Record {id} has more/less columns than expected, skipping");
                            }

                            string[] values = fields.Skip(1).ToArray();

                            _mergeStore.UpdateStore(id, propertyColumnNames, values);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Failed to process File {file}, skipping");
                }

                return new ValueTask();
            });

            _mergeStoreWriter.ToFile(outputFile, columns, _mergeStore);

            stopWatch.Stop();

            return new MergeResult()
            {
                MergeTime = stopWatch.Elapsed
            };
        }
    }
}