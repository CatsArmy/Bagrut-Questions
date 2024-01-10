using Node;

namespace Queue
{
    public class Queue<T>
    {
        private Node<T> first;

        private Node<T> lastPos;

        public Queue()
        {
            lastPos = null;
            first = null;
        }

        public bool IsEmpty()
        {
            return first == null;
        }

        public void Insert(T x)
        {
            if (first == null)
            {
                first = new Node<T>(x);
                lastPos = first;
            }
            else
            {
                lastPos.SetNext(new Node<T>(x));
                lastPos = lastPos.GetNext();
            }
        }

        public T Remove()
        {
            T value = first.GetValue();
            Node<T> node = first;
            first = first.GetNext();
            if (first == null)
            {
                lastPos = null;
            }

            node.SetNext(null);
            return value;
        }

        public T Head()
        {
            return first.GetValue();
        }

        public override string ToString()
        {
            Node<T> next = first;
            string text = "[";
            while (next != null)
            {
                text += next.GetValue().ToString();
                if (next.GetNext() != null)
                {
                    text += ",";
                }

                next = next.GetNext();
            }

            return text + "]";
        }
    }
}
