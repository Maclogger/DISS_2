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

        _sim.Frame++;
        double multiplier = (double)_sim.SpeedControl.GetSpeedMultiplier()!;

        int simTimeToAdd = CalcSimTimeToAdd((int)multiplier, _sim.Frame);

        if (_sim.Frame >= 10) _sim.Frame = 0;

        simCore.Calendar.PlanNewEvent(new SysEvent(_sim.CurrentSimTime + simTimeToAdd));
        await Task.Delay(100);
    }

    private int CalcSimTimeToAdd(int multiplier, int frame)
    {
        //Console.WriteLine($"multiplier: {multiplier}, frame: {frame}");
        if (multiplier == 1) return frame == 9 ? 1 : 0;

        if (multiplier == 2) return frame == 4 || frame == 9 ? 1 : 0;

        if (multiplier == 5) return frame % 2 == 1 ? 1 : 0;

        if (multiplier == 10) return 1;
        
        return multiplier / 10;
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