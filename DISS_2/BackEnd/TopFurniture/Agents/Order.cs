namespace DISS_2.BackEnd.TopFurniture.Agents;

public abstract class Order
{
    public int TimeArrival { get; set; }

    protected Order(int timeArrival)
    {
        TimeArrival = timeArrival;
    }
}