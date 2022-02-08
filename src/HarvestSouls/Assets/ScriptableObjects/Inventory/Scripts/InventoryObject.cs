using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public string savePath;
    public ItemDatabaseObject database;
    public Inventory Container;

    public void AddItem(Item _item, int amount = 1)
    {
        var dbItem = database.GetItem[_item.Id];
        if (dbItem.Stackable)
        {
            var invItem = Container.Items.FirstOrDefault(x => x.id == _item.Id);

            if (invItem == null)
                Container.Items.Add(new InventoryItem(_item.Id, _item, amount));
            else
                invItem.AddAmount(amount);
        }
        else        
            for (int i = 0; i < amount; i++)
                Container.Items.Add(new InventoryItem(_item.Id, _item, 1));        
    }

    [ContextMenu("Save")]
    public void Save()
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, Container);
        stream.Close();
    }

    [ContextMenu("Load")]
    public void Load()
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
        Container = (Inventory)formatter.Deserialize(stream);
        stream.Close();
    }

    [ContextMenu("Clear")]
    public void Clear()
    {
        Container = new Inventory();
    }
}

[System.Serializable]
public class Inventory
{
    public List<InventoryItem> Items = new List<InventoryItem>();
}

[System.Serializable]
public class InventoryItem
{
    public int id;
    public Item item;
    public int amount;
    public (float, float, float)? position;

    public InventoryItem(int _id, Item _item, int _amount)
    {
        id = _id;
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }
}
