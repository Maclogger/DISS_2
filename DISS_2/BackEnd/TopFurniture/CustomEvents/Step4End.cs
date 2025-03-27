using DISS_2.BackEnd.Core;
using DISS_2.BackEnd.Statistics;
using DISS_2.BackEnd.TopFurniture.Agents;

namespace DISS_2.BackEnd.TopFurniture.CustomEvents;

public class Step4End(int startTime, Order order) : OrderEvent(startTime, order)
{
    public override Task Execute(SimCore simCore)
    {
        TopFurnitureSimulation sim = (TopFurnitureSimulation)simCore;

        sim.BusyC--;
        PlanStep4OrStep2(sim);

        return Task.CompletedTask;
    }

    private void PlanStep4OrStep2(TopFurnitureSimulation sim)
    {
        if (!sim.Queues[4].IsEmpty())
        {
            Order orderFromQueue4 = sim.Queues[4].Dequeue();
            sim.Calendar.PlanNewEvent(new Step4Start(startTime, orderFromQueue4));
        } else if (!sim.Queues[2].IsEmpty())
        {
            Order orderFromQueue2 = sim.Queues[2].Dequeue();
            sim.Calendar.PlanNewEvent(new Step2Start(startTime, orderFromQueue2));
        }
    }

    public override Task AfterEvent(SimCore sim)
    {
        Console.WriteLine($"Step4End - after: {Order.GetType().Name}");
        ((TopFurnitureSimulation)sim).Sink.SinkItem(Order);
        return base.AfterEvent(sim);
    }
}
