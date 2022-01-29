using UnityEngine;

public class Growth : MonoBehaviour
{
    PlantMono plantMono;
    SpriteRenderer spriteRenderer;

    int Age = 0;

    void Start()
    {
        plantMono = GetComponent<PlantMono>();
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
        if (Age >= plantMono.Data.WiltAge)
            spriteRenderer.sprite = plantMono.Data.WiltedSprite;

        else if (Age >= plantMono.Data.Stage3Age)
            spriteRenderer.sprite = plantMono.Data.Stage3Sprite;

        else if (Age >= plantMono.Data.Stage2Age)
            spriteRenderer.sprite = plantMono.Data.Stage2Sprite;

        else if (Age >= plantMono.Data.Stage1Age)
            spriteRenderer.sprite = plantMono.Data.Stage1Sprite;

        else
            spriteRenderer.sprite = plantMono.Data.Stage0Sprite;
    }
}
