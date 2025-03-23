using DISS_2.BackEnd.Core;
using DISS_2.BackEnd.Generators.Uniform;
using DISS_2.BackEnd.TicketSelling.Agents;
using DISS_2.BackEnd.TicketSelling.CustomEvents;

namespace DISS_2.BackEnd.TicketSelling;

public class TicketSimulation : SimCore
{
    public Queue<Customer> CustomerQueue { get; set; } = new();
    public bool IsBusy { get; set; } = false;

    public TicketGenerators Gens { get; set; } = new();

    public async Task Run()
    {
        await RunOneSimulation();
    }

    protected override void BeforeReplicationRun(SimCore simCore)
    {
        simCore.Calendar.PlanNewEvent(new CustomerArrival(0));
    }
}


public class TicketGenerators
{
    public  UniformGenerator<int> ArrivalGen { get; set; }=
        UniformGeneratorFactory.CreateDiscreteUniformGenerator(10, 20);
    public UniformGenerator<int> OperationDurationGen { get; set; }=
        UniformGeneratorFactory.CreateDiscreteUniformGenerator(10, 30);
}