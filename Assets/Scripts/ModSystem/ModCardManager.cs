using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModCardManager : MonoBehaviour
{
    public ModCard card;
    public GameObject equipText;
    public GameObject modCardUI;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hero"))
        {
            if (!equipText.activeInHierarchy)
            {
                equipText.SetActive(true);
            }
            collision.GetComponentInChildren<HUDModCardManager>().currentCard = card;
            Debug.Log(collision.GetComponentInChildren<HUDModCardManager>().currentCard.modCardName);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Hero"))
        {
            if (equipText.activeInHierarchy)
            {
                equipText.SetActive(false);
            }
            collision.GetComponentInChildren<HUDModCardManager>().currentCard = null;
        }
    }
    void OnMouseEnter()
    {
        if (!modCardUI.activeInHierarchy)
        {
            modCardUI.SetActive(true);
        }
        Vector3 pos = equipText.transform.position;
        pos.y -= 2.5f;
        equipText.transform.position = pos;
    }
    void OnMouseExit()
    {
        if (modCardUI.activeInHierarchy)
        {
            modCardUI.SetActive(false);
        }
        Vector3 pos = equipText.transform.position;
        pos.y += 2.5f;
        equipText.transform.position = pos;
    }
}
