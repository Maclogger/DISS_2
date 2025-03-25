namespace DISS_2.BackEnd.Statistics;

public class WeightedStat(string name) : Statistics(name)
{
    private double _area = 0.0;
    private long _prevSimTime = 0;
    private int _prevValue = 0;

    public override double CalcMean()
    {
        return _area / _prevSimTime;
    }

    public void AddValue(int value, int simTime)
    {
        _area += (simTime - _prevSimTime) * _prevValue;
        _prevSimTime = simTime;
        _prevValue = value;
    }
}