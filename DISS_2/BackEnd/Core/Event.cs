namespace DISS_2.BackEnd.Core;

public abstract class Event
{
    public int StartTime { get; set; }


    public Event(int startTime)
    {
        StartTime = startTime;
    }

    public abstract void Execute(SimState simState);
}