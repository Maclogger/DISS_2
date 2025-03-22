using DISS_2.BackEnd.BestFurnitureSro;

namespace DISS_2.BackEnd;

public sealed class MainApp
{
    // Singleton staff
    private static readonly Lazy<MainApp> _instance = new(() => new MainApp());
    public static MainApp Instance => _instance.Value;
    // Singleton staff

    private MainApp()
    {
        MasterSeeder = new Random(0);
        TesterGenerators = new();
        Config = new();
    }

    public Random MasterSeeder { get; set; }
    public Config Config { get; set; }
    public Generators.Generators TesterGenerators { get; set; }

    public SimState? SimState { get; set; }
}
