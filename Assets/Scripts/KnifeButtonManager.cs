using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class KnifeButtonManager : MonoBehaviour
{
    public List<KnifeClass> knives;
    public GameObject[] buyButtons;
    public GameObject[] equipButtons;
    public GameObject CanvasObj_GoldText;
    void Start()
    {
        knives = DataManager.knives;
        UpdateBuyButtons();
        UpdateEquipButtons();
    }

    
    void Update()
    {
        
    }
    public void BuyIronKnife()
    {
        if (!knives[1].isBought && DataManager.totalGold >= knives[1].cost)
        {
            DataManager.totalGold -= knives[1].cost;
            knives[1].isBought = true;
            UpdateBuyButtons();
            UpdateEquipButtons();
        }
    }
    public void BuySilverKnife()
    {
        if (!knives[2].isBought && DataManager.totalGold >= knives[2].cost)
        {
            DataManager.totalGold -= knives[2].cost;
            knives[2].isBought = true;
            UpdateBuyButtons();
            UpdateEquipButtons();
        }
    }
    public void BuyGoldKnife()
    {
        if (!knives[3].isBought && DataManager.totalGold >= knives[3].cost)
        {
            DataManager.totalGold -= knives[3].cost;
            knives[3].isBought = true;
            UpdateBuyButtons();
            UpdateEquipButtons();
        }
    }
    public void BuyDiamondKnife()
    {
        if (!knives[4].isBought && DataManager.totalGold >= knives[4].cost)
        {
            DataManager.totalGold -= knives[4].cost;
            knives[4].isBought = true;
            UpdateBuyButtons();
            UpdateEquipButtons();
        }
    }

    public void EquipBronzeKnife()
    {
        if (knives[0].knifeName != DataManager.equipedKnife.knifeName)
        {
            DataManager.equipedKnife = knives[0];
        }
        UpdateBuyButtons();
        UpdateEquipButtons();
    }
    public void EquipIronKnife()
    {
        if (knives[1].knifeName != DataManager.equipedKnife.knifeName)
        {
            DataManager.equipedKnife = knives[1];
        }
        UpdateBuyButtons();
        UpdateEquipButtons();
    }
    public void EquipSilverKnife()
    {
        if (knives[2].knifeName != DataManager.equipedKnife.knifeName)
        {
            DataManager.equipedKnife = knives[2];
        }
        UpdateBuyButtons();
        UpdateEquipButtons();
    }
    public void EquipGoldKnife()
    {
        if (knives[3].knifeName != DataManager.equipedKnife.knifeName)
        {
            DataManager.equipedKnife = knives[3];
        }
        UpdateBuyButtons();
        UpdateEquipButtons();
    }
    public void EquipDiamondKnife()
    {
        if (knives[4].knifeName != DataManager.equipedKnife.knifeName)
        {
            DataManager.equipedKnife = knives[4];
        }
        UpdateBuyButtons();
        UpdateEquipButtons();
    }
    public void UpdateBuyButtons()
    {
        for(int i = 0; i < buyButtons.Length; i++)
        {
            if (!knives[i + 1].isBought)
            {
                buyButtons[i].SetActive(true);
            }
            else
            {
                buyButtons[i].SetActive(false);
            }
        }
        CanvasObj_GoldText.GetComponent<TextMeshProUGUI>().SetText(DataManager.totalGold + "");

    }
    public void UpdateEquipButtons()
    {
        
        if (DataManager.equipedKnife.knifeName == "Bronze Knife")
        {
            equipButtons[0].GetComponentInChildren<TextMeshProUGUI>().SetText("Equiped");
            equipButtons[0].GetComponent<Image>().color = new Color(0.502f, 0.765f, 0.518f, 1.0f);
        }
        else
        {
            equipButtons[0].GetComponentInChildren<TextMeshProUGUI>().SetText("Equip");
            equipButtons[0].GetComponent<Image>().color = new Color(0.749f, 0.961f, 0.322f, 1.0f);
        }

        if (DataManager.equipedKnife.knifeName == "Iron Knife")
        {
            equipButtons[1].GetComponentInChildren<TextMeshProUGUI>().SetText("Equiped");
            equipButtons[1].GetComponent<Image>().color = new Color(0.502f, 0.765f, 0.518f, 1.0f);
        }
        else
        {
            equipButtons[1].GetComponentInChildren<TextMeshProUGUI>().SetText("Equip");
            equipButtons[1].GetComponent<Image>().color = new Color(0.749f, 0.961f, 0.322f, 1.0f);
        }

        if (DataManager.equipedKnife.knifeName == "Silver Knife")
        {
            equipButtons[2].GetComponentInChildren<TextMeshProUGUI>().SetText("Equiped");
            equipButtons[2].GetComponent<Image>().color = new Color(0.502f, 0.765f, 0.518f, 1.0f);
        }
        else
        {
            equipButtons[2].GetComponentInChildren<TextMeshProUGUI>().SetText("Equip");
            equipButtons[2].GetComponent<Image>().color = new Color(0.749f, 0.961f, 0.322f, 1.0f);
        }

        if (DataManager.equipedKnife.knifeName == "Gold Knife")
        {
            equipButtons[3].GetComponentInChildren<TextMeshProUGUI>().SetText("Equiped");
            equipButtons[3].GetComponent<Image>().color = new Color(0.502f, 0.765f, 0.518f, 1.0f);
        }
        else
        {
            equipButtons[3].GetComponentInChildren<TextMeshProUGUI>().SetText("Equip");
            equipButtons[3].GetComponent<Image>().color = new Color(0.749f, 0.961f, 0.322f, 1.0f);
        }

        if (DataManager.equipedKnife.knifeName == "Diamond Knife")
        {
            equipButtons[4].GetComponentInChildren<TextMeshProUGUI>().SetText("Equiped");
            equipButtons[4].GetComponent<Image>().color = new Color(0.502f, 0.765f, 0.518f, 1.0f);
        }
        else
        {
            equipButtons[4].GetComponentInChildren<TextMeshProUGUI>().SetText("Equip");
            equipButtons[4].GetComponent<Image>().color = new Color(0.749f, 0.961f, 0.322f, 1.0f);
        }
    }
}
