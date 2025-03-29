using DISS_2.BackEnd.Core;
using DISS_2.BackEnd.Generators;
using DISS_2.BackEnd.TopFurniture.Agents;

namespace DISS_2.BackEnd.TopFurniture.CustomEvents;

public class Step1Start(int startTime, Order order, Worker worker) : OrderEvent(startTime, order)
{
    private Worker Worker { get; } = worker;

    public override Task Execute(SimCore simCore)
    {
        TopFurnitureSimulation sim = (TopFurnitureSimulation)simCore;
        if (!Worker.IsBusy)
        {
            throw new Exception($"Worker is not busy at start!: {GetType().Name}");
        }

        PlanStep1End(sim);

        return Task.CompletedTask;
    }

    private void PlanStep1End(TopFurnitureSimulation sim)
    {
        Generator<double> gen = sim.Generators.GetStepGenerator(Order, 1);
        int timeToFinishStep1 = (int)Math.Round(gen.Generate());
        sim.Calendar.PlanNewEvent(
            new Step1End(sim.CurrentSimTime + timeToFinishStep1, Order, Worker));
    }
}