using DISS_2.BackEnd.Core;
using DISS_2.BackEnd.TicketSelling.CustomEvents;

namespace DISS_2.BackEnd.TicketSelling;

public class TicketSimulation : SimCore
{
    public TicketSimulation(SimState state) : base(state)
    {
        
    }

    public void Run()
    {
        RunOneSimulation();   
    }

    protected override void BeforeSimulationRun(SimState simState)
    {
        TicketSellingSimState state = (TicketSellingSimState)simState;
        state.Calendar.PlanNewEvent(new CustomerArrival(0));
    }
}