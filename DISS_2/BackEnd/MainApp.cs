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
        Generators = new();
        Config = new();
    }

    public Random MasterSeeder { get; set; }
    public Config Config { get; set; }
    public List<double> CumulativeCosts { get; set; } = new();
    public Generators.Generators Generators { get; set; }
}