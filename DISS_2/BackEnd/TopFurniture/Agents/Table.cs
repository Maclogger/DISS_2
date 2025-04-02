namespace DISS_2.BackEnd.TopFurniture.Agents;

public class Table(int arrivalTime) : Order(arrivalTime)
{
    public Table(Table table) : this(table.TimeArrival)
    {
    }
}