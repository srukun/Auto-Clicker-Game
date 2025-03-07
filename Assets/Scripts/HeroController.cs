using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    public Vector3 position;
    public Hero hero;

    private RoomNode currentRoom;
    public GameObject SceneObject_ArenaManager;


    void Start()
    {
        position = transform.position;

    }

    void Update()
    {
        Movement();

/*        if (Input.GetKeyDown(KeyCode.W) && currentRoom.top != null)
            MoveToRoom(currentRoom.top);
        if (Input.GetKeyDown(KeyCode.S) && currentRoom.bottom != null)
            MoveToRoom(currentRoom.bottom);
        if (Input.GetKeyDown(KeyCode.A) && currentRoom.left != null)
            MoveToRoom(currentRoom.left);
        if (Input.GetKeyDown(KeyCode.D) && currentRoom.right != null)
            MoveToRoom(currentRoom.right);*/

    }

    public void SetStartRoom(RoomNode startRoom)
    {
        currentRoom = startRoom;
        transform.position = RoomToWorldPosition(currentRoom.gridPosition);
    }

    private Vector3 RoomToWorldPosition(Vector2Int gridPos)
    {
        return new Vector3(gridPos.x * 5, gridPos.y * 5, 0);
    }

    private void MoveToRoom(RoomNode newRoom)
    {
        currentRoom = newRoom;
        transform.position = RoomToWorldPosition(currentRoom.gridPosition);
        Debug.Log("Moved to room: " + newRoom.gridPosition);
    }

    public void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float speed = 3.5f;
        Vector3 moveDirection = new Vector3(horizontal, vertical, 0).normalized;

        Vector3 newPosition = transform.position + moveDirection * speed * Time.deltaTime;

        float minX = -2f;
        float maxX = 2f;
        float minY = -3.5f;
        float maxY = 3.5f;

        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        transform.position = newPosition;
    }




}
