namespace DISS_2.BackEnd.Statistics
{
    public class WeightedStat(string name) : Statistics(name)
    {
        private long _totalTime = 0;
        private int _prevValue = 0;
        private long _prevSimTime = 0;

        public override double CalcMean() => Mean;
        public override string GetTypeString()
        {
            return "Weighted statistics";
        }

        public void AddValue(int value, long simTime)
        {
            long deltaTime = ValidateAndGetDeltaTime(simTime);

            if (_totalTime > 0 && deltaTime > 0)
            {
                Mean += (deltaTime * 1.0 / (_totalTime + deltaTime)) *
                                 (_prevValue - Mean);
            }
            else if (_totalTime == 0)
            {
                Mean = _prevValue;
            }

            _totalTime += deltaTime;

            _prevValue = value;
            _prevSimTime = simTime;
            Count++;
        }

        private long ValidateAndGetDeltaTime(long simTime)
        {
            long deltaTime = simTime - _prevSimTime;
            if (deltaTime < 0)
            {
                throw new ArgumentException(
                    "The new simulation time must be greater than or equal to the previous one.");
            }

            return deltaTime;
        }

        public override string ToString()
        {
            double mean = CalcMean();
            string sol = $"Weighted Statistics: [{Name}]\n";
            sol += $"Weighted mean: {mean}";
            return sol;
        }
    }
}