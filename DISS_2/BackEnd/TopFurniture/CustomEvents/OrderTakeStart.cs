using DISS_2.BackEnd.Core;
using DISS_2.BackEnd.TopFurniture.Agents;

namespace DISS_2.BackEnd.TopFurniture.CustomEvents;

public class OrderTakeStart(int startTime, Order order, Worker worker)
    : OrderEvent(startTime, order)
{
    private Worker Worker { get; } = worker;

    public override Task Execute(SimCore simCore)
    {
        TopFurnitureSimulation sim = (TopFurnitureSimulation)simCore;
        if (!Worker.IsBusy)
        {
            throw new Exception($"Worker is not busy at start!: {GetType().Name}");
        }

        Worker.IsTakingOrder = true;
        int timeToTakeAnOrder = sim.Generators.OrderTakeTimeGen.Generate();
        sim.Calendar.PlanNewEvent(
            new OrderTakeEnd(sim.CurrentSimTime + timeToTakeAnOrder, Order, Worker)
        );
        return Task.CompletedTask;
    }
}