namespace DISS_2.BackEnd.Statistics;

public class SampleStat(string name, bool useTimeFormat = true)
    : Statistics(name, useTimeFormat)
{
    private double _sumOfSquaredDeviations = 0.0;

    public double StdDev => Count > 1
        ? Math.Sqrt(_sumOfSquaredDeviations / (Count - 1))
        : 0;

    public override double CalcMean() => Mean;
    public override double CalcStdDev() => StdDev;
    public override double CalcLowerIS() => CalculateConfidenceInterval().Lower;
    public override double CalcUpperIS() => CalculateConfidenceInterval().Upper;


    public override string GetTypeString()
    {
        return "Sample statistics";
    }

    public void AddValue(double value)
    {
        double oldMean = Mean;
        Count++;
        Mean += (value - Mean) / Count;
        _sumOfSquaredDeviations += (value - oldMean) * (value - Mean);
    }

    public (double Lower, double Upper) CalculateConfidenceInterval(double confidenceLevel = 0.95)
    {
        double standardError = StdDev / Math.Sqrt(Count);

        double criticalValue;
        if (Count <= 30) return (double.NaN, double.NaN);

        criticalValue = 1.96;
        double margin = criticalValue * standardError;
        return (Mean - margin, Mean + margin);
    }

    public override string ToString()
    {
        var ci = CalculateConfidenceInterval();
        return $"Sample Statistics: [{Name}]\n" +
               $"Mean: {CalcMean()}\n" +
               $"StdDev: {StdDev}\n" +
               $"Count: {Count}\n" +
               $"95% Confidence Interval: [{ci.Lower}, {ci.Upper}]";
    }

    public override void Clear()
    {
        Mean = 0.0;
        _sumOfSquaredDeviations = 0.0;
        base.Clear();
    }
}