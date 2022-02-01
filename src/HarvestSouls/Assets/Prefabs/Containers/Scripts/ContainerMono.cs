using System;
using System.Linq;
using UnityEngine;

public class ContainerMono : MonoBehaviour, IContainer, IDraggable
{
    [SerializeField] ContainerData Data;
    [SerializeField] ContainerSet Container;

    SpriteRenderer spriteRenderer;

    void OnValidate()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer.sprite != Data.Sprite)
            spriteRenderer.sprite = Data.Sprite;
    }

    void Start()
    {
        Container.Items.Clear();
    }

    public bool TryAdd(IContainable item)
    {
        if (Container.Items.Sum(x => x.WeightKg) > Data.CapacityKg)
            return false;
        Container.Add(item);
        item.SetParentTransform(transform);
        item.SetSpriteRendererOrder(spriteRenderer.sortingOrder + 1);
        return true;
    }
    public bool TryRemove(IContainable item)
    {
        if (!Container.Items.Contains(item))
            return false;
        Container.Remove(item);
        item.ClearParentTransform();
        item.SetSpriteRendererOrder(0);
        return true;
    }

    public void onStartDrag()
    {
    }

    public void onEndDrag()
    {
        
    }

    public bool InDragAnchor(Vector2 clickedPoint) => Data.DragAnchor.Contains(clickedPoint);

}
