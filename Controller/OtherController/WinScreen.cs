using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    public static bool isWin = false;
    private bool monsterDeath = false;
    private bool bossDeath = false;

    private void Start()
    {
        
    }
    private void Update()
    {
        GameObject[] monsterObjects = GameObject.FindGameObjectsWithTag("Gammoth");
        GameObject[] bossObjects = GameObject.FindGameObjectsWithTag("Glavenus");
        GameObject boss1 = GameObject.FindGameObjectWithTag("Glavenus");

        Slider boss2 = boss1.GetComponentInChildren<Slider>();

        foreach (GameObject monster in monsterObjects)
        {
            Slider[] sliders = monster.GetComponentsInChildren<Slider>();

            foreach (Slider slider in sliders)
            {
                if (slider.value <= 0)
                {
                    monsterDeath = true;
                }
            }
        }

        foreach (GameObject boss in bossObjects)
        {
            Slider[] sliders = boss.GetComponentsInChildren<Slider>();

            foreach (Slider slider in sliders)
            {
                if (slider.value <= 0)
                {
                    bossDeath = true;
                }
            }
        }




        Debug.Log(monsterDeath + "dan" + bossDeath);

        if (bossDeath && monsterDeath)
        {
            TextMeshProUGUI tmText;
            GameObject text = GameObject.Find("MissionTxt");
            tmText = text.GetComponent<TextMeshProUGUI>();
            tmText.color = Color.green;
            isWin = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void setWinFalse()
    {
        isWin = false;
    }
}
