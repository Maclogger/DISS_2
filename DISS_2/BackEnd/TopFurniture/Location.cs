using DISS_2.BackEnd.TopFurniture.Agents;

namespace DISS_2.BackEnd.TopFurniture;

public class Location
{

    public Location(int id)
    {
        Id = id;
    }
    public Location(Location other)
    {
        Id = other.Id;
        Workers = new();
        foreach (Worker worker in other.Workers)
        {
            Worker copyWorker = new Worker(worker);
            copyWorker.CurrentLocation = this;
            Workers.Add(copyWorker);
        }

        if (other.CurrentOrder is Chair chair)
        {
            CurrentOrder = new Chair(chair);
        } else if (other.CurrentOrder is Table table)
        {
            CurrentOrder = new Table(table);
        } else if (other.CurrentOrder is Wardrobe wardrobe)
        {
            CurrentOrder = new Wardrobe(wardrobe);
        }
    }

    public int Id { get; set; }
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