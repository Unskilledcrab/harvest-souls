using System;
using UnityEngine;

public class ItemMono : MonoBehaviour, IDraggable, IContainable
{
    public ItemData Data;
    SpriteRenderer spriteRenderer;

    public Action OnRemoveItemFromContainer { get; set; }
    public Action OnAddingItem { get; set; }
    public float WeightKg { get => Data.WeightKg; set => Data.WeightKg = value; }

    public void ContainerMoved(Vector3 delta)
    {
        transform.position += delta;
    }

    public ItemData GetData() => Data;

    public bool InDragAnchor(Vector2 vector2) => true;

    public void onEndDrag()
    {
        spriteRenderer.sortingOrder = 0;
    }

    public void onStartDrag()
    {
        spriteRenderer.sortingOrder = 1;
    }

    public void SetSpriteRendererOrder(int order)
    {
        spriteRenderer.sortingOrder = order;
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Data.Sprite;
    }

}

