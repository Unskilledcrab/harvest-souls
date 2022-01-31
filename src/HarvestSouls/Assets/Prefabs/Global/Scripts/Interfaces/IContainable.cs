using System;
using UnityEngine;

public interface IContainable
{
    public float WeightKg { get; set; }
    ItemData GetData();
    Action OnRemoveItemFromContainer { get; set; }
    Action OnAddingItem { get; set; }
    void ContainerMoved(Vector3 vector);
    void SetSpriteRendererOrder(int order);
}
