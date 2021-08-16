using System;
using System.Diagnostics;

namespace HillClimberProjects
{
    class Program
    {
        static string GenerateString(int length)
        {
            Random random = new Random();
            string returnString = "";
            for(int i = 0; i < length; i ++)
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
            if(current[mutateIndex] == 126)
            {
                changeValue = -1;
            }
            else if(current[mutateIndex] == 33)
            {
                changeValue = 1;
            }
            else if(changeValue == 0)
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
            for(int i = 0; i < target.Length; i ++)
            {
                numerator += Math.Abs(target[i] - current[i]);
            }
            return numerator / target.Length;
        }

        static void Main(string[] args)
        {
            string target = "Hello World!";
            string start = GenerateString(target.Length);
            float currentError = CalculateError(target, start);
            Console.WriteLine("StartVl: " + start + " Error: " + currentError);
            Stopwatch Delay = new Stopwatch();
            Stopwatch.StartNew();

            while(currentError != 0)
            {
                string temp = MutateString(start);
                if(currentError > CalculateError(target, temp))
                {
                    start = temp;
                    currentError = CalculateError(target, start);
                    Console.WriteLine("Mutated: " + start + " Error: " + currentError);
                    Delay.Restart();
                    while (Delay.ElapsedMilliseconds < 100) ;
                }

            }
        }
    }
}
