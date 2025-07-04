using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private RectTransform fillImage;
    [SerializeField] private RectTransform damageFillImage;
    [SerializeField] private float trailSpeed = 10000f;
    public TextMeshProUGUI healthText;
    private float targetFill = 1f;
    private float maxWidth;

    private void Awake()
    {
        maxWidth = fillImage.sizeDelta.x;

        fillImage.pivot = new Vector2(0f, 0.5f);
        damageFillImage.pivot = new Vector2(0f, 0.5f);
    }

    public void SetHealth(float current, float max)
    {
        targetFill = current / max;

        Vector2 fillSize = fillImage.sizeDelta;
        fillSize.x = maxWidth * targetFill;
        fillImage.sizeDelta = fillSize;
        if(healthText != null) 
        {
            healthText.SetText(current + "/" + max);
        }
    }

    private void Update()
    {
        float targetWidth = fillImage.sizeDelta.x;
        Vector2 damageSize = damageFillImage.sizeDelta;

        if (damageSize.x > targetWidth)
        {
            damageSize.x -= trailSpeed * Time.deltaTime;

            if (damageSize.x < targetWidth)
            {
                damageSize.x = targetWidth;
            }

            damageFillImage.sizeDelta = damageSize;
        }
    }
}
