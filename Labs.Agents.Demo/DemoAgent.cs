using Labs.Agents;
using System;
///using DemoEnvironment = Labs.Agents.Environment2<Labs.Agents.Demo.DemoAgent>;

namespace Labs.Agents.Demo
{
    public class DemoAgent 
    {
        //public DemoEnvironment Environment { get; }
        //public DemoAgentState State { get; }
        //
        //public DemoAgent(DemoEnvironment environment)
        //{
        //    Environment = environment;
        //    State = new DemoAgentState();
        //}

        public CardinalMovement GetAction()
        {
            return GetRandomAction();
            //return GetLegalRandomAction();
        }

        public CardinalMovement GetRandomAction()
        {
            return null; // Environment.Random.Next(Action2.All);
        }

        public CardinalMovement GetLegalRandomAction()
        {
            return null;
            //Action2 action;
            //
            //do
            //{
            //    action = null; // Environment.Random.Next(Action2.All);
            //}
            //while (State.IsPossible(action) == false);
            //
            //return action;
        }
    }
}
