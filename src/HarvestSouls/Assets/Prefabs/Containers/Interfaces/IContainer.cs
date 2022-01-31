
using System;
using UnityEngine;

public interface IContainer
{
    bool TryAdd(IContainable item);
    bool TryRemove(IContainable item);

    Action<Vector3> OnContainerMoved { get; set; }
}
