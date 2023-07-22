using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaScript : MonoBehaviour
{
    public Slider staminaSlider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (PlayerObject.stamina <= 0)
            {
                staminaSlider.value = 0;
            }
            else if (PlayerObject.stamina > 0)
            {
                PlayerObject.stamina -= 0.01f;
                staminaSlider.value = PlayerObject.stamina;
            }
        }
        else
        {
            if (PlayerObject.stamina == 10)
            {
                staminaSlider.value = PlayerObject.stamina;
            }
            else if (PlayerObject.stamina < 10 || PlayerObject.stamina > 0)
            {
                PlayerObject.stamina += 0.003f;
                staminaSlider.value = PlayerObject.stamina;
            }
        }
    }
}
