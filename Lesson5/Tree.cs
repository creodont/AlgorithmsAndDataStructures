using System;

namespace Lesson5
{
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
    public class Tree : ITree
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
                        tmp.RightChild = new TreeNode { Value = value };
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
}