namespace DISS_2.BackEnd.TopFurniture.Agents;

public class Wardrobe(int arrivalTime) : Order(arrivalTime)
{
    public Wardrobe(Wardrobe wardrobe) : this(wardrobe.TimeArrival)
    {
    }
}