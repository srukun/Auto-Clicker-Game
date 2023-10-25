using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIButtonManager : MonoBehaviour
{
    void Start()
    {
        if (DataManager.knives.Count < 1)
        {
            KnifeClass bronzeKnife = new KnifeClass("Bronze Knife", 1, 0, 15f, true, 2);
            DataManager.knives.Add(bronzeKnife);
            DataManager.equipedKnife = bronzeKnife;

            KnifeClass ironKnife = new KnifeClass("Iron Knife", 2, 25, 17f, false, 2);
            DataManager.knives.Add(ironKnife);

            KnifeClass silverKnife = new KnifeClass("Silver Knife", 3, 100, 19f, false, 2);
            DataManager.knives.Add(silverKnife);

            KnifeClass goldKnife = new KnifeClass("Gold Knife", 4, 250, 25f, false, 3);
            DataManager.knives.Add(goldKnife);

            KnifeClass diamondKnife = new KnifeClass("Diamond Knife", 5, 500, 30f, false, 4);
            DataManager.knives.Add(diamondKnife);
            DataManager.totalGold = 25;
        }
    }

    void Update()
    {

    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadLevel()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadShop()
    {
        SceneManager.LoadScene(2);

    }
    public void LoadUpgrade()
    {
        SceneManager.LoadScene(3);

    }
    public void LoadFoodPedia()
    {
        SceneManager.LoadScene(4);

    }
}
