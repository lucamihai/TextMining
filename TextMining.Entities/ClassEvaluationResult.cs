namespace TextMining.Entities
{
    public class ClassEvaluationResult
    {
        public string Name { get; set; }

        public int TruePositives { get; set; }
        public int FalsePositives { get; set; }

        public int TrueNegatives { get; set; }
        public int FalseNegatives { get; set; }

        public double GetAccuracy()
        {
            return ((double) TruePositives + TrueNegatives) / 
                   TruePositives + FalseNegatives + FalsePositives + TrueNegatives;
        }

        public double GetPrecision()
        {
            return (double) TruePositives / TruePositives + FalsePositives;
        }

        public double GetRecall()
        {
            return (double) TruePositives / TruePositives + FalseNegatives;
        }

        public override string ToString()
        {
            return $"Accuracy: {GetAccuracy()}; Precision: {GetPrecision()}; Recall: {GetRecall()}";
        }
    }
}