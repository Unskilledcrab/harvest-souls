using UnityEngine;

[CreateAssetMenu(menuName = "Data/PlantData")]
public class PlantData : ScriptableObject
{
    public Sprite Stage0Sprite;
    public Sprite Stage1Sprite;
    public Sprite Stage2Sprite;
    public Sprite Stage3Sprite;
    public Sprite WiltedSprite;

    public int Stage1Age;
    public int Stage2Age;
    public int Stage3Age;
    public int WiltAge;
}
