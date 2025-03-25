namespace DISS_2.BackEnd.Statistics;

public class SampleStat(string name) : Statistics(name)
{
    public long Sum { get; set; }
    public long Count { get; set; }

    public override double CalcMean()
    {
        Console.WriteLine($"Sum: {Sum}, Count: {Count}");
        return (double)Sum / Count;
    }

    public void AddValue(int value)
    {
        Sum += value;
        Count++;
    }

    public override string ToString()
    {
        double mean = CalcMean();
        string sol = $"Sample Statistics: [{Name}] \n ";
        sol += $"Mean: {mean}";
        sol += $"Count: {Count}";
        return sol;
    }
}