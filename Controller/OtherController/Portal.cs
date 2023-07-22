using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Portal : MonoBehaviour
{
    public GameObject showHUD, loadingCanvas, portalEffects;
    public TextMeshProUGUI textHUD;
    public Button btn, btnVillage;
    public static bool isMaze = false;
    // Start is called before the first frame update
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.buildIndex == 2)
        {
            isMaze = false;
        }
        else if (currentScene.buildIndex == 3)
        {
            isMaze = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMaze)
        {
            if (MissionScript.isFinal == false)
            {
                portalEffects.SetActive(false);
            }
            else if (MissionScript.isFinal == true)
            {
                portalEffects.SetActive(true);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (MissionScript.isFinal || isMaze)
            {
                showHUD.SetActive(true);
                if (isMaze)
                {
                    textHUD.SetText("Press [J] to go back to the village!");

                    if (Input.GetKeyDown(KeyCode.J) && isMaze)
                    {
                        TextMeshProUGUI tmText;
                        GameObject text = GameObject.Find("MissionTxt");
                        tmText = text.GetComponent<TextMeshProUGUI>();
                        tmText.SetText("Go back to the maze!");
                        tmText.color = Color.green;
                        isMaze = false;
                        MissionScript.isFinal = true;
                        loadingCanvas.SetActive(true);
                        ExecuteEvents.Execute(btn.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                    }
                }
                else
                {
                    if (MissionScript.isFinal)
                    {
                        textHUD.SetText("Press [J] to go to the Maze");
                        if (Input.GetKey(KeyCode.J))
                        {
                            isMaze = true;
                            loadingCanvas.SetActive(true);
                            ExecuteEvents.Execute(btn.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        showHUD.SetActive(false);
        textHUD.SetText(" ");
    }

}
