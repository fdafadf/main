using System.Collections.Generic;

namespace Labs.AI.NeuralNetworks.Ants
{
    public class History
    {
        public LinkedList<Item> Items = new LinkedList<Item>();
        public int Capacity = 100000;

        public void Add(AgentState state, Agent item2, double[] input)
        {
            Item item;

            if (Items.Count == Capacity)
            {
                item = Items.Last.Value;
                Items.RemoveLast();
            }
            else
            {
                item = new Item(state, item2.State, item2.Action, item2.Reward, input);
            }

            Items.AddFirst(item);
        }

        public class Item
        {
            public AgentState State;
            public AgentState NextState;
            public AgentAction Action;
            public double Reward;
            public double[] Input;

            internal Item(AgentState state, AgentState nextState, AgentAction action, double reward, double[] input)
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
