namespace DISS_2.BackEnd.Core;

public class SysEvent(int startTime) : Event(startTime)
{
    public override async Task Execute(SimCore simCore)
    {
        Speed currentSpeed = MainApp.Instance.SpeedControl.CurrentSpeed;
        if (currentSpeed == Speed.Stopped)
        {
            currentSpeed = await WaitForSpeedToChange();
        }

        if (currentSpeed == Speed.FullSpeed)
        {
            return;
        }

        int delay = MainApp.Instance.SpeedControl.GetDelayBetweenFrames();
        int nextSysEventSimTime = CalcNextSysEventSimTime(simCore, delay);
        simCore.Calendar.PlanNewEvent(new SysEvent(nextSysEventSimTime));

        await Task.Delay(delay);
    }

    private async Task<Speed> WaitForSpeedToChange()
    {
        while (true)
        {
            Speed currentSpeed = MainApp.Instance.SpeedControl.CurrentSpeed;

            if (currentSpeed != Speed.Stopped)
            {
                return currentSpeed;
            }

            await Task.Delay(100);
        }
    }

    private int CalcNextSysEventSimTime(SimCore simCore, int delay)
    {
        simCore.CurrentActualTimeInMs += delay;
        if (simCore.CurrentActualTimeInMs >=
            (int)(1_000.0 / MainApp.Instance.SpeedControl.GetSpeedMultiplier()!))
        {
            simCore.CurrentActualTimeInMs = 0;
            return simCore.CurrentSimTime + 1;
        }

        return simCore.CurrentSimTime;
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