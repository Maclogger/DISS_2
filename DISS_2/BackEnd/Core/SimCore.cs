namespace DISS_2.BackEnd.Core;

public abstract class SimCore
{
    public EventCalendar Calendar { get; set; } = new();
    public int CurrentSimTime { get; set; } = 0;

    public void RunOneSimulation()
    {
        Task.Run(async () =>
        {
            BeforeSimulationRun();

            while (!Calendar.IsEmpty())
            {
                Event currentEvent = Calendar.PopEvent();
                UpdateAndVerifyEventTime(currentEvent);
                currentEvent.Execute(Calendar, CurrentSimTime);
            }

            AfterSimulationRun();
        });
    }

    protected abstract void AfterSimulationRun();

    protected abstract void BeforeSimulationRun();

    private void UpdateAndVerifyEventTime(Event currentEvent)
    {
        if (currentEvent.StartTime < CurrentSimTime)
        {
            throw new ArgumentException("Event start time is less than the simulation time!!!");
        }
        
        CurrentSimTime = currentEvent.StartTime;
    }
}