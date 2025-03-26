namespace DISS_2.BackEnd.Statistics
{
    public class WeightedStat(string name) : Statistics(name)
    {
        private double _weightedMean = 0.0;
        private long _totalTime = 0;
        private int _prevValue = 0;
        private long _prevSimTime = 0;

        public override double CalcMean() => _weightedMean;

        public void AddValue(int value, long simTime)
        {
            long deltaTime = ValidateAndGetDeltaTime(simTime);

            if (_totalTime > 0 && deltaTime > 0)
            {
                _weightedMean += (deltaTime * 1.0 / (_totalTime + deltaTime)) *
                                 (_prevValue - _weightedMean);
            }
            else if (_totalTime == 0)
            {
                _weightedMean = _prevValue;
            }

            _totalTime += deltaTime;

            _prevValue = value;
            _prevSimTime = simTime;
        }

        private long ValidateAndGetDeltaTime(long simTime)
        {
            long deltaTime = simTime - _prevSimTime;
            if (deltaTime < 0)
            {
                throw new ArgumentException(
                    "Nový simulačný čas musí byť väčší alebo rovný predchádzajúcemu.");
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