using System.Collections.Generic;

namespace Labs.AI.NeuralNetworks.Ants
{
    public class History<TInput>
    {
        public LinkedList<Item> Items = new LinkedList<Item>();
        public int Capacity = 100000;

        public void Add(AgentState state, AgentState nextState, AgentAction action, double reward, TInput input)
        {
            Item item;

            if (Items.Count == Capacity)
            {
                item = Items.Last.Value;
                item.State = state;
                item.NextState = nextState;
                item.Input = input;
                item.Action = action;
                item.Reward = reward;
                Items.RemoveLast();
            }
            else
            {
                item = new Item(state, nextState, action, reward, input);
            }

            Items.AddFirst(item);
        }

        public class Item
        {
            public AgentState State;
            public AgentState NextState;
            public AgentAction Action;
            public double Reward;
            public TInput Input;

            internal Item(AgentState state, AgentState nextState, AgentAction action, double reward, TInput input)
            {
                State = state;
                NextState = nextState;
                Action = action;
                Reward = reward;
                Input = input;
            }
        }
    }
}
