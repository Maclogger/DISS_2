using DISS_2.Components;

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
            Console.WriteLine("zaciname");

            while (!State.Calendar.IsEmpty() && State.CurrentSimTime < 10000)
            {
                Event currentEvent = State.Calendar.PopEvent();
                Console.WriteLine($"Current event: {currentEvent}");
                VerifyAndUpdateEventTime(currentEvent);
                currentEvent.Execute(State);

                RefreshGui();
            }

            Console.WriteLine("hotovo");
            AfterSimulationRun(State);
        });
    }

    private void RefreshGui()
    {
        foreach (IDelegate @delegate in MainApp.Instance.Delegates)
        {
            @delegate.Refresh(State);
        }
    }

    protected virtual void AfterSimulationRun(SimState simState) {}

    protected virtual void BeforeSimulationRun(SimState simState) {}

    private void VerifyAndUpdateEventTime(Event currentEvent)
    {
        if (currentEvent.StartTime < State.CurrentSimTime)
        {
            throw new ArgumentException("Event start time is less than the current simulation time!!!");
        }
        
        State.CurrentSimTime = currentEvent.StartTime;
    }
}