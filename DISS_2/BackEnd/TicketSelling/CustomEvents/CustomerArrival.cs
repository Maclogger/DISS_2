using DISS_2.BackEnd.Core;
using DISS_2.BackEnd.Generators.Uniform;

namespace DISS_2.BackEnd.TicketSelling.CustomEvents;

public class CustomerArrival(int startTime) : Event(startTime)
{
    public override void Execute(SimState simState)
    {
        TicketSellingSimState state = (TicketSellingSimState)simState;

        int startTimeOfNextCustomerArrival = state.Gens.ArrivalGen.Generate();
        state.Calendar.PlanNewEvent(
            new CustomerArrival(state.CurrentSimTime + startTimeOfNextCustomerArrival)
        );

        if (state.IsBusy)
        {
            state.PeopleInQueue++;
            return;
        }

        state.Calendar.PlanNewEvent(new OperationStart(state.CurrentSimTime + 0));
    }
}