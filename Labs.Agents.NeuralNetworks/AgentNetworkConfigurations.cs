namespace Labs.Agents.NeuralNetworks
{
    public class AgentNetworkConfigurations
    {
        public static readonly AgentNetworkDefinition Micro1 = new AgentNetworkDefinition("Micro1", 1, 12);
        public static readonly AgentNetworkDefinition Small3 = new AgentNetworkDefinition("Small3", 3, 53, 14);
        public static readonly AgentNetworkDefinition Medium3 = new AgentNetworkDefinition("Medium3", 3, 105, 53, 27);
    }
}
