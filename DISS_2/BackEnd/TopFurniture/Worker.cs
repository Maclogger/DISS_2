using DISS_2.BackEnd.Core;

namespace DISS_2.BackEnd.TopFurniture;

public class Worker
{
    public int Id { get; set; }
    public WorkerType Type { get; set; }

    public bool IsBusy { get; set; }
    public Location? CurrentLocation { get; set; }

    public Worker(int id, WorkerType type)
    {
        Id = id;
        Type = type;
        IsBusy = false;
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
}

public enum WorkerType
{
    A,
    B,
    C
}