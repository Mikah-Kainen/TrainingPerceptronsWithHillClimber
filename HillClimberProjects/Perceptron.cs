using System;
using System.Collections.Generic;
using System.Text;

namespace HillClimberProjects
{
    public class Perceptron
    {
        double[] weights;
        public double bias;
        double mutationRate;
        Func<double, double, double> errorFunction;

        public Perceptron(double[] weights, double bias, Func<double, double, double> errorFunction, double mutationRate)
        {
            this.weights = weights;
            this.bias = bias;
            this.errorFunction = errorFunction;
            this.mutationRate = mutationRate;
        }

        public Perceptron(int amountOfInputs, Random random, Func<double, double, double> errorFunction, double mutationRate)
            : this(new double[amountOfInputs], 0, errorFunction, mutationRate)
        {
            Randomize(random, 0, 10);
        }


        public void Randomize(Random random, double min, double max)
        {
            for(int i = 0; i < weights.Length; i ++)
            {
                weights[i] = random.NextDouble(min, max);
            }
            bias = random.NextDouble(min, max);
        }

        public double Compute(double[] inputs)
        {
            double returnValue = bias;
            for(int i = 0; i < inputs.Length; i ++)
            {
                returnValue += inputs[i] * weights[i];
            }
            return returnValue;
        }

        public double[] Compute(double[][] inputs)
        {
            double[] returnArray = new double[inputs.Length];
            for(int i = 0; i < returnArray.Length; i ++)
            {
                returnArray[i] = Compute(inputs[i]);
            }
            return returnArray;
        }

        public double GetError(double[][] inputs, double[] expected)
        {
            double returnVal = 0;
            for(int i = 0; i < inputs.Length; i ++)
            {
                returnVal += errorFunction(Compute(inputs[i]), expected[i]);
            }
            return returnVal / inputs.Length;
        }

        public double Train(double[][] inputs, double[] expected, double currentError, Random random)
        {
            int mutateTarget = random.Next(0, weights.Length + 1);
            double previousValue;
            if (mutateTarget == weights.Length)
            {
                previousValue = bias;
                bias = random.NextDouble(bias - mutationRate, bias + mutationRate);
            }
            else
            {
                previousValue = weights[mutateTarget];
                weights[mutateTarget] = random.NextDouble(weights[mutateTarget] - mutationRate, weights[mutateTarget] + mutationRate);
            }
            double nextError = GetError(inputs, expected);
            if(nextError < currentError)
            {
                return nextError;
            }

            if(mutateTarget == weights.Length)
            {
                bias = previousValue;
            }
            else
            {
                weights[mutateTarget] = previousValue;
            }
            return currentError;
        }
    }
}
