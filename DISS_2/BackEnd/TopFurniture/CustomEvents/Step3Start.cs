using DISS_2.BackEnd.Core;
using DISS_2.BackEnd.Generators;
using DISS_2.BackEnd.TopFurniture.Agents;

namespace DISS_2.BackEnd.TopFurniture.CustomEvents;

public class Step3Start(int startTime, Order order, Worker worker) : OrderEvent(startTime, order)
{
    public Worker Worker { get; } = worker;

    public override Task Execute(SimCore simCore)
    {
        TopFurnitureSimulation sim = (TopFurnitureSimulation)simCore;
        if (!Worker.IsBusy)
        {
            throw new Exception($"Worker is not busy at start!: {GetType().Name}");
        }

        PlanStep3End(sim);

        return Task.CompletedTask;
    }

    private void PlanStep3End(TopFurnitureSimulation sim)
    {
        if (Order.Location == null)
        {
            throw new Exception($"No location found in order! {GetType().Name}");
        }

        int timeToFinishStep3 = 0;

        int travelTime = sim.GetTravelTimeToLocation(Worker, Order.Location);
        Order.Location.Occupy(Worker, Order);
        timeToFinishStep3 += travelTime;

        Generator<double> gen = sim.Generators.GetStepGenerator(Order, 3);
        timeToFinishStep3 += (int)Math.Round(gen.Generate());
        sim.Calendar.PlanNewEvent(new Step3End(sim.CurrentSimTime + timeToFinishStep3, Order, Worker));
    }


    public override Task BeforeEvent(SimCore sim)
    {
        Order.TimeOfStep3Start = sim.CurrentSimTime;
        return base.BeforeEvent(sim);
    }
}