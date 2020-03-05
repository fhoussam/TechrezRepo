using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace csharp_interviews.exos
{
    public class FindFile
    {
        public static void CoreFunction()
        {
            var targetDirectory = @"C:\Users\ferta\OneDrive\Desktop\aaaa";
            string fileNameToSearch = "aaa.txt";
            string result = string.Empty;
            ProcessDirectory(targetDirectory, fileNameToSearch, ref result);
            Console.WriteLine(result);
        }

        public static void ProcessDirectory(string targetDirectory, string fileNameToSearch, ref string result)
        {
            if (!string.IsNullOrEmpty(result))
                return;

            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                if (fileName.EndsWith(fileNameToSearch)) 
                    result = targetDirectory;

            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory, fileNameToSearch, ref result);
        }
    }
}
