using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Labs.Agents
{
    public class AgentIteractionCollection<TAgent, TAction> : IEnumerable<AgentIteraction<TAgent, TAction>>
    {
        AgentIteraction<TAgent, TAction>[] Items;

        public AgentIteractionCollection(IEnumerable<TAgent> agents)
        {
            Items = agents.Select(agent => new AgentIteraction<TAgent, TAction>(agent)).ToArray();
        }

        public IEnumerator<AgentIteraction<TAgent, TAction>> GetEnumerator()
        {
            return Items.Cast<AgentIteraction<TAgent, TAction>>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }
    }
}
