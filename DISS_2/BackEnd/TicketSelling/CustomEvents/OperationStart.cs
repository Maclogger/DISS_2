using DISS_2.BackEnd.Core;
using DISS_2.BackEnd.Generators.Uniform;

namespace DISS_2.BackEnd.TicketSelling.CustomEvents;

public class OperationStart(int startTime) : Event(startTime)
{
    public override Task Execute(SimState simState)
    {
        TicketSellingSimState state = (TicketSellingSimState)simState;
        state.IsBusy = true;

        int operationDuration = state.Gens.OperationDurationGen.Generate();
        state.Calendar.PlanNewEvent(
            new OperationEnd(state.CurrentSimTime + operationDuration)
        );
        return Task.CompletedTask;
    }
}