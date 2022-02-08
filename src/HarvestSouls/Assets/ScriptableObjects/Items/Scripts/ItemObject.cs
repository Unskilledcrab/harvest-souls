using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Default,
    Equipment,
    Consumable,
    Ammo
}

public enum Attributes
{
    Agility,
    Intellect,
    Stamina,
    Strength
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

    public Item CreateItem()
    {
        return new Item(this);
    }
}

[System.Serializable]
public class Item
{
    public string Name;
    public int Id;
    public ItemBuff[] buffs;
    public Item(ItemObject item)
    {
        Name = item.name;
        Id = item.Id;
        if(item is EquipmentObject)
        {
            EquipmentObject equipmentItem = (EquipmentObject)item;
            buffs = new ItemBuff[equipmentItem.buffs.Length];
            for (int i = 0; i < buffs.Length; i++)
            {
                buffs[i] = new ItemBuff(equipmentItem.buffs[i].min, equipmentItem.buffs[i].max)
                {
                    attribute = equipmentItem.buffs[i].attribute
                };
            }
        }
    }
}

[System.Serializable]
public class ItemBuff
{
    public Attributes attribute;
    public int value, min, max;
    public ItemBuff(int _min, int _max)
    {
        min = _min;
        max = _max;
        GenerateValue();
    }

    public void GenerateValue()
    {
        value = UnityEngine.Random.Range(min, max);
    }
}