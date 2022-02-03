using UnityEngine;

[CreateAssetMenu(fileName = "New Default Object", menuName = "Items/Default")]    
public class DefaultObject : ItemObject
{
    public void Awake()
    {
        Type = ItemType.Default;
    }
}

