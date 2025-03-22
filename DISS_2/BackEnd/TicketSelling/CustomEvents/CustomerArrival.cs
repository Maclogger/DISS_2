using DISS_2.BackEnd.Core;
using DISS_2.BackEnd.Generators.Uniform;

namespace DISS_2.BackEnd.TicketSelling.CustomEvents;

public class CustomerArrival(int startTime) : Event(startTime)
{
    private UniformGenerator<int> _gen =
        UniformGeneratorFactory.CreateDiscreteUniformGenerator(20, 40);

    public override void Execute(SimState simState)
    {
        TicketSellingSimState state = (TicketSellingSimState)simState;

        int startTimeOfNextCustomerArrival = (int)_gen.Generate();
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