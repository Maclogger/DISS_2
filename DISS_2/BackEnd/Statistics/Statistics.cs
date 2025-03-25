namespace DISS_2.BackEnd.Statistics;

public abstract class Statistics(string name)
{
    public string Name { get; } = name;

    public abstract float CalcMean();
}