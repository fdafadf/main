using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labs.Agents
{
    public class Settings
    {
        public static string ProductName => Process.GetCurrentProcess().MainModule.FileVersionInfo.ProductName;
        public static DirectoryInfo ApplicationDataDirectory => new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
        public static DirectoryInfo ProductDirectory => EnsureDirectory(ApplicationDataDirectory, ProductName);
        public static DirectoryInfo EnvironmentsDirectory => EnsureDirectory(ProductDirectory, "Environments");

        public static DirectoryInfo EnsureDirectory(DirectoryInfo directory, string directoryName)
        {
            return EnsureDirectory(directory.FullName, directoryName);
        }

        public static DirectoryInfo EnsureDirectory(string path, string directoryName)
        {
            var directory = new DirectoryInfo(Path.Combine(path, directoryName));

            if (directory.Exists == false)
            {
                directory.Create();
            }

            return directory;
        }
    }
}
