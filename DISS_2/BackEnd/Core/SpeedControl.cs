namespace DISS_2.BackEnd.Core;

public enum Speed
{
    Stopped,
    Speed1X,
    Speed2X,
    Speed5X,
    Speed10X,
    Speed100X,
    Speed1000X,
    Speed10000X,
    Speed100000X,
    Speed1000000X,
    FullSpeed
}

public class SpeedControl
{
    public Speed CurrentSpeed { get; set; } = Speed.Speed2X;

    public double? GetSpeedMultiplier()
    {
        switch (CurrentSpeed)
        {
            case Speed.Stopped:
                return 0.0;
            case Speed.Speed1X:
                return 1.0;
            case Speed.Speed2X:
                return 2.0;
            case Speed.Speed5X:
                return 5.0;
            case Speed.Speed10X:
                return 10.0;
            case Speed.Speed100X:
                return 100.0;
            case Speed.Speed1000X:
                return 1_000.0;
            case Speed.Speed10000X:
                return 10_000.0;
            case Speed.Speed100000X:
                return 100_000.0;
            case Speed.Speed1000000X:
                return 1_000_000.0;
            case Speed.FullSpeed:
                return null;
            default:
                return 1.0;
        }
    }
}