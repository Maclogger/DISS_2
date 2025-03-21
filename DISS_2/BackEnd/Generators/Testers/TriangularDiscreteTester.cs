using DISS_2.BackEnd.Files;

namespace DISS_2.BackEnd.Generators.Testers
{
    public class TriangularDiscreteTester : IGeneratorTester<int>
    {
        public string DoTest(Generator<int> generator)
        {
            List<int>? data = generator.Data;
            if (data is null || data.Count <= 0)
            {
                throw new ArgumentException("Generator did not have History Enabled.");
            }

            string outputFile = FileSaver.SaveToFile(data, "TRIANGULAR");

            return $"Data was saved into file: {outputFile}";
        }
    }
}
