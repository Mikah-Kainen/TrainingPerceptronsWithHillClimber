using System;
using System.Diagnostics;
using System.Drawing;

namespace HillClimberProjects
{
    class Program
    {
        static string GenerateString(int length)
        {
            Random random = new Random();
            string returnString = "";
            for (int i = 0; i < length; i++)
            {
                returnString += (char)random.Next(33, 127);
            }
            return returnString;
        }

        static string MutateString(string current)
        {
            Random random = new Random();
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

        static (float, float) MutateLine(Point[] points, (float, float) currentLine)
        {
            float currentError = CalculateError(points, currentLine);
            Random random = new Random();
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

            Point[] points = new Point[]
            {
                new Point(3,3),
                new Point(-3,3),
                new Point(-10, -20),
            };

            var bestLine = FindBestLine(points);
        }
    }
}
