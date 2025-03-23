using DISS_2.BackEnd.Core;
using DISS_2.BackEnd.TicketSelling.Agents;

namespace DISS_2.BackEnd.TicketSelling.CustomEvents;

public class OperationEnd(int startTime, Customer customer) : Event(startTime)
{
    public Customer Customer { get; } = customer;

    public override Task Execute(SimState simState)
    {
        TicketSellingSimState state = (TicketSellingSimState)simState;

        if (state.CustomerQueue.Count > 0)
        {
            Customer nextCustomerFromQueue = state.CustomerQueue.Dequeue();
            state.Calendar.PlanNewEvent(
                new OperationStart(state.CurrentSimTime, nextCustomerFromQueue)
            );
            return Task.CompletedTask;
        }

        state.IsBusy = false;
        return Task.CompletedTask;
    }
}