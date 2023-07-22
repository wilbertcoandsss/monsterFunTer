using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;

public class WizardSkillScript : MonoBehaviour
{
    public GameObject fireSkill, flySkill;
    public Animator _animator, _itemAnimator;
    public Slider slider, slider1;
    public static bool isCastingSkill = false;
    public static bool isFireSkill = false;
    public float flyY = 5f;
    public TextMeshProUGUI textMission;

    private CinemachineBrain cmBrain;

    public CinemachineFreeLook freeLookCamera;

    public Cinemachine.AxisState xAxis;
    public Cinemachine.AxisState yAxis;
    Transform cameraLookAt;
    Camera mainCamera;

    public bool isZoomedIn = false;
    public GameObject crosshair;

    public static float damageBasicAttack = 10f;
    public float range = 100f;
    public CinemachineVirtualCamera fpsCam;

    public GameObject freeLook, aim;

    public ParticleSystem muzzleFlash;
    public GameObject impactEffects;

    public GameObject projectile;
    private Vector3 destination;

    public Transform WZfirePoint;

    public Cinemachine.CinemachineImpulseSource source;
    public Rig rig;

    private PlayerObject instance = PlayerObject.getInstance();
    private bool isShowing1 = false;
    private bool isShowing2 = false;
    private bool isShowing3 = false;


    public AudioSource flySkillSound;
    public AudioSource fireSkillSound;
    public AudioSource shootSkillSound;

    public GameObject wiz;

    private Quaternion targetRotation;
    public float rotationSpeed = 2f;
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

        if (Input.GetKeyDown(KeyCode.R) && isCastingSkill == false && _animator.GetBool("isGrounded") == true && slider1.value == 1)
        {
            flySkillSound.Play();

            if (MissionScript.secondMission && MissionScript.isFlying == false)
            {
                MissionScript.advancedSkillCount += 1;
                MissionScript.isFlying = false;
            }

            if (MissionScript.advancedSkillCount == 2 && isShowing2 == false)
            {
                isShowing2 = true;
                textMission.SetText("Mission Success!");
                StartCoroutine(waitPopUp());
            }
            StartCoroutine(prepareFly());

            if (Cheat.isFlyCheat)
            {
                Invoke("FlyingFalse", 10f);
            }
            else
            {
                Invoke("FlyingFalse", 5f);
            }
            slider1.value = 0;
        }

        else
        {
            slider1.value += Time.deltaTime / 10f;
        }

        if (_animator.GetBool("isFlying"))
        {
            Quaternion rotation;

            if (Portal.isMaze)
            {
                if (instance.getIdx() == 0)
                {
                    GameObject character = GameObject.Find("Kachujin");
                    Vector3 pos = character.transform.position;
                    pos.y = 3f;
                    character.transform.position = pos;
                    if (Input.GetKey(KeyCode.E))
                    {
                        targetRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, -45f);
                    }
                    else if (Input.GetKey(KeyCode.Q))
                    {
                        targetRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 45f);
                    }
                    else
                    {
                        targetRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0f);
                    }

