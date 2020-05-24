using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Labs.Agents
{
    public class WorkspaceItemsDirectory<T> : IEnumerable<T> where T : class, INamed
    {
        public DirectoryInfo Directory { get; }
        PropertyInfo WorkspaceInjectionProperty;
        Workspace Workspace;
        string FileNameTemplate = "{0}.json";

        public WorkspaceItemsDirectory(Workspace workspace, DirectoryInfo directory, string fileNameTemplate) : this(workspace, directory)
        {
            FileNameTemplate = fileNameTemplate;
        }

        public WorkspaceItemsDirectory(Workspace workspace, DirectoryInfo directory)
        {
            Workspace = workspace;
            Directory = directory;
            WorkspaceInjectionProperty = typeof(T).GetProperty("Workspace", BindingFlags.Public | BindingFlags.Instance);
        }

        public void Add(T item)
        {
            var fileName = string.Format(FileNameTemplate, item.Name);
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
            var fileName = string.Format(FileNameTemplate, item.Name);
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
            return Directory.EnumerateFiles2(string.Format(FileNameTemplate, "*"))
                .Select(file => file.Deserialize<T>())
                .Where(item => item != null)
                .Select(InjectWorkspace)
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private T InjectWorkspace(T item)
        {
            WorkspaceInjectionProperty?.SetValue(item, Workspace);
            return item;
        }
    }
}
