using DISS_2.BackEnd.Core;
using DISS_2.BackEnd.Generators;
using DISS_2.BackEnd.TopFurniture.Agents;

namespace DISS_2.BackEnd.TopFurniture.CustomEvents;

public class Step2Start(int startTime, Order order, Worker worker) : OrderEvent(startTime, order)
{
    private Worker Worker { get; } = worker;

    public override Task Execute(SimCore simCore)
    {
        TopFurnitureSimulation sim = (TopFurnitureSimulation)simCore;
        if (!Worker.IsBusy)
        {
            throw new Exception($"Worker is not busy at start!: {GetType().Name}");
        }

        PlanStep2End(sim);

        return Task.CompletedTask;
    }

    private void PlanStep2End(TopFurnitureSimulation sim)
    {
        if (Order.Location == null)
        {
            throw new Exception($"No location found in order! {GetType().Name}");
        }

        int timeToFinishStep2 = 0;

        int travelTime = sim.GetTravelTimeToLocation(Worker, Order.Location);
        timeToFinishStep2 = travelTime;

        Order.Location.Occupy(Worker, Order);

        var genStep = sim.Generators.GetStepGenerator(Order, 2);
        timeToFinishStep2 += (int)Math.Round(genStep.Generate());

        sim.Calendar.PlanNewEvent(
            new Step2End(sim.CurrentSimTime + timeToFinishStep2, Order, Worker));
    }

    public override Task BeforeEvent(SimCore sim)
    {
        Order.TimeOfStep2Start = sim.CurrentSimTime;
        return base.BeforeEvent(sim);
    }
}