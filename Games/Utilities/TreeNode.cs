using System;
using System.Collections.Generic;

namespace Games.Utilities
{
    public static class Extensions2
    {
        public static IEnumerable<TAttribute> Flatten<TNode, TEdge, TAttribute>(this TNode self, Func<TNode, TAttribute> attribute) where TNode : TreeNode<TNode, TEdge>
        {
            List<TAttribute> result = new List<TAttribute>();
            self.Flatten<TNode, TEdge, TAttribute>(attribute, result);
            return result;
        }

        private static void Flatten<TNode, TEdge, TAttribute>(this TNode self, Func<TNode, TAttribute> attribute, List<TAttribute> result) where TNode : TreeNode<TNode, TEdge>
        {
            result.Add(attribute(self));

            foreach (TNode child in self.Children.Values)
            {
                child.Flatten<TNode, TEdge, TAttribute>(attribute, result);
            }
        }
    }

    public static class Extensions3
    {
        public static IEnumerable<TAttribute> Flatten<TNode, TData, TAttribute>(this TNode self, Func<TNode, TAttribute> attribute) where TNode : TreeNode<TData>
        {
            List<TAttribute> result = new List<TAttribute>();
            self.Flatten<TNode, TData, TAttribute>(attribute, result);
            return result;
        }

        private static void Flatten<TNode, TData, TAttribute>(this TNode self, Func<TNode, TAttribute> attribute, List<TAttribute> result) where TNode : TreeNode<TData>
        {
            result.Add(attribute(self));

            foreach (TNode child in self.Children)
            {
                child.Flatten<TNode, TData, TAttribute>(attribute, result);
            }
        }
    }

    //public class TreeNode
    //{
    //    public ICollection<TreeNode> Children { get; protected set; }
    //}

    public class TreeNode<TNode, TEdge> where TNode : TreeNode<TNode, TEdge>
    {
        public Dictionary<TEdge, TNode> Children = new Dictionary<TEdge, TNode>();
    }

    public class TreeNode<T>
    {
        public T Data;
        public List<TreeNode<T>> Children = new List<TreeNode<T>>();

        public TreeNode()
        {
        }

        public TreeNode(T data)
        {
            Data = data;
        }

        public IEnumerable<T> FlattenData()
        {
            List<T> result = new List<T>();
            FlattenData(result);
            return result;
        }
        
        private void FlattenData(List<T> result)
        {
            result.Add(Data);
            Children.ForEach(child => child.FlattenData(result));
        }
    }
}
