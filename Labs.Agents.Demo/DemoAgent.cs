using Labs.Agents;

namespace Labs.Agents.Demo
{
    public class DemoAgent : IAgent<Environment2<DemoAgent, DemoAgentState>, DemoAgent, DemoAgentState>
    {
        public DemoSimulation Simulation { get; }
        public DemoAgentState State { get; }

        public DemoAgent(DemoSimulation simulation)
        {
            Simulation = simulation;
            State = new DemoAgentState();
        }

        public Action2 GetAction()
        {
            return GetRandomAction();
            //return GetLegalRandomAction();
        }

        public Action2 GetRandomAction()
        {
            return Simulation.Random.Next(Action2.All);
        }

        public Action2 GetLegalRandomAction()
        {
            Action2 action;

            do
            {
                action = Simulation.Random.Next(Action2.All);
            }
            while (State.IsPossible(action) == false);

            return action;
        }
    }
}
