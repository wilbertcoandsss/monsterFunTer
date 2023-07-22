using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPause = false;
    public GameObject pauseMenu, HUD;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause)
            {
                resumeGame();
            }
            else
            {
                pauseGame();
            }
        }
    }

    public void resumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPause = false;
        
    }

    public void pauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPause = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void exitGame()
    {
        MissionScript.defaultMission = true;
        MissionScript.firstMission = false;
        MissionScript.secondMission = false;
        MissionScript.thirdMission = false;
        MissionScript.fourthMission = false;
        MissionScript.basicAttackCount = 0;
        MissionScript.advancedSkillCount = 0;
        MissionScript.isFire = false;
        MissionScript.isFlying = false;
        MissionScript.isRage = false;
        MissionScript.isRoll = false;
        MissionScript.NPCCount = 0;
        MissionScript.isFinal = false;
        NPCController.firstOneDialog = false;
        NPCController.firstTwoDialog = false;

        NPCController.secondOneDialog = false;
        NPCController.secondTwoDialog = false;

        NPCController.thirdOneDialog = false;
        NPCController.thirdTwoDialog = false;

        NPCController.fourthOneDialog = false;
        NPCController.fourthTwoDialog = false;

        NPCController.remyOneDialog = false;
        NPCController.remySecondDialog = false;

        NPCController.astraOneDialog = false;
        NPCController.astraSecondDialog = false;

        NPCController.firstMetAstra = false;
        NPCController.firstMetRemy = false;
        NPCController.firstMetLyra = false;

        NPCController.firstTalkAstra = false;
        NPCController.firstTalkRemy = false;
        NPCController.firstTalkLyra = false;

        NPCController.isTalking = false;
        NPCController.secondSteps = false;
        NPCController.firstSteps = false;
        NPCController.thirdSteps = false;
        NPCController.fourthSteps = false;

        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
