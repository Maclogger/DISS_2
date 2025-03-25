using DISS_2.Components;

namespace DISS_2.BackEnd.Core;

public abstract class SimCore
{
    public EventCalendar Calendar { get; set; }
    public int CurrentSimTime { get; set; } = 0;
    public int Frame { get; set; } = 0;
    public int CurrReplication { get; set; } = 0;

    public bool IsRunning { get; set; } = false;

    public SpeedControl SpeedControl { get; set; } = new();

    public SimCore()
    {
        Calendar = new EventCalendar(this);
    }

    public async Task RunSimulation(int replicationCount)
    {
        IsRunning = true;
        await Task.Run(async () =>
        {
            BeforeSimulation();
            for (CurrReplication = 0; CurrReplication < replicationCount; CurrReplication++)
            {
                if (!IsRunning) break;
                await RunOneReplication();
            }

            AfterSimulation();
        });
        IsRunning = false;
    }

    public async Task RunOneSimulation()
    {
        IsRunning = true;
        await Task.Run(async () =>
        {
            Calendar.PlanNewEvent(new SysEvent(0));
            await RunOneReplication();
        });
        IsRunning = false;
    }

    public void FinishSimulation()
    {
        IsRunning = false;
        Console.WriteLine("Simulation finished");
        SpeedControl.CurrentSpeed = Speed.Speed1X;
    }


    private async Task RunOneReplication()
    {
        BeforeReplicationRun(this);
        while (!Calendar.IsEmpty() && CurrentSimTime < 100_000)
        {
            if (!IsRunning) break;
            Event currentEvent = Calendar.PopEvent();
            VerifyAndUpdateEventTime(currentEvent);
            await currentEvent.Execute(this);
            RefreshGuiAfterEvent(currentEvent);
        }

        AfterReplicationRun(this);
    }


    private void RefreshGuiAfterEvent(Event currentEvent)
    {
        List<ISimDelegate> delegates = MainApp.Instance.SimDelegates.ToList();

        foreach (ISimDelegate @delegate in delegates)
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

    public virtual void ResetSimulation()
    {
        Calendar.Reset();
        CurrentSimTime = 0;
        Frame = 0;
        IsRunning = false;
    }
}