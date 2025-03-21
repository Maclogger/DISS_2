using DISS_2.BackEnd.Files;

namespace DISS_2.BackEnd.Generators.Testers;

public class ExponentialTester : IGeneratorTester<double>
{
    public string DoTest(Generator<double> generator)
    {
        List<double>? data = generator.Data;
        if (data is null || data.Count <= 0)
        {
            throw new ArgumentException("Generator did not have History Enabled.");
        }

        string outputFile = FileSaver.SaveToFile(data, "EXPONENTIAL");

        return $"Data was saved into file: {outputFile}";
    }
}