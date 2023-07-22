using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject playerCharacter;
    // Start is called before the first frame update
    void Start()
    {
        if (playerCharacter != null)
        {
            Instantiate(playerCharacter);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
