using System;

namespace Pred
{
    class Program
    {
        private double score = 0, baseScore = 0, logSqSum = 0;
        private int notesTotal = 0;
        private double[] logSq; // Point incrementease border
        private int sepPoint = 50;

        // 区分求積法によるスコア計算の準備
        private double BaseScoreDecision(int notesTotal)
        {
            int denominator; // 分割数
            logSq = new double[sepPoint];

            if (notesTotal >= sepPoint)
                denominator = sepPoint;
            else
                denominator = notesTotal;

            for (int i = 0; i < denominator; i++)
            {
                logSq[i] = Math.Log10(1 + (9 * ((double)i + 1) / (double)denominator));
                logSqSum += logSq[i];
            }
            return 1000000 / (logSqSum + (double)notesTotal - (double)denominator);
        }

        private double CalcMinScore(int notesTotal, int missFreq)
        {
            double returner = 0;

            // 通し
            for (int combo = 0; combo >= notesTotal;)
            {
                // ミスるまで増やすをループ
                for (int increment = 0; increment == missFreq; increment++)
                {
                    combo++; // 通しコンボ数

                    if (increment <= sepPoint && increment > 0)
                        returner += baseScore * logSq[increment - 1];
                    else if (increment <= sepPoint)
                        returner += baseScore * logSq[0];
                    else
                        returner += baseScore;
                }
            }

            return returner;
        }

        static void Main()
        {
            var ins = new Program();
            ins.baseScore = ins.BaseScoreDecision(500);
            
            Console.WriteLine("500 notes, miss in each 50 notes:");
            Console.WriteLine("Hello World!");
        }
    }
}
