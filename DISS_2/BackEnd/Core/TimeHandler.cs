namespace DISS_2.BackEnd.Core
{
    public static class TimeHandler
    {
        public const int SecondsPerDay = 8 * 60 * 60;

        public const int RealStartHour = 6;

        public static string ToReadableTime(int timeInSeconds)
        {
            int day = timeInSeconds / SecondsPerDay + 1;

            int secondsInDay = timeInSeconds % SecondsPerDay;

            int hours = secondsInDay / 3600;
            int minutes = (secondsInDay / 60) % 60;
            int seconds = secondsInDay % 60;

            hours = (RealStartHour + hours) % 24;

            return $"Day {day} - {hours:D2}:{minutes:D2}:{seconds:D2}";
        }
    }
}
