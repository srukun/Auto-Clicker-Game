using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelHeartExplosionController : MonoBehaviour
{
    public float lifetime;
    void Start()
    {
        lifetime = (float)13/60;
    }

    // Update is called once per frame
    void Update()
    {
        if(lifetime > 0)
        {
            lifetime -= Time.deltaTime;
        }
        if(lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
