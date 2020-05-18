using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Labs.Agents
{
    public class WorkspaceItemsDirectory<T> : IEnumerable<T> where T : class, INamed
    {
        DirectoryInfo Directory;

        public WorkspaceItemsDirectory(DirectoryInfo directory)
        {
            Directory = directory;
        }

        public void Add(T item)
        {
            var fileName = $"{item.Name}.json";
            var file = Directory.Ensure().GetFile(fileName);

            if (file.Exists)
            {
                throw new ArgumentException($"File '{fileName}' already exists.");
            }
            else
            {
                file.Ensure().Serialize(item);
            }
        }

        public bool Remove(T item)
        {
            var fileName = $"{item.Name}.json";
            var file = Directory.Ensure().GetFile(fileName);

            if (file.Exists)
            {
                file.Delete();
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Directory.EnumerateFiles2("*.json").Select(file => file.Deserialize<T>()).Where(item => item != null).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
