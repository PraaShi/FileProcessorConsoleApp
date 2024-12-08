using log4net;
using System;
using System.IO;

namespace FileProcessorConsoleApp.Components
{
    internal class FileOperations
    {
        private readonly ILog _logger;

        public FileOperations(ILog logger)
        {
            _logger = logger;
        }

        public void ProcessFiles(string inputFolder, string correctFolder, string errorFolder, string sampleFolder)
        {
            _logger.Info($"Starting");
            _logger.Info($"Starting {nameof(ProcessFiles)}");
            try
            {
                string[] files = Directory.GetFiles(inputFolder);
                foreach (var file in files)
                {
                    string fileName = Path.GetFileName(file);
                    try
                    {
                        if (Path.GetExtension(file).Equals(".txt", StringComparison.OrdinalIgnoreCase))
                        {
                            string content = File.ReadAllText(file);
                            _logger.Info($"Processing file: {fileName}");

                            string destinationPath = Path.Combine(correctFolder, fileName);
                            File.Move(file, destinationPath);
                            _logger.Info($"Moved to correct folder: {fileName}");
                        }
                        else
                        {
                            File.AppendAllText(sampleFolder, "Appended text using AppendAllText method.");
                            _logger.Info("Content appended to sample.txt.");

                            string destinationPath = Path.Combine(errorFolder, fileName);
                            File.Move(file, destinationPath);
                            _logger.Warn($"Moved to error folder: {fileName}");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Error($"Error processing file {fileName}", ex);
                    }
                }
            }
            finally
            {
                _logger.Info($"Ending {nameof(ProcessFiles)}");
            }
        }

        public void RenameFile(string oldFilePath, string newFilePath)
        {
            try
            {
                if (File.Exists(oldFilePath))
                {
                    File.Move(oldFilePath, newFilePath);
                    _logger.Info($"File renamed from {oldFilePath} to {newFilePath}");
                }
                else
                {
                    _logger.Warn($"File {oldFilePath} does not exist.");
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error renaming file.", ex);
            }
        }

        public void DeleteFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    _logger.Info($"File {filePath} deleted.");
                }
                else
                {
                    _logger.Warn($"File {filePath} does not exist.");
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error deleting file.", ex);
            }
        }

        public void ReadAndWrite(string sourceFilePath, string destinationFilePath)
        {
            try
            {
                if (File.Exists(sourceFilePath))
                {
                    string content = File.ReadAllText(sourceFilePath);
                    File.WriteAllText(destinationFilePath, content);
                    _logger.Info($"Read from {sourceFilePath} and wrote to {destinationFilePath}");
                }
                else
                {
                    _logger.Warn($"Source file {sourceFilePath} does not exist.");
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error reading and writing file.", ex);
            }
        }

        public string[] GetFilesByExtension(string directoryPath, string fileExtension)
        {
            try
            {
                if (Directory.Exists(directoryPath))
                {
                    string[] files = Directory.GetFiles(directoryPath, fileExtension);
                    _logger.Info($"Found {files.Length} file(s) with extension {fileExtension}.");
                    return files;
                }
                else
                {
                    _logger.Warn($"Directory {directoryPath} does not exist.");
                    return Array.Empty<string>();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error fetching files by extension.", ex);
                return Array.Empty<string>();
            }
        }

        public string CreateNewExecutionFile(string directoryPath, string baseFileName, string extension)
        {
            try
            {
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                    _logger.Info($"Directory {directoryPath} created.");
                }

                string newFile = Path.Combine(directoryPath, $"{baseFileName}_{DateTime.Now:yyyyMMddHHmmss}{extension}");
                File.Create(newFile).Close();
                _logger.Info($"New execution file created: {newFile}");
                return newFile;
            }
            catch (Exception ex)
            {
                _logger.Error("Error creating new execution file.", ex);
                return null;
            }
        }
    }
}
