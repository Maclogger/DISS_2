using DISS_2.BackEnd.Core;
using DISS_2.BackEnd.Core.Objects;
using DISS_2.BackEnd.Statistics;
using DISS_2.BackEnd.TopFurniture.Agents;
using DISS_2.BackEnd.TopFurniture.CustomEvents;
using DISS_2.BackEnd.TopFurniture.CustomObjects;

namespace DISS_2.BackEnd.TopFurniture;

public class TopFurnitureSimulation : SimCore
{
    public List<FifoQueue<Order>> Queues { get; set; }
    public int ChairsInSystem { get; set; } = 0;
    public int TablesInSystem { get; set; } = 0;
    public int WardrobesInSystem { get; set; } = 0;

    public Worker[] WorkersA { get; set; }
    private int _busyA = 0;
    public Worker[] WorkersB { get; set; }
    private int _busyB = 0;
    public Worker[] WorkersC { get; set; }
    private int _busyC = 0;

    public FurnitureSink Sink { get; set; }

    public TopFurnitureSimulation()
    {
        InitializeWorkers(1, 1, 1);

        Queues =
        [
            new FifoQueue<Order>(this), // DUMMY -> just to index from 1
            new Queue1(this),
            new Queue2(this),
            new Queue3(this),
            new Queue4(this),
        ];
        Sink = new FurnitureSink(this);
        Statistics =
        [
            new SampleStat("Average time of Chairs in system"), // 0
            new SampleStat("Average time of Tables in system"), // 1
            new SampleStat("Average time of Wardrobes in system"), // 2

            new SampleStat("Average waiting time in queue before technological STEP 1"), // 3
            new SampleStat("Average waiting time in queue before technological STEP 2"), // 4
            new SampleStat("Average waiting time in queue before technological STEP 3"), // 5
            new SampleStat("Average waiting time in queue before technological STEP 4"), // 6

            new WeightedStat("Weighted average queue length before technological STEP 1"), // 7
            new WeightedStat("Weighted average queue length before technological STEP 2"), // 8
            new WeightedStat("Weighted average queue length before technological STEP 3"), // 9
            new WeightedStat("Weighted average queue length before technological STEP 4"), // 10
        ];
    }

    private void InitializeWorkers(int a, int b, int c)
    {
        WorkersA = new Worker[a];
        WorkersB = new Worker[b];
        WorkersC = new Worker[c];
        _busyA = 0;
        _busyB = 0;
        _busyC = 0;

        int idWorker = 0;
        for (int i = 0; i < WorkersA.Length; i++)
        {
            WorkersA[i] = new Worker(idWorker, WorkerType.A);
        }

        for (int i = 0; i < WorkersB.Length; i++)
        {
            WorkersB[i] = new Worker(idWorker, WorkerType.B);
        }

        for (int i = 0; i < WorkersC.Length; i++)
        {
            WorkersC[i] = new Worker(idWorker, WorkerType.C);
        }
    }

    public void Reinitialize(int a, int b, int c, int days)
    {
        ResetSimulation();
        InitializeWorkers(a, b, c);
        OneReplicationLengthInSeconds = 60 * 60 * 8 * days;
    }

    public bool IsAvailable(WorkerType group)
    {
        return WorkersA.Length - GetBusyWorkerCount(group) > 0;
    }

    public int GetBusyWorkerCount(WorkerType group)
    {
        switch (group)
        {
            case WorkerType.A: return _busyA;
            case WorkerType.B: return _busyB;
            case WorkerType.C: return _busyC;
        }

        throw new Exception("Unknown worker type");
    }

    public Worker GetFirstAvailableWorker(WorkerType group)
    {
        Worker? worker = TryToGetFirstAvailableWorker(group);

        if (worker == null)
        {
            throw new Exception("Worker was not available!!!");
        }

        return worker;
    }

    private Worker? TryToGetFirstAvailableWorker(WorkerType group)
    {
        Worker[] workersOfThatGroup = GetWorkerArrBasedOnGroupType(group);

        foreach (Worker worker in workersOfThatGroup)
        {
            if (!worker.IsBusy) return worker;
        }

        return null;
    }

    private Worker[] GetWorkerArrBasedOnGroupType(WorkerType group)
    {
        switch (group)
        {
            case WorkerType.A:
                return WorkersA;
            case WorkerType.B:
                return WorkersB;
            case WorkerType.C:
                return WorkersC;
        }

        throw new Exception("Invalid group");
    }

    public FurnitureGenerators Generators { get; } = new();
    public int CompletedOrders { get; set; } = 0;

    public override void ResetSimulation()
    {
        base.ResetSimulation();

        foreach (FifoQueue<Order> queue in Queues)
        {
            queue.Clear();
        }

        InitializeWorkers(1, 1, 1);

        ChairsInSystem = 0;
        TablesInSystem = 0;
        WardrobesInSystem = 0;

        CompletedOrders = 0;
    }

    protected override void BeforeReplicationRun(SimCore simCore)
    {
        base.BeforeReplicationRun(simCore);
        simCore.Calendar.PlanNewEvent(new OrderArrival(0));
    }

    public Worker GetFirstAvailableWorkerAndMakeHimBusy(WorkerType workerType)
    {
        Worker worker = GetFirstAvailableWorker(workerType);
        MakeWorkerBusy(worker, workerType);
        return worker;
    }

    public void MakeWorkerBusy(Worker worker, WorkerType workerType)
    {
        if (workerType != worker.Type)
        {
            throw new Exception($"Worker's type doesn't match workerType." +
                                $" workerType: {workerType} Worker.Type: {worker.Type}");
        }
        ref int busy = ref GetBusyWorkerCountRef(workerType);
        busy++;
        worker.IsBusy = true;
    }


    public void PrintBusy()
    {
        Console.WriteLine($"BusyA: {_busyA}");
        Console.WriteLine($"BusyB: {_busyB}");
        Console.WriteLine($"BusyC: {_busyC}");
    }

    public void ReleaseWorker(Worker worker)
    {
        if (!worker.IsBusy)
        {
            throw new Exception("Worker is already set to AVAILABLE!!!");
        }
        worker.IsBusy = false;
        ref int busy = ref GetBusyWorkerCountRef(worker.Type);
        busy--;
        if (busy < 0)
        {
            throw new Exception("Busy count is < 0!!!");
        }
    }

    private ref int GetBusyWorkerCountRef(WorkerType workerType)
    {
        switch (workerType)
        {
            case WorkerType.A:
                return ref _busyA;
            case WorkerType.B:
                return ref _busyB;
            case WorkerType.C:
                return ref _busyC;
        }

        throw new Exception("Unknown worker type");
    }
}
