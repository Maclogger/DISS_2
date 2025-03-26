using DISS_2.BackEnd.Core;
using DISS_2.BackEnd.Generators;
using DISS_2.BackEnd.TopFurniture.Agents;

namespace DISS_2.BackEnd.TopFurniture.CustomEvents;

public class Step4Start(int startTime, Order order) : OrderEvent(startTime, order)
{
    public override Task Execute(SimCore simCore)
    {
        TopFurnitureSimulation sim = (TopFurnitureSimulation)simCore;

        sim.BusyC++;
        PlanStep4End(sim);

        return Task.CompletedTask;
    }

    private void PlanStep4End(TopFurnitureSimulation sim)
    {
        Generator<double> gen = sim.Generators.GetStepGenerator(Order, 4);
        int timeToFinishStep4 = (int)Math.Round(gen.Generate());
        sim.Calendar.PlanNewEvent(new Step4End(sim.CurrentSimTime + timeToFinishStep4, Order));
    }
}