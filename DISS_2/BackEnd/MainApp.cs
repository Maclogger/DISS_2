using DISS_2.BackEnd.Core;
using DISS_2.BackEnd.TicketSelling;
using DISS_2.BackEnd.TopFurniture;
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


    public TicketSimulation? TicketSimulation { get; set; }
    public TopFurnitureSimulation? TopFurnitureSimulation { get; set; }
    public TopFurnitureSimulation? TopFurnitureRepSimulation { get; set; }
}
