using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class Door : MonoBehaviour
{
    private static bool doorClosed = false;
    private bool isAnimating = false; // Add this line

    public PlayableDirector doorOpenedTimeline;
    public PlayableDirector doorClosedTimeline;
    private MeshCollider thisDoor;
    private MeshCollider wallEntrance;
    public AudioSource openDoor, closedDoor;

    public GameObject HUDItem;
    public TextMeshProUGUI tmItem;

    public static bool isEndEnemy = false;
    // Start is called before the first frame update
    void Start()
    {
        thisDoor = GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            HUDItem.SetActive(true);
            if (doorClosed)
            {
                tmItem.SetText("Press [B] to close the door");
            }
            else
            {
                tmItem.SetText("Press [B] to open the door");
            }

            if (RoomDetector.isEnemyDoor == true && isEndEnemy == false)
            {
                StartCoroutine(waitForTwoSeconds());
            }

            if (Input.GetKeyDown(KeyCode.B) && !isAnimating) // Add !isAnimating condition
            {
                if (!doorClosed)
                {
                    if (RoomDetector.isEnemyDoor == true && EnemyController.isDeathMonster == false)
                    {

                    }
                    else
                    {
                        StartCoroutine(PlayDoorOpenedAnimation());
                    }
                }
                else if (doorClosed)
                {
                    StartCoroutine(PlayDoorClosedAnimation());
                }
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        HUDItem.SetActive(false);
        tmItem.SetText("");
    }

    IEnumerator waitForTwoSeconds()
    {
        yield return new WaitForSeconds(1.1f);
        isEndEnemy = true;
        StartCoroutine(PlayDoorClosedAnimation());
    }

    // New coroutines for playing door animations
    IEnumerator PlayDoorOpenedAnimation()
    {
        isAnimating = true;
        doorOpenedTimeline.Play();
        yield return new WaitForSeconds((float)doorOpenedTimeline.duration);
        doorClosed = true;
        thisDoor.enabled = false;
        openDoor.Play();
        isAnimating = false;
    }

    IEnumerator PlayDoorClosedAnimation()
    {
        isAnimating = true;
        doorClosedTimeline.Play();
        yield return new WaitForSeconds((float)doorClosedTimeline.duration);
        doorClosed = false;
        thisDoor.enabled = true;
        closedDoor.Play();
        isAnimating = false;
    }
}
