namespace Node
{
    public static class NodeUtils
    {
        //public static bool HasNext<T>(this Node<T> node) => !(node.GetNext() is null);
        public static Node<T> GetReversed<T>(this Node<T> node)
        {
            if (!node.HasNext())
            {
                return new Node<T>(node.GetValue());
            }
            Node<T> revNode = node.GetNext().GetReversed();
            revNode.Goto().SetNext(new Node<T>(node.GetValue()));
            return revNode;
        }
        public static Node<T> Goto<T>(this Node<T> node, int i)
        {
            if (i <= 0 || !node.HasNext())
            {
                return node;
            }
            return node.GetNext().Goto(--i);
        }
        public static Node<T> Goto<T>(this Node<T> node, Node<T> i)
        {
            if (node == i || !node.HasNext())
            {
                return node;
            }
            return node.GetNext().Goto(i);
        }
        public static Node<T> Goto<T>(this Node<T> node)
        {
            if (!node.HasNext())
            {
                return node;
            }
            return node.GetNext().Goto();
        }
        public static Node<T> Insert<T>(this Node<T> node, T value)
        {
            Node<T> GoTo = node.Goto();
            GoTo.SetNext(new Node<T>(value));
            return node;
        }
        public static Node<T> Insert<T>(this Node<T> node, Node<T> i, T value)
        {
            Node<T> GoTo = node.Goto(i);
            GoTo.SetNext(new Node<T>(value));
            return node;
        }
        public static Node<T> Insert<T>(this Node<T> node, int i, T value)
        {
            Node<T> GoTo = node.Goto(i);
            GoTo.SetNext(new Node<T>(value));
            return node;
        }
        public static Node<int> Max(this Node<int> node) => Max(node, node);
        public static Node<int> Max(this Node<int> node, Node<int> max)
        {
            if (node is null)
            {
                return max;
            }
            if (node.GetValue() > max.GetValue())
            {
                max = node;
            }
            return node.GetNext().Max(max);
        }
        public static Node<T> ToNode<T>(this T[] Values)
        {
            int i = Values.Length - 1;
            Node<T> node = new Node<T>(Values[i]);
            return ToNode(Values, --i, node);
        }
        public static Node<char> ToNode(this string str)
        {
            return str.ToCharArray().ToNode();
        }
        private static Node<T> ToNode<T>(this T[] Values, int i, Node<T> node)
        {
            if (i < 0)
            {
                return node;
            }
            node = new Node<T>(Values[i], node);
            return ToNode(Values, --i, node);
        }
    }
}
