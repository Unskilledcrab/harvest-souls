using UnityEngine;

[CreateAssetMenu(menuName = "Event/TimeEvent"), System.Serializable]
public class TimeEvent : BaseGameEvent<TimeEventListener>
{
    public void Raise(GameTime gameTime)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(gameTime);
    }
}