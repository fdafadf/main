using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Labs.Agents
{
    class AgentInteractionCollection<TAgent, TAction, TInteraction> : IEnumerable<TInteraction>
        where TInteraction : AgentInteraction<TAgent, TAction>
    {
        TInteraction[] Items;

        public AgentInteractionCollection(IEnumerable<TAgent> agents)
        {
            Items = agents.Select(agent => new AgentInteraction<TAgent, TAction>(agent)).ToArray();
        }

        public IEnumerator<TInteraction> GetEnumerator()
        {
            return Items.Cast<TInteraction>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }
    }
}
