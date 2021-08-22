using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;

namespace HillClimberProjects
{
    class Program
    {
        static string GenerateString(int length)
        {
            string returnString = "";
            for (int i = 0; i < length; i++)
            {
                returnString += (char)random.Next(33, 127);
            }
            return returnString;
        }

        static string MutateString(string current)
        {
            int mutateIndex = random.Next(0, current.Length);
            int changeValue = random.Next(0, 2);
            if (current[mutateIndex] == 126)
            {
                changeValue = -1;
            }
            else if (current[mutateIndex] == 33)
            {
                changeValue = 1;
            }
            else if (changeValue == 0)
            {
                changeValue = -1;
            }


            string returnString = current;
            char[] returnValue = current.ToCharArray();
            returnValue[mutateIndex] = (char)(returnString[mutateIndex] + changeValue);
            return new string(returnValue);
        }

        static float CalculateError(string target, string current)
        {
            float numerator = 0;
            for (int i = 0; i < target.Length; i++)
            {
                numerator += Math.Abs(target[i] - current[i]);
            }
            return numerator / target.Length;
        }

        static float CalculateError(Point[] points, (float, float) currentLine)
        {
            float currentError = 0;
            foreach (Point point in points)
            {
                currentError += Math.Abs(point.Y - currentLine.Item1 * point.X - currentLine.Item2);
            }
            return currentError;
        }

        static Random random = new Random();
        static (float, float) MutateLine(Point[] points, (float, float) currentLine)
        {
            float currentError = CalculateError(points, currentLine);
            int whatToChange;
            (float, float) nextLine;
            do
            {
                whatToChange = random.Next() % 2;
                nextLine = currentLine;
                if (whatToChange == 0)
                {
                    nextLine.Item1 += random.Next(1, 201) / 100f - 1;
                }
                else
                {
                    nextLine.Item2 += random.Next(1, 201) / 100f - 1;
                }
            }
            while (currentError < CalculateError(points, nextLine));
            return nextLine;
        }

        static (float, float) FindBestLine(Point[] points)
        {
            (float, float) line = (0, 0);
            for (int i = 0; i < 1000; i++)
            {
                line = MutateLine(points, line);
            }
            return line;
        }

        static double ErrorFunction(double input1, double input2)
        {
            return (input1 - input2) * (input1 - input2);
        }

        static void Main(string[] args)
        {
            //string target = "Hello World!";
            //string start = GenerateString(target.Length);
            //float currentError = CalculateError(target, start);
            //Console.WriteLine("StartVl: " + start + " Error: " + currentError);
            //Stopwatch Delay = new Stopwatch();
            //Stopwatch.StartNew();

            //while(currentError != 0)
            //{
            //    string temp = MutateString(start);
            //    if(currentError > CalculateError(target, temp))
            //    {
            //        start = temp;
            //        currentError = CalculateError(target, start);
            //        Console.WriteLine("Mutated: " + start + " Error: " + currentError);
            //        Delay.Restart();
            //        while (Delay.ElapsedMilliseconds < 100) ;
            //    }

            //}

            //Point[] points = new Point[]
            //{
            //    new Point(1,1),
            //    new Point(0,0),
            //    new Point(-3, -3),
            //    new Point(-4, -3),
            //    new Point(-4, -5),
            //};

            //Point[] points = new Point[]
            //{
            //    new Point(3,3),
            //    new Point(-3,3),
            //    new Point(-10, -20),
            //};

            //var bestLine = FindBestLine(points);
            //{.75, -1.25}, .5
            //Perceptron perceptron = new Perceptron(new double[] { .6, -1.4 }, .5, ErrorFunction);
            //double[][] inputs = new double[][]
            //{
            //     new double[] {0, 0},
            //     new double[] {0.3, -0.7},
            //     new double[] {1, 1},
            //     new double[] {-1, -1},
            //     new double[] {-0.5, 0.5},
            //};
            //double[] expected = new double[]
            //{
            //    0.5, 
            //    1.6, 
            //    0, 
            //    1,
            //    -0.5
            //};
            //double currentError = 0;
            //do
            //{
            //    currentError = perceptron.Train(inputs, expected, perceptron.GetError(inputs, expected), random);
            //    Console.SetCursorPosition(0, 0);
            //    Console.WriteLine($"{currentError}");
            //    var actual = perceptron.Compute(inputs);
            //    for (int x = 0; x < actual.Length; x++)
            //    {
            //        Console.WriteLine($"Value: {x + 1}");
            //        Console.WriteLine($"\tActual: {actual[x]}");
            //        Console.WriteLine($"\tExpected: {expected[x]}");
            //    }
            //    Thread.Sleep(10);
            //}
            //while (currentError > .0001);
            //var result = perceptron.Compute(inputs);

            //Perceptron line = new Perceptron(3, random, ErrorFunction, .01);

            //double[][] inputs = new double[][]
            //{
            //    new double[]{0,0,0},
            //    new double[]{0,0,1},
            //    new double[]{0,1,0},
            //    new double[]{0,1,1},

            //    new double[]{1,0,0},
            //    new double[]{1,0,1},
            //    new double[]{1,1,0},
            //    new double[]{1,1,1},
            //};

            //double[] expected = new double[]
            //{
            //    0,
            //    0,
            //    0,
            //    1,

            //    0,
            //    1,
            //    1,
            //    1,
            //};

            Perceptron line = new Perceptron(2, random, ErrorFunction, .01);
            double[][] inputs = new double[][]
            {
                new double[] { 1, 4},
                new double[] { 3, 5},
                new double[] { 3.5, 3},
                new double[] { 4, 1.5},

                new double[] { 2, 2},
                new double[] { 2, 1},
            };

            double[] expected = new double[]
            {
                1,
                1,
                1,
                1,

                0,
                0,
            };

            double currentError = line.GetError(inputs, expected);
            while (true)
            {
                currentError = line.Train(inputs, expected, currentError, random);
                Console.SetCursorPosition(0, 0);
                Console.WriteLine($"{currentError}");
                double[] actual = line.Compute(inputs);
                for (int i = 0; i < actual.Length; i++)
                {
                    Console.WriteLine($"Index: {i}");
                    Console.WriteLine($"\tValue: {actual[i]}");
                    Console.WriteLine($"\tExpected: {expected[i]}");
                }
                Console.WriteLine($"Bias: {line.bias}");
                foreach(double weight in line.weights)
                {
                    Console.WriteLine($"Weight: {weight}");
                }
                //Thread.Sleep(10);
            }
        }
    }
}
