using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XfsConsoleTest.TestAwait
{
    public class TestNodeTask
    {
        public TestNodeTask()
        {
            Node root = GetNode();
            DisplayTree(root);
        }
        Node GetNode()
        {
            Node root = new Node
            {
                Left = new Node
                {
                    Left = new Node
                    {
                        Text = "L-L"
                    },
                    Right = new Node
                    {
                        Text = "L-R"
                    },
                    Text = "L"
                },
                Right = new Node
                {
                    Left = new Node
                    {
                        Text = "R-L"
                    },
                    Right = new Node
                    {
                        Text = "R-R"
                    },
                    Text = "R"
                },
                Text = "Root"
            };
            return root;
        }
        void DisplayTree(Node root)
        {
            var task = Task.Factory.StartNew(() =>
                       DisplayNode(root), 
                       CancellationToken.None,                                      
                       TaskCreationOptions.None,                                    
                       TaskScheduler.Default);

            task.Wait();
        }
        void DisplayNode(Node current)
        {

            if (current.Left != null)
                Task.Factory.StartNew(() => DisplayNode(current.Left),
                                            CancellationToken.None,
                                            TaskCreationOptions.AttachedToParent,
                                            TaskScheduler.Default);
            if (current.Right != null)
                Task.Factory.StartNew(() => DisplayNode(current.Right),
                                            CancellationToken.None,
                                            TaskCreationOptions.AttachedToParent,
                                            TaskScheduler.Default);
            Console.WriteLine("当前节点的值为{0};处理的ThreadId={1}", current.Text, Thread.CurrentThread.ManagedThreadId);
        }




    }
    public class Node
    {
        public Node Left { get; set; }
        public Node Right { get; set; }
        public string Text { get; set; }
    }



}
