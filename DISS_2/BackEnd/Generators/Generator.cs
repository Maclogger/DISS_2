using DISS_2.BackEnd.Generators.Testers;

namespace DISS_2.BackEnd.Generators;

public abstract class Generator<T>
{
    public Random Random { get; set; }
    int Seed { get; set; } = 0;

    protected Generator()
    {
        Seed = MainApp.Instance.MasterSeeder.Next();
        Random = new Random(Seed);
        Name = GetType().Name;
    }

    protected abstract T GenerateValue();

    public T Generate()
    {
        T value = GenerateValue();
        bool isHistoryEnabled = Data is not null;
        if (isHistoryEnabled)
        {
            Data!.Add(value);
        }
        return value;
    }
    public abstract IGeneratorTester<T>? GetTester();

    public List<T>? Data { get; set; } = null;
    public string Name { get; }

    public void EnableHistory()
    {
        Data = new List<T>();
    }
}