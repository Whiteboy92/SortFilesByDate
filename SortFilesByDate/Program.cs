namespace SortFilesByDate
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            string sourceFolderPath = @"C:\Users\Desktop\Source";
            string targetFolderPath = @"C:\Users\Desktop\Sorted";

            // Ensure the target folder exists
            Directory.CreateDirectory(targetFolderPath);
            
            string[] allFiles = Directory.GetFiles(sourceFolderPath, "*.*", SearchOption.AllDirectories);

            Parallel.ForEach(allFiles, filePath =>
            {
                try
                {
                    DateTime modifiedDate = File.GetLastWriteTime(filePath);
                    string folderName = modifiedDate.ToString("yyyy-MM-dd");
                    
                    string destinationFolder = Path.Combine(targetFolderPath, folderName);
                    Directory.CreateDirectory(destinationFolder);
                    
                    string fileName = Path.GetFileName(filePath);
                    string destinationFilePath = Path.Combine(destinationFolder, fileName);
                    
                    if (!File.Exists(destinationFilePath))
                    {
                        File.Copy(filePath, destinationFilePath);
                        Console.WriteLine($"Copied '{fileName}' to '{destinationFolder}'");
                    }
                    else
                    {
                        Console.WriteLine($"File '{fileName}' already exists in '{destinationFolder}'");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            });

            Console.WriteLine("File copying completed.");
        }
    }
}
