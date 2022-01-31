using System;
using System.Linq;
using UnityEngine;

public class ContainerMono : MonoBehaviour, IContainer, IDraggable
{
    public Action<Vector3> OnContainerMoved { get; set; }
    public ContainerData Data;
    public ContainerSet Container;

    SpriteRenderer spriteRenderer;

    Vector3 _lastPosition;


    void Start()
    {
        Container.Items.Clear();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Data.Sprite;
        _lastPosition = transform.position;
    }

    void Update()
    {
        if (transform.hasChanged)
        {
            var delta = transform.position - _lastPosition;
            OnContainerMoved?.Invoke(delta);
            _lastPosition = transform.position;
            transform.hasChanged = false;
        }
    }

    public bool TryAdd(IContainable item)
    {
        if (Container.Items.Sum(x => x.WeightKg) > Data.CapacityKg)
            return false;

        Container.Add(item);
        item.SetSpriteRendererOrder(spriteRenderer.sortingOrder + 1);
        return true;
    }
    public bool TryRemove(IContainable item)
    {
        if (!Container.Items.Contains(item))
            return false;

        Container.Remove(item);
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
