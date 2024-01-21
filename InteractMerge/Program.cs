using InteractMerge.Internal;
using InteractMergeEngine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.CommandLine;
using Microsoft.Extensions.Logging.Configuration;
using InteractMergeEngine.MergeStore;

namespace InteractTechChallange
{
    internal class Program
    {
        static async Task<int> Main(string[] args)
        {
            var inputFilesOption = new Option<string>("-i", "Input file paths separated by commas.");
            var outputFileOption = new Option<string>("-o", "Output file path.");

            var rootCommand = new RootCommand("Interact file merge technical challange")
                {
                    inputFilesOption,
                    outputFileOption
                };

            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appsettings.json")
                .Build();

            var serviceCollection = new ServiceCollection();

            serviceCollection.AddTransient<IMergeEngine, MergeEngine>();
            serviceCollection.AddTransient<ISplitter,FileNameSplitter> ();
            serviceCollection.AddTransient<IMergeEngineStore, MergeStore>();
            serviceCollection.AddTransient<IMergeStoreWriter, MergeStoreWriter>();

            serviceCollection.AddLogging(builder => builder.AddConsole().AddConfiguration());

            var serviceProvider = serviceCollection.BuildServiceProvider();

            rootCommand.SetHandler(async (inputFiles, outputFile) =>
            {
                try
                {
                    var splitter = serviceProvider.GetRequiredService<ISplitter>();
                    var files = splitter.Split(inputFiles);

                    var mergeEngine = serviceProvider.GetRequiredService<IMergeEngine>();

                    var result = await mergeEngine.MergeFilesAsync(files, outputFile);

                    Console.WriteLine($"Time to run : {result.MergeTime}");

                } catch (Exception ex) {

                    var logger = serviceProvider.GetRequiredService<ILogger>();
                    logger.LogError(ex,"A fatal erro has occured during the merge and it can not continue");
                }

            }, inputFilesOption, outputFileOption);


            return await rootCommand.InvokeAsync(args);
        }
    }
}