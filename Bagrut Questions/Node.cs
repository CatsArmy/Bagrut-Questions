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
            if (this is null)
            {
                return string.Empty;
            }
            string str = $"{this.Value}";
            int count = 1;
            Node<T> i;
            for (i = this.GetNext(); !(i is null); i = i.GetNext())
            {
                str += $", {i.Value}";
                count++;
            }
            return $"[{str} Length: {count}]";
        }
        public string ToPlainString(bool AddWhitespaces = false)
        {
            string plainString = string.Empty;
            Node<T> i;
            for (i = this; !(i is null); i = i.GetNext())
            {
                plainString += $"{i.Value}{(AddWhitespaces ? " " : string.Empty)}";
            }
            return plainString;
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

        public bool AllEquals(Node<T> other)
        {
            //expecting both nodes to have the same length
            for (var i = this; !(i is null) && !(other is null); i = i.GetNext(), other = other.GetNext())
            {
                if (!(i.Value.Equals(other.Value)))
                {
                    return false;
                }
            }
            return true;

        }
    }
}
