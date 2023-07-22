using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMap : MonoBehaviour
{
    private PlayerObject instance = PlayerObject.getInstance();
    public GameObject maze;
    public Vector2 terrainSize;
    public float cameraHeight = 100f; // You can adjust this value based on your requirements

    private Camera cam;
    private bool isStart = false;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isStart == false)
        {
            transform.position = GameObject.Find("Titik").transform.position;
            isStart = true;
            //Vector3 playerPosition = instance.getC().transform.position;
            //Vector3 newPosition = new Vector3(playerPosition.x, cameraHeight, playerPosition.z);
            //transform.position = newPosition;
            //isStart = true;
            //transform.rotation = Quaternion.LookRotation(playerPosition - newPosition);
        }

    }
}
