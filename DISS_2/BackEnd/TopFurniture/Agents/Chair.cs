namespace DISS_2.BackEnd.TopFurniture.Agents;

public class Chair(int arrivalTime) : Order(arrivalTime)
{
    public Chair(Chair other) : this(other.TimeArrival)
    {
    }
}