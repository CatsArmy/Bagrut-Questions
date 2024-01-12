using System;
using Node;
using Queue;
using Bagrut = Bagrut_Questions.BagrutQuestions;

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

        public static Node<int> BagrutQuestion1(Node<int> node)
        {
            //Can assume node is not empty.
            //foreach subnode of nodes that are in up order
            //the new node will add the sum of all the nodes in the subnode
            if (node is null)
            {
                return null;
            }
            //logic given does not match example...
            //shame on whoever made that for wasting hundruds of hours of students in total. cringe af
            Node<int> i = node.FirstOrDefualt(match =>
            match.HasNext() && match.GetValue() >= match.GetNext().GetValue());
            return new Node<int>(node.Sum(i), Bagrut.BagrutQuestion1(i?.GetNext()));
        }
        public static bool BagrutQuestion2<T>(Node<T> node)
        {
            if (node is null)
            {
                return false;
            }
            int length = node.Count();
            if (length % 3 != 0)
            {
                return false;
            }
            Node<T> nextSubnode = node.Goto(length);
            Console.WriteLine(nextSubnode);
            return BagrutQuestion2(nextSubnode, node.SubNode(nextSubnode), length);
        }
        //IsTrianginal
        private static bool BagrutQuestion2<T>(Node<T> node, Node<T> subnode, int length, int i = 2)
        {

            Node<T> nextSubnode = null;
            if (i != 1)
            {
                nextSubnode = node.Goto(length / 3 * i);
            }

            if (nextSubnode is null)
            {
                Console.WriteLine(subnode);
                return true;
            }
            if (!nextSubnode.AllEquals(node.SubNode(subnode)))
            {
                Console.WriteLine(nextSubnode);
                return false;
            }
            Console.WriteLine(nextSubnode);
            return BagrutQuestion2(nextSubnode, node.SubNode(nextSubnode), length, --i);
        }
        public class Program
        {
            private static int[] list1 = { 1, 2, };
            private static int[] list2 = { 1, 2, 3, 4, 6, 5 };
            private static int[] list3 = { 1, 2, 3, 4 };
            private static int[] array = { 6, 2, 3, 4 };
            private static int[] array1 = { 1, 2, 3, 6 };
            private static int[] array2 = { 4, 2, 3, 1 };
            private static int[] array3 = { 6, 2, 3, 4 };
            private static int[] array4 = { 1, 2, 3, 6 };
            private static Node<int> node1;
            private static Node<int> node2;
            private static Node<int> node3;
            private static void Ex1()
            {
                Queue<int> q1 = new Queue<int>();
                q1.Insert(5);
                q1.Insert(25);
                q1.Insert(51);
                Console.WriteLine($"Get Last Q:{q1.GetLast()}\n");
                Queue<Node<int>> q = new Queue<Node<int>>();
                q.Insert(node1);
                q.Insert(node2);
                q.Insert(node3);

                Console.Write("Ex1:");
                Console.WriteLine(Bagrut.MaxEx1(q));
            }
            private static void Ex2()
            {
                Console.Write("Ex2:");
                string[] s1 = { "this", "ex" };
                string[] s2 = { "is", "very", "hard" };
                string[] s3 = { "dont", "you", "think?" };
                Queue<Node<string>> q2 = new Queue<Node<string>>();
                q2.Insert(null);
                q2.Insert(null);
                q2.Insert(s1.ToNode());
                q2.Insert(s2.ToNode());
                q2.Insert(s3.ToNode());

                Node<string> ex2list = Bagrut.Ex2(q2);
                Console.WriteLine(ex2list.ToPlainString(true));
            }
            private static void Ex3()
            {
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

                Console.WriteLine($"Ex3 {Bagrut.Ex3(listq)}");
            }
            private static void Ex4()
            {
                Console.WriteLine("Ex4");
                Node<int[]> listArr = new Node<int[]>(array);
                listArr = new Node<int[]>(array1, listArr);
                listArr = new Node<int[]>(array2, listArr);
                listArr = new Node<int[]>(array3, listArr);
                listArr = new Node<int[]>(array4, listArr);
                Console.WriteLine($"Case: Good: {Bagrut.Ex4(listArr)}");
                int[] a16 = { 10, 2, 3, 6 };
                listArr = new Node<int[]>(a16, listArr);
                Console.WriteLine($"Case: Bad: {Bagrut.Ex4(listArr)}");
            }
            private static void Ex5()
            {
                Console.WriteLine("Ex5:");
                Node<int>[] ex5arr = new Node<int>[3];
                ex5arr[0] = node1;
                ex5arr[1] = node2;
                ex5arr[2] = node3;
                Node<int>[] ex5arrRev = Bagrut.Ex5(ex5arr);
                for (int i = 0; i < ex5arr.Length; i++)
                {
                    Console.WriteLine(ex5arr[i]);
                }
                Console.WriteLine();
                for (int i = 0; i < ex5arr.Length; i++)
                {
                    Console.WriteLine(ex5arrRev[i]);

                }
            }
            private static void Ex6()
            {
                int stringsLength = "IDONOTMAKEGRAMMERMISTAKES".Length;
                string badstr = "i DO NOT MAKE GRAMMER MISTAKES";
                string funnystr = "I DO NOT MAKE GRAMMER MISTAKES";
                string grammaticlyCorrectstr = "I do not make grammer mistakes";
                Node<char> badNode = badstr.ToCharArray().ToNode();
                Node<char> funnyNode = funnystr.ToCharArray().ToNode();
                Node<char> grammaticlyCorrectNode = grammaticlyCorrectstr.ToCharArray().ToNode();
                Node<char>[] nodes = { badNode, funnyNode, grammaticlyCorrectNode };
                Node<Node<char>> nodewhat = nodes.ToNode();
                string bad = $"{badstr} is {Bagrut.CountCapitalsEx6(nodewhat)}/{stringsLength} only uppercase(not include whitespaces) and grammaticly inverse? how what why";
                string funny = $"{funnystr} is {Bagrut.CountCapitalsEx6(nodewhat)}/{stringsLength} only uppercase(not include whitespaces) is just all caps what the hell man";
                string grammaticlyCorrect = $"{grammaticlyCorrectNode} is {Bagrut.CountCapitalsEx6(nodewhat)}/{stringsLength} only uppercase(not include whitespaces) is grammaticly correct wow finally";
                string punchline =
                    $"jokes on you turns out this functions name({nameof(CountCapitalsEx6)}) is missleading" +
                    $"and does not give you the count of how many capitals where found in each node but all combinded" +
                    $"what a shame lol do better man not cool ruined the joke";
                Console.WriteLine(bad);
                Console.WriteLine(funny);
                Console.WriteLine(grammaticlyCorrect);
                Console.WriteLine(punchline);
            }
            private static void Ex5B()
            {
                Node<int> list4 = new Node<int>(node1);
                list4.Goto().SetNext(new Node<int>(node2));
                list4.Goto().SetNext(new Node<int>(node3));
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
                    $"Node4{list4} {(list4.Contains(node1) ? $"Does({true})" : $"Doesn't({false})")} Contain Node1{node1}");
                Console.WriteLine(
                    $"Node4{list4} {(list4.Contains(node2) ? $"Does({true})" : $"Doesn't({false})")} Contain Node2{node2}");
                Console.WriteLine(
                    $"Node4{list4} {(list4.Contains(node3) ? $"Does({true})" : $"Doesn't({false})")} Contain Node3{node3}");
                Console.WriteLine(
                    $"Node4{list4} {(list4.IndividualyContains(node1) ? $"Does({true})" : $"Doesn't({false})")} Individualy Contain Node1{node1}");
                Console.WriteLine(
                    $"Node4{list4} {(list4.IndividualyContains(node2) ? $"Does({true})" : $"Doesn't({false})")} Individualy Contain Node2{node2}");
                Console.WriteLine(
                    $"Node4{list4} {(list4.IndividualyContains(node3) ? $"Does({true})" : $"Doesn't({false})")} Individualy Contain Node3{node3}");
            }
            private static void Ex1B()
            {
                int[] arr = { 7, 2, 4, 8, 20, 18, 19, 20, 20, 5, -3, 0, 9 };
                int[] expectedAsArray = { 7, 34, 57, 20, 5, 6 };
                Node<int> expected = expectedAsArray.ToNode();
                Node<int> node = arr.ToNode();
                string result = $"{node}\nResulted: {node = Bagrut.BagrutQuestion1(node)}\nExpected: {expected}";
                Console.WriteLine(result);
                Console.WriteLine(node.AllEquals(expected) ? "Success" : "Failed");
            }
            private static void Ex2B()
            {
                int[] arr = { 2, 5, 3, 7, 2, 5, 3, 7, 2, 5, 3, 7 };
                Node<int> node = arr.ToNode();
                Console.WriteLine($"node{node}");
                Console.WriteLine($"node is{(BagrutQuestion2(node) ? $"triangilar [{true}]" : $"'nt triangilar [{false}]")}");
            }
            public static void Play()
            {
                node1 = list1.ToNode();
                node2 = list2.ToNode();
                node3 = list3.ToNode();
                Ex2B();

            }
        }
    }
}
