using UnityEngine;
using TMPro;
using TMPro.EditorUtilities;

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

        /*text.extraPadding = true;
        text.fontMaterial = new Material(text.fontMaterial);
        text.fontMaterial.SetFloat(ShaderUtilities.ID_OutlineWidth, 0.2f);
        text.fontMaterial.SetColor(ShaderUtilities.ID_OutlineColor, Color.black);*/

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
