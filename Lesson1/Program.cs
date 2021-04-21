using System;
using System.Collections.Generic;

namespace Lesson1
{
    class Program
    {
        public const string VALID = "VALID TEST";
        public const string INVALID = "INVALID TEST";
        public const string NotNatural = " It is not a natural number";
        public const string Simple = "Simple number";
        public const string NotSimple = "Not simple number";

 
        /// <summary>
        /// Класс для проверки метода CheckNumber
        /// </summary>
        public class TestCaseCheckNumber
        {
            public int Number { get; set; }
            public string Expected { get; set; }
            public Exception ExpectedExeption { get; set; }
        }

        /// <summary>
        /// Метод для проверки метода CheckNumber
        /// </summary>
        /// <param name="testCaseCheckNumber"></param>
        static void TestCheckNumber(TestCaseCheckNumber testCaseCheckNumber)
        {
            try
            {
                var actual = CheckNumber(testCaseCheckNumber.Number);
                if (actual == testCaseCheckNumber.Expected)
                {
                    Console.WriteLine(VALID);
                }
                else
                {
                    Console.WriteLine(INVALID);
                }
            }
            catch (Exception e)
            {
                if (testCaseCheckNumber.ExpectedExeption != null)
                {
                    Console.WriteLine($"{VALID}: {e.Message}");
                }
                else
                {
                    Console.WriteLine($"{INVALID}: {e.Message}");
                }

            }
        }

        /// <summary>
        /// Метод определения типа числа (простое или составное)
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        static string CheckNumber(int n)
        {
            // выдать исключение, если число ненатуральное
            if (n <= 0) throw new Exception(NotNatural);

            int d = 0;

            for (int i = 2; i < n; i++)
            {
                if ((n % i) == 0) d++;
            }

            return (d == 0) ? Simple : NotSimple;
        }

        /// <summary>
        /// Метод рекурсивного вычисления чисел Фибоначчи
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        static int CalcFibonacciRec(int number)
        {
            // создать исключение, если число не натуральное
            if (number < 1) throw new Exception(NotNatural);

            // терминальное условие
            if (number == 1) return 0;
            if (number == 2) return 1;

            return CalcFibonacciRec(number - 1) + CalcFibonacciRec(number - 2);
        }

        /// <summary>
        /// Метод безрекурсивного вычисления чисел Фибоначчи
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        static int CalcFibonacciLoop(int number)
        {
            // создать исключение, если число не натуральное
            if (number < 1) throw new Exception(NotNatural);

            int fib = (number == 1)? 0: 1;
            int prev = 1;
            int pprev = 0;

            for (int i = 1; i < number-1; i++)
            {
                fib = prev + pprev;
                pprev = prev;
                prev = fib;
            }

            return fib;
        }


        static void Main(string[] args)
        {
            // 1. Напишите на C# функцию согласно блок-схеме
            #region case1
            List<TestCaseCheckNumber> MyTChkList = new List<TestCaseCheckNumber>() {
                new TestCaseCheckNumber() { Number = 1, Expected = Simple, ExpectedExeption = null },
                new TestCaseCheckNumber() { Number = 2, Expected = Simple, ExpectedExeption = null },
                new TestCaseCheckNumber() { Number = 5, Expected = Simple, ExpectedExeption = null },
                new TestCaseCheckNumber() { Number = 15, Expected = NotSimple, ExpectedExeption = null },
                new TestCaseCheckNumber() { Number = -1, Expected = String.Empty, ExpectedExeption = new Exception(NotNatural) }
            };

            foreach (TestCaseCheckNumber testcase in MyTChkList)
            {
                TestCheckNumber(testcase);
            }

            Console.ReadLine();
            #endregion

            // 2. Посчитайте сложность функции
            // O(inputArray.Length^3)

            // 3. Реализуйте функцию вычисления числа Фибоначчи
            #region case3
            Dictionary<int, int> fibPairs = new Dictionary<int, int>
            {
                {1, 0},
                {2, 1},
                {8, 13},
                {32, 1346269}
            };

            foreach (KeyValuePair<int, int> pfib in fibPairs)
            {
                Console.WriteLine($"Число Фибоначчи {pfib.Key}: " +
                    $"рекурсивно - \"{CalcFibonacciRec(pfib.Key)}\", " +
                    $"без рекурсии - \"{CalcFibonacciLoop(pfib.Key)}\", " +
                    $"ожидаемое - \"{pfib.Value}\"");
            }

            // проверка на некорректный ввод
            bool flag = true;
            try
            {
                CalcFibonacciRec(-1);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Исключение при отрицательном аргументе: {e.Message}");
                flag = false;
            }
            if (flag) Console.WriteLine("Ошибка, не возникло исключения!!!");

            flag = true;
            try
            {
                CalcFibonacciLoop(-1);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Исключение при отрицательном аргументе: {e.Message}");
                flag = false;
            }
            if (flag) Console.WriteLine("Ошибка, не возникло исключения!!!");

            Console.ReadLine();
            #endregion
        }
    }
}
