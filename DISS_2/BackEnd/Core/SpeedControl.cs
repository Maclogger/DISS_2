namespace DISS_2.BackEnd.Core;



public enum Speed
{
    Speed1X,
    Speed2X,
    Speed3X,
    FullSpeed
}

public class SpeedControl
{
    public Speed CurrentSpeed { get; set; }

    public double? GetSpeedMultiplier()
    {
        switch (CurrentSpeed)
        {
            case Speed.Speed1X:
                return 1.0;
            case Speed.Speed2X:
                return 2.0;
            case Speed.Speed3X:
                return 3.0;
            case Speed.FullSpeed:
                return null;
            default:
                return 1.0;
        }
    }
}