namespace DISS_2.BackEnd.Core;

public abstract class Event : IComparable<Event>
{
    public int StartTime { get; set; }


    public Event(int startTime)
    {
        StartTime = startTime;
    }

    public abstract Task Execute(SimCore sim);

    public override string ToString()
    {
        return $"{TimeHandler.ToReadableDateTime(StartTime)}: {GetType().Name}";
    }

    public int CompareTo(Event? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (other is null) return 1;
        return StartTime.CompareTo(other.StartTime);
    }

    public virtual Task BeforeEvent(SimCore sim)
    {
        return Task.CompletedTask;
    }

    public virtual Task AfterEvent(SimCore sim)
    {
        if (Config.DebugMode) sim.PrintState(this);
        return Task.CompletedTask;
    }
}