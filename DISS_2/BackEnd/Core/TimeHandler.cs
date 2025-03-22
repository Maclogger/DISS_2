namespace DISS_2.BackEnd.Core;

public static class TimeHandler
{
    public static string ToReadableTime(int timeInSeconds)
    {
        int hours = (timeInSeconds / 3600) % 24;
        int minutes = (timeInSeconds / 60) % 60;
        int seconds = timeInSeconds % 60;

        return $"{hours:D2}:{minutes:D2}:{seconds:D2}";
    }
}