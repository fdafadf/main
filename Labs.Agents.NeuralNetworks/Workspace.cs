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
        public readonly DirectoryInfo NetworksDirectory;
        public readonly DirectoryInfo NeuralTrainingsDirectory;

        private Workspace()
        {
            NetworksDirectory = Settings.ProductDirectory.GetDirectory("Neural Networks").Ensure();
            NeuralTrainingsDirectory = Settings.ProductDirectory.GetDirectory("Neural Trainings").Ensure();
        }

        public void Add(string name, AgentNetwork network) => SaveNetwork(name, network, false);

        public FileInfo GetNetworkFile(string name)
        {
            return NetworksDirectory.GetFile($"{name}.model");
        }

        public void SaveNetwork(string name, AgentNetwork network, bool overwrite)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"Name can't be empty.");
            }

            var fileName = GetNetworkFileName(name);
            var networkFile = NetworksDirectory.GetFile(fileName);

            if (networkFile.Exists && overwrite == false)
            {
                throw new ArgumentException($"File '{fileName}' already exists.");
            }
            else
            {
                network.Save(networkFile);
            }
        }

        Dictionary<string, string> networksDescriptions = new Dictionary<string, string>();

        public string GetNetworkDescription(string name)
        {
            if (networksDescriptions.TryGetValue(name, out string description) == false)
            {
                description = networksDescriptions[name] = new AgentNetwork(GetNetworkFile(name)).ToString();
            }

            return description;
        }

        private string GetNetworkFileName(string name) => $"{name}.model";
    }
}
