using Games.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

namespace Labs.Agents
{
    public abstract class Environment<TEnvironment, TAgent, TState, TInteraction> : IEnvironment<TEnvironment, TAgent, TState>
        where TAgent : IAgent<TEnvironment, TAgent, TState>
        where TState : AgentState<TEnvironment, TAgent, TState>
    {
        public Random Random { get; }
        public int Width { get; }
        public int Height { get; }
        public IEnumerable<TAgent> Agents => agents;
        protected readonly List<TAgent> agents = new List<TAgent>();
        protected readonly EnvironmentField<TEnvironment, TAgent, TState>[,] fields;
        EnvironmentField<TEnvironment, TAgent, TState> fieldOutside;
        
        public Environment(Random random, int width, int height)
        {
            Random = random;
            Width = width;
            Height = height;
            fields = new EnvironmentField<TEnvironment, TAgent, TState>[width, height];
            fieldOutside = CreateField(-1, -1);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    fields[x, y] = CreateField(x, y);
                }
            }
        }

        public abstract void Apply(IEnumerable<TInteraction> iteractions);
        protected abstract TInteraction CreateInteraction(TAgent agent);
        protected abstract EnvironmentField<TEnvironment, TAgent, TState> CreateField(int x, int y);

        public IEnvironmentField<TEnvironment, TAgent, TState> this[int x, int y] => fields.IsOutside(x, y) ? fieldOutside : fields[x, y];
        IEnvironmentField IEnvironment.this[int x, int y] => this[x, y];

        public void AddObstacle(int x, int y, int width, int height)
        {
            int ex = x + width;
            int ey = y + height;

            for (int oy = y; oy < ey; oy++)
            {
                for (int ox = x; ox < ex; ox++)
                {
                    var field = fields[ox, oy];

                    if (field.IsAgent)
                    {
                        throw new Exception();
                    }

                    field.IsObstacle = true;
                    field.IsEmpty = false;
                }
            }
        }

        public virtual TInteraction AddAgent(TAgent agent, Point point)
        {
            var field = fields[point.X, point.Y];

            if (field.IsEmpty)
            {
                agent.State.Field = field;
                field.Agent = agent;
                agents.Add(agent);
                return CreateInteraction(agent);
            }
            else
            {
                throw new Exception();
            }
        }

        public Point GetRandomUnusedPosition()
        {
            Point point = new Point();

            do
            {
                point.X = Random.Next(fields.GetLength(0));
                point.Y = Random.Next(fields.GetLength(1));
            }
            while (this[point.X, point.Y].IsEmpty == false);

            return point;
        }
    }
}
