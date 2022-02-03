using UnityEngine;

public class GroundItem : MonoBehaviour, IDraggable
{
    public ItemObject item;

    private void OnValidate()
    {
        GetComponent<SpriteRenderer>().sprite = item.Icon;
        name = item.name;
    }

    private void Awake()
    {
    }

    public bool InDragAnchor(Vector2 vector2)
    {
        return true;
    }

    public void onEndDrag()
    {
        //spriteRenderer.sortingOrder = 0;
    }

    public void onStartDrag()
    {
        //spriteRenderer.sortingOrder = 1;
    }
}
