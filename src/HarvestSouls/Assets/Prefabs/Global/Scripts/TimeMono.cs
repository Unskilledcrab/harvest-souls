using UnityEngine;

public class TimeMono : MonoBehaviour
{
    public TimeData Data;

    public GameEvent OnDayStartedEvent;
    public GameEvent OnDayEndedEvent;

    public StringReference GameTime;

    float _gameHourFloat;
    float _gameHoursPerRealSecond;
    int _lastMinute;

    float GetGameHourFloat() => (Time.time % Data.RealSecondsPerDay * _gameHoursPerRealSecond + Data.DayStartHour) % Data.GameHoursPerDay;

    // Start is called before the first frame update
    void Start()
    {
        _gameHoursPerRealSecond = Data.GameHoursPerDay * 1.0f / Data.RealSecondsPerDay;
        _gameHourFloat = Data.DayStartHour;
    }

    // Update is called once per frame
    void Update()
    {
        var gameHourFloat = GetGameHourFloat();

        UpdateTime(gameHourFloat);
        EvaluateTimeOfDayEvents(gameHourFloat);

        _gameHourFloat = gameHourFloat;
    }

    private void UpdateTime(float gameHourFloat)
    {
        var hour = Mathf.FloorToInt(gameHourFloat);
        var minute = Mathf.FloorToInt((gameHourFloat - hour) * Data.GameMinsPerHour);

        if (_lastMinute != minute)        
            GameTime.Variable.Value = $"{hour:00}:{minute:00}";        

        _lastMinute = minute;
    }

    private void EvaluateTimeOfDayEvents(float gameHourFloat)
    {
        if (gameHourFloat > Data.DayStartHour && _gameHourFloat <= Data.DayStartHour)
        {
            OnDayStartedEvent?.Raise();
        }

        else if (gameHourFloat > Data.DayEndHour && _gameHourFloat <= Data.DayEndHour)
        {
            OnDayEndedEvent?.Raise();
        }
    }    
}
