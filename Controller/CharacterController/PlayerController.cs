using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] private Animator _animator = null;
    private PlayerObject instance = PlayerObject.getInstance();
    //public GameObject[] listChar;
    public GameObject p;
    public GameObject k;
    public CinemachineFreeLook freelookCam;
    private GameObject active;
    //public Transform cam;
    // Start is called before the first frame update
    void Start()
    {
       // GameManagerScript gameManager = FindObjectOfType<GameManagerScript>();
       // gameManager.playerCharacter = p;

        //p = Instantiate(listChar[instance.getIdx()], rsp, Quaternion.identity);
        //Debug.Log(p);
        if(instance.getIdx() == 0)
        {
            active = k;
            //k.SetActive(true);
            //p.SetActive(false);
        }
        else
        {
            active = p;
            //p.SetActive(true);
            //k.SetActive(false);
        }
        active.SetActive(true);
        instance.setC(active);
        freelookCam.Follow = active.transform;
        freelookCam.LookAt = active.transform;
    }

    // Update is called once per frame
    void Update()
    {

    }



}


