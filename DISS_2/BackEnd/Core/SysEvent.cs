namespace DISS_2.BackEnd.Core;

public class SysEvent(int startTime) : Event(startTime)
{
    public override async Task Execute(SimState simState)
    {
        if (MainApp.Instance.SpeedControl.CurrentSpeed == Speed.FullSpeed)
        {
            return;
        }

        int delay = MainApp.Instance.SpeedControl.GetDelayBetweenFrames();
        int nextSysEventSimTime = CalcNextSysEventSimTime(simState, delay);
        simState.Calendar.PlanNewEvent(new SysEvent(nextSysEventSimTime));

        await Task.Delay(delay);
    }

    private int CalcNextSysEventSimTime(SimState simState, int delay)
    {
        simState.CurrentActualTimeInMs += delay;
        if (simState.CurrentActualTimeInMs >= (int)(1_000.0 / MainApp.Instance.SpeedControl.GetSpeedMultiplier()!))
        {
            simState.CurrentActualTimeInMs = 0;
            return simState.CurrentSimTime + 1;
        }
        return simState.CurrentSimTime; 
    }
}

/*

00:00
00:01
00:02
00:03
00:04
00:05
00:06
00:07
00:08
00:09
00:10

*/