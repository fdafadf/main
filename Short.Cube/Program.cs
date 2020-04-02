using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Short.Cube
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Graph g = new Graph();
            Factory factory1 = new Factory();
            Factory factory2 = new Factory();
            Store store1 = new Store();
            Road road1 = new Road();
            Road road2 = new Road();

            //g.AddNode(factory1);
            //g.AddNode(factory2);
            //g.AddNode(store1);
            factory1.AddConnection(road1, store1);
            factory1.AddConnection(road2, store2);

            var factories = g.GetNodes<Factory>();
            var typedConnection1 = factories.First().GetConnections<Road, Factory>().First();
            var typedConnection2 = factories.First().GetConnections<Road, Store>().First();
            Factory f = typedConnection2.Source;
            Store s = typedConnection2.Destination;
            var genericConnection = factories.First().GetConnections<Road>().First();
            //genericConnection as IConnection<>

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CubeForm());
        }
    }

    public interface IMine : INode<IMine>
    {

    }

    public interface IFactory : INode<IFactory>
    {

    }

    public interface IConnection<TSource, TEdge, TDestination>
    {
        TSource Source { get; }
        TEdge Edge { get; }
        TDestination Destination { get; }
    }

    public interface INode<TNode> : INode
    {
        IEnumerable<IConnection<TNode, TEdge, TDestination>> GetConnections<TEdge, TDestination>();
    }

    public interface INode
    {

    }

    public interface IEdge
    {

    }

    public class Store : Node<Store>
    {
        public int Capacity;
    }

    public class Factory : Node<Factory>
    {
        // te atrybuty są reprezentowane przez Order
        //public int ProductionCapacity;
        //public int ProductionCycleInHours;
        //public int StorageSize;
    }

    public class Order
    {
        public Factory Factory;
        public int Size;
    }

    public class Road
    {
        public int Length;
    }

    public class Map
    {
        public ICollection<Store> Stores;
        public ICollection<Factory> Factories;
    }

    public class Node<TNode> : INode<TNode>
    {
        //public ICollection<Connection> Connections;

        //public IEnumerable<ICollection> GetConnections
        public IEnumerable<IConnection<TNode, TEdge, TDestination>> GetConnections<TEdge, TDestination>()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IConnection<TNode, TEdge, INode>> GetConnections<TEdge>()
        {
            throw new NotImplementedException();
        }

        public void AddConnection(TEdge edge, )
    }

    public class Connection
    {
        //Node From;
        Road Road;
        //Node To;
    }

    public class Graph
    {
        private List<INode> Nodes;

        public IEnumerable<TNode> GetNodes<TNode>() where TNode : INode<TNode>
        {
            return null;
        }

        public void AddNode<TNode>(TNode node) where TNode : INode<TNode>
        {
            Nodes.Add(node);
        }
    }
}
