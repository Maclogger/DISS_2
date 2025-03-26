namespace DISS_2.BackEnd.Statistics
{
    public class SampleStat(string name) : Statistics(name)
    {
        public long Count { get; private set; } = 0;
        private double _mean = 0.0;

        public override double CalcMean() => _mean;

        public void AddValue(int value)
        {
            Count++;
            _mean += (value - _mean) / Count;
        }

        public override string ToString()
        {
            return $"Sample Statistics: [{Name}]\n" +
                   $"Mean: {CalcMean()}\n" +
                   $"Count: {Count}";
        }
    }
}
