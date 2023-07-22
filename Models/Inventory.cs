using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Meat meat = new Meat();
    public static Potion potion = new Potion();
    public GameObject listItem;
    public TextMeshProUGUI itemName, itemAmount;
    public GameObject meatActive, meatInactive;
    public GameObject potionActive, potionInactive;

    public GameObject HUD;
    // Start is called before the first frame update
    void Start()
    {
        meat.setName("Meat");
        meat.setAmount(1);
        potion.setName("Potion");
        potion.setAmount(2);

        potionActiveScene();
    }

    // Update is called once per frame
    public void Update()
    {

       if (Input.GetKeyDown(KeyCode.T))
        {
            if (meatActive.activeSelf)
            {
                potionActiveScene();
            }
            else if (potionActive.activeSelf)
            {
       
                meatActiveScene();            
            }
        }


        if (meat.getAmount() == 0 && potion.getAmount() == 0)
            {
            meatActive.SetActive(false);
            potionActive.SetActive(false);
            meatInactive.SetActive(false);
            potionInactive.SetActive(false);
            itemName.SetText("");
            itemAmount.SetText("");
            }


        if (meat.getAmount() > 0 && potion.getAmount() == 0)
                {
                    meatActive.SetActive(true);
                    itemAmount.SetText(meat.getAmount().ToString());
                    itemName.SetText("Meat");

                    potionActive.SetActive(false);
            meatInactive.SetActive(false);
                }
                else if (meat.getAmount() == 0 && potion.getAmount() > 0)
                {
                    
                    potionActive.SetActive(true);
                    itemAmount.SetText(potion.getAmount().ToString());
                    itemName.SetText("Potion");

                    meatActive.SetActive(false);
                    potionInactive.SetActive(false);
                
                }
    }

    void meatActiveScene()
    {
        meatActive.SetActive(true);
        potionInactive.SetActive(true);
        potionActive.SetActive(false);
        meatInactive.SetActive(false);
        itemName.SetText("Meat");
        itemAmount.SetText(meat.getAmount().ToString());
    }

    void potionActiveScene()
    {
        potionActive.SetActive(true);
        meatInactive.SetActive(true);
        potionInactive.SetActive(false);
        meatActive.SetActive(false);
        itemName.SetText("Potion");
        itemAmount.SetText(potion.getAmount().ToString());
    }
}
