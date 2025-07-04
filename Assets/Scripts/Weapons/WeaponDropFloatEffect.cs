using UnityEngine;

public class WeaponDropFloatEffect : MonoBehaviour
{
    private bool goingUp = true;
    private float floatSpeed = 0.5f;
    private float maxHeight = 0.5f;
    private float minHeight = 0f;
    private float localZ = -4f;

    void LateUpdate()
    {
        float deltaY = floatSpeed * Time.deltaTime;
        Vector3 pos = transform.localPosition;

        if (goingUp)
        {
            pos.y += deltaY;
            if (pos.y >= maxHeight) goingUp = false;
        }
        else
        {
            pos.y -= deltaY;
            if (pos.y <= minHeight) goingUp = true;
        }

        pos.z = localZ;
        transform.localPosition = pos;
    }
}
