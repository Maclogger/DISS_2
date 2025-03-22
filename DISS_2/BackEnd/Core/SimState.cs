namespace DISS_2.BackEnd.Core;

public abstract class SimState
{
    protected SimState()
    {
        Calendar = new EventCalendar(this);
    }

    public EventCalendar Calendar { get; set; }
    public int CurrentSimTime { get; set; } = 0;
    public int CurrentActualTimeInMs { get; set; } = 0;
}