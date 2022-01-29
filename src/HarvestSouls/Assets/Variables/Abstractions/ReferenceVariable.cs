using UnityEngine;

public class ReferenceVariable<T> : ScriptableObject
{
    public T Value { get; set; }
}