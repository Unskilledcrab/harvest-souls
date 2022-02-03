using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Items/Equipment")]
public class EquipmentObject : ItemObject
{
    private void Awake()
    {
        Type = ItemType.Equipment;
    }
}
