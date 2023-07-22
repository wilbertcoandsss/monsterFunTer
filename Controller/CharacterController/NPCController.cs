using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public NavMeshAgent agent;
    public float speed;
    public float idleTime = 2f;
    public float walkRadius;
    public Animator animator;

    private PlayerObject instance = PlayerObject.getInstance();

    private enum NpcState { Idle, Moving, Interact };
    private NpcState currentState = NpcState.Idle;
    private float idleTimer;

    public GameObject interactiveText;

    private GameObject character;
    public GameObject wizard, paladdin;
    public GameObject talkContainer;

    public static bool firstOneDialog = false;
    public static bool firstTwoDialog = false;

    public static bool secondOneDialog = false;
    public static bool secondTwoDialog = false;

    public static bool thirdOneDialog = false;
    public static bool thirdTwoDialog = false;

    public static bool fourthOneDialog = false;
    public static bool fourthTwoDialog = false;

    public static bool remyOneDialog = false;
    public static bool remySecondDialog = false;

    public static bool astraOneDialog = false;
    public static bool astraSecondDialog = false;

    public static bool firstMetLyra = false;
    public static bool firstMetRemy = false;
    public static bool firstMetAstra = false;

    public static bool firstTalkLyra = false;
    public static bool firstTalkRemy = false;
    public static bool firstTalkAstra = false;

    [Header("Text Settings")]
    [SerializeField] [TextArea] private string[] dialog;
    [SerializeField] private float textSpeed = 0.01f;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI dialogText;
    private int currentDisplayingText = 0;

    public static bool isTalking = false;

    public static bool secondSteps = false;

    public static bool firstSteps = false;

    public static bool thirdSteps = false;

    public static bool fourthSteps = false;
    void Start()
    {
        Debug.Log("Instance siapa " + instance.getIdx());
        animator.SetBool("isWalking", false);

        if (instance.getIdx() == 0)
        {
            character = wizard;
        }
        else
        {
            character = paladdin;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case NpcState.Idle:
                //Debug.Log("Masuk idle ga ni bang")
                animator.SetBool("isWalking", false);
                Invoke("StateMove", 20f);
                break;

            case NpcState.Moving:
                //Debug.Log("Masuk moving ga ni bang")
                animator.SetBool("isWalking", true);
                SetDestination();
                break;

            case NpcState.Interact:
                //Debug.Log("Masuk interact ga ni bang")
                agent.SetDestination(transform.position);
                var lookPos = character.transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);
                animator.SetBool("isWalking", false);
                break;
        }
    }

    public void SetDestination(Vector3? target = null)
    {
        if (target == null)
        {
            Vector3 randomPosition = Random.insideUnitSphere * walkRadius;
            NavMesh.SamplePosition(randomPosition + transform.position, out NavMeshHit hit, walkRadius, 1);
            agent.SetDestination(hit.position);
            currentState = NpcState.Moving;
        }
        else
        {
            agent.SetDestination((Vector3)target);
            currentState = NpcState.Interact;
        }
    }

    public Vector3 RandomMeshLocation()
    {
        Vector3 finalPosition = Vector3.zero;
        Vector3 randomPosition = Random.insideUnitSphere * walkRadius;

        randomPosition += transform.position;
        if (NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, walkRadius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    private IEnumerator MoveTrigger()
    {
        while (true)
        {
            yield return new WaitForSeconds(4f);
            currentState = NpcState.Moving;
        }
    }
    public  void StateInteract()
    {
        currentState = NpcState.Interact;
    }

    public  void StateIdle()
    {
        currentState = NpcState.Idle;
    }

    public void StateMove()
    {
        currentState = NpcState.Moving;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            string[] str = new string[5];
            interactiveText.SetActive(true);
            StateInteract();

            if (gameObject.name == "Lyra")
            {
                if (firstMetLyra == false && firstTalkLyra == false)
                {
                    firstMetLyra = true;
                    firstTalkLyra = true;
                    MissionScript.NPCCount += 1;
                }
                if (Input.GetKeyDown(KeyCode.C) && firstOneDialog == false && firstSteps == false)
                {
                    isTalking = true;
                    firstOneDialog = true;
                    talkContainer.SetActive(true);
                    str[0] = "Oh hi there ! Must be a new heroes here right? [Press J to continue]";
                    ActivateText(str);
                }

                else if (Input.GetKeyDown(KeyCode.C) && firstOneDialog == true && firstSteps == false)
                {
                    isTalking = true;
                    talkContainer.SetActive(true);
                    str[0] = "Please show me your basic skills 10 times in order to see what your skills look like";
                    ActivateText(str);
                    MissionScript.defaultMission = false;
                    MissionScript.firstMission = true;
                }

                else if (Input.GetKeyDown(KeyCode.C) && secondSteps == true && secondOneDialog == false)
                {

                    isTalking = true;
                    secondOneDialog = true;
                    talkContainer.SetActive(true);
                    str[0] = "Great job ! I can see your basic skills. Really awesome!";
                    ActivateText(str);
                    MissionScript.secondMission = true;
                }

                else if (Input.GetKeyDown(KeyCode.C) && secondOneDialog == true && secondSteps == true)
                {
                    isTalking = true;
                    Debug.Log("Kedua ini");
                    talkContainer.SetActive(true);
                    str[0] = "Now, show me your advance skills!";
                    ActivateText(str);
                    MissionScript.firstMission = false;
                    MissionScript.secondMission = true;
                }

                else if (Input.GetKeyDown(KeyCode.C) && thirdSteps == true && thirdOneDialog == false)
                {
                    isTalking = true;
                    talkContainer.SetActive(true);
                    thirdOneDialog = true;
                    str[0] = "Really impressive! Your skills are really beautiful and mighty!";
                    ActivateText(str);
                    MissionScript.secondMission = false;
                    MissionScript.thirdMission = true;
                }

                else if (Input.GetKeyDown(KeyCode.C) && thirdOneDialog == true && thirdSteps == true)
                {
                    isTalking = true;
                    talkContainer.SetActive(true);
                    str[0] = "Now meet all of my friends here and talk to them!";
                    ActivateText(str);
                }

                else if (Input.GetKeyDown(KeyCode.C) && fourthSteps == true && fourthOneDialog == false)
                {
                    isTalking = true;
                    talkContainer.SetActive(true);
                    fourthOneDialog = true;
                    str[0] = "Awesome, now I thinks its the time...";
                    // Insert cutscene here
                    ActivateText(str);
                    MissionScript.thirdMission = false;
                    MissionScript.fourthMission = true;
                }

                else if(Input.GetKeyDown(KeyCode.C) && fourthSteps == true && fourthOneDialog == true)
                {
                    isTalking = true;
                    talkContainer.SetActive(true);
                    str[0] = "Go to the maze and kill the monster! Good luck my friends!";
                    ActivateText(str);
                }
                else
                {
                    isTalking = false;
                }
            }

            else if (gameObject.name == "Remy")
            {
                if (Input.GetKeyDown(KeyCode.C))
                {
                    talkContainer.SetActive(true);
                    
                    if (MissionScript.thirdMission == false)
                    {
                        str[0] = "Excuse me.. Do i know you ? Have you introduce yourself to Lyla?";
                        ActivateText(str);
                    }
                    else
                    {
                        if (firstMetRemy == false && firstTalkRemy == false)
                        {
                            firstMetRemy = true;
                            firstTalkRemy = true;
                            MissionScript.NPCCount += 1;
                        }

                        if (thirdOneDialog && thirdSteps && remyOneDialog == false)
                        {
                            remyOneDialog = true;
                            firstMetRemy = true;
                            str[0] = "Hi.. you must be new hero here right ?";
                            ActivateText(str);
                        }
                        else if (thirdOneDialog && remyOneDialog == true)
                        {
                            str[0] = "My name is Remy.. Nice to meet you ";
                            ActivateText(str);
                        }
                    }
                   
                }
            }

            else if (gameObject.name == "Astro")
            {
                if (Input.GetKeyDown(KeyCode.C))
                {
                    talkContainer.SetActive(true);

                    if (MissionScript.thirdMission == false)
                    {
                        str[0] = "Excuse me.. Do i know you ? Have you introduce yourself to Lyla?";
                        ActivateText(str);
                    }
                    else
                    {
                        if (firstMetAstra == false && firstTalkAstra == false)
                        {
                            firstMetAstra = true;
                            firstTalkAstra = true;
                            MissionScript.NPCCount += 1;
                        }

                        if (thirdOneDialog && thirdSteps && astraOneDialog == false)
                        {
                            firstMetAstra = true;
                            astraOneDialog = true;
                            str[0] = "Hi.. you must be new hero here right ?";
                            ActivateText(str);
                        }
                        else if (thirdOneDialog && astraOneDialog == true)
                        {
                            str[0] = "My name is Astra.. Nice to meet you ";
                            ActivateText(str);
                        }

                    }

                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        interactiveText.SetActive(false);
        talkContainer.SetActive(false);
        StateIdle();
    }

    public void ActivateText(string[] context)
    {
        StartCoroutine(AnimateText(context));
    }

    IEnumerator AnimateText(string[] context)
    {
        for (int i = 0; i < context[currentDisplayingText].Length + 1; i++)
        {
            dialogText.text = context[currentDisplayingText].Substring(0, i);
            yield return new WaitForSeconds(textSpeed);
        }
    }
}

