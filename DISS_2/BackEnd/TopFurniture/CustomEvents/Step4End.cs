using DISS_2.BackEnd.Core;
using DISS_2.BackEnd.TopFurniture.Agents;

namespace DISS_2.BackEnd.TopFurniture.CustomEvents;

public class Step4End(int startTime, Order order, Worker worker) : OrderEvent(startTime, order)
{
    public Worker Worker { get; } = worker;

    public override Task Execute(SimCore simCore)
    {
        TopFurnitureSimulation sim = (TopFurnitureSimulation)simCore;

        sim.ReleaseWorker(Worker);
        PlanStep4OrStep2(sim);

        return Task.CompletedTask;
    }

    private void PlanStep4OrStep2(TopFurnitureSimulation sim)
    {
        if (!sim.Queues[4].IsEmpty())
        {
            Order orderFromQueue4 = sim.Queues[4].Dequeue();
            Worker worker = sim.GetFirstAvailableWorkerAndMakeHimBusy(WorkerType.C);
            sim.Calendar.PlanNewEvent(new Step4Start(startTime, orderFromQueue4, worker));
        } else if (!sim.Queues[2].IsEmpty())
        {
            Order orderFromQueue2 = sim.Queues[2].Dequeue();
            Worker worker = sim.GetFirstAvailableWorkerAndMakeHimBusy(WorkerType.C);
            sim.Calendar.PlanNewEvent(new Step2Start(startTime, orderFromQueue2, worker));
        }
    }

    public override Task AfterEvent(SimCore sim)
    {
        ((TopFurnitureSimulation)sim).Sink.SinkItem(Order);
        return base.AfterEvent(sim);
    }
}
