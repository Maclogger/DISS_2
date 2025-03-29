using DISS_2.BackEnd.Core;

namespace DISS_2.BackEnd.TopFurniture;

public class Worker
{
    public int Id { get; set; }
    public WorkerType Type { get; set; }

    public bool IsBusy { get; set; }

    public Worker(int id, WorkerType type)
    {
        Id = id;
        Type = type;
        IsBusy = false;
    }
}

public enum WorkerType
{
    A,
    B,
    C
}