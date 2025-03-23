using DISS_2.BackEnd.Core;
using DISS_2.BackEnd.Generators.Uniform;
using DISS_2.BackEnd.TicketSelling.Agents;

namespace DISS_2.BackEnd.TicketSelling.CustomEvents;

public class OperationStart(int startTime, Customer customer) : Event(startTime)
{
    public Customer Customer { get; } = customer;

    public override Task Execute(SimState simState)
    {
        TicketSellingSimState state = (TicketSellingSimState)simState;
        state.IsBusy = true;

        int operationDuration = state.Gens.OperationDurationGen.Generate();
        state.Calendar.PlanNewEvent(
            new OperationEnd(state.CurrentSimTime + operationDuration, Customer)
        );
        return Task.CompletedTask;
    }
}