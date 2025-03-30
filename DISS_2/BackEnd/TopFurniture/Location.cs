using DISS_2.BackEnd.TopFurniture.Agents;

namespace DISS_2.BackEnd.TopFurniture;

public class Location(int id)
{
    public int Id { get; set; } = id;
    public HashSet<Worker> Workers { get; set; } = new();
    public Order? CurrentOrder { get; set; }

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
        if (CurrentOrder != null && CurrentOrder != order)
        {
            throw new Exception($"Location {ToString()} is already occupied");
        }

        worker.LeaveLocation();
        if (!IsWorkerPresent(worker))
        {
            Workers.Add(worker);
        }

        worker.CurrentLocation = this;
        order.Location = this;
        CurrentOrder = order;
    }

    public void Leave(Worker worker)
    {
        if (!IsWorkerPresent(worker))
        {
            throw new Exception($"Location {ToString()} is not occupied by " +
                                $"the worker in Leave: {worker}");
        }

        Workers.Remove(worker);
    }


    public override string ToString()
    {
        return
            $"   Location: {Id} - {CurrentOrder} - " +
            $"Workers: {string.Join(", ", Workers.Select(w => w.Id))}";
    }
}