using Labs.Agents;
using System;
using DemoEnvironment = Labs.Agents.Environment2<Labs.Agents.Demo.DemoAgent, Labs.Agents.Demo.DemoAgentState>;

namespace Labs.Agents.Demo
{
    public class DemoAgent : IAgent<Environment2<DemoAgent, DemoAgentState>, DemoAgent, DemoAgentState>
    {
        public DemoEnvironment Environment { get; }
        public DemoAgentState State { get; }

        public DemoAgent(DemoEnvironment environment)
        {
            Environment = environment;
            State = new DemoAgentState();
        }

        public Action2 GetAction()
        {
            return GetRandomAction();
            //return GetLegalRandomAction();
        }

        public Action2 GetRandomAction()
        {
            return Environment.Random.Next(Action2.All);
        }

        public Action2 GetLegalRandomAction()
        {
            Action2 action;

            do
            {
                action = Environment.Random.Next(Action2.All);
            }
            while (State.IsPossible(action) == false);

            return action;
        }
    }
}
