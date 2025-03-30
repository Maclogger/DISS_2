using DISS_2.BackEnd.Core;
using DISS_2.BackEnd.TopFurniture.Agents;

namespace DISS_2.BackEnd.TopFurniture.CustomEvents;

public abstract class OrderEvent : Event
{
    public Order Order { get; set; }

    public OrderEvent(int startTime, Order order) : base(startTime)
    {
        Order = order;
    }
    public abstract override Task Execute(SimCore sim);

    public override string ToString()
    {
        return $"{TimeHandler.ToReadableDateTime(StartTime)}: {GetType().Name} with{Order}";
    }
}