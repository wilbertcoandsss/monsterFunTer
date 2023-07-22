using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;
    public CinemachineVirtualCamera virtualCamera;

    private CinemachineBrain cinemachineBrain;

    void Start()
    {
        cinemachineBrain = GetComponent<CinemachineBrain>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Right mouse button is pressed
        {
            // Disable the free look camera and enable the virtual camera
            freeLookCamera.enabled = false;
            virtualCamera.enabled = true;

            // Switch to the virtual camera instantly
        }
    }
}
