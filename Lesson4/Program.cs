using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;

namespace Lesson4
{
    class Program
    {
        static int size = 1_000_000;
        static string[] strArr = new string[size];
        static HashSet<string> hash = new HashSet<string>();
        /// <summary>
        /// Метод сранения поиска в массиве и в HashSet
        /// </summary>
        /// <param name="seachEl"></param>
        /// <param name="index"></param>
        public static void SearchIn(string searchEl, int index)
        {
            Console.WriteLine($"\nПроверка при поиске до {index} элемента");

            // поиск в массиве
            Stopwatch stopwatchArr = new Stopwatch();
            bool flag = true;
            //stopwatchArr.Reset();
            stopwatchArr.Start();
            foreach (var item in strArr)
            {
                if (item == searchEl)
                {
                    stopwatchArr.Stop();
                    Console.WriteLine($"При поиске в массиве затрачено: {stopwatchArr.ElapsedTicks}");
                    flag = false;
                }
            }

            if (flag)
            {
                Console.WriteLine("Cтрока ненайдена в массиве");
                stopwatchArr.Reset();
                return;
             }
            
            // поиск в HashSet
            Stopwatch stopwatchHash = new Stopwatch();
            flag = false;
            //stopwatchHash.Reset();
            stopwatchHash.Start();

            if (hash.Contains(searchEl))
            {
                stopwatchHash.Stop();
                Console.WriteLine($"При поиске в HashSet затрачено: {stopwatchHash.ElapsedTicks}");

            }
            else
            {
                Console.WriteLine("Cтрока ненайдена в HashSet");
                stopwatchHash.Reset();
            }
            
            if (stopwatchArr.ElapsedTicks > stopwatchHash.ElapsedTicks) Console.WriteLine("Быстрее поиск в HashSet");
            else Console.WriteLine("Быстрее поиск в массиве");

        }

        /// <summary>
        /// Интерфейс для бинарного дерева
        /// </summary>
        public interface ITree
        {
            TreeNode GetRoot();
            void AddItem(int value); // добавить узел
            void RemoveItem(int value); // удалить узел по значению
            TreeNode GetNodeByValue(int value); //получить узел дерева по значению
            void PrintTree(); //вывести дерево в консоль
        }

        /// <summary>
        /// Класс узла дерева
        /// </summary>
        public class TreeNode
        {
            public int Value { get; set; }

            public TreeNode Parrent { get; set; }
            public TreeNode LeftChild { get; set; }
            public TreeNode RightChild { get; set; }
        }

        /// <summary>
        /// Класс бинарного дерева
        /// </summary>
        public class Tree: ITree
        {
            //int count = 0;

            public TreeNode Root { get; set; }

            public Tree()
            {
                Root = null;
            }
            public Tree(int value)
            {
                Root = new TreeNode { Value = value };
            }
            public void AddItem(int value)
            {
                if (Root == null)
                {
                    Root = new TreeNode { Value = value };
                    //count++;
                    return;
                }
                TreeNode tmp = Root;
                while (tmp != null)
                {
                    if (value >= tmp.Value)
                    {
                        if (tmp.RightChild != null)
                        {
                            tmp = tmp.RightChild;
                            continue;
                        }
                        else
                        {
                            tmp.RightChild = new TreeNode { Value = value};
                            tmp.RightChild.Parrent = tmp;
                            //count++;
                            return;
                        }
                    }
                    else if (value < tmp.Value)
                    {
                        if (tmp.LeftChild != null)
                        {
                            tmp = tmp.LeftChild;
                            continue;
                        }
                        else
                        {
                            tmp.LeftChild = new TreeNode { Value = value };
                            tmp.LeftChild.Parrent = tmp;
                            //count++;
                            return;
                        }
                    }
                    else
                    {
                        throw new Exception("Wrong tree state"); //Дерево построено неправильно
                    }
                }
                return;
            }

            public TreeNode GetNodeByValue(int value)
            {
                if (Root == null)
                {
                    return null;
                }

                TreeNode node = Root;
                while (node != null)
                {
                    if (node.Value == value) return node;

                    if (node.Value < value)
                    {
                        node = node.RightChild;
                    }
                    else
                    {
                        node = node.LeftChild;
                    }
                }

                return null;
            }

            public TreeNode GetRoot()
            {
                return Root;
            }

            public void PrintTree()
            {
                if (Root != null)
                {
                    PreOrderTravers(Root);
                }
                else 
                {
                    Console.WriteLine("Дерево пустое");
                }
            }

            private void PreOrderTravers(TreeNode root)
            {
                if (root != null)
                {
                    Console.Write($"{root.Value}");
                    if (root.LeftChild != null || root.RightChild != null)
                    {
                        Console.Write("(");
                        if (root.LeftChild != null) PreOrderTravers(root.LeftChild);
                        else Console.Write("empty");

                        Console.Write(",");

                        if (root.RightChild != null) PreOrderTravers(root.RightChild);
                        else Console.Write("empty");

                        Console.Write(")");
                    }
                }
            }

