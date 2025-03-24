namespace DISS_2.BackEnd.Core;

public class SysEvent(int startTime) : Event(startTime)
{
    private SimCore? _sim;
    public override async Task Execute(SimCore simCore)
    {
        _sim = simCore;
        Speed currentSpeed = _sim.SpeedControl.CurrentSpeed;
        if (currentSpeed == Speed.Stopped)
        {
            currentSpeed = await WaitForSpeedToChange();
        }

        if (currentSpeed == Speed.FullSpeed)
        {
            return;
        }

        int simTimeToAdd = CalcHowMuchSimTimeToAdd();
        simCore.Calendar.PlanNewEvent(new SysEvent(_sim.CurrentSimTime + simTimeToAdd));
        await Task.Delay(1000);
    }

    private async Task<Speed> WaitForSpeedToChange()
    {
        while (true)
        {
            Speed currentSpeed = _sim!.SpeedControl.CurrentSpeed;

            if (currentSpeed != Speed.Stopped)
            {
                return currentSpeed;
            }

            await Task.Delay(100);
        }
    }

    private int CalcHowMuchSimTimeToAdd()
    {
        double? speedMultiplier = _sim!.SpeedControl.GetSpeedMultiplier();
        if (speedMultiplier == null)
        {
            throw new Exception(
                "Speed was FULL_SPEED in SysEvent: WaitForSpeedToChange did not work.");
        }

        return (int)speedMultiplier.Value;
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