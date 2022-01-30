using UnityEngine;

public class PlantMono : MonoBehaviour
{
    public PlantData Data;
    SpriteRenderer spriteRenderer;

    int Age = 0;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetSprite();
    }

    public void IncreaseAge()
    {
        Age++;
        SetSprite();
    }

    private void SetSprite()
    {
        if (Age >= Data.WiltAge)
            spriteRenderer.sprite = Data.WiltedSprite;

        else if (Age >= Data.Stage3Age)
            spriteRenderer.sprite = Data.Stage3Sprite;

        else if (Age >= Data.Stage2Age)
            spriteRenderer.sprite = Data.Stage2Sprite;

        else if (Age >= Data.Stage1Age)
            spriteRenderer.sprite = Data.Stage1Sprite;

        else
            spriteRenderer.sprite = Data.Stage0Sprite;
    }

}
