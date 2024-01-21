 Interact Technical Challenge application

 About The Solution

 This solution has been created based on the specification PDF Provided by Interact.

 The overall idea is to provide a console application that will take a series of input files to be merged and the name of an output file which will contain the merged results

 Observations/Thoughts

 This was an interesting challenge, especially in terms of performance and going for speed, opting for Parallel Processing, but that brought its own challenges in terms of locking and consistency which did cost some time.

 I would have preferred to go with a test first approach, arguably a reason things took a little longer than I anticipated, but again would have led to potential even longer time frame.

 Overall, I am ok with the code, but am not overaly happy with the structure. The code does achieve the goal, most of the, if not all the decision logic has been moved into injectable dependencies.

Time to execute, in debug from Visual Studio
Time to run : 00:00:05.3242680

Time to Implement
Approx 3.5 to 4 hours

 Getting Started

 The following section makes the assumption that you have unzipped the provided into a folder of its own

 Prerequisites

 In order to build and run this console application you will need;

    • .Net7 (minimum)
      https://dotnet.microsoft.com/en-us/download/dotnet/7.0

 Dependencies

    • Microsoft.Extensions.DependencyInjection Version="8.0.0"
    • Microsoft.Extensions.Configuration Version="8.0.0"
    • Microsoft.Extensions.Configuration.Json Version="8.0.0"
    • Microsoft.Extensions.Logging Version="8.0.0"
    • Microsoft.Extensions.Logging.Configuration Version="8.0.0"
    • Microsoft.Extensions.Logging.Console Version="8.0.0"
 Doing stuff

 This section makes the assumption you have opened a terminal window and have navigated into the solution folder TechnicalChallange. In this folder you should see the InteractTechChallange.sln file

 Building the solution

 Rather than build the solution we are going to publish the solution to a folder where we can run the program, to do this we type the following

 dotnet publish InteractMerge -c Release -o ..\bin

 This will build the code and then publish the executable we need into a bin folder.

 Running the application

 Assuming you are still in the TechnicalChallange solution folder, navigate into the bin folder created when publishing

 cd ..\bin
Once you have changed folders to the bin folder you can now run the application

The application requires you to provide two parameters

-i csv list of files to be merged (can be local or full path)
-o name of the file into which the data will be merged

An Example of the command is as follows

InteractMerge -i c:\Data\Live\large_file_1.csv,c:\Data\Live\large_file_2.csv,c:\Data\Live\large_file_3.csv,c:\Data\Live\large_file_4.csv,c:\Data\Live\large_file_5.csv      -o merged_files.csv
or
InteractMerge.exe -i c:\Data\Live\large_file_1.csv,c:\Data\Live\large_file_2.csv,c:\Data\Live\large_file_3.csv,c:\Data\Live\large_file_4.csv,c:\Data\Live\large_file_5.csv      -o merged_files.csv
Assumptions

The following assumptions where made

   1. If a file fails application continues, but lists the failure
Improvements

Some maybe improvements

   1. Improve the logging so that whilst debugging output is more verbose
   2. Provide a more robust commandline parser, rather than the really basic thing thats there
