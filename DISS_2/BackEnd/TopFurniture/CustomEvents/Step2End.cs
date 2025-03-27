using DISS_2.BackEnd.Core;
using DISS_2.BackEnd.TopFurniture.Agents;

namespace DISS_2.BackEnd.TopFurniture.CustomEvents;

public class Step2End(int startTime, Order order) : OrderEvent(startTime, order)
{
    public override Task Execute(SimCore simCore)
    {
        TopFurnitureSimulation sim = (TopFurnitureSimulation)simCore;

        sim.BusyC--;
        PlanStep3(sim);
        PlanStep4Or2(sim);

        return Task.CompletedTask;
    }

    private static void PlanStep4Or2(TopFurnitureSimulation sim)
    {
        if (!sim.Queues[4].IsEmpty())
        {
            Order orderFromQueue4 = sim.Queues[4].Dequeue();
            sim.Calendar.PlanNewEvent(new Step4Start(sim.CurrentSimTime, orderFromQueue4));
        }
        else if (!sim.Queues[2].IsEmpty())
        {
            Order orderFromQueue2 = sim.Queues[2].Dequeue();
            sim.Calendar.PlanNewEvent(new Step2Start(sim.CurrentSimTime, orderFromQueue2));
        }
    }

    private void PlanStep3(TopFurnitureSimulation sim)
    {
        if (sim.IsAvailable('B'))
        {
            sim.Calendar.PlanNewEvent(new Step3Start(sim.CurrentSimTime, Order));
        }
        else
        {
            sim.Queues[3].Enqueue(Order);
        }
    }
}
