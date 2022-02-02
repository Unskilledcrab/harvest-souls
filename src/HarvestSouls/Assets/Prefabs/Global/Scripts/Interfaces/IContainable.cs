using System;
using UnityEngine;

public interface IContainable
{
    public string Name { get; }
    public float WeightKg { get; }
    ItemData GetData();
    Action OnRemoveItemFromContainer { get; set; }
    Action OnAddingItem { get; set; }
    void SetSpriteRendererOrder(int order);
    void SetParentTransform(Transform parent);
    void ClearParentTransform();
}
