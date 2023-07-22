using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WizardAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator = null;
    public Animator _itemAnimator;
    public Transform cam;
    private Rigidbody rb;
    public float jumpForce = 0.5f;
    private float ySpeed;
    private Vector3 movement;
    private float jumpTime, dodgeTime, dodgePeriod;
    private float jumpPeriod, landPeriod;
    public AudioSource footStep;

    public GameObject itemHUD;
    public GameObject itemPicked;
    public TextMeshProUGUI tmproHUD, itemAmount;

    private float lastDodge = -100f;

    public GameObject meatActive;
    public GameObject potionActive;

    public Slider staminaBar, HPBar;

    private PlayerObject instance = PlayerObject.getInstance();

    private bool isShowHUD = false;
    public GameObject showHUD;

    bool isShowing3 = false;

    public TextMeshProUGUI textMission;

    public PlayableDirector winTimeline, loseTimeline;
    public GameObject winScreen, loseScreen;

    private bool isDeath = false;
    private bool isWinning = false;

    public static float movementSpeed = 0.03f;

    public Material revealMaterial;
    public float revealRadius = 10f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jumpPeriod = 0.8f;
        landPeriod = 0.5f;
        //ItemScript.isPicking = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (PauseMenu.isPause == false)
        {

            HPBar.value = instance.getHealth();
            if (instance.getHealth() <= 0 && !isDeath)
            {
                Cursor.lockState = CursorLockMode.None;
                isDeath = true;
                _animator.SetBool("isDeath", true);
                StartCoroutine(waitDeath());
            }

            if (WinScreen.isWin && !isWinning)
            {
                Cursor.lockState = CursorLockMode.None;
                isWinning = true;
                _animator.SetBool("isWin", true);
                StartCoroutine(waitWin());

            }
            //if (Input.GetKeyDown(KeyCode.O))
            //{
            //    Debug.Log("Win bang");
            //}


            if (Input.GetKeyDown(KeyCode.M) && isShowHUD == false)
            {
                isShowHUD = true;
                showHUD.SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.M) && isShowHUD == true)
            {
                isShowHUD = false;
                showHUD.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.V))
            {
                _animator.SetBool("isDodge", true);
                StartCoroutine(Dodge());
            }
            else
            {
                _animator.SetBool("isDodge", false);
            }

            if (Input.GetKeyDown(KeyCode.C))
            {

                if (ItemScript.isPicking == true)
                {
                    _animator.SetBool("isPicking", true);
                    StartCoroutine(waitPopUp());
                }
                else if (NPCController.isTalking)
                {

                }

                else if (MissionScript.NPCCount == 3 && isShowing3 == false)
                {
                    isShowing3 = true;
                    textMission.SetText("Mission Success!");
                    StartCoroutine(waitPopUp());
                }
            }
            else
            {
                _animator.SetBool("isPicking", false);
                ItemScript.isPicking = false;
                //_itemAnimator.SetBool("isShowing", false);
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                if (Inventory.meat.getAmount() == 0 && Inventory.potion.getAmount() == 0)
                {
                    _animator.SetBool("isEating", false);
                }
                else
                {
                    _animator.SetBool("isEating", true);

                    if (meatActive.activeSelf)
                    {
                        if (Inventory.meat.getAmount() == 0)
                        {

                        }
                        else
                        {
                            Inventory.meat.setAmount(Inventory.meat.getAmount() - 1);

                            itemAmount.SetText(Inventory.meat.getAmount().ToString());
                            StartCoroutine(meatEating());
                        }
                    }
                    else if (potionActive.activeSelf)
                    {
                        if (Inventory.potion.getAmount() == 0)
                        {

                        }
                        else
                        {
                            Inventory.potion.setAmount(Inventory.potion.getAmount() - 1);

                            itemAmount.SetText(Inventory.potion.getAmount().ToString());
                            StartCoroutine(potionEating());

                        }
                    }
                }
            }
            else
            {
                _animator.SetBool("isEating", false);
            }

            if (Input.GetKey(KeyCode.Space) && _animator.GetBool("isGrounded"))
            {
                Debug.Log(_animator.GetBool("isGrounded"));
                ySpeed = jumpForce;
                jumpTime = Time.time;
                _animator.SetBool("isJumping", true);
                _animator.SetBool("isGrounded", false);
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }

            if (Time.time >= jumpTime + jumpPeriod)
            {
                //land.Play();
                _animator.SetBool("isJumping", false);
                _animator.SetBool("isGrounded", true);
                jumpTime = 0f;

            }
            float verticalAxis = Input.GetAxis("Vertical");

            // A -> ke kanan sampe 1
            // D -> ke kiri sampe 1
            // Range -1 1
            float horizontalAxis = Input.GetAxis("Horizontal");


            // Vector3 (x, y, z)
            movement = verticalAxis * transform.forward + horizontalAxis * transform.right;


            if (movement.magnitude != 0)
            {

                footStep.enabled = true;
                footStep.pitch = 0.4f;
                if (Input.GetKeyDown(KeyCode.V))
                {
                    _animator.SetBool("isDodge", true);
                    StartCoroutine(Dodge());
                }
                else
                {
                    _animator.SetBool("isDodge", false);
                }

                _animator.SetBool("isMoving", true);
                float targetAngle = cam.eulerAngles.y;



                if (Input.GetKey(KeyCode.LeftShift) && staminaBar.value != 0)
                {
                    footStep.pitch = 1f;
                    if (Input.GetKeyDown(KeyCode.V))
                    {
                        _animator.SetBool("isDodge", true);
                    }
                    else
                    {
                        _animator.SetBool("isDodge", false);
                    }

                    _animator.SetBool("isRunning", true);

                    if (Input.GetKey(KeyCode.Space) && _animator.GetBool("isGrounded"))
                    {
                        ySpeed = jumpForce;
                        jumpTime = Time.time;
                        _animator.SetBool("isJumping", true);
                        _animator.SetBool("isGrounded", false);
                        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    }

                    if (Time.time >= jumpTime + jumpPeriod)
                    {
                        //land.Play();
                        _animator.SetBool("isJumping", false);
                        _animator.SetBool("isGrounded", true);
                        jumpTime = 0f;

                    }

                    if (Input.GetKey("w") && Input.GetKey("d"))
                    {
                        Vector3 moveDir = Quaternion.Euler(0f, targetAngle + 45, 0f) * Vector3.forward;
                        transform.position += moveDir.normalized * (movementSpeed * 2);
                    }

                    else if (Input.GetKey("w") && Input.GetKey("a"))
                    {
                        Vector3 moveDir = Quaternion.Euler(0f, targetAngle - 45, 0f) * Vector3.forward;
                        transform.position += moveDir.normalized * (movementSpeed * 2);
                    }

                    else if (Input.GetKey("s") && Input.GetKey("d"))
                    {
                        Vector3 moveDir = Quaternion.Euler(0f, targetAngle + 45, 0f) * Vector3.back;
                        transform.position += moveDir.normalized * (movementSpeed * 2);
                    }

                    else if (Input.GetKey("s") && Input.GetKey("a"))
                    {
                        Vector3 moveDir = Quaternion.Euler(0f, targetAngle - 45, 0f) * Vector3.back;
                        transform.position += moveDir.normalized * (movementSpeed * 2);
                    }

                    else if (Input.GetKey("w"))
                    {
                        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                        transform.position += moveDir.normalized * (movementSpeed * 2);
                    }

                    else if (Input.GetKey("s"))
                    {
                        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.back;
                        transform.position += moveDir.normalized * (movementSpeed * 2);
                    }
                    else if (Input.GetKey("a"))
                    {
                        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.left;
                        transform.position += moveDir.normalized * (movementSpeed * 2);
                    }
                    else if (Input.GetKey("d"))
                    {
                        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.right;
                        transform.position += moveDir.normalized * (movementSpeed * 2);
                    }

                }
                else
                {

                    if (Input.GetKeyDown(KeyCode.V))
                    {
                        _animator.SetBool("isDodge", true);
                        StartCoroutine(Dodge());
                    }
                    else
                    {
                        _animator.SetBool("isDodge", false);
                    }

                    _animator.SetBool("isRunning", false);
                    if (Input.GetKey("w") && Input.GetKey("d"))
                    {
                        Vector3 moveDir = Quaternion.Euler(0f, targetAngle + 45, 0f) * Vector3.forward;
                        transform.position += moveDir.normalized * movementSpeed;
                    }

                    else if (Input.GetKey("w") && Input.GetKey("a"))
                    {
                        Vector3 moveDir = Quaternion.Euler(0f, targetAngle - 45, 0f) * Vector3.forward;
                        transform.position += moveDir.normalized * movementSpeed;
                    }

                    else if (Input.GetKey("s") && Input.GetKey("d"))
                    {
                        Vector3 moveDir = Quaternion.Euler(0f, targetAngle + 45, 0f) * Vector3.back;
                        transform.position += moveDir.normalized * movementSpeed;
                    }

                    else if (Input.GetKey("s") && Input.GetKey("a"))
                    {
                        Vector3 moveDir = Quaternion.Euler(0f, targetAngle - 45, 0f) * Vector3.back;
                        transform.position += moveDir.normalized * movementSpeed;
                    }

                    else if (Input.GetKey("w"))
                    {
                        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                        transform.position += moveDir.normalized * movementSpeed;
                    }

                    else if (Input.GetKey("s"))
                    {
                        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.back;
                        transform.position += moveDir.normalized * movementSpeed;
                    }
                    else if (Input.GetKey("a"))
                    {
                        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.left;
                        transform.position += moveDir.normalized * movementSpeed;
                    }
                    else if (Input.GetKey("d"))
                    {
                        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.right;
                        transform.position += moveDir.normalized * movementSpeed;
                    }

                }

            }
            else
            {
                _animator.SetBool("isMoving", false);
                footStep.enabled = false;
            }

            // Handle animation
            _animator.SetFloat("inputY", verticalAxis);
            _animator.SetFloat("inputX", horizontalAxis);
        }

        else
        {
            
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            _animator.SetBool("isDodge", true);
            StartCoroutine(Dodge());
        }
        else
        {
            _animator.SetBool("isDodge", false);
        }

        IEnumerator Dodge()
        {
            float duration = 0.8f;
            float speed = 1f;
            float distance = 2f;

            Vector3 startPosition = transform.position;
            Vector3 endPosition = transform.position - transform.forward * distance;

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


    IEnumerator meatEating()
    {
        yield return new WaitForSeconds(0.1f);
        instance.setFullStamina();
    }

    IEnumerator potionEating()
    {
        yield return new WaitForSeconds(0.1f);
        instance.setHalfHP();
    }

    IEnumerator waitDeath()
    {
        yield return new WaitForSeconds(3.8f);
        loseScreen.SetActive(true);
        loseTimeline.Play();
        yield return new WaitForSeconds(1.5f);
        instance.setHealth(100f);
    }

    IEnumerator waitWin()
    {
        yield return new WaitForSeconds(3.3f);
        winScreen.SetActive(true);
        winTimeline.Play();
    }

    IEnumerator waitPopUp()
    {
        Debug.Log("Masuk ga popup");
        _itemAnimator.SetBool("isShowing", true);
        yield return new WaitForSeconds(2f);
        _itemAnimator.SetBool("isShowing", false);
    }
}
