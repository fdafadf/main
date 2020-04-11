using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Labs.Agents
{
    public class AgentInteractionCollection<TAgent, TAction> : IEnumerable<AgentInteraction<TAgent, TAction>>
    {
        AgentInteraction<TAgent, TAction>[] Items;

        public AgentInteractionCollection(IEnumerable<TAgent> agents)
        {
            Items = agents.Select(agent => new AgentInteraction<TAgent, TAction>(agent)).ToArray();
        }

        public IEnumerator<AgentInteraction<TAgent, TAction>> GetEnumerator()
        {
            return Items.Cast<AgentInteraction<TAgent, TAction>>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }
    }
}
