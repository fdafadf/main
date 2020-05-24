using System.Linq;

namespace Labs.Agents
{
    public class SimulationTemplate
    {
        public SimulationTemplateDefinition Definition { get; }
        ISpaceTemplateFactory SpaceTemplateFactory;
        ISimulationPluginFactory SimulationPluginFactory;

        public SimulationTemplate(SimulationTemplateDefinition definition)
        {
            Definition = definition;
            SpaceTemplateFactory = Definition.GetSpaceTemplateFactory();
            SimulationPluginFactory = Definition.GetSimulationPluginFactory();
        }

        public ISimulation CreateSimulation()
        {
            var simulationPlugin = SimulationPluginFactory.CreatePlugin();
            var method = GetType().GetMethods().Where(m => m.Name == nameof(CreateSimulation)).Skip(1).First();
            method = method.MakeGenericMethod(simulationPlugin.GetType(), simulationPlugin.AgentType);
            return method.Invoke(this, new object[] { simulationPlugin, SimulationPluginFactory.Name }) as ISimulation;
        }

        public ISimulation CreateSimulation<TPlugin, TAgent>(TPlugin simulationPlugin, string pluginName)
            where TPlugin : SimulationPlugin<DestructibleInteractiveSpace<CardinalMovementSpace<TAgent>, TAgent>, TAgent>
            where TAgent : IAnchoredAgent<TAgent>, IInteractiveAgent<CardinalMovement, InteractionResult>, IDestructibleAgent, IGoalAgent
        {
            return new SimulationModel1<TPlugin, TAgent>(SpaceTemplateFactory, simulationPlugin, pluginName, Definition.Iterations, Definition.AgentsCollisionModel, Definition.AgentDestructionModel);
        }

        public ISimulationViualisation CreateSimulationForm()
        {
            var simulationPlugin = SimulationPluginFactory.CreatePlugin();
            var method = GetType().GetMethods().Where(m => m.Name == nameof(CreateSimulationForm)).Skip(1).First();
            method = method.MakeGenericMethod(simulationPlugin.GetType(), simulationPlugin.AgentType);
            return method.Invoke(this, new object[] { simulationPlugin, SimulationPluginFactory.Name }) as ISimulationViualisation;
        }

        public ISimulationViualisation CreateSimulationForm<TPlugin, TAgent>(TPlugin simulationPlugin, string pluginName)
            where TPlugin : SimulationPlugin<DestructibleInteractiveSpace<CardinalMovementSpace<TAgent>, TAgent>, TAgent>
            where TAgent : IAnchoredAgent<TAgent>, IInteractiveAgent<CardinalMovement, InteractionResult>, IDestructibleAgent, IGoalAgent
        {
            var simulation = new SimulationModel1<TPlugin, TAgent>(SpaceTemplateFactory, simulationPlugin, pluginName, Definition.Iterations, Definition.AgentsCollisionModel, Definition.AgentDestructionModel);
            return new SimulationModel1Visualisation<TPlugin, TAgent>(simulation, Definition.AnimationInterval);
        }

        public override string ToString()
        {
            return $"{Definition} - {SimulationPluginFactory}";
        }
    }
}
