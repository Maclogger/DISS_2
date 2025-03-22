namespace DISS_2.BackEnd.Core;

public abstract class SimCore
{
    private SimState State { get; set; }

    protected SimCore(SimState state)
    {
        State = state;
    }

    public void RunOneSimulation()
    {
        Task.Run(() =>
        {
            BeforeSimulationRun(State);

            while (!State.Calendar.IsEmpty())
            {
                Event currentEvent = State.Calendar.PopEvent();
                UpdateAndVerifyEventTime(currentEvent);
                currentEvent.Execute(State);
            }

            AfterSimulationRun(State);
        });
    }

    protected virtual void AfterSimulationRun(SimState simState) {}

    protected virtual void BeforeSimulationRun(SimState simState) {}

    private void UpdateAndVerifyEventTime(Event currentEvent)
    {
        if (currentEvent.StartTime < State.CurrentSimTime)
        {
            throw new ArgumentException("Event start time is less than the simulation time!!!");
        }
        
        State.CurrentSimTime = currentEvent.StartTime;
    }
}