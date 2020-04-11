using System.Drawing;

namespace Labs.Agents.Demo
{
    public class DemoAgentState : AgentState2<DemoAgent, DemoAgentState>
    {
        public bool IsPossible(Action2 action) => Field.Environment[Field.X + action.X, Field.Y + action.Y].IsEmpty;
    }
}
