namespace Games.Utilities
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
}
