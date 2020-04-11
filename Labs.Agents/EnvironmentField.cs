namespace Labs.Agents
{
    public class EnvironmentField<TEnvironment, TAgent, TState>
        where TAgent : IAgent<TEnvironment, TAgent, TState>
        where TState : AgentState<TEnvironment, TAgent, TState>
    {
        public readonly TEnvironment Environment;
        public bool IsOutside;
        public bool IsEmpty;
        public bool IsObstacle;
        public bool IsAgent;
        public readonly int X;
        public readonly int Y;
        TAgent agent;

        public EnvironmentField(TEnvironment environment, int x, int y)
        {
            Environment = environment;
            X = x;
            Y = y;
            IsOutside = x == -1;
            IsEmpty = true;
            IsObstacle = false;
            IsAgent = false;
        }

        public TAgent Agent
        {
            get
            {
                return agent;
            }
            internal set
            {
                agent = value;

                if (agent == null)
                {
                    IsEmpty = true;
                    IsAgent = false;
                }
                else
                {
                    IsEmpty = false;
                    IsAgent = true;
                }
            }
        }
    }
}
