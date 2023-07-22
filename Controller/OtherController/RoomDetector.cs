using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class RoomDetector : MonoBehaviour
{
    private PlayerObject instance = PlayerObject.getInstance();
    private bool isNormalRoom1 = false;
    private bool isNormalRoom2 = false;
    private bool isNormalRoom3 = false;
    private bool isItemRoom1 = false;
    private bool isItemRoom2 = false;
    private bool isItemRoom3 = false;
    private bool isEnemyRoom1 = false;
    private bool isEnemyRoom2 = false;
    private bool isEnemyRoom3 = false;

    public static bool isEnemyDoor = false;
    private SpriteRenderer spriteRenderer, monsterRenderer;
    private Color color;

    public GameObject monsterIcon, monsterIcon2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionStay(Collision collision)
    {
        // Check if the collided game object is the floor
        if (collision.gameObject.tag == "Floor")
        {
            // Get the parent game object of the floor
            GameObject room = collision.gameObject.transform.parent.gameObject;

            // Access the room's information, such as its name or type
            string roomName = room.name;

            if (roomName.Equals("NormalRoom(Clone)"))
            {
                Transform colorNormalRoom = room.transform.Find("MinimapIconNormalRoom");
                spriteRenderer = colorNormalRoom.GetComponent<SpriteRenderer>();
                spriteRenderer.color = Color.green;

            }
            else if (roomName.Equals("NormalRoom2(Clone)"))
            {
                Transform colorNormalRoom = room.transform.Find("MinimapIconNormalRoom2");
                spriteRenderer = colorNormalRoom.GetComponent<SpriteRenderer>();
                spriteRenderer.color = Color.green;

            }
            else if (roomName.Equals("ItemRoom(Clone)"))
            {
                Transform colorNormalRoom = room.transform.Find("MinimapIconItemRoom");
                spriteRenderer = colorNormalRoom.GetComponent<SpriteRenderer>();
                spriteRenderer.color = Color.yellow;

            }
            else if (roomName.Equals("ItemRoom2(Clone)"))
            {
                Transform colorNormalRoom = room.transform.Find("MinimapIconItemRoom2");
                spriteRenderer = colorNormalRoom.GetComponent<SpriteRenderer>();
                spriteRenderer.color = Color.yellow;

            }
            else if (roomName.Equals("EnemyRoom(Clone)"))
            {
                Color orange = new Color(1.0f, 0.5f, 0.0f, 1.0f);
                Transform colorNormalRoom = room.transform.Find("MinimapIconEnemyRoom");
                spriteRenderer = colorNormalRoom.GetComponent<SpriteRenderer>();
                spriteRenderer.color = orange;
                monsterIcon.SetActive(true);
                monsterIcon2.SetActive(true);
            }

            else if (roomName.Equals("BossRoom(Clone)"))
            {
                Transform colorNormalRoom = room.transform.Find("MinimapIconBossRoom");
                spriteRenderer = colorNormalRoom.GetComponent<SpriteRenderer>();
                spriteRenderer.color = Color.red;
            }

            //else if (roomName.Equals("Corridor(Clone)"))
            //{
            //    Transform colorCorridor = room.transform.Find("MinimapIconCorridor");
            //    spriteRenderer = colorCorridor.GetComponent<SpriteRenderer>();
            //    spriteRenderer.color = Color.blue;
            //}
            // Use the room's information as needed


        }
        if (collision.gameObject.tag == "Door" || collision.gameObject.tag == "Floor")
        {

            if (collision.gameObject.tag == "Floor")
            {
                // Get the parent game object of the floor
                GameObject room = collision.gameObject.transform.parent.gameObject;

                // Access the room's information, such as its name or type
                string roomName = room.name;

                if (roomName.Equals("EnemyRoom(Clone)") || roomName.Equals("BossRoom(Clone)"))
                {
                    isEnemyDoor = true;
                }
            }
        }
    }
}
