using DISS_2.BackEnd.Core;
using DISS_2.BackEnd.Statistics;
using DISS_2.BackEnd.TopFurniture.Agents;

namespace DISS_2.BackEnd.TopFurniture.CustomEvents;

public class Step2End(int startTime, Order order, Worker worker) : OrderEvent(startTime, order)
{
    public Worker Worker { get; } = worker;

    public override Task Execute(SimCore simCore)
    {
        TopFurnitureSimulation sim = (TopFurnitureSimulation)simCore;

        sim.ReleaseWorker(Worker);
        PlanStep3(sim);
        PlanStep4Or2(sim);

        return Task.CompletedTask;
    }

    private static void PlanStep4Or2(TopFurnitureSimulation sim)
    {
        if (!sim.Queues[4].IsEmpty())
        {
            Order orderFromQueue4 = sim.Queues[4].Dequeue();
            Worker worker = sim.GetFirstAvailableWorkerAndMakeHimBusy(WorkerType.C);
            sim.Calendar.PlanNewEvent(new Step4Start(sim.CurrentSimTime, orderFromQueue4, worker));
        }
        else if (!sim.Queues[2].IsEmpty())
        {
            Order orderFromQueue2 = sim.Queues[2].Dequeue();
            Worker worker = sim.GetFirstAvailableWorkerAndMakeHimBusy(WorkerType.C);
            sim.Calendar.PlanNewEvent(new Step2Start(sim.CurrentSimTime, orderFromQueue2, worker));
        }
    }

    private void PlanStep3(TopFurnitureSimulation sim)
    {
        if (sim.IsAvailable(WorkerType.B))
        {
            Worker worker = sim.GetFirstAvailableWorkerAndMakeHimBusy(WorkerType.B);
            sim.Calendar.PlanNewEvent(new Step3Start(sim.CurrentSimTime, Order, worker));
        }
        else
        {
            sim.Queues[3].Enqueue(Order);
        }
    }
    public override Task AfterEvent(SimCore sim)
    {
        ((SampleStat)sim.Statistics[13]).AddValue(sim.CurrentSimTime - Order.TimeOfStep2Start);
        return base.AfterEvent(sim);
    }
}
