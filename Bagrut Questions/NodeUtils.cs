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
        #region InsertValue
        public static Node<T> Insert<T>(this Node<T> node, T value)
        {
            Node<T> InsertAt = node.Goto();
            InsertAt.SetNext(new Node<T>(value));
            return node;
        }
        public static Node<T> Insert<T>(this Node<T> node, Node<T> i, T value)
        {
            Node<T> InsertAt = node.Goto(i);
            InsertAt.SetNext(new Node<T>(value));
            return node;
        }
        public static Node<T> Insert<T>(this Node<T> node, int i, T value)
        {
            Node<T> InsertAt = node.Goto(i);
            InsertAt.SetNext(new Node<T>(value));
            return node;
        }
        #endregion
        #region InsertNode
        //Merge Nodes
        public static Node<T> Insert<T>(this Node<T> node, Node<T> value, Node<T> i)
        {
            Node<T> InsertAt = node.Goto(i);
            value = new Node<T>(value);
            if (!InsertAt.HasNext())
            {
                InsertAt.SetNext(value);
                return node;
            }
            Node<T> next = InsertAt.GetNext();
            InsertAt.SetNext(value);
            value.Goto().SetNext(next);
            return node;
        }
        public static Node<T> Insert<T>(this Node<T> node, Node<T> value, int i)
        {
            Node<T> InsertAt = node.Goto(i);
            value = new Node<T>(value);
            if (!InsertAt.HasNext())
            {
                InsertAt.SetNext(value);
                return node;
            }
            Node<T> next = InsertAt.GetNext();
            InsertAt.SetNext(value);
            value.Goto().SetNext(next);
            return node;
        }
        public static Node<T> Insert<T>(this Node<T> node, Node<T> value)
        {
            Node<T> InsertAt = node.Goto();
            InsertAt.SetNext(new Node<T>(value));
            return node;
        }
        #endregion

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
        public static bool Contains<T>(this Node<T> node, T value)
        {
            return Contains(node, new Node<T>(value));
            if (node is null)
            {
                return false;
            }
            if (true)
            //if (value == node)
            {
                return true;
            }
            return node.GetNext().Contains(value);
        }
        public static bool Contains<T>(this Node<T> node, Node<T> value) => Contains(node, value,
            value, node);
        private static bool Contains<T>(this Node<T> node, Node<T> value, Node<T> i, Node<T> j)
        {
            if (node is null)
            {
                return value is null;
            }
            if (j.GetValue().Equals(i.GetValue()))
            {
                return Contains(node, value, value, j.GetNext());
            }
            if (!i.HasNext())
            {
                return true;
            }
            if (!j.HasNext())
            {
                return false;
            }
            return Contains(node.GetNext(), value, i.GetNext(), j.GetNext());
        }
        public static bool IndividualyContains<T>(this Node<T> node, Node<T> value)
        {
            if (node is null)
            {
                return value is null;
            }
            for (Node<T> i = value; i.HasNext(); i = i.GetNext())
            {
                if (!node.Contains(value.GetValue()))
                {
                    return false;
                }
            }
            return true;

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
        public static bool SubNode<T>(this Node<T> node, Node<T> contains)
        {
            return node.Contains(contains);
            //if node.Contains(contains.Val)
            //node = node.Goto(contains.Val)
            //var j = node.Nex
            //for (var i = contains.Nex; i.HasNex && j.HasNex; i = i.Nex, j = j.Nex)
            //if (i.Val != j.Val)
            //return node.Nex.SubNode(contains)
            //
            //return j.HasNex;
        }
    }
}
