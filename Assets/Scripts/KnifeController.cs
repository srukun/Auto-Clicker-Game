using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeController : MonoBehaviour
{
    public GameObject SceneObject_Camera;
    public Transform aimTransform;
    public Vector3 mousePosition;
    public Vector3 aimDirection;
    public GameObject equipedKnifePrefab;
    public GameObject Prefab_BronzeKnife;
    public GameObject Prefab_IronKnife;
    public GameObject Prefab_SilverKnife;
    public GameObject Prefab_GoldKnife;
    public GameObject Prefab_DiamondKnife;

    public float shootTimer;
    void Start()
    {
        equipedKnifePrefab = Prefab_BronzeKnife;
        KnifeClass bronzeKnife = new KnifeClass("Bronze Knife", 1, 0, 15f, true, 2);
        DataManager.knives.Add(bronzeKnife);
        DataManager.equipedKnife = bronzeKnife;
    }

    
    void FixedUpdate()
    {
        Aim();
        Shoot();
    }
    public void Shoot()
    {
        if (Input.GetButton("Fire1") && shootTimer <= 0) 
        {
            GameObject SceneObject_KnifeProjectile = Instantiate(equipedKnifePrefab, transform.position, aimTransform.rotation);
            shootTimer = (float)1 / DataManager.equipedKnife.fireRate;

            SceneObject_KnifeProjectile.GetComponent<Rigidbody2D>().AddForce(SceneObject_KnifeProjectile.transform.up * DataManager.equipedKnife.speed, ForceMode2D.Impulse);
            SceneObject_KnifeProjectile.GetComponent<KnifeProjectileScript>().thisKnife = DataManager.equipedKnife;
        }
        if(shootTimer > 0)
        {
            shootTimer -= Time.deltaTime;
        }

    }
    public void Aim()
    {
        mousePosition = SceneObject_Camera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        aimDirection = (mousePosition - aimTransform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle - 90);
    }
}
