using DISS_2.BackEnd.Core;
using DISS_2.BackEnd.Generators.Uniform;
using DISS_2.BackEnd.TicketSelling.Agents;

namespace DISS_2.BackEnd.TicketSelling.CustomEvents;

public class OperationStart(int startTime, Customer customer) : Event(startTime)
{
    public Customer Customer { get; } = customer;

    public override Task Execute(SimCore simCore)
    {
        TicketSimulation sim = (TicketSimulation)simCore;
        sim.IsBusy = true;

        int operationDuration = sim.Gens.OperationDurationGen.Generate();
        sim.Calendar.PlanNewEvent(
            new OperationEnd(sim.CurrentSimTime + operationDuration, Customer)
        );
        return Task.CompletedTask;
    }
}