using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class KnifeSceneManager : MonoBehaviour
{
    public KnifeClass thisKnife;
    public List<KnifeClass> knives;

    //scene texts
    public TextMeshProUGUI knifeName;
    public TextMeshProUGUI informationText;
    public TextMeshProUGUI goldText;

    public Sprite bronzeKnifeSprite;
    public Sprite ironKnifeSprite;
    public Sprite silverKnifeSprite;
    public Sprite goldKnifeSprite;
    public Sprite diamondKnifeSprite;
    public Image knifeImage;

    public GameObject goLeftButton;
    public GameObject goRightButton;
    //management
    
    void Start()
    {
        thisKnife = DataManager.equipedKnife;
        knives = DataManager.knives;
        
        SetTexts();
        SetKnifeImage();
        HideLeftButton();
        HideRightButton();
    }

    void Update()
    {
        
    }
    public void LevelUpKnife()
    {
        if(thisKnife.levelUpCost <= DataManager.totalGold)
        {
            thisKnife.LevelUp();
            DataManager.totalGold -= thisKnife.levelUpCost;
            SetTexts();
        }


    }
    public void SetTexts()
    {
        knifeName.SetText(thisKnife.knifeName);
        informationText.SetText("Level: \t\t"+ thisKnife.level + "\r\nDamage: \t" + thisKnife.damage + 
            "\r\nSpeed:\t"+ thisKnife.speed + "\r\nFirerate: " + thisKnife.fireRate + "/sec");
        goldText.SetText(DataManager.totalGold + "");
    }
    public void GoLeft()
    {
        if (!goRightButton.activeInHierarchy)
        {
            goRightButton.SetActive(true);
        }

        for(int i = GetIndexOfEquipedKnife(); i >= 0; i--)
        {
            if (knives[i].isBought && thisKnife.knifeName != knives[i].knifeName)
            {
                thisKnife = knives[i];
                SetTexts();
                SetKnifeImage();
                break;
            }
        }
        goLeftButton.SetActive(false);
    }
    public void HideLeftButton()
    {
        for (int i = GetIndexOfEquipedKnife(); i >= 0; i--)
        {
            if (!knives[i].isBought && thisKnife.knifeName != knives[i].knifeName)
            {
                goLeftButton.SetActive(false);
            }
            else if(knives[i].isBought && thisKnife.knifeName != knives[i].knifeName)
            {
                goLeftButton.SetActive(true);
            }
        }
    }
    public void HideRightButton()
    {
        for (int i = GetIndexOfEquipedKnife(); i < knives.Count; i++)
        {
            if (!knives[i].isBought && thisKnife.knifeName != knives[i].knifeName)
            {
                goRightButton.SetActive(false);
            }
            else if(knives[i].isBought && thisKnife.knifeName != knives[i].knifeName)
            {
                goRightButton.SetActive(true);
            }
        }
    }
    public void GoRight()
    {
        if (!goLeftButton.activeInHierarchy)
        {
            goLeftButton.SetActive(true);
        }
        for (int i = GetIndexOfEquipedKnife(); i < knives.Count; i++)
        {
            if (knives[i].isBought && thisKnife.knifeName != knives[i].knifeName)
            {
                thisKnife = knives[i];
                SetTexts();
                SetKnifeImage();
                break;
            }
        }
        goRightButton.SetActive(false);
    }
    public int GetIndexOfEquipedKnife()
    {
        
        for (int i = 0; i < knives.Count; i++)
        {
            if (knives[i].knifeName == thisKnife.knifeName)
            {
                return i;
            }
        }
        return 0;
    }
    public void SetKnifeImage()
    {
        if(thisKnife.knifeName == "Bronze Knife")
        {
            knifeImage.sprite = bronzeKnifeSprite;
        }
        else if (thisKnife.knifeName == "Iron Knife")
        {
            knifeImage.sprite = ironKnifeSprite;
        }
        else if(thisKnife.knifeName == "Silver Knife")
        {
            knifeImage.sprite = silverKnifeSprite;
        }
        else if(thisKnife.knifeName == "Gold Knife")
        {
            knifeImage.sprite = goldKnifeSprite;
        }
        else if(thisKnife.knifeName == "Diamond Knife")
        {
            knifeImage.sprite = diamondKnifeSprite;
        }
    }
}
