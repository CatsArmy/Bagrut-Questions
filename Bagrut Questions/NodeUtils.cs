using System;

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
        public static Node<T> ToNode<T>(this T[] Values)
        {
            int i = Values.Length - 1;
            Node<T> node = new Node<T>(Values[i]);
            return ToNode(Values, --i, node);
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
            /*
            //if node.Contains(contains.Val)
            //node = node.Goto(contains.Val)
            //var j = node.Nex
            //for (var i = contains.Nex; i.HasNex && j.HasNex; i = i.Nex, j = j.Nex)
            //if (i.Val != j.Val)
            //return node.Nex.SubNode(contains)
            //
            //return j.HasNex;
            */
        }
        public static Node<T> Remove<T>(this Node<T> node, int i)
        {
            return node.Remove(node.Goto(i));
        }
        /// <returns><see cref="Node{T}"/> with <typeparamref name="T"/> <paramref name="value"/> 
        /// <see langword="if"/> removed successfuly 
        /// <see langword="else if"/> Failed to remove <see cref="Node{T}"/> 
        /// with <typeparamref name="T"/> <paramref name="value"/> Returns: <see langword="null"/></returns>
        public static Node<T> Remove<T>(this Node<T> node, T value)
        {
            return Remove(node, new Node<T>(value));
        }
        /// <returns><paramref name="current"/> <see langword="if"/> removed successfuly 
        /// <see langword="else if"/> Failed to remove <paramref name="current"/> Returns: <see langword="null"/></returns>
        public static Node<T> Remove<T>(this Node<T> node, Node<T> current)
        {
            if (node != current)
            {
                node = node.GetPreviousNode(current);
                if (node is null)
                {
                    return null;//no remove
                }
                node.SetNext(current.GetNext());//removed
                current.SetNext(null);
                return current;
            }
            if (!current.HasNext())
            {
                return null;//no removed
            }
            Node<T> next = current.GetNext();
            node = new Node<T>(current.GetValue());
            current.SetValue(next.GetValue());//removed
            current.SetNext(next.GetNext());
            return node;
        }
        /// <param name="FirstOrLast">
        /// When <see langword="true"/> Removes the Start of the node,
        /// When <see langword="false"/> Remove the Last of the node
        /// </param>
        /// <returns><paramref name="node"/> <see langword="if"/> removed successfuly 
        /// <see langword="else if"/> Failed to remove <paramref name="node"/> Returns: <see langword="null"/></returns>
        public static Node<T> Remove<T>(this Node<T> node, bool FirstOrLast)
        {
            return node.Remove(FirstOrLast ? node.Goto(0) : node.Goto());
        }
        ///<summary>Removes All <see cref="Node{T}"/> that <paramref name="match"/> the condition</summary>
        /// <returns>how many instances of  <paramref name="node"/> where removed</returns>
        internal static int RemoveAll<T>(this Node<T> node, Predicate<T> match)
        {
            if (match == null || node is null)
            {
                return 0;
            }
            node = node.PreviousOrDefualt(match);
            if (node is null)
            {
                return 0;
            }

            if (!(node.Remove(node.GetNext()) is null))
            {
                return 1 + RemoveAll(node, match);
            }
            return RemoveAll(node, match);
        }
        public static Node<char> RemoveAllMatching(this Node<char> node, Node<char> compare)
        {
            for (Node<char> i = node; !(i is null); i = i.GetNext())
            {
                if (!compare.Contains(i.GetValue()))
                {
                    node.RemoveAll(p => p == i.GetValue());
                }
            }
            return node;
        }
        public static Node<T> GetPreviousNode<T>(this Node<T> node, Node<T> current)
        {
            Node<T> first = node;
            for (Node<T> next = first.GetNext(); !(first is null); first = next, next = first.GetNext())
            {
                if (next == current)
                {
                    return first;
                }
            }
            return first;
        }
        public static Node<T> FirstOrDefualt<T>(this Node<T> node, T value)
        {
            return node.FirstOrDefualt(predicate: new Predicate<T>(p => new Node<T>(p) == node));
        }
        public static Node<T> FirstOrDefualt<T>(this Node<T> node, Predicate<T> predicate)
        {
            if (node is null)
            {
                return null;
            }
            if (predicate(node.GetValue()))
            {
                return node;
            }
            if (!node.HasNext())
            {
                return null;
            }
            return node.GetNext().FirstOrDefualt(predicate);
        }
        public static Node<T> PreviousOrDefualt<T>(this Node<T> node, Predicate<T> predicate)
        {
            if (node is null)
            {
                return null;
            }
            if (!node.HasNext())
            {
                return null;
            }
            Node<T> next = node.GetNext();
            if (predicate(next.GetValue()))
            {
                return node;
            }
            return next.PreviousOrDefualt(predicate);
        }
        public static int Count<T>(this Node<T> node)
        {
            if (node is null)

            {
                return 0;
            }
            int count = node.GetNext().Count();
            return ++count;
        }
    }
}
