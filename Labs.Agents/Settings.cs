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
        public static DirectoryInfo ApplicationDataDirectory => new DirectoryInfo(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData));
        public static DirectoryInfo ProductDirectory => ApplicationDataDirectory.EnsureDirectory(ProductName);
        public static DirectoryInfo SpacesDirectory => ProductDirectory.EnsureDirectory("Environments");
    }
}
