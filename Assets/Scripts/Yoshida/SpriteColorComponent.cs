using UnityEngine;

public class SpriteColorComponent : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private float flashInterval = 1.5f;

    bool isLight = true;
    float time = 0.0f;
    void Update()
    {
        if (time >= flashInterval)
        {
            if (isLight)
            {
                isLight = false;
            }
            else
            {
                isLight = true;
            }
            time = 0.0f;
        }

        UpdateSprite(time / flashInterval);
        time += Time.deltaTime;
    }

    private void UpdateSprite(float percent)
    {
        Color color = sprite.color;

        if (isLight)
        {
            color.a = percent;
        }
        else
        {
            color.a = 1.0f - percent;
        }

        sprite.color = color;
    }
}
