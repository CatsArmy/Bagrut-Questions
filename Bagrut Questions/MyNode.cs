using Console = System.Console;
namespace Node
{
    internal class MyNode
    {
        private Node<string> Node;

        private int Length;
        public MyNode()
        {
            this.Node = null;
            this.Length = 0;
        }
        public MyNode(string[] strings)
        {
            this.Node = strings.ToNode();
            this.Length = this.Node.Count();
        }
        public MyNode(MyNode my)
        {
            this.Node = new Node<string>(my.Node);
            this.Length = my.Length;
        }
        public override string ToString()
        {
            return $"{this.Node} Length: {this.Length}";
        }
        public int GetLength()
        {
            return this.Length;
        }
        public Node<string> GetNode()
        {
            if (this.Node is null)
            {
                return null;
            }
            return new Node<string>(this.Node);
        }
        public Node<string> GetLast()
        {
            if (this.Node is null)
            {
                return null;
            }
            return new Node<string>(this.Node.Goto());
        }
        public Node<string> GetReversedNode()
        {
            if (this.Node is null)
            {
                return null;
            }
            return this.Node.GetReversed();
        }
        public void Add(string str)
        {
            if (this.Node is null)
            {
                this.Node = new Node<string>(str);
                this.Length++;
                return;
            }
            this.Node.Insert(str);
            this.Length++;
        }
        public int Remove(string str)
        {
            int count = this.Node.RemoveAll(value => str == value);
            this.Length -= count;
            return count;
        }
        public void Remove(char firstChar)
        {
            Node<string> node = this.Node.FirstOrDefualt(value => value[0] == firstChar);
            node = node.Remove(node);
            if (node is null)
            {
                return;
            }
            Length--;
            Remove(firstChar);
        }
        public string RemoveLast()
        {
            Node<string> node = this.Node.Remove(false);
            if (node is null)
            {
                return string.Empty;
            }
            this.Length--;
            return node.GetValue();
        }
        public string RemoveFirst()
        {
            Node<string> node = this.Node.Remove(true);
            if (node is null)
            {
                return string.Empty;
            }
            this.Length--;
            return node.GetValue();
        }
        public void Empty()
        {
            this.Node.RemoveAll(match => true);
            this.Node = null;
            this.Length = 0;
        }
        public class Program
        {
            //.1 כתוב תכנית ראשית המייצרת עצם מסוג MyList ריק mlist. Check
            //"I", "love" , "my", "List"," ", "List", "is", "mush" :המחרוזות את הוסף .2 Check
            //.3 הדפס כמה מחרוזות יש בעצם mlist. Check
            //.4 הדפס את mlist.הדפס את תוכן הרשימה של 1mesi מהסוף להתחלה. Check
            //.5 צור עותק נוסף של העצם – קרא לו 2mlist. Check
            //.6 מחק את המחרוזת הראשונה שהכנסת לmlist . הדפס את mlist ל וודא שהוסרה.
            //.7 מחק את המחרוזת האחרונה שהכנסת לmlist . הדפס את mlist ל וודא שהוסרה.
            //.8 רוקן את הרשימה של mlist.הדפס אותו על מנת לוודא שאכן ריק.
            //.9 הדפס את 2mlist.ודא שלא התרוקן.
            //.10 מחק מ 2mlist את המילים "List". הדפס את 2mlist.
            //.11 מחק מ 2mlist את המילים המתחילות באות "m". הדפס את 2mlist.
            public static void Play()
            {
                string[] strings = { "I", "Love", "My", "Node", " ", "Node", "Is", "Mush" };
                MyNode node = new MyNode(strings);
                Console.WriteLine($"{nameof(node)}{node}");
                Node<string> reverseNode = node.GetReversedNode();
                Node<string> copy = node.GetNode();
                Console.WriteLine(node.RemoveFirst());
                Console.WriteLine(node.RemoveLast());
                Console.WriteLine($"{nameof(node)}{node}");
                Console.WriteLine($"{nameof(reverseNode)}{reverseNode}");
                Console.WriteLine($"{nameof(copy)}{copy}");
                node.Empty();
                Console.WriteLine($"{nameof(node)}{node}");
                string str = $"{nameof(copy)}{copy}";
                Console.WriteLine(str);
                Console.Write($"Removed {copy.RemoveAll(value => value == "Node")} from {str}");
                Console.WriteLine($"After removing {nameof(copy)}{copy}");
                str = $"{nameof(copy)}{copy}";
                Console.Write($"Removed {copy.RemoveAll(value => value.ToLower().Contains("m"))} " +
                    $"Nodes that had the value (not case sensitive)\"m\"from {str}");
                str = $"{nameof(copy)}{copy}";
                Console.WriteLine($"After removing {str}");
            }
        }
    }
}
