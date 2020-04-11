using System;
using System.Linq;

namespace Labs.AI.NeuralNetworks.Ants
{
    public class AntNetworkV2Input : AntNetworkInput
    {
        public static readonly int[] HistoryIndexes = { 0 }; //, 3, 5, 8, 13, 21, 34, 55, 89 };
        public static readonly int EncodedItemSize = AgentState.EncodedSize + AgentAction.EncodedSize;
        public static readonly int EncodedSize = (HistoryIndexes.Length + 1) * EncodedItemSize;

        public AntNetworkV2Input(AgentState state, AgentAction action, History<AntNetworkV2Input> history) : base(EncodedSize)
        {
            EncodeState(state, 0);

            if (action != null)
            {
                EncodeAction(action, 0);
            }

            for (int i = 0; i < HistoryIndexes.Length; i++)
            {
                int historyIndex = HistoryIndexes[i];

                if (historyIndex < history.Items.Count)
                {
                    Encode(history.Items.ElementAt(historyIndex), i + 1);
                }
            }
        }

        public void Encode(History<AntNetworkV2Input>.Item item, int index)
        {
            EncodeState(item.State, index);
            EncodeAction(item.Action, index);
        }

        public AntNetworkV2Input EncodeState(AgentState state, int index)
        {
            Array.Copy(state.Encoded, 0, Encoded, index * EncodedItemSize, AgentState.EncodedSize);
            return this;
        }

        public AntNetworkV2Input EncodeAction(AgentAction action, int index)
        {
            Array.Copy(action.Encoded, 0, Encoded, index * EncodedItemSize + AgentState.EncodedSize, AgentAction.EncodedSize);
            return this;
        }

        public override AntNetworkInput EncodeAction(AgentAction action)
        {
            return EncodeAction(action, 0);
        }
    }
}
