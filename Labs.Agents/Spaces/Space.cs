using System;

namespace Labs.Agents
{
    public abstract class Space<TAgent> : ISpace where TAgent : IAnchoredAgent<TAgent>
    {
        public int Width { get; }
        public int Height { get; }
        public ISpaceField this[int x, int y] => fields.IsOutside(x, y) ? SpaceField<TAgent>.Outside : fields[x, y];
        protected SpaceField<TAgent>[,] fields;
        protected AgentsCollisionModel AgentsCollisionModel;

        public Space(int width, int height, AgentsCollisionModel agentsCollisionModel)
        {
            Width = width;
            Height = height;
            fields = new SpaceField<TAgent>[width, height];
            AgentsCollisionModel = agentsCollisionModel;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    fields[x, y] = new SpaceField<TAgent>(this, x, y, AgentsCollisionModel == AgentsCollisionModel.Ghost);
                }
            }
        }

        public Space(SpaceTemplate template, AgentsCollisionModel agentsCollisionModel) : this(template.Width, template.Height, agentsCollisionModel)
        {
            AddObstacles(template.Obstacles);
        }

        public AgentAnchor<TAgent> CreateAgentAnchor(TAgent agent, int x, int y)
        {
            var field = fields[x, y];

            if (field.IsEmpty)
            {
                var anchor = new AgentAnchor<TAgent>(agent, field);
                fields[x, y].AddAnchor(anchor);
                return anchor;
            }
            else
            {
                throw new Exception();
            }
        }

        public void AddObstacles(bool[,] obstaclesMap)
        {
            for (int x = 0; x < obstaclesMap.GetLength(0); x++)
            {
                for (int y = 0; y < obstaclesMap.GetLength(1); y++)
                {
                    if (obstaclesMap[x, y])
                    {
                        AddObstacle(x, y, 1, 1);
                    }
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
                    fields[ox, oy].IsObstacle = true;
                }
            }
        }
    }
}
