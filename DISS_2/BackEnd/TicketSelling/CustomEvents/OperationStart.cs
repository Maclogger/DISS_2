using DISS_2.BackEnd.Core;
using DISS_2.BackEnd.Generators.Uniform;

namespace DISS_2.BackEnd.TicketSelling.CustomEvents;

public class OperationStart(int startTime) : Event(startTime)
{
    private UniformGenerator<int> _operationDurationGenerator =
        UniformGeneratorFactory.CreateDiscreteUniformGenerator(10, 30);

    public override void Execute(SimState simState)
    {
        TicketSellingSimState state = (TicketSellingSimState)simState;
        state.IsBusy = true;

        int operationDuration = _operationDurationGenerator.Generate();
        state.Calendar.PlanNewEvent(
            new OperationEnd(state.CurrentSimTime + operationDuration)
        );
    }
}