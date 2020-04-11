using Games.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

namespace Labs.Agents
{
    public abstract class Environment<TEnvironment, TAgent, TState, TIteraction>
        where TAgent : IAgent<TEnvironment, TAgent, TState>
        where TState : AgentState<TEnvironment, TAgent, TState>
    {
        public readonly int Width;
        public readonly int Height;
        protected readonly EnvironmentField<TEnvironment, TAgent, TState>[,] fields;
        EnvironmentField<TEnvironment, TAgent, TState> fieldOutside;
        
        public Environment(int width, int height)
        {
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

        protected abstract EnvironmentField<TEnvironment, TAgent, TState> CreateField(int x, int y);

        public EnvironmentField<TEnvironment, TAgent, TState> this[int x, int y]
        {
            get
            {
                return fields.IsOutside(x, y) ? fieldOutside : fields[x, y];
            }
        }

        public void Add(TAgent agent, Point point)
        {
            if (this[point.X, point.Y].IsEmpty)
            {
                agent.State.Field = fields[point.X, point.Y];
                agent.State.Field.Agent = agent;
            }
            else
            {
                throw new Exception();
            }
        }

        public abstract void Apply(IEnumerable<TIteraction> iteractions);

        public Point GetRandomUnusedPosition(Random random)
        {
            Point point = new Point();

            do
            {
                point.X = random.Next(fields.GetLength(0));
                point.Y = random.Next(fields.GetLength(1));
            }
            while (this[point.X, point.Y].IsEmpty == false);

            return point;
        }
    }
}
