using UnityEngine;

public class TimeEventListener : MonoBehaviour
{
    public TimeEvent Event;
    public TimeUnityEvent Response;

    void OnEnable() => Event.RegisterListener(this);
    void OnDisable() => Event.UnregisterListener(this);
    public void OnEventRaised(GameTime gameTime) => Response.Invoke(gameTime);
}