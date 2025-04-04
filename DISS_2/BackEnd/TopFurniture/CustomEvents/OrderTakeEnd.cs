
using DISS_2.BackEnd.Core;
using DISS_2.BackEnd.TopFurniture.Agents;

namespace DISS_2.BackEnd.TopFurniture.CustomEvents;

public class OrderTakeEnd(int startTime, Order order, Worker worker) : OrderEvent(startTime, order)
{
    public Worker Worker { get; } = worker;

    public override Task Execute(SimCore simCore)
    {
        TopFurnitureSimulation sim = (TopFurnitureSimulation)simCore;
        if (!Worker.IsTakingOrder)
        {
            throw new Exception("Worker is not taking an order!");
        }
        if (!Worker.IsBusy)
        {
            throw new Exception($"Worker is not busy at start!: {GetType().Name}");
        }

        Worker.IsTakingOrder = false;
        sim.Calendar.PlanNewEvent(new Step1Start(startTime, Order, Worker));
        return Task.CompletedTask;
    }
}