using System;
using System.Threading;
using Node;
using Queue;
using Bagrut = Bagrut_Questions.BagrutQuestions;

namespace Bagrut_Questions
{
    public class BagrutQuestions
    {
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
        public static Node<Range> BagrutQuestion3(Node<int> node)
        {
            if (node is null)
            {
                return null;
            }
            Node<int> i = node.FirstOrDefualt(match =>
                match.HasNext() && 1 + match.GetValue() != match.GetNext().GetValue());
            if (i is null)
            {
                return new Node<Range>(new Range(node.GetValue(), node.Goto().GetValue()));
            }
            return new Node<Range>(new Range(node.GetValue(), i.GetValue()), Bagrut.BagrutQuestion3(i?.GetNext()));
        }
        public static Node<int> BagrutQuestion4(Node<int> node)
        {
            if (node is null)
            {
                return null;
            }
            Node<int> i = node.PreviousOrDefualt(match =>
                match == -9);
            return new Node<int>(node.BuildDigit(i), BagrutQuestion4(i?.GetNext()?.GetNext()));
        }
        public static Node<char> Sod1(Node<char> lst, char ch)
        {
            if (lst == null)
            {
                return null;
            }
            if (lst.GetValue() == ch)
            {
                return lst;
            }
            return Sod1(lst.GetNext(), ch);
        }
        public static bool BagrutQuestion5(Node<char> node, char value = 'a', char nextValue = 'b')
        {
            if (node is null)
            {
                return false;
            }
            Node<char> a = Sod1(node, value);
            Node<char> b = Sod1(node, nextValue);
            if (a is null && b is null)
            {
                return false;
            }
            if (a is null && b.HasNext())
            {
                return b.GetNext().GetValue() == value;
            }
            if (b is null && a.HasNext())
            {
                return a.GetNext().GetValue() == nextValue;

            }
            return false;
            //omg that is so disgusting to read and look at the code i had to comment out was so much cleaner and 
            //nicer wow i hate this code style.
            //wow such nicer code i had written before they said to use Sod1 the non generic trash func
            //fight me if you disagree as you just dont know c# that well its called delegation you just delegate
            //not that hard. lets you reuse your code alot more
            //node.FirstOrDefualt(match => match.GetValue() == nextValue && match.GetNext().GetValue() == value);
            //node.FirstOrDefualt(match => match.GetValue() == value && match.GetNext().GetValue() == nextValue);

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
            private static Node<int> node1 = null;
            private static Node<int> node2 = null;
            private static Node<int> node3 = null;
            private static void Ex1()
            {
                Console.WriteLine($"{nameof(Ex1)}:");
                Queue<int> q1 = new Queue<int>();
                q1.Insert(5);
                q1.Insert(25);
                q1.Insert(51);
                Console.WriteLine($"Get Last Q:{q1.GetLast()}\n");
                Queue<Node<int>> q = new Queue<Node<int>>();
                q.Insert(node1);
                q.Insert(node2);
                q.Insert(node3);
                Console.WriteLine(Bagrut.MaxEx1(q));
            }
            private static void Ex2()
            {
                Console.WriteLine($"{nameof(Ex2)}:");
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
                Console.WriteLine($"{nameof(Ex3)}:");
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

                Console.WriteLine($"{Bagrut.Ex3(listq)}");
            }
            private static void Ex4()
            {
                Console.WriteLine($"{nameof(Ex4)}:");
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
                Console.WriteLine($"{nameof(Ex5)}:");
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
                Console.WriteLine($"{nameof(Ex6)}:");
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
                Console.WriteLine($"{nameof(Ex5B)} Decode:");
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

            //from bagrut powerpoint
            private static void Ex1B()
            {
                Console.WriteLine($"{nameof(Ex1B)}:");
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
                Console.WriteLine($"{nameof(Ex2B)}:");
                int[] arr = { 2, 5, 3, 7, 2, 5, 3, 7, 2, 5, 3, 7 };
                Node<int> node = arr.ToNode();
                Console.WriteLine($"node{node}");
                Console.WriteLine($"node is{(BagrutQuestion2(node) ? $"triangilar [{true}]" : $"'nt triangilar [{false}]")}");
            }
            private static void Ex3B()
            {
                Console.WriteLine($"{nameof(Ex3B)}:");
                int[] arr = { 3, 4, 5, 12, 19, 20, 100, 101, 102, 103, 104 };
                Node<int> node = arr.ToNode();
                Node<Range> NodeRange = new Node<Range>(new Range(3, 5),
                    new Node<Range>(new Range(12, 12)));

                Node<Range> Expected = NodeRange.GetNext();
                Expected.SetNext(new Node<Range>(new Range(19, 20),
                    new Node<Range>(new Range(100, 104))));
                Expected = new Node<Range>(NodeRange);

                NodeRange = Bagrut.BagrutQuestion3(node);

                Console.WriteLine($"{nameof(NodeRange)}{NodeRange}");
                Console.WriteLine($"{nameof(Expected)}{Expected}");
            }
            private static void Ex4B()
            {
                Console.WriteLine($"{nameof(Ex4B)}:");
                int[] expectedArr = { 92, 4, 543 };
                //int[] arr = { 9, 2, -9, 4, -9, 5, 4, 3, -9 };
                int[] arr = { 2, 9, -9, 4, -9, 3, 4, 5, -9 };
                Node<int> node = arr.ToNode();
                Node<int> Expected = expectedArr.ToNode();
                Node<int> Resulted = BagrutQuestion4(node);
                Console.WriteLine($"{nameof(Expected)}{Expected}");
                Console.WriteLine($"{nameof(Resulted)}{Resulted}");
            }
            private static void Ex6B()
            {
                Console.WriteLine($"{nameof(Ex6B)}:");
                Console.WriteLine("Not Implimented yet...");
            }
            /// <summary>
            /// <paramref name="i"/> = 0 will play all tests <see langword="else"/> will run the test in
            /// the index of <paramref name="i"/>
            /// </summary>
            ///<remarks>
            ///when <paramref name="i"/> is bigger than 6 it will loop back to the second 
            ///set of tests but for the second set of questions we got aka 
            ///Ex<paramref name="i"/> or Ex<paramref name="i"/>B
            ///</remarks>
            public static void Play(int i = 0)
            {
                if (node1 is null)
                {
                    node1 = list1.ToNode();
                }
                if (node2 is null)
                {
                    node2 = list2.ToNode();
                }
                if (node3 is null)
                {
                    node3 = list3.ToNode();
                }
#if DEBUG
                TimeSpan timeout = new TimeSpan(0, 0, 0, 1, 500);
#else
                TimeSpan timeout = new TimeSpan(0, 0, 10);
#endif
                switch (i)
                {
                    case 1:
                        goto Case1;

                    case 2:
                        goto Case2;

                    case 3:
                        goto Case3;
                    case 4:
                        goto Case4;
                    case 5:
                        goto Case5;
                    case 6:
                        goto Case6;
                    case 7:
                        goto Case1B;
                    case 8:
                        goto Case2B;
                    case 9:
                        goto Case3B;
                    case 10:
                        goto Case4B;
                    case 11:
                        goto Case5B;
                    case 12:
                        goto Case6B;
                    default:
                        for (i = 1; i <= 12; i++, Thread.Sleep(timeout))
                        {
                            Play(i);
                        }
                        Console.Clear();
                        break;
                }
            #region CaseLabels
            Case1:
                if (i == 1)
                {
                    Console.Clear();
                    Ex1();
                    Thread.Sleep(timeout);
                }
            Case2:
                if (i == 2)
                {
                    Console.Clear();
                    Ex2();
                    Thread.Sleep(timeout);
                }
            Case3:
                if (i == 3)
                {
                    Console.Clear();
                    Ex3();
                    Thread.Sleep(timeout);
                }
            Case4:
                if (i == 4)
                {
                    Console.Clear();
                    Ex4();
                    Thread.Sleep(timeout);
                }
            Case5:
                if (i == 5)
                {
                    Console.Clear();
                    Ex5();
                    Thread.Sleep(timeout);
                }
            Case6:
                if (i == 6)
                {
                    Console.Clear();
                    Ex6();
                    Thread.Sleep(timeout);
                }
            Case1B:
                if (i == 7)
                {
                    Console.Clear();
                    Ex1B();
                    Thread.Sleep(timeout);
                }
            Case2B:
                if (i == 8)
                {
                    Console.Clear();
                    Ex2B();
                    Thread.Sleep(timeout);
                }
            Case3B:
                if (i == 9)
                {
                    Console.Clear();
                    Ex3B();
                    Thread.Sleep(timeout);
                }
            Case4B:
                if (i == 10)
                {
                    Console.Clear();
                    Ex4B();
                    Thread.Sleep(timeout);
                }
            Case5B:
                if (i == 11)
                {
                    Console.Clear();
                    Ex5B();
                    Thread.Sleep(timeout);
                }
            Case6B:
                if (i == 12)
                {
                    Console.Clear();
                    Ex6B();
                    Thread.Sleep(timeout);
                }
                #endregion
            }

        }
    }
    public class Competitor
    {
        private int minutes = 0;
        private int seconds = 0;
        private string name = string.Empty;
        public Competitor(TimeSpan time, string name)
        {
            this.minutes = time.Minutes;
            this.seconds = time.Seconds;
            this.name = name.ToString();
        }
        public Competitor(Competitor competitor)
        {
            this.minutes = competitor.minutes;
            this.seconds = competitor.seconds;
            this.name = competitor.name.ToString();
        }

        public void SetMinutes(TimeSpan time) => this.minutes = time.Minutes;
        public void SetSeconds(TimeSpan time) => this.seconds = time.Seconds;
        public void SetName(string name) => this.name = name.ToString();
        public TimeSpan GetTime() => new TimeSpan(0, this.minutes, this.seconds);
        public int GetMinutes() => this.minutes;
        public int GetSeconds() => this.seconds;
        public string GetName() => this.name.ToString();
    }
    public class Race
    {
        private Node<Competitor> node = null;
        public Race(Node<Competitor> node)
        {
            if (!node.HasNext())
            {
                return;
            }
            Competitor runner = node.GetValue();
            this.node = new Node<Competitor>(runner);
            node = node.GetNext();
            for (runner = node.GetValue(); node.HasNext(); node = node.GetNext(),
                runner = node.GetValue())
            {
                if (runner is null)
                {
                    continue;
                }
                this.Add(runner);
            }
        }
        public Race(Competitor runner)
        {
            if (runner is null)
            {
                return;
            }
            this.node = new Node<Competitor>(new Competitor(runner));
        }
        public void Add(Competitor runner)
        {
            if (runner is null)
            {
                return;
            }
            if (this.node is null)
            {
                this.node = new Node<Competitor>(new Competitor(runner));
                return;
            }
            // rank => first = fastest | last = slowest, O(n)
            this.node.SortedInsert(new Competitor(runner),
                match => match.GetTime() > runner.GetTime());
        }
        /// <param name="i"><paramref name="i"/> = --<paramref name="i"/></param>
        public string Rank(int i)
        {
            if (this.node is null)
            {
                return string.Empty;
            }
            return this.node.Goto(--i).GetValue().GetName();
        }
    }
}
