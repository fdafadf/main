using AI.NeuralNetworks;
using AI.NeuralNetworks.Games;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pathfinder
{
    public partial class Form1 : Form
    {
        public Network Network;
        GraphPainter graphPainter;
        GraphPathPainter dijkstraPathPainter;
        GraphPathPainter networkPathPainter;
        Pen dijkstraPathPen = new Pen(Brushes.Green, 4);
        Pen networkPathPen = new Pen(Brushes.Red, 8);
        GraphPath[] graphs;

        public Form1()
        {
            InitializeComponent();
        }


        public GraphPath[] Graphs 
        { 
            get 
            { 
                return graphs; 
            }
            set 
            {
                graphs = value;
                selectedGraphIndex = 0;
                OnSelectedGraphIndexChanged();
            }
        }
        int selectedGraphIndex = -1;

        protected override void OnPaint(PaintEventArgs e)
        {
            networkPathPainter?.Paint(e.Graphics);
            dijkstraPathPainter?.Paint(e.Graphics);
            graphPainter?.Paint(e.Graphics);
        }

        protected override void OnResizeEnd(EventArgs e)
        {
            base.OnResizeEnd(e);
            graphPainter.Scale = Math.Min(ClientSize.Width, ClientSize.Height);
            Refresh();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            switch (e.KeyCode)
            {
                case Keys.R:
                    break;
                case Keys.Left:
                    selectedGraphIndex--;

                    if (selectedGraphIndex < 0)
                    {
                        selectedGraphIndex = Graphs.Length - 1;
                    }

                    OnSelectedGraphIndexChanged();
                    break;
                case Keys.Right:
                    selectedGraphIndex = (selectedGraphIndex + 1) % Graphs.Length;
                    OnSelectedGraphIndexChanged();
                    break;
            }
        }

        protected void OnSelectedGraphIndexChanged()
        {
            Text = $"{selectedGraphIndex + 1}/{Graphs.Length}";
            GraphPath graphPath = Graphs[selectedGraphIndex];
            Graph graph = graphPath.Graph;
            graphPainter = new GraphPainter(graphPath);
            graphPainter.Scale = Math.Min(ClientSize.Width, ClientSize.Height);
            dijkstraPathPainter = new GraphPathPainter(graphPainter, graphPath.Path, dijkstraPathPen);

            var input = new NetworkSampleInput(graphPath.Graph, 0);
            bool[] visitStatus = new bool[graph.Vertices.Length];
            visitStatus[0] = true;
            int currentVertex = 0;
            List<int> path = new List<int>();
            path.Add(currentVertex);

            do
            {
                visitStatus[currentVertex] = true;
                input.SetCurrentVertex(currentVertex);
                double[] prediction = Network.Evaluate(input.Values);
                currentVertex = prediction.IndexOfMax(0);
                path.Add(currentVertex);
            }
            while (visitStatus[currentVertex] == false && currentVertex != graph.Vertices.Length - 1);

            networkPathPainter = new GraphPathPainter(graphPainter, path.ToArray(), networkPathPen);
            Refresh();
        }

        //protected void GenerateGraph(Random random)
        //{
        //    graphPainter = new GraphPainter(GraphGenerator.Generate(random, graphSize));
        //    graphPainter.Scale = Math.Min(ClientSize.Width, ClientSize.Height) - margin * 2;
        //    graphPainter.Path = Dijkstra.Find(graphPainter.Graph);
        //}
    }
}
