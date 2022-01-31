using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Sets/Container")]
public class ContainerSet : RuntimeSet<IContainable>
{
    public override void Add(IContainable item)
    {
        Debug.Log(item.GetData().name + " added");
        base.Add(item);
    }

    public override void Remove(IContainable item)
    {
        Debug.Log(item.GetData().name + " removed");
        base.Remove(item);
    }
}