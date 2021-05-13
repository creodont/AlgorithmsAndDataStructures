using System;
using System.Collections.Generic;

namespace Lesson5
{
    class Program
    {
        /// <summary>
        /// Метод поиска в ширину
        /// </summary>
        static void BFS(Tree tree)
        {
            Console.WriteLine("\n =Обход в ширину=");
            Queue<TreeNode> que = new Queue<TreeNode>();
            que.Enqueue(tree.Root);
            while(que.Count != 0)
            {
                var node = que.Dequeue();
                if (node != null)
                {
                    Console.WriteLine(node.Value);
                    if (node.LeftChild != null) que.Enqueue(node.LeftChild);
                    if (node.RightChild != null) que.Enqueue(node.RightChild);
                }

            }

        }

        /// <summary>
        /// Метод поиска в глубину
        /// </summary>
        static void DFS(ref Stack<TreeNode> stk, TreeNode node)
        {
            stk.Push(node);
            if (stk.Count == 0) return;
            var mnode = stk.Pop();
            Console.WriteLine(mnode.Value);
            if (mnode.LeftChild != null)
            {
                stk.Push(mnode.LeftChild);
                DFS(ref stk, mnode.LeftChild);
            }
            if (mnode.RightChild != null)
            {
                stk.Push(mnode.RightChild);
                DFS(ref stk, mnode.RightChild);
            }

        }

        static void Main(string[] args)
        {
            // Реализуйте DFS и BFS для дерева с выводом каждого шага в консоль.
            int[] arr = new int[] { 8, 3, 10, 1, 6, 9, 14, 4, 7, 12, 16, 11, 13, 15, 17 };

            Tree tree = new Tree();

            for (int i = 0; i < arr.Length; i++)
            {
                tree.AddItem(arr[i]);
            }
            Console.Write("Исходное дерево: ");
            tree.PrintTree();

            // обход в ширину
            BFS(tree);

            // Обход в длину
            Console.WriteLine("\n =Обход в длину=");
            Stack<TreeNode> stk = new Stack<TreeNode>();
            DFS(ref stk, tree.Root);
            Console.ReadLine();
        }
    }
}
