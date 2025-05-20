namespace fastmed_api.Misc;

public static class MedUtils
{
    public static bool isInTimeRange(DateTime dateTime, int dayOfWeek, TimeSpan startTime, TimeSpan endTime)
    {
        if (startTime == endTime) return false;
        return dateTime.DayOfWeek == (DayOfWeek) dayOfWeek && dateTime.TimeOfDay >= startTime && dateTime.TimeOfDay <= endTime;
    }
}