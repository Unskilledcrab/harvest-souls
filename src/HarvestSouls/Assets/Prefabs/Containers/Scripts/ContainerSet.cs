using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Sets/Container")]
public class ContainerSet : RuntimeSet<IContainable>
{
    [SerializeField] List<ItemData> ContainedItems;

    private void OnValidate()
    {
        ContainedItems = Items.Select(x => x.GetData()).ToList();
    }

    public override void Add(IContainable item)
    {
        Debug.Log(item.GetData().name + " added");
        ContainedItems.Add(item.GetData());
        base.Add(item);
    }

    public override void Remove(IContainable item)
    {
        Debug.Log(item.GetData().name + " removed");
        ContainedItems.Remove(item.GetData());
        base.Remove(item);
    }
}