using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    private Monster gammoth;
    private Monster glavenus;
    public NavMeshAgent agent;
    public float speed;
    public float walkRadius;
    public Animator animator;
    public CapsuleCollider dmgBoxLeft, dmgBoxRight, dmgBoxBottom;
    private PlayerObject instance = PlayerObject.getInstance();
    public enum BossState { Idle, Chase, Attacking, Die };
    public GameObject meat, potion, fireMouth;


    private BossState currentState = BossState.Idle;
    public static bool isAttack = false;
    public Slider health;

    public TextMeshProUGUI monsterTxt;

    private System.Random rand = new System.Random();

    private string str = null;
    private MonsterFactory monsterFactory;

    private bool isPickedItem = false;

    public static bool isDeathMonster = false;

    public SpriteRenderer spriteRendererMonster;
    private void Start()
    {
        isAttack = false;
        currentState = BossState.Idle;
        dmgBoxLeft.enabled = false;
        dmgBoxRight.enabled = false;
        dmgBoxBottom.enabled = false;
        //legBox.enabled = false;

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
        dmgBoxLeft.enabled = false;
        dmgBoxRight.enabled = false;
        dmgBoxBottom.enabled = false;
        if (health.value == 0)
        {
            gameObject.tag = "Death";
            isDeathMonster = true;
            currentState = BossState.Die;
            animator.SetBool("isDeath", true);
            spriteRendererMonster.enabled = false;
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
            case BossState.Idle:
                animator.SetBool("isWalking", false);
                if (str != null)
                    animator.SetBool(str, false);

                break;
            case BossState.Chase:
                animator.SetBool("isWalking", true);
                var loc = instance.getC().transform.position;
                agent.SetDestination(loc);

                var distance = Vector3.Distance(gameObject.transform.position, loc);

                if (distance <= 2.1f)
                {
                    animator.SetBool("isWalking", false);
                    currentState = BossState.Attacking;
                }
             

                break;
            case BossState.Attacking:
                dmgBoxLeft.enabled = true;
                dmgBoxRight.enabled = true;
                dmgBoxBottom.enabled = true;
                if (!isAttack)
                {
                    isAttack = true;
                    var attackRandom = rand.Next(4) + 1;
                    str = "isAttack" + attackRandom;
                    animator.SetBool(str, true);
                    if (animator.GetBool("isAttack4"))
                    {
                        StartCoroutine(fireSkill());
                        Invoke("AttackFalse", 5f);
                    }
                    else
                    {
                        Invoke("AttackFalse", 3.5f);
                    }
                }
                break;
            case BossState.Die:
                if (health.value <= 0)
                {
                    animator.SetBool("isWalking", false);
                    if (str != null)
                        animator.SetBool(str, false);
                    //animator.SetBool("isDeath", true);
                }
                else
                {
                    currentState = BossState.Idle;
                }
                break;
        }
    }

    public void StateDie()
    {
        currentState = BossState.Die;
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
            glavenus.setHP(glavenus.getHP() - amount);
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
            currentState = BossState.Chase;
            agent.isStopped = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            currentState = BossState.Idle;
            agent.isStopped = true;
        }
    }

    IEnumerator fireSkill()
    {
        yield return new WaitForSeconds(2.2f);
        fireMouth.SetActive(true);
        yield return new WaitForSeconds(3f);
        fireMouth.SetActive(false);
    }
}
