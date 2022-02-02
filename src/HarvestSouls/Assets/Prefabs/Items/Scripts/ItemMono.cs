using System;
using UnityEngine;

public class ItemMono : MonoBehaviour, IDraggable, IContainable
{
    [SerializeField] 
    ItemData Data;

    SpriteRenderer spriteRenderer;

    private void OnValidate()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer.sprite != Data.Sprite)        
            spriteRenderer.sprite = Data.Sprite;        

        name = Data.Name;
    }

    public Action OnRemoveItemFromContainer { get; set; }
    public Action OnAddingItem { get; set; }
    public float WeightKg => Data.WeightKg;
    public string Name => Data.Name;

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

    public void SetParentTransform(Transform parent)
    {
        transform.parent = parent;
    }

    public void ClearParentTransform()
    {
        transform.parent = null;
    }
}

