using DISS_2.BackEnd.Core;
using DISS_2.BackEnd.Generators.Uniform;
using DISS_2.BackEnd.TicketSelling.Agents;

namespace DISS_2.BackEnd.TicketSelling.CustomEvents;

public class CustomerArrival(int startTime) : Event(startTime)
{
    public override Task Execute(SimState simState)
    {
        TicketSellingSimState state = (TicketSellingSimState)simState;

        Customer customer = new()
        {
            TimeArrival = state.CurrentSimTime,
        };


        int startTimeOfNextCustomerArrival = state.CurrentSimTime + state.Gens.ArrivalGen.Generate();
        state.Calendar.PlanNewEvent(
            new CustomerArrival(startTimeOfNextCustomerArrival)
        );

        if (state.IsBusy)
        {
            state.CustomerQueue.Enqueue(customer);
            return Task.CompletedTask;
        }

        state.Calendar.PlanNewEvent(new OperationStart(state.CurrentSimTime + 0, customer));
        return Task.CompletedTask;
    }
}