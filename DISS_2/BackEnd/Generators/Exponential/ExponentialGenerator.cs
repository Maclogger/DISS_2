using DISS_2.BackEnd.Generators.Testers;

namespace DISS_2.BackEnd.Generators.Exponential;

public class ExponentialGenerator : Generator<double>
{
    public double Lambda { get; set; }

    public ExponentialGenerator(double lambda)
    {
        if (lambda <= 0)
        {
            throw new ArgumentException("Lambda musí byť väčšia ako nula.");
        }

        Lambda = lambda;
    }

    protected override double GenerateValue()
    {
        double u = Random.NextDouble();
        double x = -Math.Log(1 - u) / Lambda;
        return x;
    }

    public override IGeneratorTester<double> GetTester()
    {
        return new ExponentialTester();
    }
}