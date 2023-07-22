using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMap : MonoBehaviour
{
    public GameObject HUDMap;
    private bool tabDown = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            HUDMap.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            HUDMap.SetActive(false);
        }
    }
}
