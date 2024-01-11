using System;
using Node;
using Queue;

namespace Bagrut_Questions
{
    public class BagrutQuestions
    {
        //Todo Refactor function names
        public static Node<int> MaxEx1(Queue<Node<int>> queue)
        {
            return MaxEx1(queue, new Queue<Node<int>>(), queue.Head());
        }
        private static Node<int> MaxEx1(Queue<Node<int>> queue, Queue<Node<int>> stash, Node<int> max)
        {
            if (queue.IsEmpty())
            {
                queue.Fix(stash);
                return max;
            }
            Node<int> node = queue.Remove();
            stash.Insert(node);
            if (max.Max().GetValue() < node.Max().GetValue())
            {
                max = node;
            }
            return MaxEx1(queue, stash, max);
        }
        public static Node<string> Ex2(Queue<Node<string>> queue)
        {
            //[You May Destroy the queue]
            if (queue.IsEmpty())
            {
                return null;
            }

            Node<string> node = queue.Remove();
            if (node is null)
            {
                node = Ex2(queue);
                return node;
            }
            node = new Node<string>(node.ToPlainString(true), Ex2(queue));
            return node;
        }
        private static Node<string> Ex2(Queue<Node<string>> queue, Node<string> node)
        {
            if (queue.IsEmpty())
            {
                return node;
            }
            Node<string> value = queue.Remove();
            if (value is null)
            {
                return Ex2(queue, node.GetNext());

            }
            return new Node<string>(value.ToPlainString(true), Ex2(queue, node.GetNext()));
        }
        public static Queue<int> Ex3(Node<Queue<int>> node)
        {
            //do not destroy queue
            //Scan queue
            //get to last
            //insert last into returning value
            //fix queue
            //loop
            Queue<int> queue = new Queue<int>();
            for (; !(node is null); node = node.GetNext())
            {
                queue.Insert(node.GetValue().GetLast());
            }
            return queue;
        }
        public static bool Ex4(Node<int[]> node)
        {
            if (!node.HasNext())
            {
                return true;
            }
            int[] array = node.GetNext().GetValue();
            int[] arr = node.GetValue();
            if (arr[arr.Length - 1] != array[0])
            {//ret true if all the arr[0] == LastArr[len -1]
                return false;
            }
            return Ex4(node.GetNext());
            //return true if node.getvalue()[length-1] == node.getnext()[0]
        }
        public static Node<T>[] Ex5<T>(Node<T>[] nodes)
        {
            Node<T>[] arr = new Node<T>[nodes.Length];
            for (int i = 0; i < nodes.Length; i++)
            {
                arr[i] = nodes[i].GetReversed();
            }
            return arr;
        }
        public static int CountCapitalsEx6(Node<Node<char>> node)
        {
            int count = 0;
            for (Node<Node<char>> i = node; !(i is null); i = i.GetNext())
            {
                for (Node<char> j = i.GetValue(); !(j is null); j = j.GetNext())
                {
                    char value = j.GetValue();
                    if (char.IsUpper(value))
                    {
                        count++;
                    }
                }
            }
            return count;
        }
        public static Node<char> Decode(Node<char>[] nodes, int i)
        {
            if (i >= nodes.Length || i < 0 || nodes is null)
            {
                return null;
            }
            char str = nodes[i].GetValue();
            if (nodes[i].HasNext())
            {
                var node = new Node<char>(str);
                Decode(nodes, nodes[i].GetNext(), node);
                return node;
            }

            //case #
            if (str == '#')
            {
                return new Node<char>('.');
            }
            //case num
            if (int.TryParse($"{str}", out int index))
            {
                return Decode(nodes, index);
            }
            //case error
            return null;
        }
        private static void Decode(Node<char>[] nodes, Node<char> i, Node<char> node)
        {
            //'#' at the end of the node, node == '.' //end sentence
            //a num at the end of the node, num == ' '//the index of the continution of the string
            char str = i.GetValue();
            Node<char> next;
            if (i.HasNext())
            {
                next = new Node<char>(str);
                node.SetNext(next);
                Decode(nodes, i.GetNext(), next);
                return;
            }

            if (str == '#')
            {
                next = new Node<char>('.');
                node.SetNext(next);
                return;
            }
            int index = int.Parse($"{str}");
            next = new Node<char>(' ');
            node.SetNext(next);
            Decode(nodes, nodes[index], next);
        }
        public class Program
        {
            public static void Play()
            {
                Queue<int> q1 = new Queue<int>();
                q1.Insert(5);
                q1.Insert(25);
                q1.Insert(51);
                Console.WriteLine("Get Last Q:" + q1.GetLast());

                int[] a1 = { 1, 2, };
                int[] a2 = { 1, 2, 3, 4, 6, 5 };
                int[] a3 = { 1, 2, 3, 4 };
                Node<int> list1 = a1.ToNode();
                Node<int> list2 = a2.ToNode();
                Node<int> list3 = a3.ToNode();
                //PrintList(list1);
                //PrintList(list2);
                //PrintList(list3);
                Console.WriteLine();
                Queue<Node<int>> q = new Queue<Node<int>>();
                q.Insert(list1);
                q.Insert(list2);
                q.Insert(list3);

                Console.Write("Ex1:");
                Console.WriteLine(MaxEx1(q));


                Console.Write("Ex2:");
                Queue<Node<string>> q2 = new Queue<Node<string>>();
                string[] s1 = { "this", "ex" };
                string[] s2 = { "is", "very", "hard" };
                string[] s3 = { "dont", "you", "think?" };
                q2.Insert(null);
                q2.Insert(null);
                q2.Insert(s1.ToNode());
                q2.Insert(s2.ToNode());
                q2.Insert(s3.ToNode());

                Node<string> ex2list = Ex2(q2);
                Console.WriteLine(ex2list.ToPlainString(true));
                Queue<int> q11 = new Queue<int>();
                q11.Insert(5);
                q11.Insert(25);
                q11.Insert(51);
                Queue<int> q12 = new Queue<int>();
                q12.Insert(4);
                q12.Insert(24);
                q12.Insert(41);
                Queue<int> q13 = new Queue<int>();
                q13.Insert(9);
                q13.Insert(29);
                q13.Insert(91);
                Node<Queue<int>> listq = new Node<Queue<int>>(q11);
                listq = new Node<Queue<int>>(q12, listq);
                listq = new Node<Queue<int>>(q13, listq);

                Console.WriteLine($"Ex3 {Ex3(listq)}");
                int[] a11 = { 6, 2, 3, 4 };
                int[] a12 = { 1, 2, 3, 6 };
                int[] a13 = { 4, 2, 3, 1 };
                int[] a14 = { 6, 2, 3, 4 };
                int[] a15 = { 1, 2, 3, 6 };
                Console.WriteLine("Ex4");
                Node<int[]> listArr = new Node<int[]>(a11);
                listArr = new Node<int[]>(a12, listArr);
                listArr = new Node<int[]>(a13, listArr);
                listArr = new Node<int[]>(a14, listArr);
                listArr = new Node<int[]>(a15, listArr);
                Console.WriteLine($"Case: Good: {Ex4(listArr)}");
                int[] a16 = { 10, 2, 3, 6 };
                listArr = new Node<int[]>(a16, listArr);
                Console.WriteLine($"Case: Bad: {Ex4(listArr)}");
                Console.WriteLine("Ex5:");
                Node<int>[] ex5arr = new Node<int>[3];
                ex5arr[0] = list1;
                ex5arr[1] = list2;
                ex5arr[2] = list3;
                Node<int> list4 = new Node<int>(list1);
                list4.Goto().SetNext(new Node<int>(list2));
                list4.Goto().SetNext(new Node<int>(list3));
                Node<int>[] ex5arrRev = Ex5(ex5arr);
                for (int i = 0; i < ex5arr.Length; i++)
                {
                    Console.WriteLine(ex5arr[i]);
                }
                Console.WriteLine();
                for (int i = 0; i < ex5arr.Length; i++)
                {
                    Console.WriteLine(ex5arrRev[i]);

                }
                Node<Node<int>> listOlist = null;
                Console.WriteLine("Ex5: Decode");
                string[] strings = { "IS A2", "BSK", "BIG CAT#", "$", "THIS0", "@" };
                Node<char>[] nodes = new Node<char>[5];
                for (int i = 0; i < nodes.Length; i++)
                {
                    nodes[i] = strings[i].ToNode();
                }
                var node = Decode(nodes, 4);
                Console.WriteLine(node);
                string str = "";
                for (Node<char> i = node; i.HasNext(); i = i.GetNext())
                {
                    str += i.GetValue();
                }
                Console.WriteLine(str);

                Console.WriteLine(
                    $"Node4{list4} {(list4.Contains(list1) ? $"Does({true})" : $"Doesn't({false})")} Contain Node1{list1}");
                Console.WriteLine(
                    $"Node4{list4} {(list4.Contains(list2) ? $"Does({true})" : $"Doesn't({false})")} Contain Node2{list2}");
                Console.WriteLine(
                    $"Node4{list4} {(list4.Contains(list3) ? $"Does({true})" : $"Doesn't({false})")} Contain Node3{list3}");
                Console.WriteLine(
                    $"Node4{list4} {(list4.IndividualyContains(list1) ? $"Does({true})" : $"Doesn't({false})")} Individualy Contain Node1{list1}");
                Console.WriteLine(
                    $"Node4{list4} {(list4.IndividualyContains(list2) ? $"Does({true})" : $"Doesn't({false})")} Individualy Contain Node2{list2}");
                Console.WriteLine(
                    $"Node4{list4} {(list4.IndividualyContains(list3) ? $"Does({true})" : $"Doesn't({false})")} Individualy Contain Node3{list3}");
            }
        }
    }
}
