namespace DISS_2.BackEnd.Statistics;

public class WeightedStat(string name) : Statistics(name)
{

    public override float CalcMean()
    {
        throw new NotImplementedException();
    }

    public void AddValue(int value, int weight)
    {

    }
}