
using UnityEngine.Events;

[System.Serializable]
public class TimeUnityEvent : UnityEvent<GameTime>
{
}

public class GameTime
{
    public int Hour;
    public int Minute;
}