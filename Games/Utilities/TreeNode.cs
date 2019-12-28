using System.Collections.Generic;

namespace Games.Utilities
{
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

        public IEnumerable<T> Flatten()
        {
            List<T> result = new List<T>();
            Flatten(result);
            return result;
        }

        private void Flatten(List<T> result)
        {
            result.Add(Data);
            Children.ForEach(child => child.Flatten(result));
        }
    }
}
