using DISS_2.BackEnd.Core;

namespace DISS_2.BackEnd.TopFurniture;

public class Worker
{
    public int Id { get; set; }
    public WorkerType Type { get; set; }

    public bool IsBusy { get; set; }
    public Location? CurrentLocation { get; set; }

    public int TimeOfWorkStart { get; set; } = -1;
    public int WorkTime { get; set; } = 0;
    public bool IsTakingOrder { get; set; }

    public Worker(int id, WorkerType type)
    {
        Id = id;
        Type = type;
        IsBusy = false;
        IsTakingOrder = false;
    }

    public Worker(Worker other)
    {
        Id = other.Id;
        Type = other.Type;
        IsBusy = other.IsBusy;
        IsTakingOrder = other.IsTakingOrder;
    }

    public void LeaveLocation()
    {
        if (CurrentLocation != null)
        {
            CurrentLocation.Leave(this);
        }
    }

    public override string ToString()
    {
        return $"   WORKER: {Id} - {Type} - IsBusy:{IsBusy} - CurrentLocation:{CurrentLocation?.Id}";
    }

    public void MakeBusy(int simulationTime)
    {
        if (TimeOfWorkStart >= 0)
        {
            throw new Exception("Worker was already busy.");
        }
        IsBusy = true;
        TimeOfWorkStart = simulationTime;
    }

    public void MakeAvailable(int simulationTime)
    {
        if (TimeOfWorkStart < 0)
        {
            throw new Exception("Worker was already available.");
        }
        IsBusy = false;
        WorkTime += simulationTime - TimeOfWorkStart;
        TimeOfWorkStart = -1;
    }
}

public enum WorkerType
{
    A,
    B,
    C
}