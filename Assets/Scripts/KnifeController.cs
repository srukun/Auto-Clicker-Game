using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeController : MonoBehaviour
{
    public GameObject SceneObject_Camera;
    public Transform aimTransform;
    public Vector3 mousePosition;
    public Vector3 aimDirection;

    //knives
    public GameObject equipedKnifePrefab;
    public GameObject Prefab_BronzeKnife;
    public GameObject Prefab_IronKnife;
    public GameObject Prefab_SilverKnife;
    public GameObject Prefab_GoldKnife;
    public GameObject Prefab_DiamondKnife;

    public float shootTimer;
    public SpriteRenderer knifeSprite;
    public Sprite ironKnife;
    public Sprite silverKnife;
    public Sprite goldKnife;
    public Sprite diamondKnife;

    GameObject closestEnemy;
    void Start()
    {
        if(DataManager.equipedKnife.knifeName == "Iron Knife")
        {
            knifeSprite.sprite = ironKnife;
        }
        else if (DataManager.equipedKnife.knifeName == "Silver Knife")
        {
            knifeSprite.sprite = silverKnife;
        }
        else if (DataManager.equipedKnife.knifeName == "Gold Knife")
        {
            knifeSprite.sprite = goldKnife;
        }
        else if (DataManager.equipedKnife.knifeName == "Diamond Knife")
        {
            knifeSprite.sprite = diamondKnife;
        }
        equipedKnifePrefab = Prefab_BronzeKnife;

    }

    
    void FixedUpdate()
    {
        Aim();
        Shoot();
    }
    public void Shoot()
    {
        if (shootTimer <= 0 && Input.GetButton("Fire1"))
        {
            GameObject SceneObject_KnifeProjectile = Instantiate(equipedKnifePrefab, transform.position, aimTransform.rotation);

            if (DataManager.equipedKnife.knifeName == "Iron Knife")
            {
                SceneObject_KnifeProjectile.GetComponent<SpriteRenderer>().sprite = ironKnife;
            }
            else if (DataManager.equipedKnife.knifeName == "Silver Knife")
            {
                SceneObject_KnifeProjectile.GetComponent<SpriteRenderer>().sprite = silverKnife;
            }
            else if (DataManager.equipedKnife.knifeName == "Gold Knife")
            {
                SceneObject_KnifeProjectile.GetComponent<SpriteRenderer>().sprite = goldKnife;
            }
            else if (DataManager.equipedKnife.knifeName == "Diamond Knife")
            {
                SceneObject_KnifeProjectile.GetComponent<SpriteRenderer>().sprite = diamondKnife;
            }


            shootTimer = 0.33f;
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
