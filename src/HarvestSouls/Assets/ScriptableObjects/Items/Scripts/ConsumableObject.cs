using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Stack", menuName = "Items/Consumable")]
public class ConsumableObject : ItemObject
{
    [SerializeField]
    public int HealthAffect;

    [SerializeField]
    public int ManaAffect;

    [SerializeField]
    public int DurationSeconds;

    private void Awake()
    {
        Type = ItemType.Consumable;
        Stackable = true;
    }
}

