using DISS_2.BackEnd.Files;

namespace DISS_2.BackEnd.Generators.Testers
{
    public class TriangularIntTester : IGeneratorTester<int>
    {
        public string DoTest(Generator<int> generator)
        {
            List<int>? data = generator.Data;
            if (data is null || data.Count <= 0)
            {
                throw new ArgumentException("Generator did not have History Enabled.");
            }

            string outputFile = FileSaver.SaveToFile(data, "TRIANGULAR_DISC");

            return $"Data was saved into file: {outputFile}";
        }
    }

    public class TriangularRealTester : IGeneratorTester<double>
    {
        public string DoTest(Generator<double> generator)
        {
            List<double>? data = generator.Data;
            if (data is null || data.Count <= 0)
            {
                throw new ArgumentException("Generator did not have History Enabled.");
            }

            string outputFile = FileSaver.SaveToFile(data, "TRIANGULAR_REAL");

            return $"Data was saved into file: {outputFile}";
        }
    }
}
