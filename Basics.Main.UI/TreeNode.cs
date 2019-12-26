using System.Collections.Generic;

namespace Basics.Main
{

    public class NamedObject<T>
    {
        public string Name { get; set; }
        public T Value { get; set; }

        public NamedObject(string name, T value)
        {
            Name = name;
            Value = value;
        }

        public override string ToString()
        {
            return Name;
        }
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
