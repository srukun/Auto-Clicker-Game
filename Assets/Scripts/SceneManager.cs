using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    //Gold Management
    public int goldEarned;
    public GameObject Text_GoldEarned;


    //Spawn Management
    public GameObject canvasObject;


    public GameObject heroGameObject;
    //Skins
    public GameObject[] skins;

    //Scene
    public GameObject sceneCamera;
    public GameObject mapManagerObject;
    public RoomManager roomManager;
    public ScreenTransitionManager transitionManager;

    //Minimap
    public GameObject mapObject;
    public GameObject mapNodeObject;
    public GameObject mapPlayerPoint;
    //Map
    public int[,] levelMap = new int[,]
    {
        {0, 0, 0, 3, 0},
        {0, 0, 2, 2, 0},
        {0, 0, 2, 0, 0},
        {0, 0, 2, 0, 0},
        {0, 2, 1, 0, 0}
    };
    public Vector2Int currentPosition;
    public Dictionary<Vector2Int, bool> clearedRooms = new Dictionary<Vector2Int, bool>();

    void Start()
    {
        SpawnHero();
        AssignPosition();

        roomManager.RoomSetUp();
        Physics.IgnoreLayerCollision(6, 7);
        Physics.IgnoreLayerCollision(6, 8);
        Physics.IgnoreLayerCollision(7, 9);
        Physics.IgnoreLayerCollision(8, 9);

    }


    void Update()
    {

    }
    public HeroController GetHeroController()
    {
        HeroController heroController = heroGameObject.GetComponent<HeroController>();
        return heroController;
    }

    public void SpawnHero()
    {
        GameObject heroObject = Instantiate(skins[0], new Vector3(0, 0, -2), Quaternion.identity);
        heroObject.GetComponentInChildren<KnifeController>().SceneObject_Camera = sceneCamera;
        this.heroGameObject = heroObject;
        sceneCamera.transform.SetParent(heroObject.transform, false );
    }
    public void IncreaseEarnedGold(int gold)
    {
        goldEarned += gold;
        DataManager.totalGold += gold;
        Text_GoldEarned.GetComponent<TextMeshProUGUI>().SetText(goldEarned + "");
    }
    public void HideMinimap()
    {
        mapObject.SetActive(false);

        for (int i = mapObject.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(mapObject.transform.GetChild(i).gameObject);
        }
    }
    public void ViewMap()
    {
        if (mapObject.activeInHierarchy)
        {
            HideMinimap();
        }
        else
        {
            mapObject.SetActive(true);
        }

        int rows = levelMap.GetLength(0);
        int cols = levelMap.GetLength(1);

        Vector3 startPosition = new Vector3(-0.8f, 0.8f, -10);
        Vector3 position = startPosition;

        for (int i = 0; i < rows; i++) 
        {
            for (int j = 0; j < cols; j++) 
            {
                if (levelMap[i, j] != 0)
                {

                    if (i == currentPosition.x &&  j == currentPosition.y)
                    {
                        GameObject mapNode = Instantiate(mapPlayerPoint, position, Quaternion.identity);
                        mapNode.transform.SetParent(mapObject.transform, true);
                    }
                    else
                    {
                        GameObject mapNode = Instantiate(mapNodeObject, position, Quaternion.identity);
                        mapNode.transform.SetParent(mapObject.transform, true);
                    }
                }
                position.x += 0.4f; 
            }

            position.x = startPosition.x;
            position.y -= 0.4f;
        }
    }

    public void AssignPosition()
    {
        for(int i = 0; i < levelMap.GetLength(0); i++)
        {
            for(int j = 0;  j < levelMap.GetLength(1); j++)
            {
                if (levelMap[i, j] == 1)
                {
                    currentPosition.x = i;
                    currentPosition.y = j;
                }

            }
        }
    }
    public void MarkRoomAsCleared(Vector2Int roomPosition)
    {
        if (clearedRooms.ContainsKey(roomPosition))
        {
            clearedRooms[roomPosition] = true;
        }
    }

    public bool IsRoomCleared(Vector2Int roomPosition)
    {
        return clearedRooms.ContainsKey(roomPosition) && clearedRooms[roomPosition];
    }


}
