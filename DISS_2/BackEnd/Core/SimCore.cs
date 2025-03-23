using DISS_2.Components;

namespace DISS_2.BackEnd.Core;

public abstract class SimCore
{
    public EventCalendar Calendar { get; set; }
    public int CurrentSimTime { get; set; } = 0;
    public int CurrentActualTimeInMs { get; set; } = 0;
    public int CurrReplication { get; set; } = 0;

    public SimCore()
    {
        Calendar = new EventCalendar(this);
    }


    public async Task RunSimulation(int replicationCount)
    {
        await Task.Run(async () =>
        {
            BeforeSimulation();
            for (CurrReplication = 0; CurrReplication < replicationCount; CurrReplication++)
            {
                await RunOneReplication();
            }
            AfterSimulation();
        });
    }

    public async Task RunOneSimulation()
    {
        await Task.Run(async () =>
        {
            Calendar.PlanNewEvent(new SysEvent(0));
            await RunOneReplication();
        });
    }

    private async Task RunOneReplication()
    {
        BeforeReplicationRun(this);
        while (!Calendar.IsEmpty() && CurrentSimTime < 100_000)
        {
            Event currentEvent = Calendar.PopEvent();
            VerifyAndUpdateEventTime(currentEvent);
            await currentEvent.Execute(this);
            RefreshGuiAfterEvent(currentEvent);
        }

        AfterReplicationRun(this);
    }

    private void RefreshGuiAfterEvent(Event currentEvent)
    {
        foreach (ISimDelegate @delegate in MainApp.Instance.SimDelegates)
        {
            @delegate.UpdateUi(this, currentEvent);
        }
    }

    private void RefreshGuiAfterRep(int currentReplication)
    {
        /*foreach (ISimDelegate @delegate in MainApp.Instance.SimDelegates)
        {
            @delegate.UpdateUi(State, currentReplication);
        }*/
    }


    protected virtual void BeforeSimulation()
    {
    }


    protected virtual void BeforeReplicationRun(SimCore simCore)
    {
    }

    protected virtual void AfterReplicationRun(SimCore simCore)
    {
    }

    protected virtual void AfterSimulation()
    {
    }

    private void VerifyAndUpdateEventTime(Event currentEvent)
    {
        if (currentEvent.StartTime < CurrentSimTime)
        {
            throw new ArgumentException(
                "Event start time is less than the current simulation time!!!");
        }

        CurrentSimTime = currentEvent.StartTime;
    }
}