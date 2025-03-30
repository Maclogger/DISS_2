using DISS_2.BackEnd.Core;
using DISS_2.BackEnd.TopFurniture.Agents;

namespace DISS_2.BackEnd.TopFurniture.CustomEvents;

public class OrderArrival(int startTime) : Event(startTime)
{
    public override Task Execute(SimCore simCore)
    {
        TopFurnitureSimulation sim = (TopFurnitureSimulation)simCore;

        Order order = CreateOrder(sim);
        if (Config.DebugMode) sim.Orders.Add(order);

        PlanNextArrival(sim);
        PlanStep1StartOrQueue(sim, order);

        return Task.CompletedTask;
    }

    private static Order CreateOrder(TopFurnitureSimulation sim)
    {
        Order order = Order.CreateRandomOrder(sim.Generators.OrderTypeGen, sim.CurrentSimTime);

        if (order is Chair)
        {
            sim.ChairsInSystem++;
        } else if (order is Table)
        {
            sim.TablesInSystem++;
        } else if (order is Wardrobe)
        {
            sim.WardrobesInSystem++;
        }

        return order;
    }

    private void PlanStep1StartOrQueue(TopFurnitureSimulation sim, Order order)
    {
        if (sim.IsAvailable(WorkerType.A))
        {
            Worker worker = sim.GetFirstAvailableWorkerAndMakeHimBusy(WorkerType.A);
            sim.Calendar.PlanNewEvent(new Step1Start(sim.CurrentSimTime, order, worker));
        }
        else
        {
            sim.Queues[1].Enqueue(order);
        }
    }

    private void PlanNextArrival(TopFurnitureSimulation sim)
    {
        int timeToNextArrival = (int)Math.Round(sim.Generators.ArrivalGen.Generate());
        sim.Calendar.PlanNewEvent(new OrderArrival(sim.CurrentSimTime + timeToNextArrival));
    }
}