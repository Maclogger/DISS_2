using DISS_2.Components;

namespace DISS_2.BackEnd.Core;

public abstract class SimCore
{
    public List<ISimDelegate> SimDelegates { get; set; } = new();

    public List<IRepDelegate> RepDelegates { get; set; } = new();
    public EventCalendar Calendar { get; set; }
    public int CurrentSimTime { get; set; } = 0;
    public int Frame { get; set; } = 0;
    public int CurrReplication { get; set; } = 0;

    public bool IsRunning { get; set; } = false;
    public int OneReplicationLengthInSeconds { get; set; } = 60 * 60 * 8 * 100;

    public SpeedControl SpeedControl { get; set; } = new();

    public List<Statistics.Statistics> Statistics { get; set; } = new();

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
        while (!Calendar.IsEmpty() && CurrentSimTime < OneReplicationLengthInSeconds)
        {
            if (!IsRunning) break;
            Event currentEvent = Calendar.PopEvent();
            VerifyAndUpdateEventTime(currentEvent);
            await currentEvent.BeforeEvent(this);
            await currentEvent.Execute(this);
            await currentEvent.AfterEvent(this);
            RefreshGuiAfterEvent(currentEvent);
        }

        RefreshGuiAfterRep();
        AfterReplicationRun(this);
    }


    private void RefreshGuiAfterEvent(Event currentEvent)
    {
        List<ISimDelegate> delegates = SimDelegates.ToList(); // thread safe

        foreach (ISimDelegate @delegate in delegates)
        {
            @delegate.UpdateUi(this, currentEvent);
        }
    }


    private void RefreshGuiAfterRep()
    {
        List<IRepDelegate> delegates = RepDelegates.ToList(); // thread safe

        foreach (IRepDelegate @delegate in delegates)
        {
            @delegate.UpdateUi(this);
        }
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
                $"Event start time is less than the current simulation time!!! (SimTime = {CurrentSimTime}, StartTime = {currentEvent.StartTime})");
        }

        CurrentSimTime = currentEvent.StartTime;
    }

    public virtual void ResetSimulation()
    {
        Calendar.Reset();
        CurrentSimTime = 0;
        Frame = 0;
        IsRunning = false;
        foreach (Statistics.Statistics statistic in Statistics)
        {
            statistic.Clear();
        }
    }

    public abstract void PrintState(Event @event);
}