using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class MissionScript : MonoBehaviour
{
    // Start is called before the first frame update

    public TextMeshProUGUI txtMission;

    public static bool defaultMission = true;
    public static bool firstMission = false;
    public static bool secondMission = false;
    public static bool thirdMission = false;
    public static bool fourthMission = false;
    public static int basicAttackCount = 0;
    public static int advancedSkillCount = 0;
    public static bool isFire = false;
    public static bool isFlying = false;
    public static bool isRage = false;
    public static bool isRoll = false;
    public static int NPCCount = 0;
    public static bool isFinal = false;
    float timer = 0f;
    public CinemachineVirtualCamera mazeCutCam;
    public PlayableDirector timeline;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (isFinal == true)
        {
            txtMission.SetText("Go to the portal!");
        }

        if (defaultMission)
        {
            txtMission.SetText("Find & Talk to Lyra!");
        }

        else if (firstMission)
        {
            if (basicAttackCount <= 10)
            {
                if (basicAttackCount == 10)
                {
                    NPCController.secondSteps = true;
                    txtMission.color = Color.green;
                    firstMission = false;
                    NPCController.firstTwoDialog = false;
                    NPCController.firstOneDialog = false;
                    NPCController.firstSteps = true;
                }
                txtMission.SetText("Use basic attack " + basicAttackCount + " / 10");
            }
        }

        else if (secondMission)
        {
            txtMission.color = Color.white;
            if (advancedSkillCount <= 2)
            {
                if (advancedSkillCount == 2)
                {
                    NPCController.secondSteps = false;
                    txtMission.color = Color.green;
                    secondMission = false;
                    NPCController.secondOneDialog = false;
                    NPCController.secondTwoDialog = false;
                    NPCController.thirdSteps = true;
                }
                txtMission.SetText("Use your skills " + advancedSkillCount + " / 2");
            }
            else
            {
                Debug.Log("Kapan masuk sininnya");
                txtMission.color = Color.white;

            }
        }

        else if (thirdMission)
        {
            txtMission.color = Color.white;
            if (NPCCount <= 3)
            {
                Debug.Log(NPCCount);
                if (NPCCount == 3)
                {
                    NPCController.thirdSteps = false;
                    txtMission.color = Color.green;
                    thirdMission = false;
                    NPCController.thirdOneDialog = false;
                    NPCController.thirdTwoDialog = false;
                    NPCController.fourthSteps = true;
                }
                txtMission.SetText("Meet all the npc " + NPCCount + " / 3");
            }
            else
            {
                txtMission.color = Color.white;

            }
        }

        else if (fourthMission)
        {
            mazeCutCam.Priority = 35;
            timeline.Play();

            timer += Time.deltaTime;
            if (timer >= 5f)
            {
                timeline.Stop();
                 mazeCutCam.Priority = 0;
            }
            txtMission.color = Color.white;
            txtMission.SetText("Go to the portal!");
            isFinal = true;
        }
    }

    void changeMazeCam()
    {
        mazeCutCam.Priority = 0;
    }

}
