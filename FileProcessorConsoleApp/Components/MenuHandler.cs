using FileProcessorConsoleApp.Components;
using log4net;
using System;

namespace FileProcessorConsoleApp
{
    internal class MenuHandler
    {
        private readonly FileOperations _fileOperations;
        private readonly ILog _logger;
        private readonly string _inputFolder;
        private readonly string _correctFolder;
        private readonly string _errorFolder;
        private readonly string _sampleFile;

        public MenuHandler(FileOperations fileOperations, ILog logger, string inputFolder, string correctFolder, string errorFolder, string sampleFile)
        {
            _fileOperations = fileOperations;
            _logger = logger;
            _inputFolder = inputFolder;
            _correctFolder = correctFolder;
            _errorFolder = errorFolder;
            _sampleFile = sampleFile;
        }

        public void DisplayMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("====== File Operations Menu ======");
                Console.WriteLine("1. Process Files");
                Console.WriteLine("2. Rename File");
                Console.WriteLine("3. Delete File");
                Console.WriteLine("4. Read and Write File");
                Console.WriteLine("5. Get Files by Extension");
                Console.WriteLine("6. Create New Execution File");
                Console.WriteLine("0. Exit");
                Console.WriteLine("==================================");
                Console.Write("Enter your choice: ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    try
                    {
                        switch (choice)
                        {
                            case 1:
                                ProcessFiles();
                                break;
                            case 2:
                                RenameFile();
                                break;
                            case 3:
                                DeleteFile();
                                break;
                            case 4:
                                ReadAndWriteFile();
                                break;
                            case 5:
                                GetFilesByExtension();
                                break;
                            case 6:
                                CreateNewExecutionFile();
                                break;
                            case 0:
                                exit = true;
                                _logger.Info("Exiting the application.");
                                break;
                            default:
                                Console.WriteLine("Invalid choice. Please try again.");
                                _logger.Warn($"Invalid menu choice: {choice}");
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An error occurred. Check logs for details.");
                        _logger.Error("An error occurred while processing the menu choice.", ex);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    _logger.Warn("Invalid input for menu choice.");
                }
            }
        }

        private void ProcessFiles()
        {
            Console.WriteLine("Processing files...");
            _logger.Info("Starting file processing...");

            try
            {
                // Process the files, this might take some time depending on the number of files
                _fileOperations.ProcessFiles(_inputFolder, _correctFolder, _errorFolder, _sampleFile);

                Console.WriteLine("File processing complete.");
                _logger.Info("File processing complete.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while processing files.");
                _logger.Error("An error occurred during file processing.", ex);
            }
        }


        private void RenameFile()
        {
            Console.Write("Enter the old file name (e.g., oldOne.txt): ");
            string oldFileName = Console.ReadLine();

            Console.Write("Enter the new file name (e.g., newOne.txt): ");
            string newFileName = Console.ReadLine();

            string directoryPath = @"D:\example";  // The directory where the files are located
            string oldFilePath = Path.Combine(directoryPath, oldFileName);  // Full path for the old file
            string newFilePath = Path.Combine(directoryPath, newFileName);  // Full path for the new file

            // Call the RenameFile method from FileOperations class
            _fileOperations.RenameFile(oldFilePath, newFilePath);
        }



        private void DeleteFile()
        {
            Console.Write("Enter the file name to delete (e.g., fileToDelete.txt): ");
            string fileName = Console.ReadLine();

            string directoryPath = @"D:\example";  
            string filePath = Path.Combine(directoryPath, fileName); 

            _fileOperations.DeleteFile(filePath);
        }


        private void ReadAndWriteFile()
        {
            Console.Write("Enter the source file name (e.g., sourceFile.txt): ");
            string sourceFileName = Console.ReadLine();

            Console.Write("Enter the destination file name (e.g., destinationFile.txt): ");
            string destinationFileName = Console.ReadLine();

            string directoryPath = @"D:\example";  // The directory where the files are located

            // Combine directory path with the provided file names to get full paths
            string sourceFilePath = Path.Combine(directoryPath, sourceFileName);
            string destinationFilePath = Path.Combine(directoryPath, destinationFileName);

            _fileOperations.ReadAndWrite(sourceFilePath, destinationFilePath);
        }


        private void GetFilesByExtension()
        {
            string directoryPath = @"D:\example";

            Console.Write("Enter file extension (e.g., *.txt): ");
            string fileExtension = Console.ReadLine();

            var files = _fileOperations.GetFilesByExtension(directoryPath, fileExtension);
            Console.WriteLine("Files found:");
            foreach (var file in files)
            {
                Console.WriteLine(file);
            }
        }

        private void CreateNewExecutionFile()
        {
            string directoryPath = @"D:\example";


            string baseFileName = "ThisFile";

            //Console.Write("Enter file extension (e.g., .txt): ");
            string extension = ".txt";

            string newFile = _fileOperations.CreateNewExecutionFile(directoryPath, baseFileName, extension);
            Console.WriteLine($"New execution file created: {newFile}");
        }
    }
}
