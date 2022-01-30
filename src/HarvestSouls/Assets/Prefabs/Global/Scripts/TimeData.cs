using UnityEngine;

[CreateAssetMenu(menuName = "Data/TimeData")]
public class TimeData : ScriptableObject
{
    public int RealSecondsPerDay;
    public int GameHoursPerDay;
    public int GameMinsPerHour;

    public int DayStartHour;
    public int DuskHour;
    public int NightHour;
    public int DayEndHour;
}
