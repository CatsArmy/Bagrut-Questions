namespace Queue
{
    public static class QueueUtils
    {
        public static int Count<T>(Queue<T> queue, Queue<T> stash, int i = 0)
        {
            if (queue.IsEmpty())
            {
                queue.Fix(stash);
                return i;
            }
            stash.Insert(queue.Remove());
            return Count(queue, stash, ++i);

        }
        public static T GetLast<T>(this Queue<T> queue)
        {
            return GetLast(queue, new Queue<T>(), queue.Head());
        }
        private static T GetLast<T>(this Queue<T> queue, Queue<T> stash, T value)
        {
            if (queue.IsEmpty())
            {
                queue.Fix(stash);
                return value;
            }
            value = queue.Remove();
            stash.Insert(value);
            return GetLast(queue, stash, value);
        }
        internal static void Fix<T>(this Queue<T> queue, Queue<T> stash, Queue<T> other = null)
        {
            T value;
            if (stash.IsEmpty()) return;
            value = stash.Remove();
            queue.Insert(value);
            other?.Insert(value);
            Fix(queue, stash, other);


            //while (!stash.IsEmpty())
            //{
            //    value = stash.Remove();
            //    queue.Insert(value);
            //    other?.Insert(value);
            //}
        }

    }
}
