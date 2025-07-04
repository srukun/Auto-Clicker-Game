using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDropManager : MonoBehaviour
{
    public string weaponName;

    public GameObject player;
    public GameObject equipText;
    public Sprite[] weaponSprites;
    public GameObject[] weaponPrefabs;
    public SpriteRenderer sr;

    void Start()
    {
        SetSprite();
    }

    void Update()
    {


    }
    void DisplayEquipText()
    {
        if(!equipText.activeInHierarchy)
        {
            equipText.SetActive(true);
        }
        else
        {
            equipText.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hero"))
        {
            other.GetComponent<PlayerInteraction>().SetNearbyWeapon(gameObject);
            player = other.gameObject;
            DisplayEquipText();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Hero"))
        {
            other.GetComponent<PlayerInteraction>().ClearNearbyWeapon(gameObject);
            DisplayEquipText();
        }
    }
    private void SetSprite()
    {

        if (sr == null) return;

        if (weaponName == "Guardians Sword")
        {
            sr.sprite = weaponSprites[0];
        }
        else if (weaponName == "Sheepskin Bow")
        {
            sr.sprite = weaponSprites[1];
        }
        else if (weaponName == "Steels Edge")
        {
            sr.sprite = weaponSprites[2];
        }
    }
}
