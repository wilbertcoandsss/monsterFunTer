using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PaladinSkillScript : MonoBehaviour
{

    public float cooldownTime = 2f;
    private float nextFireTime = 0f;
    public static int noOfClicks = 0;
    float lastClickedTime = 0;
    float maxComboDelay = 1;

    public static float damageBasicAttack = 10f;
    float attackSpeed = 0.9f;
    float lastRageTime = 0;
    float lastRollTime = 0;

    private float lastDodge = -100f;

    public GameObject rageSkill, attackSkill, aim;
    public Animator _animator, _itemAnimator;
    public Slider slider, slider1;
    public static bool isCastingSkill = false;

    public TextMeshProUGUI textMission;

    public PlayerObject instance = PlayerObject.getInstance();
    private bool isShowing1 = false;
    private bool isShowing2 = false;
    private bool isShowing3 = false;

    public AudioSource rageSkillSound;
    public AudioSource rollSound, hitSound, damageSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value == 1)
        {
            GameObject fillArea = slider.fillRect.gameObject;
            Image fillImg = fillArea.GetComponent<Image>();

            Color fillAreaColor = fillImg.color;
            fillAreaColor.a = 0f;

            fillImg.color = fillAreaColor;
        }

        else
        {
            GameObject fillArea = slider.fillRect.gameObject;
            Image fillImg = fillArea.GetComponent<Image>();

            Color fillAreaColor = fillImg.color;
            fillAreaColor.a = 0.65f;

            fillImg.color = fillAreaColor;
        }

        if (slider1.value == 1)
        {
            GameObject fillArea = slider1.fillRect.gameObject;
            Image fillImg = fillArea.GetComponent<Image>();

            Color fillAreaColor = fillImg.color;
            fillAreaColor.a = 0f;

            fillImg.color = fillAreaColor;
        }

        else
        {
            GameObject fillArea = slider1.fillRect.gameObject;
            Image fillImg = fillArea.GetComponent<Image>();

            Color fillAreaColor = fillImg.color;
            fillAreaColor.a = 0.65f;

            fillImg.color = fillAreaColor;
        }


        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > attackSpeed && _animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            _animator.SetBool("isAttack1", false);
        }
        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > attackSpeed && _animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
        {
            _animator.SetBool("isAttack2", false);
        }
        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > attackSpeed && _animator.GetCurrentAnimatorStateInfo(0).IsName("Attack3"))
        {
            _animator.SetBool("isAttack3", false);
            noOfClicks = 0;
        }

        if (Time.time - lastClickedTime > maxComboDelay)
        {
            noOfClicks = 0;
        }
        if (Time.time > nextFireTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (MissionScript.firstMission)
                {
                    MissionScript.basicAttackCount += 1;
                }

                if (MissionScript.basicAttackCount == 10 && isShowing1 == false)
                {
                    isShowing1 = true;
                    textMission.SetText("Mission Success!");
                    StartCoroutine(waitPopUp());
                }
                OnClick();
            }
        }


        if (Input.GetKeyDown(KeyCode.R) && (Time.time - lastRageTime) > 5f && slider1.value == 1)
        {
            if (MissionScript.secondMission && MissionScript.isRage == false)
            {
                MissionScript.advancedSkillCount += 1;
                MissionScript.isRage = false;
            }

            rageSkillSound.Play();
            _animator.SetBool("isRage", true);
            PaladinAnimation.movementSpeed = 0.08f;
            attackSpeed = 0.1f;
            lastRageTime = Time.time;
            slider.value = 0;
        }
        else
        {
            _animator.SetBool("isRage", false);
            slider.value += Time.deltaTime / 5f;
            
            if (Time.time - lastRageTime > 5f)
            {
                //skill1.SetActive(true);
                //skill1Cooldown.SetActive(false)
                //

                PaladinAnimation.movementSpeed = 0.05f;
                attackSpeed = 0.5f;
                rageSkillSound.Stop();
            }
        }

        if (Input.GetKeyDown(KeyCode.F) && (Time.time - lastRollTime) > 3f)
        {
            if (MissionScript.secondMission && MissionScript.isRoll == false)
            {
                MissionScript.advancedSkillCount += 1;
                MissionScript.isRoll = true;
            }

            if (MissionScript.advancedSkillCount == 2 && isShowing2 == false)
            {
                isShowing2 = true;
                textMission.SetText("Mission Success!");
                StartCoroutine(waitPopUp());
            }
            rollSound.Play();
            _animator.SetBool("isRoll", true);
            lastRollTime = Time.time;
            StartCoroutine(Roll());
            //skill2.SetActive(false);
            //skill2Cooldown.SetActive(true);
            slider1.value = 0;
        }
        else
        {
            _animator.SetBool("isRoll", false);
                slider1.value += Time.deltaTime / 3f;
            if (Time.time - lastRollTime > 3f)
            {
                rollSound.Stop();
                //skill2.SetActive(true);
                //skill2Cooldown.SetActive(false);
            }
        }

        IEnumerator Roll()
        {
            float duration = 0.8f;
            float speed = 1f;
            float distance = 2f;

            Vector3 startPosition = transform.position;
            Vector3 endPosition = transform.position + transform.forward * distance;

            float t = 0f;
            while (t < duration)
            {
                transform.position = Vector3.Lerp(startPosition, endPosition, t / duration);
                t += Time.deltaTime * speed;
                yield return null;
            }
            transform.position = endPosition;

            yield return new WaitForSeconds(duration / 2f);
            lastDodge = Time.time;
        }
    }

    void OnClick()
    {
        lastClickedTime = Time.time;
        noOfClicks++;
        if (noOfClicks == 1)
        {
            _animator.SetBool("isAttack1", true);
            Shoot();
        }
        noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);

        if (noOfClicks >= 2 && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > attackSpeed && _animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            _animator.SetBool("isAttack1", false);
            _animator.SetBool("isAttack2", true);
            Shoot();
        }
        if (noOfClicks >= 3 && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > attackSpeed && _animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
        {
            _animator.SetBool("isAttack2", false);
            _animator.SetBool("isAttack3", true);
            Shoot();
        }

    }
    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(aim.transform.position, aim.transform.forward, out hit, 100f))
        {
            EnemyController target = hit.transform.GetComponent<EnemyController>();
            BossController boss = hit.transform.GetComponent<BossController>();
            Debug.Log(hit.transform.name);
            if (target != null)
            {
                Debug.Log(target.name);
                target.damaged(damageBasicAttack);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * 2f);
            }

            if (boss != null)
            {
                boss.damaged(damageBasicAttack);
            }
        }
        damageSound.Play();
    }
    public IEnumerator waitPopUp()
    {
        _itemAnimator.SetBool("isShowing", true);
        yield return new WaitForSeconds(2f);
        _itemAnimator.SetBool("isShowing", false);
    }
}

