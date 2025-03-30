using DISS_2.BackEnd.Core;
using DISS_2.BackEnd.Generators.Uniform;
using DISS_2.BackEnd.Statistics;
using DISS_2.BackEnd.TicketSelling.Agents;
using DISS_2.BackEnd.TicketSelling.CustomEvents;

namespace DISS_2.BackEnd.TicketSelling;

public class TicketSimulation : SimCore
{
    public TicketSimulation()
    {
        Statistics =
        [
            new SampleStat("Average time in system"),
            new WeightedStat("Weighted average time in queue")
        ];
    }

    public Queue<Customer> CustomerQueue { get; set; } = new();
    public bool IsBusy { get; set; } = false;

    public TicketGenerators Gens { get; set; } = new();

    public async Task Run()
    {
        await RunOneSimulation();
    }

    protected override void BeforeReplicationRun()
    {
        Calendar.PlanNewEvent(new CustomerArrival(0));
    }

    public override void ResetSimulation()
    {
        base.ResetSimulation();
        CustomerQueue.Clear();
        IsBusy = false;
    }

    public override void PrintState(Event @event)
    {
    }

    protected override void AfterReplicationRun()
    {
        Console.WriteLine(Statistics[0]);
        Console.WriteLine(Statistics[1]);
        base.AfterReplicationRun();
    }
}

public class TicketGenerators
{
    public UniformGenerator<int> ArrivalGen { get; } =
        UniformGeneratorFactory.CreateDiscreteUniformGenerator(5, 10);

    public UniformGenerator<int> OperationDurationGen { get; } =
        UniformGeneratorFactory.CreateDiscreteUniformGenerator(5, 8);
}