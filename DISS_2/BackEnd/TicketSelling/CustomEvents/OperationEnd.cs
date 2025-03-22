using DISS_2.BackEnd.Core;

namespace DISS_2.BackEnd.TicketSelling.CustomEvents;

public class OperationEnd(int startTime) : Event(startTime)
{
    public override void Execute(SimState simState)
    {
        TicketSellingSimState state = (TicketSellingSimState)simState;

        if (state.PeopleInQueue > 0)
        {
            state.PeopleInQueue--;
            state.Calendar.PlanNewEvent(
                new OperationStart(state.CurrentSimTime)
            );
            return;
        }

        state.IsBusy = false;
    }
}