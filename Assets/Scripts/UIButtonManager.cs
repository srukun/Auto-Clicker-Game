using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIButtonManager : MonoBehaviour
{
    void Start()
    {

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
