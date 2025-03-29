
using DISS_2.BackEnd.Generators.Testers;

namespace DISS_2.BackEnd.Generators.Triangular;

public class TriangularRealGenerator : Generator<double>
{
    public double Min { get; set; }
    public double Mode { get; set; }
    public double Max { get; set; }

    public TriangularRealGenerator(double min, double mode, double max)
    {
        if (min > mode || mode > max)
        {
            throw new ArgumentException("Hodnoty musia plniť: min ≤ mode ≤ max.");
        }

        Min = min;
        Mode = mode;
        Max = max;
    }

    protected override double GenerateValue()
    {
        double f = (Mode - Min) / (Max - Min);
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
        return sol;
    }

    public override IGeneratorTester<double>? GetTester()
    {
        return new TriangularRealTester();
    }
}