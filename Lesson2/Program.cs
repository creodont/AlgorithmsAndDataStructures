using System;

namespace Lesson2
{
    class Program
    {
        //Начальную и конечную ноду нужно хранить в самой реализации интерфейса
        public interface ILinkedList
        {
            int GetCount(); // возвращает количество элементов в списке
            void AddNode(int value); // добавляет новый элемент списка
            void AddNodeAfter(Node node, int value); // добавляет новый элемент списка после определённого элемента
            void RemoveNode(int index); // удаляет элемент по порядковому номеру
            void RemoveNode(Node node); // удаляет указанный элемент
            Node FindNode(int searchValue); // ищет элемент по его значению
        }

        /// <summary>
        /// Класс узла
        /// </summary>
        public class Node
        {
            public int Value { get; set; }
            public Node NextNode { get; set; }
            public Node PrevNode { get; set; }
        }

        /// <summary>
        /// Двусвязанный список
        /// </summary>
        public class NodeLinkedList : ILinkedList
        {
            int count = 0;


            Node head; // головной элемент
            Node tail; // конечный элемент


            public void AddNode(int value)
            {
                Node newNode = new Node { Value = value };

                if (head == null)
                {
                    head = newNode;
                }
                else
                {
                    tail.NextNode = newNode;
                    newNode.PrevNode = tail;
                }
                tail = newNode;
                count++;
            }

            public void AddNodeAfter(Node node, int value)
            {
                if (head == null)
                {
                    throw new Exception("Не найден узел, список пуст");
                }
                else
                {
                    if (node == tail) AddNode(value);
                    else
                    {
                        Node current = head;
                        Node newNode = new Node { Value = value };
                        bool flag = false;
                        do
                        {
                            if (current == node)
                            {
                                flag = true;
                                // встраиваем элемент в список
                                newNode.NextNode = current.NextNode;
                                newNode.PrevNode = current;
                                current.NextNode.PrevNode = newNode;
                                current.NextNode = newNode;
                                break;
                            }
                            current = current.NextNode;
                        } while (current != null);

                        if (!flag) throw new Exception("На найден узел");
                        count++;
                    }
                }
            }

            public Node FindNode(int searchValue)
            {
                Node node = head;

                while (node != null)
                {
                    if (node.Value == searchValue) return node;

                    node = node.NextNode;
                }
                return null;
            }

            public int GetCount()
            {
                return count;
            }

            public void RemoveNode(int index)
            {
                if(index < 0) throw new Exception($"Ошибка!!! Индекс \"{index}\" отрицательное число ");
                if (count == 0) throw new Exception("Список пуст");
                if(index > count-1) throw new Exception($"Ошибка!!! Индекс \"{index}\" за пределами размера списка из \"{count}\" элементов");


                int i = 0;
                Node node = head;

                while(true)
                {
                    if (i == index)
                    {
                        
                        if (i == 0) head = node.NextNode;
                        else node.PrevNode.NextNode = node.NextNode;

                        if (i == (count - 1)) tail = node.PrevNode;
                        else node.NextNode.PrevNode = node.PrevNode;

                        count--;
                        break;
                    }
                    node = node.NextNode;
                    if (node == null) throw new Exception("Ошибка!!! Индекс не найден");

                }


            }

            public void RemoveNode(Node node)
            {
                if (node == null) throw new Exception("Ошибка!!! Передан пустой элемент");

                Node sNode = head;

                while (true)
                {
                    if (sNode == null) throw new Exception("Ошибка!!! Элемент не найден");

                    if (sNode == node)
                    {
                        
                        if (node == head) head = node.NextNode;
                        else node.PrevNode.NextNode = node.NextNode;

                        if (node == tail) tail = node.PrevNode;
                        else node.NextNode.PrevNode = node.PrevNode;

                        count--;
                        break;

                    }
                    sNode = sNode.NextNode;
                }

            }

            /// <summary>
            /// Метод вывода значений списка (для отладки) 
            /// </summary>
            public void PrintValueList()
            {
                Node node = tail;
                Console.Write("Значения списка (порядок с конца): ");
                if(node == null)
                {
                    Console.Write("пусто");
                    return;
                }
                while (node != null)
                {
                    Console.Write($"{node.Value} ");
                    node = node.PrevNode;
                }
                Console.WriteLine();
            }
        }
        
        /// <summary>
        /// Метод бинарного поиска
        /// </summary>
        /// <param name="inputArray"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static int BinarySearch(int[] inputArray, int searchValue)
        {
            int min = inputArray[0];
            int max = inputArray.Length - 1;
            while (min <= max)
            {
                int mid = (min + max) / 2;
                if (searchValue == inputArray[mid])
                {
                    return mid;
                }
                else if (searchValue < inputArray[mid])
                {
                    max = mid - 1;
                }
                else
                {
                    min = mid + 1;
                }
            }
            return -1;
        }

        static void Main(string[] args)
        {
            // 1. Двусвязный список
            #region case1
            NodeLinkedList list = new NodeLinkedList();

            Console.WriteLine("Добавить элементы в список");
            list.AddNode(6);
            list.AddNode(3);
            list.AddNode(2);
            list.PrintValueList();
            Console.WriteLine($"Количество элеметов в списке = {list.GetCount()}\n");

            Console.WriteLine("Добавить элемент с Value=10 в список после элемента с Value=3");
            list.AddNodeAfter(list.FindNode(3), 10);
            list.PrintValueList();
            Console.WriteLine($"Количество элеметов в списке = {list.GetCount()}\n");

            Console.WriteLine("Удалить элемент с индексом 0");
            list.RemoveNode(0);
            list.PrintValueList();
            Console.WriteLine($"Количество элеметов в списке = {list.GetCount()}\n");

            Console.WriteLine("Удалить элемент с Value=10");
            list.RemoveNode(list.FindNode(10));
            list.PrintValueList();
            Console.WriteLine($"Количество элеметов в списке = {list.GetCount()}\n");

            Console.WriteLine("Удалить пустой элемент");
            try 
            {
                list.RemoveNode(null);

            }
            catch (Exception e)
            {
                Console.WriteLine($"Произошло исключение: {e.Message}\n");
            }

            Console.WriteLine("Удалить несуществующий элемент");
            try
            {
                list.RemoveNode(new Node { Value = 0});

            }
            catch (Exception e)
            {
                Console.WriteLine($"Произошло исключение: {e.Message}\n");
            }

            Console.ReadLine();
            #endregion

            // 2. Двоичный поиск
            // О(log2 n)
            #region case2

            int n = 100;
            int[] segment = new int[100];

            for (int i = 0; i < n; i++)
            {
                segment[i] = i + 5; 
            }

            Console.WriteLine($"Границы отсортированного массива: Min={segment[0]}, Max={segment[segment.Length-1]}");
            int searchValue = 25;
            Console.WriteLine($"Найти индекс числа {searchValue} в массиве: {BinarySearch(segment, searchValue)}");
            searchValue = 50;
            Console.WriteLine($"Найти индекс числа {searchValue} в массиве: {BinarySearch(segment, searchValue)}");
            searchValue = 100;
            Console.WriteLine($"Найти индекс числа {searchValue} в массиве: {BinarySearch(segment, searchValue)}");

            searchValue = 1;
            Console.WriteLine($"Найти индекс числа {searchValue} в массиве: {BinarySearch(segment, searchValue)}");
            searchValue = 107;
            Console.WriteLine($"Найти индекс числа {searchValue} в массиве: {BinarySearch(segment, searchValue)}");
            Console.ReadLine();
            #endregion
        }
    }
}
