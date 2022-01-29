using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTracker : MonoBehaviour
{
    public GameEvent OnNewDayStartedEvent;
    public int SecondsPerDay;
    float prevTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        var time = Time.time;

        if (time % SecondsPerDay < prevTime % SecondsPerDay)
        {
            Debug.Log("OnNewDayStartedEvent Game event raised");
            OnNewDayStartedEvent.Raise();
        }

        prevTime = time;
    }
}
