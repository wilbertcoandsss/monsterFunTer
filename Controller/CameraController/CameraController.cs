using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CameraController : MonoBehaviour
{
    Camera mainCamera;
    public GameObject freeLook, introScene;
    public Cinemachine.AxisState yAxis;
    public Cinemachine.AxisState xAxis;
    public Transform cameraLookAt;


    public PlayableDirector playableDirector;
    public CinemachineVirtualCamera cutSceneCam;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        cutSceneCam.Priority = 20;
        Invoke("changeCutscene", 11);
        

    }

    // Update is called once per frame
    void Update()
    {
        if (freeLook.activeSelf)
        {
            float yCamera = mainCamera.transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yCamera, 0), 20 * Time.deltaTime);
        }
        else
        {
            yAxis.Update(Time.deltaTime);
            xAxis.Update(Time.deltaTime);

            cameraLookAt.eulerAngles = new Vector3(yAxis.Value, xAxis.Value, 0);

            float yCamera = mainCamera.transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yCamera, 0), 20 * Time.deltaTime);
        }
    }

    void changeCutscene()
    {
        cutSceneCam.Priority = 0;
        introScene.SetActive(false);
    }


}
