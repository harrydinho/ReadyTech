namespace ReadyTech.API.Services;

public class DateTimeService
{
    private static DateTime? customDateTime;
    public static DateTime Now
    {
        get => customDateTime ?? DateTime.Now;
    }

    public static void SetCurrentDateTime(DateTime customNow)
    {
        customDateTime = customNow;
    }

    public static void ResetDateTime()
    {
        customDateTime = null;
    }
}
