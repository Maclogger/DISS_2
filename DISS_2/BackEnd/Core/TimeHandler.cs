namespace DISS_2.BackEnd.Core
{
    public static class TimeHandler
    {
        public const int SecondsPerDay = 8 * 60 * 60;

        public const int RealStartHour = 6;

        public static string ToReadableDateTime(int timeInSeconds)
        {
            int day = timeInSeconds / SecondsPerDay + 1;

            int secondsInDay = timeInSeconds % SecondsPerDay;

            int hours = secondsInDay / 3600;
            int minutes = (secondsInDay / 60) % 60;
            int seconds = secondsInDay % 60;

            hours = (RealStartHour + hours) % 24;

            return $"Day {day} - {hours:D2}:{minutes:D2}:{seconds:D2}";
        }

        public static string ToReadableTime(double timeInSeconds)
        {
            if (timeInSeconds < 0.0)
            {
                timeInSeconds = 0.0;
            }

            int totalHours = (int)Math.Floor(timeInSeconds / 3600);
            int minutes = (int)Math.Floor(timeInSeconds / 60) % 60;
            double seconds = timeInSeconds % 60.0;

            string formattedSeconds = seconds.ToString("00.0000");

            return $"{totalHours:D2}:{minutes:D2}:{formattedSeconds}";
        }
    }
}