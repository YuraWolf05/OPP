namespace Lab4
{
    // Інтерфейс для підрахунку споживання електроенергії
    public interface IPowerUsage
    {
        double GetDailyConsumption();  // кВт·год за день
        double GetWeeklyConsumption(); // кВт·год за тиждень
    }
}
