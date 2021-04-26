using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Lesson3
{

    class Program
    {

        static void Main(string[] args)
        {
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
            Console.ReadLine();
        }


    }

    public class BechmarkClass
    {
        public class PointClass
        {
            public int X;
            public int Y;
        }

        public struct PointStruct
        {
            public int X;
            public int Y;
        }

        /// <summary>
        /// Обычный метод расчёта дистанции со ссылочным типом (PointClass — координаты типа float)
        /// </summary>
        /// <param name="pointOne"></param>
        /// <param name="pointTwo"></param>
        /// <returns></returns>
        public float PointDistance(PointClass pointOne, PointClass pointTwo)
        {
            float x = pointOne.X - pointTwo.X;
            float y = pointOne.Y - pointTwo.Y;
            return MathF.Sqrt((x * x) + (y * y));
        }

        /// <summary>
        /// Обычный метод расчёта дистанции со значимым типом (PointStruct — координаты типа float)
        /// </summary>
        /// <param name="pointOne"></param>
        /// <param name="pointTwo"></param>
        /// <returns></returns>
        public float PointDistance(PointStruct pointOne, PointStruct pointTwo)
        {
            float x = pointOne.X - pointTwo.X;
            float y = pointOne.Y - pointTwo.Y;
            return MathF.Sqrt((x * x) + (y * y));
        }

        /// <summary>
        /// Обычный метод расчёта дистанции со значимым типом (PointStruct — координаты типа double).
        /// </summary>
        /// <param name="pointOne"></param>
        /// <param name="pointTwo"></param>
        /// <returns></returns>
        public static double PointDistanceDouble(PointStruct pointOne, PointStruct pointTwo)
        {
            double x = pointOne.X - pointTwo.X;
            double y = pointOne.Y - pointTwo.Y;
            return Math.Sqrt((x * x) + (y * y));
        }

        /// <summary>
        /// Метод расчёта дистанции без квадратного корня со значимым типом (PointStruct — координаты типа float).
        /// </summary>
        /// <param name="pointOne"></param>
        /// <param name="pointTwo"></param>
        /// <returns></returns>
        public static float PointDistanceShort(PointStruct pointOne, PointStruct pointTwo)
        {
            float x = pointOne.X - pointTwo.X;
            float y = pointOne.Y - pointTwo.Y;
            return (x * x) + (y * y);
        }

        public PointClass[] pointClassArray1 = new PointClass[] { new PointClass{X = 1, Y = 2 },
                                                                  new PointClass{X = 2, Y = 4 },
                                                                  new PointClass{X = 36, Y = -1 },
                                                                  new PointClass{X = 7, Y = 12 },
                                                                };
        public PointClass[] pointClassArray2 = new PointClass[] { new PointClass{X = 2, Y = 12 },
                                                                  new PointClass{X = 4, Y = 2 },
                                                                  new PointClass{X = -1, Y = 36 },
                                                                  new PointClass{X = 12, Y = 7 },
                                                                };
        public PointStruct[] pointStructArray1 = new PointStruct[]{ new PointStruct{X = 1, Y = 2 },
                                                                     new PointStruct{X = 2, Y = 4 },
                                                                     new PointStruct{X = 36, Y = -1 },
                                                                     new PointStruct{X = 7, Y = 12 },
                                                                };
        public PointStruct[] pointStructArray2 = new PointStruct[] { new PointStruct{X = 2, Y = 12 },
                                                                     new PointStruct{X = 4, Y = 2 },
                                                                     new PointStruct{X = -1, Y = 36 },
                                                                     new PointStruct{X = 12, Y = 7 },
                                                                };

        [Params(0, 1, 2, 3)]
        public int arg;


        [Benchmark]
        public void TestPointDistance_Class()
        {
            PointDistance(pointClassArray1[arg], pointClassArray2[arg]);
        }

        [Benchmark]
        public void TestPointDistance_Struct()
        {
            PointDistance(pointStructArray1[arg], pointStructArray2[arg]);
        }

        [Benchmark]
        public void TestPointDistanceDouble()
        {
            PointDistanceDouble(pointStructArray1[arg], pointStructArray2[arg]);
        }

        [Benchmark]
        public void TestPointDistanceShort()
        {
            PointDistanceShort(pointStructArray1[arg], pointStructArray2[arg]);
        }

    }

}
