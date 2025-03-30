using DISS_2.BackEnd.Core;
using DISS_2.BackEnd.Core.Objects;
using DISS_2.BackEnd.Statistics;
using DISS_2.BackEnd.TopFurniture.Agents;

namespace DISS_2.BackEnd.TopFurniture.CustomObjects;

public class FurnitureSink(SimCore core) : Sink<Order>(core)
{
    private TopFurnitureSimulation Sim { get; } = (TopFurnitureSimulation)core;

    protected override void AfterSink(Order item)
    {
        Sim.CompletedOrders++;
        if (item.Location == null)
        {
            throw new Exception($"No location found in order! {GetType().Name}");
        }

        item.Location.CurrentOrder = null;

        if (Config.DebugMode) Sim.Orders.Remove(item);


        ((SampleStat)Sim.Statistics[11]).AddValue(Sim.CurrentSimTime - item.TimeArrival);
        if (item is Chair)
        {
            Sim.ChairsInSystem--;
            ((SampleStat)Sim.Statistics[0]).AddValue(Sim.CurrentSimTime - item.TimeArrival);
        }
        else if (item is Table)
        {
            Sim.TablesInSystem--;
            ((SampleStat)Sim.Statistics[1]).AddValue(Sim.CurrentSimTime - item.TimeArrival);
        }
        else if (item is Wardrobe)
        {
            Sim.WardrobesInSystem--;
            ((SampleStat)Sim.Statistics[2]).AddValue(Sim.CurrentSimTime - item.TimeArrival);
        }
        else
        {
            throw new Exception("Unknown ORDER TYPE in sink!");
        }
    }
}