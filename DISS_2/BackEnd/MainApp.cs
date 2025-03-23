using DISS_2.BackEnd.Core;
using DISS_2.Components;
using DISS_2.Components.Basic;

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

    public List<ISimDelegate> SimDelegates { get; set; } = new();
    public List<IRepDelegate> RepDelegates { get; set; } = new();

    public SpeedControl SpeedControl { get; set; } = new();
}
