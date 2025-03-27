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

    protected override void AfterEnqueue(Order item)
    {
        base.AfterEnqueue(item);
        ((WeightedStat)Sim.Statistics[7]).AddValue(GetCount(), Sim.CurrentSimTime);
    }

    protected override void AfterDequeue(Order item)
    {
        base.AfterDequeue(item);
        ((SampleStat)Sim.Statistics[3]).AddValue(Sim.CurrentSimTime - item.TimeArrivalAtQueue1);
        ((WeightedStat)Sim.Statistics[7]).AddValue(GetCount(), Sim.CurrentSimTime);
    }
}

public class Queue2(SimCore core) : FifoQueue<Order>(core)
{
    protected override void BeforeEnqueue(Order item)
    {
        item.TimeArrivalAtQueue2 = Sim.CurrentSimTime;
        base.BeforeEnqueue(item);
    }

    protected override void AfterEnqueue(Order item)
    {
        base.AfterEnqueue(item);
        ((WeightedStat)Sim.Statistics[8]).AddValue(GetCount(), Sim.CurrentSimTime);
    }

    protected override void AfterDequeue(Order item)
    {
        base.AfterDequeue(item);
        ((SampleStat)Sim.Statistics[4]).AddValue(Sim.CurrentSimTime - item.TimeArrivalAtQueue2);
        ((WeightedStat)Sim.Statistics[8]).AddValue(GetCount(), Sim.CurrentSimTime);
    }
}

public class Queue3(SimCore core) : FifoQueue<Order>(core)
{
    protected override void BeforeEnqueue(Order item)
    {
        item.TimeArrivalAtQueue3 = Sim.CurrentSimTime;
        base.BeforeEnqueue(item);
    }

    protected override void AfterEnqueue(Order item)
    {
        base.AfterEnqueue(item);
        ((WeightedStat)Sim.Statistics[9]).AddValue(GetCount(), Sim.CurrentSimTime);
    }

    protected override void AfterDequeue(Order item)
    {
        base.AfterDequeue(item);
        ((SampleStat)Sim.Statistics[5]).AddValue(Sim.CurrentSimTime - item.TimeArrivalAtQueue3);
        ((WeightedStat)Sim.Statistics[9]).AddValue(GetCount(), Sim.CurrentSimTime);
    }
}


public class Queue4(SimCore core) : FifoQueue<Order>(core)
{
    protected override void BeforeEnqueue(Order item)
    {
        item.TimeArrivalAtQueue4 = Sim.CurrentSimTime;
        base.BeforeEnqueue(item);
    }

    protected override void AfterEnqueue(Order item)
    {
        base.AfterEnqueue(item);
        ((WeightedStat)Sim.Statistics[10]).AddValue(GetCount(), Sim.CurrentSimTime);
    }

    protected override void AfterDequeue(Order item)
    {
        base.AfterDequeue(item);
        ((SampleStat)Sim.Statistics[6]).AddValue(Sim.CurrentSimTime - item.TimeArrivalAtQueue4);
        ((WeightedStat)Sim.Statistics[10]).AddValue(GetCount(), Sim.CurrentSimTime);
    }
}