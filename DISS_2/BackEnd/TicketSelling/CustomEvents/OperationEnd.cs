using DISS_2.BackEnd.Core;
using DISS_2.BackEnd.TicketSelling.Agents;

namespace DISS_2.BackEnd.TicketSelling.CustomEvents;

public class OperationEnd(int startTime, Customer customer) : Event(startTime)
{
    public Customer Customer { get; } = customer;

    public override Task Execute(SimCore simCore)
    {
        TicketSimulation sim = (TicketSimulation)simCore;

        if (sim.CustomerQueue.Count > 0)
        {
            Customer nextCustomerFromQueue = sim.CustomerQueue.Dequeue();
            sim.Calendar.PlanNewEvent(
                new OperationStart(sim.CurrentSimTime, nextCustomerFromQueue)
            );
            return Task.CompletedTask;
        }

        sim.IsBusy = false;
        return Task.CompletedTask;
    }
}