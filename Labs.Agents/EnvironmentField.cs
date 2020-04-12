namespace Labs.Agents
{
    public class EnvironmentField<TEnvironment, TAgent, TState> : IEnvironmentField<TEnvironment, TAgent, TState>
        where TAgent : IAgent<TEnvironment, TAgent, TState>
        where TState : AgentState<TEnvironment, TAgent, TState>
    {
        public TEnvironment Environment { get; }
        public bool IsOutside { get; internal set; }
        public bool IsEmpty { get; internal set; }
        public bool IsObstacle { get; internal set; }
        public bool IsAgent { get; internal set; }
        public int X { get; }
        public int Y { get; }
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
