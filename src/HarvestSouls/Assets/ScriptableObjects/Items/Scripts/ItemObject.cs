using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Default,
    Equipment,
    Consumable,
    Ammo
}

public abstract class ItemObject : ScriptableObject
{
    public int Id;

    public Sprite Icon;

    public ItemType Type;

    [TextArea(15, 20)]
    public string Description;

    public float WeightKg;

    public bool Stackable;       
}

[System.Serializable]
public class Item
{
    public string Name;
    public int Id;
    public Item(ItemObject item)
    {
        Name = item.name;
        Id = item.Id;
    }
}