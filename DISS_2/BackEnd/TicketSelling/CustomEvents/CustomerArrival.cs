using DISS_2.BackEnd.Core;
using DISS_2.BackEnd.Generators.Uniform;
using DISS_2.BackEnd.Statistics;
using DISS_2.BackEnd.TicketSelling.Agents;

namespace DISS_2.BackEnd.TicketSelling.CustomEvents;

public class CustomerArrival(int startTime) : Event(startTime)
{

    public override Task Execute(SimCore simCore)
    {
        TicketSimulation sim = (TicketSimulation)simCore;

        Customer customer = new()
        {
            TimeArrival = sim.CurrentSimTime,
        };

        int startTimeOfNextCustomerArrival = sim.CurrentSimTime + sim.Gens.ArrivalGen
            .Generate();

        sim.Calendar.PlanNewEvent(
            new CustomerArrival(startTimeOfNextCustomerArrival)
        );

        if (sim.IsBusy)
        {
            sim.CustomerQueue.Enqueue(customer);
            ((WeightedStat)sim.Statistics[1]).AddValue(sim.CustomerQueue.Count, sim.CurrentSimTime);
            return Task.CompletedTask;
        }

        sim.Calendar.PlanNewEvent(new OperationStart(sim.CurrentSimTime + 0, customer));
        return Task.CompletedTask;
    }
}