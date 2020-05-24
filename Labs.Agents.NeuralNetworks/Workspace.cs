using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Labs.Agents.NeuralNetworks
{
    public class Workspace : Agents.Workspace
    {
        public static readonly Workspace Instance;

        static Workspace()
        {
            Instance = new Workspace();
            Instance.Load();
        }

        public IEnumerable<string> NetworkNames => NetworksDirectory.EnumerateFiles2("*.model").Select(file => Path.GetFileNameWithoutExtension(file.Name));
        DirectoryInfo NetworksDirectory;

        private Workspace()
        {
            NetworksDirectory = Settings.ProductDirectory.GetDirectory("Networks").Ensure();
        }

        public void Add(string name, AgentNetwork network)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"Name can't be empty.");
            }

            var fileName = $"{name}.model";
            var networkFile = NetworksDirectory.GetFile(fileName);

            if (networkFile.Exists)
            {
                throw new ArgumentException($"File '{fileName}' already exists.");
            }
            else
            {
                network.Save(networkFile);
            }
        }

        public AgentNetworkFile GetNetworkFile(string name)
        {
            return new AgentNetworkFile(NetworksDirectory.GetFile($"{name}.model"));
        }

        Dictionary<string, string> networksDescriptions = new Dictionary<string, string>();

        public string GetNetworkDescription(string name)
        {
            if (networksDescriptions.TryGetValue(name, out string description) == false)
            {
                description = networksDescriptions[name] = GetNetworkFile(name).Load().ToString();
            }

            return description;
        }
    }

    public class AgentNetworkFile
    {
        FileInfo File;

        public AgentNetworkFile(FileInfo file)
        {
            File = file;

            if (File.Exists == false)
            {
                throw new ArgumentException();
            }
        }

        public AgentNetwork Load()
        {
            return new AgentNetwork(File);
        }

        public void Save(AgentNetwork network)
        {
            network.Save(File);
        }
    }
}
