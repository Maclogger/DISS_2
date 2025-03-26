using DISS_2.BackEnd.Core;
using DISS_2.BackEnd.Statistics;
using DISS_2.BackEnd.TopFurniture.Agents;
using DISS_2.BackEnd.TopFurniture.Queues;

namespace DISS_2.BackEnd.TopFurniture;

public class TopFurnitureSimulation : SimCore
{
    public List<FifoQueue<Order>> Queues { get; set; }

    public TopFurnitureSimulation()
    {
        Queues =
        [
            new Queue1(this),
            new Queue2(this),
            new Queue3(this),
            new Queue4(this),
        ];
    }

    public override void ResetSimulation()
    {
        foreach (var queue in Queues)
        {
            queue.Clear();
        }

        foreach (var statistic in Statistics)
        {
            statistic.Clear();
        }

        base.ResetSimulation();
    }
}