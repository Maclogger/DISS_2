namespace DISS_2.BackEnd.Statistics
{
    public class SampleStat(string name, bool useTimeFormat = true) : Statistics(name, useTimeFormat)
    {
        public override double CalcMean() => Mean;
        public override string GetTypeString()
        {
            return "Sample statistics";
        }

        public void AddValue(double value)
        {
            Count++;
            Mean += (value - Mean) / Count;
        }

        public override string ToString()
        {
            return $"Sample Statistics: [{Name}]\n" +
                   $"Mean: {CalcMean()}\n" +
                   $"Count: {Count}";
        }

        public override void Clear()
        {
            Mean = 0.0;
            base.Clear();
        }
    }
}
