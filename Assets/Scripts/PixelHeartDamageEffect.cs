using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelHeartDamageEffect : MonoBehaviour
{
    public GameObject whiteHeart;
    public float effectTimer;
    public GameObject SceneObject_WhiteHeart;
    void Start()
    {
        
    }

    
    void Update()
    {
        if(effectTimer > 0)
        {
            effectTimer -= Time.deltaTime;
        }
        if(effectTimer <= 0 && SceneObject_WhiteHeart != null)
        {
            Destroy(SceneObject_WhiteHeart);
        }
    }
    public void HeartDamageEffect()
    {
        if(effectTimer <= 0)
        {
            SceneObject_WhiteHeart = Instantiate(whiteHeart, transform.position, Quaternion.Euler(transform.rotation.x, transform.rotation.z, -2));
            effectTimer = (float)1 / DataManager.equipedKnife.fireRate;
        }
    }
}
