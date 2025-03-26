using DISS_2.BackEnd.Core;
using DISS_2.BackEnd.Generators;
using DISS_2.BackEnd.Generators.Empiric;
using DISS_2.BackEnd.Generators.Exponential;
using DISS_2.BackEnd.Generators.Uniform;
using DISS_2.BackEnd.TopFurniture.Agents;
using DISS_2.BackEnd.TopFurniture.CustomEvents;
using DISS_2.BackEnd.TopFurniture.Queues;

namespace DISS_2.BackEnd.TopFurniture;

public class TopFurnitureSimulation : SimCore
{
    public int A { get; private set; }
    public int B { get; private set; }
    public int C { get; private set; }
    public List<FifoQueue<Order>> Queues { get; set; }

    public int BusyA { get; set; } = 0;
    public int BusyB { get; set; } = 0;
    public int BusyC { get; set; } = 0;

    public void Reinitialize(int a, int b, int c)
    {
        ResetSimulation();
        A = a;
        B = b;
        C = c;
    }

    public bool IsAvailable(char group)
    {
        switch (group)
        {
            case 'A': return BusyA < A;
            case 'B': return BusyB < B;
            case 'C': return BusyC < C;
        }
        throw new Exception("Invalid group");
    }

    public FurnitureGenerators Generators { get; } = new();

    public TopFurnitureSimulation()
    {
        A = 1;
        B = 1;
        C = 1;
        Queues =
        [
            new FifoQueue<Order>(this), // DUMMY -> just to index from 1
            new Queue1(this),
            new Queue2(this),
            new Queue3(this),
            new Queue4(this),
        ];
    }

    public override void ResetSimulation()
    {
        base.ResetSimulation();

        foreach (FifoQueue<Order> queue in Queues)
        {
            queue.Clear();
        }

        BusyA = 0;
        BusyB = 0;
        BusyC = 0;
    }

    protected override void BeforeReplicationRun(SimCore simCore)
    {
        base.BeforeReplicationRun(simCore);
        simCore.Calendar.PlanNewEvent(new OrderArrival(0));
    }
}

public class FurnitureGenerators
{
    public UniformGenerator<double> OrderTypeGen { get; } = UniformGeneratorFactory
        .CreateRealUniformGenerator(0.0, 1.0);

    public ExponentialGenerator ArrivalGen { get; } =
        new ExponentialGenerator((double)1 / (30 * 60)); // 2 order per hour -> 30 min interval

    private EmpiricGenerator<double> Table1Gen { get; } =
        EmpiricGeneratorFactory.CreateRealGenerator([
            (10, 25, 0.6),
            (25, 50, 0.4),
        ]);

    private UniformGenerator<double> Table2Gen { get; } = UniformGeneratorFactory
        .CreateRealUniformGenerator(200, 610);

    private UniformGenerator<double> Table3Gen { get; } = UniformGeneratorFactory
        .CreateRealUniformGenerator(30, 60);


    private UniformGenerator<double> Chair1Gen { get; } = UniformGeneratorFactory
        .CreateRealUniformGenerator(12, 16);

    private UniformGenerator<double> Chair2Gen { get; } = UniformGeneratorFactory
        .CreateRealUniformGenerator(210, 540);

    private UniformGenerator<double> Chair3Gen { get; } = UniformGeneratorFactory
        .CreateRealUniformGenerator(14, 24);


    private UniformGenerator<double> Wardrobe1Gen { get; } = UniformGeneratorFactory
        .CreateRealUniformGenerator(15, 80);

    private UniformGenerator<double> Wardrobe2Gen { get; } = UniformGeneratorFactory
        .CreateRealUniformGenerator(600, 700);

    private UniformGenerator<double> Wardrobe3Gen { get; } = UniformGeneratorFactory
        .CreateRealUniformGenerator(35, 75);

    private UniformGenerator<double> Wardrobe4Gen { get; } = UniformGeneratorFactory
        .CreateRealUniformGenerator(15, 25);

    public Generator<double> GetStepGenerator(Order order, int step)
    {
        if (order is Chair)
        {
            switch (step)
            {
                case 1: return Chair1Gen;
                case 2: return Chair2Gen;
                case 3: return Chair3Gen;
            }
        }

        if (order is Table)
        {
            switch (step)
            {
                case 1: return Table1Gen;
                case 2: return Table2Gen;
                case 3: return Table3Gen;
            }
        }

        if (order is Wardrobe)
        {
            switch (step)
            {
                case 1: return Wardrobe1Gen;
                case 2: return Wardrobe2Gen;
                case 3: return Wardrobe3Gen;
                case 4: return Wardrobe4Gen;
            }
        }

        throw new Exception($"Combination of Order and technological step is not supported!");
    }
}