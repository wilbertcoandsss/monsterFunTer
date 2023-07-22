using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject
{
    private static PlayerObject instance;
    private static GameObject c;
    private static int idx;
    public static float stamina = 10f;
    public static float movementSpeed = 0.01f;
    public static float fly = 5f;
    // Start is called before the first frame update
    public static float health = 100f;
    public static PlayerObject getInstance()
    {
        if (instance == null)
        {
            instance = new PlayerObject();
        }

        return instance;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public int getIdx()
    {
        return idx;
    }

    public void setIdx(int index)
    {
        idx = index;
    }

    public GameObject getC()
    {
        return c;
    }

    public void setC(GameObject character)
    {
        c = character;
    }

    public void setFullStamina()
    {
        stamina = 10f;
    }

    public float getMovementSpeed()
    {
        return movementSpeed;
    }

    public void setMovementSpeed(float speed)
    {
        movementSpeed = speed;
    }

    public float getHealth()
    {
        return health;
    }
    public void setHealth(float thisHealth)
    {
        health = thisHealth;
    }
    
    public void setHalfHP()
    {
        health += 50f;
    }

    public void setFullHP()
    {
        health = 100f;
    }
    public float getFly()
    {
        return fly;
    }

}
