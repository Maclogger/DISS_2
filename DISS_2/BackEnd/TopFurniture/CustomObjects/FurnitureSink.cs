using DISS_2.BackEnd.Core;
using DISS_2.BackEnd.Core.Objects;
using DISS_2.BackEnd.TopFurniture.Agents;

namespace DISS_2.BackEnd.TopFurniture.CustomObjects;

public class FurnitureSink(SimCore core) : Sink<Order>(core)
{
    public TopFurnitureSimulation Sim { get; set; } = (TopFurnitureSimulation)core;

    protected override void AfterSink(Order item)
    {
        if (item is Chair)
        {
            Sim.ChairsInSystem--;
        } else if (item is Table)
        {
            Sim.TablesInSystem--;
        } else if (item is Wardrobe)
        {
            Sim.WardrobesInSystem--;
        }
    }
}