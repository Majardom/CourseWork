using System;

namespace Recognito.Algorithms
{
    public class LinearPredictiveCoding
    {
        private readonly int windowSize;
        private readonly int poles;
        private readonly double[] output;
        private readonly double[] error;
        private readonly double[] k;
        private readonly double[][] matrix;


        public LinearPredictiveCoding(int windowSize, int poles)
        {
            this.windowSize = windowSize;
            this.poles = poles;
            output = new double[poles];
            error = new double[poles];
            k = new double[poles];
            matrix = new double[poles][];
        }


        public double[][] ApplyLinearPredictiveCoding(double[] window)
        {

            if (windowSize != window.Length)
            {
                throw new ArgumentException($"Given window length was not equal to the one provided in constructor : [{window.Length}] != [{windowSize}]");
            }

            ArrayHelper.Fill(k, 0.0d);
            ArrayHelper.Fill(output, 0.0d);
            ArrayHelper.Fill(error, 0.0d);

            for (int i = 0; i < matrix.Length; i++)
            {
                matrix[i] = matrix[i] ?? new double[poles];
                ArrayHelper.Fill(matrix[i], 0.0d);

            }

            DiscreteAutocorrelationAtLagJ dalj = new DiscreteAutocorrelationAtLagJ();
            double[] autocorrelations = new double[poles];
            for (int i = 0; i < poles; i++)
            {
                autocorrelations[i] = dalj.Autocorrelate(window, i);
            }

            error[0] = autocorrelations[0];

            for (int m = 1; m < poles; m++)
            {
                double tmp = autocorrelations[m];
                for (int i = 1; i < m; i++)
                {
                    tmp -= matrix[m - 1][i] * autocorrelations[m - i];
                }
                k[m] = tmp / error[m - 1];

                for (int i = 0; i < m; i++)
                {
                    matrix[m][i] = matrix[m - 1][i] - k[m] * matrix[m - 1][m - i];
                }
                matrix[m][m] = k[m];
                error[m] = (1 - (k[m] * k[m])) * error[m - 1];
            }

            for (int i = 0; i < poles; i++)
            {
                if (Double.IsNaN(matrix[poles - 1][i]))
                {
                    output[i] = 0.0;
                }
                else
                {
                    output[i] = matrix[poles - 1][i];
                }
            }

            return new double[][] { output, error };
        }
    }
}