                    GameObject.Find("Kachujin").transform.rotation = Quaternion.Lerp(GameObject.Find("Kachujin").transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                    //if (Input.GetKeyDown(KeyCode.E))
                    //{
                    //    rotation = Quaternion.Euler(character.transform.rotation.eulerAngles.x, character.transform.rotation.eulerAngles.y, -45f);
                    //    character.transform.rotation = rotation;
                    //}
                    //else if (Input.GetKey(KeyCode.Q))
                    //{
                    //    rotation = Quaternion.Euler(character.transform.rotation.eulerAngles.x, character.transform.rotation.eulerAngles.y, 45f);
                    //    character.transform.rotation = rotation;
                    //}
                    //else
                    //{
                    //    rotation = character.transform.rotation;
                    //}
                }
            }
            else if (Portal.isMaze == false)
            {
                if (instance.getIdx() == 0)
                {
                    var pos = GameObject.Find("Kachujin").transform.position;
                    pos.y = 9f;
                    GameObject.Find("Kachujin").transform.position = pos;

                    if (Input.GetKey(KeyCode.E))
                    {
                        Debug.Log("Masuk sini ga si ajg");
                        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, -45f);
                    }
                    else if (Input.GetKey(KeyCode.Q))
                    {
                        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 45f);
                    }
                    else
                    {
                        rotation = transform.rotation;
                    }
                }
            }

            if (Cheat.isFlyCheat)
            {
                transform.position += transform.forward * 0.7f;
            }
            else
            {
                transform.position += transform.forward * 0.3f;
            }
        }

        if (Input.GetKeyDown(KeyCode.F) && isCastingSkill == false && _animator.GetBool("isGrounded") == true)
        {

            isCastingSkill = true;

            if (MissionScript.secondMission && MissionScript.isFire == false)
            {
                MissionScript.advancedSkillCount += 1;
                MissionScript.isFire = true;
            }

            if (MissionScript.advancedSkillCount == 2 && isShowing2 == false)
            {
                isShowing2 = true;
                textMission.SetText("Mission Success!");
                StartCoroutine(waitPopUp());
            }

            slider.value = 0;
            _animator.SetBool("isCasting", true);
            StartCoroutine(Cast());
        }
        else
        {
            slider.value += Time.deltaTime / 5f;
        }


        if (Input.GetMouseButton(1))
        {
            rig.weight = 1;
            crosshair.SetActive(true);
            freeLook.SetActive(false);
            aim.SetActive(true);
            _animator.SetBool("isAiming", true);

            if (Input.GetMouseButtonDown(0))
            {
                shootSkillSound.Play();
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

                
                var pos = instance.getC().transform.position;
                ShootProjectile();
                Shoot();
            }
        }
        else
        {
            rig.weight = 0;
            freeLook.SetActive(true);
            aim.SetActive(false);
            crosshair.SetActive(false);
            _animator.SetBool("isAiming", false);
        }
    }

    IEnumerator Cast()
    {
        yield return new WaitForSeconds(1.2f);
        isFireSkill = true;
        fireSkill.SetActive(true);
        yield return new WaitForSeconds(3f);
        fireSkill.SetActive(false);
        isFireSkill = false;
        _animator.SetBool("isCasting", false);
        isCastingSkill = false;
    }

    void Shoot()
    {
        muzzleFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(aim.transform.position, aim.transform.forward, out hit, range))
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
                Debug.Log("Masuk cok");
            }
            
            GameObject impactGO = Instantiate(impactEffects, hit.point, Quaternion.LookRotation(hit.normal));
            source.GenerateImpulse(Camera.main.transform.forward);
            Destroy(impactGO, 1f);
        }
        shootSkillSound.Play();
    }

    void ShootProjectile()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hitpoint;

        if (Physics.Raycast(ray, out hitpoint))
        {
            destination = hitpoint.point;
        }
        else
        {
            destination = ray.GetPoint(1000);
        }
        projectile.SetActive(true);
        InstantiateProjectile(WZfirePoint) ;
        projectile.SetActive(false);
    }

    void InstantiateProjectile(Transform firePoint)
    {
        GameObject projectileObj = Instantiate(projectile, firePoint.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * 20;
        Destroy(projectileObj, 1f);
    }

    public void FlyingFalse()
    {
        _animator.SetBool("isFlying", false);
        flySkillSound.Stop();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_animator.GetBool("isFlying"))
        {
            _animator.SetBool("isFlying", false);
        }
    }

    public IEnumerator waitPopUp()
    {
        _itemAnimator.SetBool("isShowing", true);
        yield return new WaitForSeconds(2f);
        _itemAnimator.SetBool("isShowing", false);
    }

    private IEnumerator prepareFly()
    {
        _animator.SetBool("isPrepare", true);
        yield return new WaitForSeconds(0.4f);
        _animator.SetBool("isPrepare", false);
        _animator.SetBool("isFlying", true);
        
    }

}

