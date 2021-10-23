using System.IO;

namespace csharp_interviews.exos
{
    public class FindFile
    {
        public static string CoreFunction()
        {
            var targetDirectory = @"C:\Users\ferta\OneDrive\Desktop";
            string fileNameToSearch = "eopp.com.crt";
            var result = ProcessDirectory(targetDirectory, fileNameToSearch);
            return !string.IsNullOrEmpty(result) ? result : "not found";
        }

        public static string ProcessDirectory(string targetDirectory, string fileNameToSearch)
        {
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                if (fileName.EndsWith(fileNameToSearch))
                    return targetDirectory;

            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
            {
                string result = ProcessDirectory(subdirectory, fileNameToSearch);
                if(!string.IsNullOrEmpty(result))
                    return result;
            }

            return null;
        }
    }
}
