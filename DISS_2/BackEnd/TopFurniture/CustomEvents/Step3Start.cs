using DISS_2.BackEnd.Core;
using DISS_2.BackEnd.Generators;
using DISS_2.BackEnd.TopFurniture.Agents;

namespace DISS_2.BackEnd.TopFurniture.CustomEvents;

public class Step3Start(int startTime, Order order) : OrderEvent(startTime, order)
{
    public override Task Execute(SimCore simCore)
    {
        TopFurnitureSimulation sim = (TopFurnitureSimulation)simCore;

        sim.BusyB++;
        PlanStep3End(sim);

        return Task.CompletedTask;
    }

    private void PlanStep3End(TopFurnitureSimulation sim)
    {
        Generator<double> gen = sim.Generators.GetStepGenerator(Order, 3);
        int timeToFinishStep3 = (int)Math.Round(gen.Generate());
        sim.Calendar.PlanNewEvent(new Step3End(sim.CurrentSimTime + timeToFinishStep3, Order));
    }
}