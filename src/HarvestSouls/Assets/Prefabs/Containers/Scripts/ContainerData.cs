using UnityEngine;

[CreateAssetMenu(menuName = "Data/ContainerData")]
public class ContainerData : ScriptableObject
{
    public Sprite Sprite;
    public float CapacityKg;
    public Rect DragAnchor;
}
