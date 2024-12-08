using System;
using log4net;
using log4net.Config;
using System.IO;
using FileProcessorConsoleApp.Components;
using System.Reflection;

namespace FileProcessorConsoleApp
{
    class Program
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            // Configure Log4Net
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("web.config"));


            // Log an info message
            Logger.Info("Application started.");

            try
            {
                // Initialize the FileOperations and MenuHandler classes
                string inputFolder = @"D:\example\Input";
                string correctFolder = @"D:\example\Correct";
                string errorFolder = @"D:\example\Error";
                string sampleFile = @"D:\example\sample.txt";

                FileOperations fileOperations = new FileOperations(Logger);
                MenuHandler menuHandler = new MenuHandler(fileOperations, Logger, inputFolder, correctFolder, errorFolder, sampleFile);

                // Display the menu and process user input
                menuHandler.DisplayMenu();
            }
            catch (Exception ex)
            {
                // Log error if an exception occurs
                Logger.Error("An error occurred during application processing.", ex);
            }

            // Log an info message indicating the end of the application
            Logger.Info("Application ended.");
        }
    }
}
