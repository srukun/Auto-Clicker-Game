using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class HUDModCardManager : MonoBehaviour
{
    public ModCard[] cards;
    public ModCard nearbyCard;
    public GameObject[] modSlots;
    public GameObject[] modSlotsBackground;
    public GameObject parentObj;
    void Start()
    {
        cards = new ModCard[3];
        cards[0] = null;
        cards[1] = null;
        cards[2] = null;
        nearbyCard = null;
    }

    void Update()
    {
        
    }
    public void UpdateIcons()
    {

    }
    public void RemoveFirstCard()
    {

    }
    public void RemoveSecondCard()
    {

    }
    public void RemoveThirdCard()
    {

    }
    public void EquipCard()
    {
        int emptyIndex = -1;

        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i] == null)
            {
                emptyIndex = i;
                break;
            }
        }

        if (emptyIndex != -1)
        {
            cards[emptyIndex] = nearbyCard;
            modSlots[emptyIndex].SetActive(true);
            Sprite newSprite = Resources.Load<Sprite>("ModIcons/" + nearbyCard.cardIcon);
            modSlots[emptyIndex].GetComponent<Image>().sprite = newSprite;
            Destroy(parentObj);
            nearbyCard = null;
        }
        else
        {
            Debug.LogWarning("No empty card slots available!");
        }



    }
}
