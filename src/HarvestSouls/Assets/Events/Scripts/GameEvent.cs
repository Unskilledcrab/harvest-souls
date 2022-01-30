using UnityEngine;

[CreateAssetMenu(menuName = "Event/GameEvent"), System.Serializable]
public class GameEvent : BaseGameEvent<GameEventListener>
{
    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised();
    }
}