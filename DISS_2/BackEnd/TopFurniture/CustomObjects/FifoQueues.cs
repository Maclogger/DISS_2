using DISS_2.BackEnd.Core;
using DISS_2.BackEnd.Core.Objects;
using DISS_2.BackEnd.Statistics;
using DISS_2.BackEnd.TopFurniture.Agents;

namespace DISS_2.BackEnd.TopFurniture.CustomObjects;

public class Queue1(SimCore core) : FifoQueue<Order>(core)
{
    protected override void BeforeEnqueue(Order item)
    {
        item.TimeArrivalAtQueue1 = Sim.CurrentSimTime;
        base.BeforeEnqueue(item);
    }

    protected override void AfterDequeue(Order item)
    {
        ((SampleStat)Sim.Statistics[3]).AddValue(Sim.CurrentSimTime - item.TimeArrivalAtQueue1);
        base.AfterDequeue(item);
    }
}

public class Queue2(SimCore core) : FifoQueue<Order>(core)
{
    protected override void BeforeEnqueue(Order item)
    {
        item.TimeArrivalAtQueue2 = Sim.CurrentSimTime;
        base.BeforeEnqueue(item);
    }

    protected override void AfterDequeue(Order item)
    {
        ((SampleStat)Sim.Statistics[4]).AddValue(Sim.CurrentSimTime - item.TimeArrivalAtQueue2);
        base.AfterDequeue(item);
    }
}

public class Queue3(SimCore core) : FifoQueue<Order>(core)
{
    protected override void BeforeEnqueue(Order item)
    {
        item.TimeArrivalAtQueue3 = Sim.CurrentSimTime;
        base.BeforeEnqueue(item);
    }

    protected override void AfterDequeue(Order item)
    {
        ((SampleStat)Sim.Statistics[5]).AddValue(Sim.CurrentSimTime - item.TimeArrivalAtQueue3);
        base.AfterDequeue(item);
    }
}


public class Queue4(SimCore core) : FifoQueue<Order>(core)
{
    protected override void BeforeEnqueue(Order item)
    {
        item.TimeArrivalAtQueue4 = Sim.CurrentSimTime;
        base.BeforeEnqueue(item);
    }

    protected override void AfterDequeue(Order item)
    {
        ((SampleStat)Sim.Statistics[6]).AddValue(Sim.CurrentSimTime - item.TimeArrivalAtQueue4);
        base.AfterDequeue(item);
    }
}