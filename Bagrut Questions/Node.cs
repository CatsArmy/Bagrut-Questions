
namespace Node
{
    public class Node<T>
    {
        private T Value;
        private Node<T> Next;
        public void SetValue(T value)
        {
            this.Value = value;
        }
        public T GetValue()
        {
            return this.Value;
        }
        public void SetNext(Node<T> node)
        {
            this.Next = node;
        }
        public Node<T> GetNext()
        {
            return this.Next;
        }

        public bool HasNext() => !(this.Next is null);
        public override string ToString()
        {
            string str = string.Empty;
            Node<T> i;
            for (i = this; i.HasNext(); i = i.GetNext())
            {
                str += $"{i.Value}, ";
            }
            return $"[{str}{i.Value}]";
        }
        public string ToPlainString(bool AddWhitespaces = false)
        {
            string str = string.Empty;
            Node<T> i;
            for (i = this; !(i is null); i = i.GetNext())
            {
                str += $"{i.Value}{(AddWhitespaces ? " " : string.Empty)}";
            }
            return str;
        }
        public Node(T value, Node<T> next = null)
        {
            this.Value = value;
            this.Next = next;
        }
        public Node(Node<T> node)
        {
            this.Value = node.Value;
            this.Next = node.Next is null ? null : new Node<T>(node.Next);
        }
    }
}
