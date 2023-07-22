using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    private Monster gammoth;
    private Monster glavenus;
    public NavMeshAgent agent;
    public float speed;
    public float walkRadius;
    public Animator animator;
    public CapsuleCollider dmgBox;
    private PlayerObject instance = PlayerObject.getInstance();
    public enum EnemyState { Idle, Chase, Attacking, Die };
    public GameObject meat, potion;


    private  EnemyState currentState = EnemyState.Idle;
    public static bool isAttack = false;
    public Slider health;

    public TextMeshProUGUI monsterTxt;

    private System.Random rand = new System.Random();

    private string str = null;
    private MonsterFactory monsterFactory;

    private bool isPickedItem = false;

    public static bool isDeathMonster = false;

    public GameObject spriteRendererMonster;
    private void Start()
    {
        isAttack = false;
        currentState = EnemyState.Idle;
        dmgBox.enabled = false;

        if (gameObject.tag == "Gammoth")
        {
            monsterFactory = new GammothFactory();
            gammoth = monsterFactory.createMonster();
            monsterTxt.text = gammoth.getName();
        }
        else if (gameObject.tag == "Glavenus")
        {
            monsterFactory = new GlavenusFactory();
            glavenus = monsterFactory.createMonster();
            monsterTxt.text = glavenus.getName();
        }
    }

    private void Update()
    {
        dmgBox.enabled = false;
        if (health.value == 0)
        {
            CapsuleCollider cd = gameObject.GetComponent<CapsuleCollider>();
            cd.enabled = false;
            isDeathMonster = true;
            currentState = EnemyState.Die;
            animator.SetBool("isDeath", true);
            spriteRendererMonster.SetActive(false);
            Door.isEndEnemy = true;
            agent.isStopped = true;

            int randomInt = rand.Next(2) + 1;

            if (randomInt == 1 && !isPickedItem)
            {
                isPickedItem = true;
                meat.SetActive(true);   
            }
            else if (randomInt == 2 && !isPickedItem)
            {
                isPickedItem = true;
                potion.SetActive(true);
            }
        }

        switch (currentState)
        {
            case EnemyState.Idle:
                animator.SetBool("isWalking", false);
                if (str != null)
                    animator.SetBool(str, false);

                break;
            case EnemyState.Chase:
                animator.SetBool("isWalking", true);
                var loc = instance.getC().transform.position;
                agent.SetDestination(loc);
                var distance = Vector3.Distance(gameObject.transform.position, loc);

                if (distance <= 2.1f)
                {
                    animator.SetBool("isWalking", false);
                    currentState = EnemyState.Attacking;
                }
                break;
            case EnemyState.Attacking:
                dmgBox.enabled = true;
                if (!isAttack)
                {
                    isAttack = true;
                    var attackRandom = rand.Next(3) + 1;
                    str = "isAttack" + attackRandom;
                    animator.SetBool(str, true);
                    Invoke("AttackFalse", 1.5f);
                }
                break;
            case EnemyState.Die:
                if (health.value <= 0)
                {
                    animator.SetBool("isWalking", false);
                    if (str != null)
                        animator.SetBool(str, false);
                    //animator.SetBool("isDeath", true);
                }
                else
                {
                    currentState = EnemyState.Idle;
                }
                break;
        }
    }

    public void StateDie()
    {
        currentState = EnemyState.Die;
    }

    public void damaged(float amount)
    {
        if (gameObject.tag == "Gammoth")
        {
            gammoth.setHP(gammoth.getHP() - amount);
            health.value = gammoth.getHP();
        }
        else if (gameObject.tag == "Glavenus")
        {
            glavenus.setHP(glavenus.getHP() - amount); ;
            health.value = glavenus.getHP();
        }
    }

    private void AttackFalse()
    {
        animator.SetBool(str, false);
        isAttack = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            currentState = EnemyState.Chase;
            agent.isStopped = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            currentState = EnemyState.Idle;
            agent.isStopped = true;
        }
    }
}