            public void RemoveItem(int value)
            {
                TreeNode node = GetNodeByValue(value);
                if (node != null)
                {
                    // если найденный узел - это лист
                    if (node.LeftChild == null && node.RightChild == null)
                    {
                        if (node.Value >= node.Parrent.Value) node.Parrent.RightChild = null;
                        else node.Parrent.LeftChild = null;
                        return;
                    }
                    // если найденный узел - это корень
                    if (node == Root)
                    {
                        // нет левого потомка
                        if (Root.LeftChild == null)
                        {
                            Root = Root.RightChild;
                            return;
                        }
                        // нет правого потомка
                        if (Root.RightChild == null)
                        {
                            Root = Root.LeftChild;
                            return;
                        }
                        // есть оба потомка
                        TreeNode tmp = Root.RightChild;
                        // ищем самый левый лист у правого потомка
                        while (tmp.LeftChild != null) tmp = tmp.LeftChild;
                        
                        tmp.LeftChild = Root.LeftChild;
                        Root = Root.RightChild;
                    }
                    else
                    {
                        // если у найденного узла есть левый потомок
                        if (node.LeftChild != null)
                        {
                            // ищем самый правый лист у левого потомка
                            TreeNode tmp = node.LeftChild;
                            while (tmp.RightChild != null) tmp = tmp.RightChild;
                            // присваиваем правый потомок правому потомоку самого правого листа левого потомка
                            tmp.RightChild = node.RightChild;
                            node.RightChild.Parrent = tmp;
                            node.LeftChild.Parrent = node.Parrent;
                            if (node.Parrent.RightChild == node) node.Parrent.RightChild = node.LeftChild;
                            else node.Parrent.LeftChild = node.LeftChild;

                        }
                        else
                        {
                            if (node.Parrent.RightChild == node) node.Parrent.RightChild = node.RightChild;
                            else node.Parrent.LeftChild = node.RightChild;
                            node.RightChild.Parrent = node.Parrent;
                        }
                    }

                }

            }
        }
        static void Main(string[] args)
        {
            // 1. Протестируйте поиск строки в HashSet и в массиве
            
            #region case1

            string str100 = String.Empty;
            string str1000 = String.Empty;
            string str5000 = String.Empty;
            string str10k = String.Empty;
            string str900k = String.Empty;


            for (int i = 0; i < size; i++)
            {
                string temp = Guid.NewGuid().ToString();
                strArr[i] = temp;
                hash.Add(temp);
                switch (i)
                {
                    case 200: str100 = temp; break;
                    case 1000: str1000 = temp; break;
                    case 5000: str5000 = temp; break;
                    case 10_000: str10k = temp; break;
                    case 900_000: str900k = temp; break;
                }
            }
            //100
            for (int i = 0; i < 5; i++)
            {
                SearchIn(str100, 100);
                //1000
                SearchIn(str1000, 1000);
                //10k
                SearchIn(str10k, 1000);
                //900k
                SearchIn(str900k, 900_000);
            }

            Console.WriteLine("\nВывод: для количества элементов порядка 1000 стоит использовать массив,"
                              + "\nдо 10_000 ситуация паритетная, стоит испрользовать HashSet" 
                                );

            Console.ReadLine();
 
            #endregion
           
            // 2. Реализуйте двоичное дерево и метод вывода его в консоль
            #region case2
            int[] arr = new int[] { 8, 3, 10, 1, 6, 9, 14, 4, 7, 12, 16, 11, 13, 15, 17};

            Tree tree = new Tree();

            for (int i = 0; i < arr.Length; i++)
            {
                tree.AddItem(arr[i]);
            }

            Console.Write("Исходное дерево: ");
            tree.PrintTree();
            Console.WriteLine($"\tRootValue = {tree.GetRoot().Value}");
            Console.WriteLine();

            int value = 17;
            //удаление листа
            Console.WriteLine($"Удаление листа с Value={value}");
            tree.RemoveItem(value);
            Console.Write("Перестроенное дерево: ");
            tree.PrintTree();
            Console.WriteLine();

            value = 14;
            // удаление узла
            Console.WriteLine($"Удаление узла с Value={value}");
            tree.RemoveItem(value);
            Console.Write("Перестроенное дерево: ");
            tree.PrintTree();
            Console.WriteLine();

            value = 4;
            // удаление узла
            Console.WriteLine($"Удаление листа с Value={value}");
            tree.RemoveItem(value);
            Console.Write("Перестроенное дерево: ");
            tree.PrintTree();
            Console.WriteLine();

            value = 6;
            // удаление узла
            Console.WriteLine($"Удаление узла с Value={value}");
            tree.RemoveItem(value);
            Console.Write("Перестроенное дерево: ");
            tree.PrintTree();
            Console.WriteLine();

            value = 8;
            // удаление корня
            Console.WriteLine($"\nУдаление корня с Value={value}");
            tree.RemoveItem(value);
            Console.Write("Перестроенное дерево: ");
            tree.PrintTree();
            Console.WriteLine($"\tRootValue = {tree.GetRoot().Value}");
            Console.WriteLine();


            Console.ReadLine();

            #endregion
        }
    }
}
