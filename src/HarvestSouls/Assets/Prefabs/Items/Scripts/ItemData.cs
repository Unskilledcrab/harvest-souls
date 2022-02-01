using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/ItemData")]
public class ItemData : ScriptableObject
{
    public string Name;
    public Sprite Sprite;
    public float WeightKg;
}