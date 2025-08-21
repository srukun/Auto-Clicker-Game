using UnityEngine;

public class HexagonVignetteGenerator : MonoBehaviour
{
    public GameObject hexPrefab;
    public float startX = -14f;
    public float hexWidth = 0.866f; // Width between columns
    public float hexHeight = 0.75f; // Height between rows
    public int columns = 33;

    void Start()
    {
        // TOP
        CreateRowHexagons(0f, 12f, 0.5f);
        CreateRowHexagons(hexWidth / 2f * -1, 12.75f, 0.75f);
        CreateRowHexagons(0f, 13.5f, 0.9f);
        CreateRowHexagons(hexWidth / 2f * -1, 14.25f, 1f);

        // BOTTOM
        CreateRowHexagons(0f, -12f, 0.5f);
        CreateRowHexagons(hexWidth / 2f * -1, -12.75f, 0.75f);
        CreateRowHexagons(0f, -13.5f, 0.9f);
        CreateRowHexagons(hexWidth / 2f * -1, -14.25f, 1f);

        // LEFT
        CreateColumnHexagons(-14f, 0f, 0.5f);
        CreateColumnHexagons(-14.433f, hexHeight / 2f, 0.75f);
        CreateColumnHexagons(-15f, 0f, 0.9f);
        CreateColumnHexagons(-15.433f, hexHeight / 2f, 1f);

        // RIGHT
        CreateColumnHexagons(14f, 0f, 0.5f);
        CreateColumnHexagons(14.433f, hexHeight / 2f, 0.75f);
        CreateColumnHexagons(15f, 0f, 0.9f);
        CreateColumnHexagons(15.433f, hexHeight / 2f, 1f);
    }

    void CreateRowHexagons(float xOffset, float y, float alpha)
    {
        Vector2 position = new Vector2(startX + xOffset, y);
        for (int i = 0; i < columns; i++)
        {
            GameObject hexagon = Instantiate(hexPrefab, position, Quaternion.identity);
            SetAlpha(hexagon, alpha);
            position.x += hexWidth;
        }
    }

    void CreateColumnHexagons(float x, float yOffset, float alpha)
    {
        Vector2 position = new Vector2(x, -14f + yOffset);
        for (int i = 0; i < columns; i++)
        {
            GameObject hexagon = Instantiate(hexPrefab, position, Quaternion.identity);
            SetAlpha(hexagon, alpha);
            position.y += hexHeight;
        }
    }

    void SetAlpha(GameObject obj, float a)
    {
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            Color c = sr.color;
            c.a = a;
            sr.color = c;
        }
    }
}
