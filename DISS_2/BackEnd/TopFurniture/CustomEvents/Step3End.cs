using DISS_2.BackEnd.Core;
using DISS_2.BackEnd.TopFurniture.Agents;

namespace DISS_2.BackEnd.TopFurniture.CustomEvents;

public class Step3End(int startTime, Order order) : OrderEvent(startTime, order)
{
    public override Task Execute(SimCore simCore)
    {
        TopFurnitureSimulation sim = (TopFurnitureSimulation)simCore;

        sim.BusyB--;
        PlanStep3IfInQueue(sim);

        if (Order is Wardrobe wardrobe)
        {
            PlanStep4(sim, wardrobe);
        }
        else
        {
            Console.WriteLine($"Step3End - after: {Order.GetType().Name}");
            sim.Sink.SinkItem(Order);
        }

        return Task.CompletedTask;
    }

    private void PlanStep4(TopFurnitureSimulation sim, Wardrobe wardrobe)
    {
        if (sim.IsAvailable('C'))
        {
            sim.Calendar.PlanNewEvent(new Step4Start(sim.CurrentSimTime, wardrobe));
        }
        else
        {
            sim.Queues[4].Enqueue(wardrobe);
        }
    }

    private void PlanStep3IfInQueue(TopFurnitureSimulation sim)
    {
        if (!sim.Queues[3].IsEmpty())
        {
            Order orderFromQueue3 = sim.Queues[3].Dequeue();
            sim.Calendar.PlanNewEvent(new Step3Start(sim.CurrentSimTime, orderFromQueue3));
        }
    }
}