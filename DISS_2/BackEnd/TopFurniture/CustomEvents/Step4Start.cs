using DISS_2.BackEnd.Core;
using DISS_2.BackEnd.Generators;
using DISS_2.BackEnd.TopFurniture.Agents;

namespace DISS_2.BackEnd.TopFurniture.CustomEvents;

public class Step4Start(int startTime, Order order, Worker worker) : OrderEvent(startTime, order)
{
    public Worker Worker { get; } = worker;

    public override Task Execute(SimCore simCore)
    {
        if (!Worker.IsBusy)
        {
            throw new Exception($"Worker is not busy at start!: {GetType().Name}");
        }

        if (Order is not Wardrobe)
        {
            throw new Exception("Only Wardrobe is supported in STEP 4!!!");
        }

        TopFurnitureSimulation sim = (TopFurnitureSimulation)simCore;

        PlanStep4End(sim);

        return Task.CompletedTask;
    }

    private void PlanStep4End(TopFurnitureSimulation sim)
    {
        if (Order.Location == null)
        {
            throw new Exception($"No location found in order! {GetType().Name}");
        }

        Generator<double> gen = sim.Generators.GetStepGenerator(Order, 4);
        int timeToFinishStep4 = (int)Math.Round(gen.Generate());

        int travelTime = sim.GetTravelTimeToLocation(Worker, Order.Location);
        timeToFinishStep4 += travelTime;

        Order.Location.Occupy(Worker, Order);

        sim.Calendar.PlanNewEvent(new Step4End(
            sim.CurrentSimTime + timeToFinishStep4, Order, Worker)
        );
    }

    public override Task BeforeEvent(SimCore sim)
    {
        Order.TimeOfStep4Start = sim.CurrentSimTime;
        return base.BeforeEvent(sim);
    }
}