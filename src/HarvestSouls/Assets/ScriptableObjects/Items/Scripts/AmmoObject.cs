using UnityEngine;

[CreateAssetMenu(fileName = "New Ammo Stack", menuName = "Items/Ammo")]
public class AmmoObject : ItemObject
{
    private void Awake()
    {
        Type = ItemType.Ammo;
        Stackable = true;
    }
}
