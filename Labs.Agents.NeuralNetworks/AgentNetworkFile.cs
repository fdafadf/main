using System;
using System.IO;

namespace Labs.Agents.NeuralNetworks
{
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

        public void Random(int seed)
        {
            var network = Load();
            network.InitializeLayers(seed);
            Save(network);
        }
    }
}
