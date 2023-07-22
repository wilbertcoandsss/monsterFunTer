using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public Slider healthSlider;
    private PlayerObject instance = PlayerObject.getInstance();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //healthSlider.value = instance.getHealth();
        //Debug.Log("Health slilder ni bos :" + healthSlider.value);
    }
}
