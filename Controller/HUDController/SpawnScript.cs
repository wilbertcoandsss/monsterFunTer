using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    private PlayerObject instance = PlayerObject.getInstance();
    // Start is called before the first frame update
    void Start()
    {
        //instance.setIdx(1);
        GameObject parentClone = GameObject.Find("SpawnRoom(Clone)");
        Transform spawnCloned = parentClone.transform.Find("SpawnPoint");
        Vector3 position = spawnCloned.transform.position;
        Debug.Log("Start position" + position);
        instance.getC().transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
