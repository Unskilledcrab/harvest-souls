using UnityEngine;

[CreateAssetMenu]
public class ReferenceVariable<T> : ScriptableObject
{
    public T Value { get; set; }
}