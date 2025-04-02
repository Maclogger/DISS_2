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

    public List<Location> Locations { get; set; } = new();

    public FurnitureSink Sink { get; set; }

    public TopFurnitureSimulation()
    {
        InitializeWorkersAndLocations(2, 2, 18);

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

            new SampleStat("Average time of Order in system"), // 11

            new SampleStat("Average time of Order during Step1"), // 12
            new SampleStat("Average time of Order during Step2"), // 13
            new SampleStat("Average time of Order during Step3"), // 14
            new SampleStat("Average time of Order during Step4"), // 15
        ];

        RepStatistics =
        [
            new SampleStat("Average times of Chairs in system"), // 0
            new SampleStat("Average times of Tables in system"), // 1
            new SampleStat("Average times eof Wardrobes in system"), // 2

            new SampleStat("Average waiting times in queue before technological STEP 1"), // 3
            new SampleStat("Average waiting times in queue before technological STEP 2"), // 4
            new SampleStat("Average waiting times in queue before technological STEP 3"), // 5
            new SampleStat("Average waiting times in queue before technological STEP 4"), // 6

            new SampleStat(
                "Weighted average queue lengths before technological STEP 1", false), // 7
            new SampleStat(
                "Weighted average queue lengths before technological STEP 2", false), // 8
            new SampleStat(
                "Weighted average queue lengths before technological STEP 3", false), // 9
            new SampleStat(
                "Weighted average queue lengths before technological STEP 4", false), // 10

            new SampleStat("Average times of Order in system"), // 11

            new SampleStat("Average times of Order during Step1"), // 12
            new SampleStat("Average times of Order during Step2"), // 13
            new SampleStat("Average times of Order during Step3"), // 14
            new SampleStat("Average times of Order during Step4"), // 15
        ];

        ReinitializeWorkerStats();
    }

    public const int IndexOfAutoStats = 16;

    private void ReinitializeWorkerStats()
    {
        if (RepStatistics.Count >= IndexOfAutoStats)
        {
            RepStatistics.RemoveRange(IndexOfAutoStats, RepStatistics.Count - IndexOfAutoStats);
        }

        foreach (Worker worker in GetAllWorkers())
        {
            RepStatistics.Add(new SampleStat($"Average work times of worker {worker.Id} - " +
                                             $"{worker.Type}"));
        }
    }

    private void InitializeWorkersAndLocations(int a, int b, int c)
    {
        a = Math.Max(a, 1);
        b = Math.Max(b, 1);
        c = Math.Max(c, 1);

        WorkersA = new Worker[a];
        WorkersB = new Worker[b];
        WorkersC = new Worker[c];
        _busyA = 0;
        _busyB = 0;
        _busyC = 0;

        Locations.Clear();

        int idWorker = 0;
        for (int i = 0; i < WorkersA.Length; i++)
        {
            WorkersA[i] = new Worker(idWorker + i, WorkerType.A);
        }

        for (int i = 0; i < WorkersB.Length; i++)
        {
            WorkersB[i] = new Worker(idWorker + WorkersA.Length + i, WorkerType.B);
        }

        for (int i = 0; i < WorkersC.Length; i++)
        {
            WorkersC[i] = new Worker(idWorker + WorkersA.Length + WorkersB.Length + i, WorkerType
                .C);
        }

        ReinitializeWorkerStats();
    }

    public void Reinitialize(int a, int b, int c, int days)
    {
        ResetSimulation();
        InitializeWorkersAndLocations(a, b, c);
        OneReplicationLengthInSeconds = 60 * 60 * 8 * days;
    }


    protected override Task AfterReplicationRun()
    {
        for (int i = 0; i < Statistics.Count; i++)
        {
            ((SampleStat)RepStatistics[i]).AddValue(Statistics[i].CalcMean());
        }

        List<Worker> allWorkers = GetAllWorkers();
        for (int i = 0; i < allWorkers.Count; i++)
        {
            Worker worker = allWorkers[i];
            ((SampleStat)RepStatistics[i + IndexOfAutoStats]).AddValue(
                (double)worker.WorkTime /
                OneReplicationLengthInSeconds);
        }
        return Task.CompletedTask;
    }

    public bool IsAvailable(WorkerType group)
    {
        return GetAllWorkersCount(group) - GetBusyWorkerCount(group) > 0;
    }

    private int GetAllWorkersCount(WorkerType group)
    {
        switch (group)
        {
            case WorkerType.A: return WorkersA.Length;
            case WorkerType.B: return WorkersB.Length;
            case WorkerType.C: return WorkersC.Length;
        }

        throw new Exception("Invalid worker type in GetAllWorkersCount!!!");
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
    public List<Order> Orders { get; set; } = new();

    public override void ResetSimulation()
    {
        base.ResetSimulation();

        foreach (FifoQueue<Order> queue in Queues)
        {
            queue.Clear();
        }

        InitializeWorkersAndLocations(WorkersA.Length, WorkersB.Length, WorkersC.Length);

        ChairsInSystem = 0;
        TablesInSystem = 0;
        WardrobesInSystem = 0;

        CompletedOrders = 0;
        Orders.Clear();
    }


    public override void PrintState(Event @event)
    {
        if (@event is SysEvent) return;
        Console.WriteLine($"-----------------------------------------------------" +
                          $"--------------------------------------" +
                          $"----------\nAfter Event: {@event}\n");
        Console.WriteLine("Locations:");
        foreach (Location location in Locations)
        {
            Console.WriteLine(location);
        }

        Console.WriteLine("Orders:");
        foreach (Order order in Orders)
        {
            Console.WriteLine(order);
        }

        Console.WriteLine("Workers:");
        foreach (Worker worker in WorkersA)
        {
            Console.WriteLine(worker);
        }

        foreach (Worker worker in WorkersB)
        {
            Console.WriteLine(worker);
        }

        foreach (Worker worker in WorkersC)
        {
            Console.WriteLine(worker);
        }
    }

    protected override void BeforeReplicationRun()
    {
        base.BeforeReplicationRun();
        Calendar.PlanNewEvent(new OrderArrival(0));
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
        worker.MakeBusy(CurrentSimTime);
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

        worker.MakeAvailable(CurrentSimTime);
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

    public Location GetFirstAvailableOrNewLocation()
    {
        foreach (Location location in Locations)
        {
            if (location.CurrentOrder == null)
            {
                return location;
            }
        }

        Location newLocation = new(Locations.Count);
        Locations.Add(newLocation);
        return newLocation;
    }

    public int GetTravelTimeToLocation(Worker worker, Location location)
    {
        bool workerIsInWarehouse = worker.CurrentLocation == null;
        if (workerIsInWarehouse)
        {
            var genTravelTime = Generators.WarehouseTravelTimeGen;
            return (int)Math.Round(genTravelTime.Generate());
        }

        bool workerIsAlreadyAtTheRightLocation = worker.CurrentLocation == location;
        if (!workerIsAlreadyAtTheRightLocation)
        {
            var genTravelTimeGen = Generators.InterLocationTravelTimeGen;
            return (int)Math.Round(genTravelTimeGen.Generate());
        }

        return 0;
    }

    public List<Worker> GetAllWorkers()
    {
        List<Worker> allWorkers = new();
        allWorkers.AddRange(WorkersA);
        allWorkers.AddRange(WorkersB);
        allWorkers.AddRange(WorkersC);
        return allWorkers;
    }
}