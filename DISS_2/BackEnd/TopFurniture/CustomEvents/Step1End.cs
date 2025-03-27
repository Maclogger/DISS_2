using DISS_2.BackEnd.Core;
using DISS_2.BackEnd.TicketSelling.CustomEvents;
using DISS_2.BackEnd.TopFurniture.Agents;

namespace DISS_2.BackEnd.TopFurniture.CustomEvents;

public class Step1End(int startTime, Order order) : OrderEvent(startTime, order)
{
    public override Task Execute(SimCore simCore)
    {
        TopFurnitureSimulation sim = (TopFurnitureSimulation)simCore;

        sim.BusyA--;
        PlanStep1StartIfInQueue(sim);
        PlanStep2(sim);


        return Task.CompletedTask;
    }

    private void PlanStep2(TopFurnitureSimulation sim)
    {
        if (sim.IsAvailable('C'))
        {
            sim.Calendar.PlanNewEvent(new Step2Start(sim.CurrentSimTime, Order));
        }
        else
        {
            sim.Queues[2].Enqueue(Order);
        }
    }

    private static void PlanStep1StartIfInQueue(TopFurnitureSimulation sim)
    {
        if (!sim.Queues[1].IsEmpty())
        {
            Order orderFromQueue = sim.Queues[1].Dequeue();
            sim.Calendar.PlanNewEvent(new Step1Start(sim.CurrentSimTime, orderFromQueue));
        }
    }
}
