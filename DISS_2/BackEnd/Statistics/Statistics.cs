namespace DISS_2.BackEnd.Statistics;

public abstract class Statistics(string name, bool useTimeFormat = true)
{
    public string Name { get; } = name;
    public long Count { get; set; }
    public double Mean { get; set; }

    public bool UseTimeFormat { get; set; } = useTimeFormat;
    public abstract double CalcMean();
    public abstract double CalcStdDev();
    public abstract double CalcLowerIS();
    public abstract double CalcUpperIS();

    public virtual string GetTypeString()
    {
        return "Statistic";
    }

    public virtual void Clear()
    {
        Count = 0;
        Mean = 0;
    }
}