using System;
using System.Collections.Generic;
using System.Drawing;

namespace Labs.Agents
{
    public interface IEnvironment
    {
        Random Random { get; }
        int Width { get; }
        int Height { get; }
        void AddObstacle(int x, int y, int width, int height);
        Point GetRandomUnusedPosition();
    }

    public interface IEnvironment<TEnvironment, TAgent, TState> : IEnvironment
        where TAgent : IAgent<TEnvironment, TAgent, TState>
        where TState : AgentState<TEnvironment, TAgent, TState>
    {
        IEnumerable<TAgent> Agents { get; }
        IEnvironmentField<TEnvironment, TAgent, TState> this[int x, int y] { get; }
    }

}
