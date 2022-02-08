using UnityEngine;

public class GroundItem : MonoBehaviour, IDraggable
{
    public ItemDatabaseObject database;
    public Item item;

    private void OnValidate()
    {
        var dbItem = database.GetItem[item.Id];
        GetComponent<SpriteRenderer>().sprite = dbItem.Icon;
        name = dbItem.name;
    }

    public void Validate() => OnValidate();

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
