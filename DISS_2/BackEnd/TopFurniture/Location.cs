using DISS_2.BackEnd.TopFurniture.Agents;

namespace DISS_2.BackEnd.TopFurniture;

public class Location
{
    public int Id { get; set; }
    public HashSet<Worker> Workers { get; set; } = new();
    public Order? CurrentOrder { get; set; }
    public Location? CurrentLocation { get; set; } = null;

    public bool IsOccupied()
    {
        return CurrentOrder != null;
    }

    public bool IsWorkerPresent(Worker worker)
    {
        return Workers.Contains(worker);
    }

    public void Occupy(Worker worker, Order order)
    {
        if (CurrentOrder != null)
        {
            throw new Exception($"Location {ToString()} is already occupied");
        }

        if (!IsWorkerPresent(worker))
        {
            Workers.Add(worker);
        }

        CurrentOrder = order;
    }


    public override string ToString()
    {
        return
            $"Location: {Id} - {CurrentOrder} - " +
            $"{string.Join(", ", Workers.Select(w => w.ToString()))}";
    }
}