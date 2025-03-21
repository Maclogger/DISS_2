namespace DISS_2.BackEnd.Generators.Testers;

public interface IGeneratorTester<T>
{
    public string DoTest(Generator<T> generator);
}