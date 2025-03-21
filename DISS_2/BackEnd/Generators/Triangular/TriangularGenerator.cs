using DISS_2.BackEnd.Generators.Testers;

namespace DISS_2.BackEnd.Generators.Triangular
{
    public class TriangularIntGenerator : Generator<int>
    {
        public int Min { get; set; }
        public int Mode { get; set; }
        public int Max { get; set; }

        public TriangularIntGenerator(int min, int mode, int max)
        {
            if (min > mode || mode > max)
            {
                throw new ArgumentException("Hodnoty musia plniť: min ≤ mode ≤ max.");
            }

            Min = min;
            Mode = mode;
            Max = max;
        }

        protected override int GenerateValue()
        {
            double f = ((double)Mode - Min) / (Max - Min);
            double u = Random.NextDouble();
            double sol;
            if (u < f)
            {
                sol = Min + Math.Sqrt(u * (Max - Min) * (Mode - Min));
            }
            else
            {
                sol = Max - Math.Sqrt((1 - u) * (Max - Min) * (Max - Mode));
            }
            return (int)Math.Round(sol);
        }

        public override IGeneratorTester<int>? GetTester()
        {
            return new TriangularDiscreteTester();
        }
    }
}
