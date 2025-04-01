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
        var warehouseTravelTimeGen = sim.Generators.WarehouseTravelTimeGen;

        int timeToFinishStep1 = 0;
        bool isInWarehouse = Worker.CurrentLocation == null;

       // if (!isInWarehouse)
        //{
            int timeFromLocationToWarehouse = (int)Math.Round(warehouseTravelTimeGen.Generate());
            timeToFinishStep1 += timeFromLocationToWarehouse;
        //}

        if (Order.Location == null)
        {
            throw new Exception($"No location found in order! {GetType().Name}");
        }

        Order.Location.Occupy(worker, Order);

        var warehouseMaterialPrepTimeGen = sim.Generators.WarehouseMaterialPrepTimeGen;
        //int timeToPrepareMaterial = (int)Math.Round(warehouseMaterialPrepTimeGen.Generate());
        //timeToFinishStep1 += timeToPrepareMaterial;

        int timeFromWarehouseToLocation = (int)Math.Round(warehouseTravelTimeGen.Generate());
        timeToFinishStep1 += timeFromWarehouseToLocation;

        Generator<double> stepGenerator = sim.Generators.GetStepGenerator(Order, 1);
        timeToFinishStep1 = (int)Math.Round(stepGenerator.Generate());
        sim.Calendar.PlanNewEvent(
            new Step1End(sim.CurrentSimTime + timeToFinishStep1, Order, Worker));
    }
}