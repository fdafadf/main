using Core.NetStandard;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.NetFramework
{
    public class Storage : IStorage
    {
        public static readonly Storage Instance = new Storage();

        protected Storage()
        {
        }

        public T Read<T>(string resourceName)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(GetResourceFilePath(resourceName)));
        }

        public void Write<T>(string resourceName, T resource)
        {
            File.WriteAllText(GetResourceFilePath(resourceName), JsonConvert.SerializeObject(resource));
        }

        public string GetResourceFilePath(string resourceName)
        {
            return Path.Combine(ResourceDirectoryPath, resourceName);
        }

        public string ResourceDirectoryPath
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            }
        }
    }
}
