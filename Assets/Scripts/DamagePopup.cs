using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    public float floatSpeed = 1f;
    public float lifetime = 1f;
    public Vector3 moveDirection = new Vector3(0f, 1f, 0f);

    public float timer;
    public TextMeshProUGUI text;
    public TextMeshProUGUI outlineText;

    public void Setup(int damage)
    {
        if (text == null)
            text = GetComponentInChildren<TextMeshProUGUI>();

        text.text = damage.ToString();
        outlineText.text = damage.ToString();


        timer = lifetime;
    }

    private void Update()
    {
        transform.position += moveDirection * floatSpeed * Time.deltaTime;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
