namespace DISS_2.BackEnd.Core;

public abstract class SimState
{
    public EventCalendar Calendar { get; set; } = new();
    public int CurrentSimTime { get; set; } = 0;
}