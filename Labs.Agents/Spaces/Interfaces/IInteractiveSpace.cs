using System.Collections.Generic;

namespace Labs.Agents
{
    public interface IInteractiveSpace<TAgent>
    {
        void Interact(IEnumerable<TAgent> agents);
    }
}
