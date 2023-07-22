using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meat : Item
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setAmount(int amount)
    {
        this.itemAmount = amount;
    }

    public int getAmount()
    {
        return this.itemAmount;
    }

    public void setName(string name)
    {
        this.itemName = name;
    }

    public string getName()
    {
        return this.itemName;
    }
}
