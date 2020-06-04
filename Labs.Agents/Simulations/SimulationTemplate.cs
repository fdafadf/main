using System;
using System.Linq;
using System.Reflection;

namespace Labs.Agents
{
    public class SimulationTemplate
    {
        public SimulationTemplateDefinition Definition { get; }
        public ISpaceTemplateFactory SpaceTemplateFactory;
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
            return CreateSimulation(simulationPlugin, SimulationPluginFactory.Name, SpaceTemplateFactory, Definition.Model);
        }

        public static ISimulation CreateSimulation(SimulationPlugin plugin, string pluginName, ISpaceTemplateFactory spaceTemplateFactory, SimulationModelConfiguration modelConfiguration)
        {
            var method = typeof(SimulationTemplate).GetMethods(BindingFlags.Static| BindingFlags.Public).Where(m => m.Name == nameof(CreateSimulation)).Skip(1).First();
            method = method.MakeGenericMethod(plugin.GetType(), plugin.AgentType);
            return method.Invoke(null, new object[] { plugin, pluginName, spaceTemplateFactory, modelConfiguration }) as ISimulation;
        }

        public static ISimulation CreateSimulation<TPlugin, TAgent>(TPlugin simulationPlugin, string pluginName, ISpaceTemplateFactory spaceTemplateFactory, SimulationModelConfiguration modelConfiguration)
            where TPlugin : SimulationPlugin<DestructibleInteractiveSpace<CardinalMovementSpace<TAgent>, TAgent>, TAgent>
            where TAgent : IAnchoredAgent<TAgent>, IInteractiveAgent<CardinalMovement, InteractionResult>, IDestructibleAgent, IGoalAgent
        {
            return new SimulationModel1<TPlugin, TAgent>(spaceTemplateFactory, simulationPlugin, pluginName, modelConfiguration);
        }

        public ISimulationViualisation CreateSimulationForm()
        {
            var simulationPlugin = SimulationPluginFactory.CreatePlugin();
            var method = GetType().GetMethods(BindingFlags.Static | BindingFlags.Public).Where(m => m.Name == nameof(CreateSimulationForm)).First();
            method = method.MakeGenericMethod(simulationPlugin.GetType(), simulationPlugin.AgentType);
            return method.Invoke(this, new object[] { simulationPlugin, SimulationPluginFactory.Name, SpaceTemplateFactory, Definition.Model, Definition.AnimationInterval }) as ISimulationViualisation;
        }

        public static ISimulationViualisation CreateSimulationForm<TPlugin, TAgent>(TPlugin simulationPlugin, string pluginName, ISpaceTemplateFactory spaceTemplateFactory, SimulationModelConfiguration modelConfiguration, int animationInterval)
            where TPlugin : SimulationPlugin<DestructibleInteractiveSpace<CardinalMovementSpace<TAgent>, TAgent>, TAgent>
            where TAgent : IAnchoredAgent<TAgent>, IInteractiveAgent<CardinalMovement, InteractionResult>, IDestructibleAgent, IGoalAgent
        {
            var simulation = new SimulationModel1<TPlugin, TAgent>(spaceTemplateFactory, simulationPlugin, pluginName, modelConfiguration);
            return new SimulationModel1Visualisation<TPlugin, TAgent>(simulation, animationInterval);
        }

        public SimulationTemplate Clone()
        {
            return null;
        }

        public override string ToString()
        {
            return $"{Definition} - {SimulationPluginFactory}";
        }
    }
}
