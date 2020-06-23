using Labs.Agents.Forms;
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
            return CreateSimulation(SimulationPluginFactory.CreatePlugin(), SimulationPluginFactory.Name, SpaceTemplateFactory, Definition.Model);
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

        public SimulationForm CreateSimulationForm()
        {
            return CreateSimulationForm(SimulationPluginFactory.CreatePlugin(), SimulationPluginFactory.Name, SpaceTemplateFactory, Definition.Model, Definition.AnimationInterval);
        }

        public static SimulationForm CreateSimulationForm(SimulationPlugin simulationPlugin, string pluginName, ISpaceTemplateFactory spaceTemplateFactory, SimulationModelConfiguration modelConfiguration, int animationInterval)
        {
            var method = typeof(SimulationTemplate).GetMethods(BindingFlags.Static | BindingFlags.Public).Where(m => m.Name == nameof(CreateSimulationForm)).Skip(1).First();
            method = method.MakeGenericMethod(simulationPlugin.GetType(), simulationPlugin.AgentType);
            return method.Invoke(null, new object[] { simulationPlugin, pluginName, spaceTemplateFactory, modelConfiguration, animationInterval }) as SimulationForm;
        }

        public static SimulationForm CreateSimulationForm<TPlugin, TAgent>(TPlugin simulationPlugin, string pluginName, ISpaceTemplateFactory spaceTemplateFactory, SimulationModelConfiguration modelConfiguration, int animationInterval)
            where TPlugin : SimulationPlugin<DestructibleInteractiveSpace<CardinalMovementSpace<TAgent>, TAgent>, TAgent>
            where TAgent : IAnchoredAgent<TAgent>, IInteractiveAgent<CardinalMovement, InteractionResult>, IDestructibleAgent, IGoalAgent
        {
            var simulation = new SimulationModel1<TPlugin, TAgent>(spaceTemplateFactory, simulationPlugin, pluginName, modelConfiguration);
            var form = new SimulationForm();
            form.Simulation = simulation;
            var width = simulation.Space.InteractiveSpace.Width;
            var height = simulation.Space.InteractiveSpace.Height;
            var obstaclesLayer = new BitmapLayer(form.Space, simulation.Space.InteractiveSpace.GetObstacles());
            var agentsLayer = new AnimatedLayer(form.Space, width, height);
            var goalsLayer = new AnimatedLayer(form.Space, width, height);

            void OnAgentCreated(TAgent agent)
            {
                var agentOnLayer = new AgentLayerObject<TAgent>(agent);
                agentsLayer.Objects.Add(agentOnLayer);
                goalsLayer.Objects.Add(new GoalLayerObject<TAgent>(agentOnLayer));
            }

            void OnAgentRemoved(TAgent agent)
            {
                var agentOnLayer = agentsLayer.Objects.OfType<AgentLayerObject<TAgent>>().FirstOrDefault(o => o.Agent.Equals(agent));
                agentsLayer.Objects.Remove(agentOnLayer);
                var goalOnLayer = goalsLayer.Objects.OfType<GoalLayerObject<TAgent>>().FirstOrDefault(o => o.Agent.Equals(agentOnLayer));
                goalsLayer.Objects.Remove(goalOnLayer);
            }

            simulation.Agents.ForEach(OnAgentCreated);
            simulation.AgentCreated += OnAgentCreated;
            simulation.AgentRemoved += OnAgentRemoved;
            simulationPlugin.OnSimulationCreated(form, simulation);
            return form;
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
