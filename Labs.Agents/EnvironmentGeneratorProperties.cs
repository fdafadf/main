using System.ComponentModel;

namespace Labs.Agents
{
    public class EnvironmentGeneratorProperties
    {
        [Category("Size")]
        public int Width { get; set; }
        [Category("Size")]
        public int Height { get; set; }
        [Category("Generator")]
        public int Seed { get; set; }
        [Category("Generator")]
        public int NumberOfAgents { get; set; }
        [Category("Generator")]
        public int NumberOfObstacles { get; set; }
        [Category("Generator")]
        public int ObstacleMinSize { get; set; }
        [Category("Generator")]
        public int ObstacleMaxSize { get; set; }

        public EnvironmentGeneratorProperties()
        {
            Width = 200;
            Height = 150;
            NumberOfAgents = 5;
            NumberOfObstacles = 80;
            ObstacleMinSize = 3;
            ObstacleMaxSize = 20;
            Seed = 0;
        }
    }
}
