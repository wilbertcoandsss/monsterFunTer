using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public GameObject wizard, paladin, informationHUD;
    public float pickupRange = 2f;
    public GameObject itemHUD;
    public TextMeshProUGUI tmproHUD, itemAmount, itemInformationText;
    private bool isPicked = false;
    private bool isCollision = false;
    public Animator _animator, _itemAnimator;
    public GameObject meatInactive, potionInactive;
    private bool isEmptyMeat = false;
    private bool isPotionMeat = false;
    private bool isShow = false;
    public static bool isPicking = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && isCollision == true)
        {
            isPicking = true;
            Debug.Log("Lah kalo disini" + isPicking);
            isPicked = true;
            itemHUD.SetActive(false);
            tmproHUD.SetText("");
            //StartCoroutine(waitfor3sec());
            itemInformationText.SetText(gameObject.name + " obtained!");

            if (gameObject.name == "Meat")
            {
                Inventory.meat.setAmount(Inventory.meat.getAmount() + 1);

                if (Inventory.meat.getAmount() - 1 == 0 && Inventory.potion.getAmount() > 0)
                {
                    isEmptyMeat = true;
                    meatInactive.SetActive(true);
                }

                else
                {

                    itemAmount.SetText(Inventory.meat.getAmount().ToString());
                }

            }
            else if (gameObject.name == "Potion")
            {

                Inventory.potion.setAmount(Inventory.potion.getAmount() + 1);

                if (Inventory.potion.getAmount() - 1 == 0 && Inventory.meat.getAmount() > 0)
                {
                    potionInactive.SetActive(true);
                }

                else
                {
                    itemAmount.SetText(Inventory.potion.getAmount().ToString());
                }
            }

            Destroy(this.gameObject);
        }
        else
        {
            isShow = false;

        }

    }



    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player") && isPicked == false)
    //    {
    //        itemHUD.SetActive(true);
    //        tmproHUD.SetText("Press [C] to pickup the " + gameObject.name);
    //        isCollision = true;
    //    }
    //}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            itemHUD.SetActive(true);
            tmproHUD.SetText("Press [C] to pickup the " + gameObject.name);
            isCollision = true;
            Debug.Log("Sekarang is collisionnya + " + isCollision);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isCollision = false;
        itemHUD.SetActive(false);
        tmproHUD.SetText(" ");
    }
    //private void OnCollisionExit(Collision collision)
    //{
    //    isCollision = false;
    //    itemHUD.SetActive(false);
    //    tmproHUD.SetText(" ");
    //}


}

