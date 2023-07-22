using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    private LinkedList<string> inputs;

    private PlayerObject instance = PlayerObject.getInstance();
    public Animator _itemAnimator;
    private const string HEAL_CODE = "hesoyam";
    private const string RATAAMPAS_CODE = "rataampas221";
    private const string SPEED_CODE = "ihateyou";
    private const string DEATH_CODE = "iloveyou";
    private const string FLIGHT_CODE = "icanfly";
    private const string SWITCH_CODE = "budi";
    private const string MAZE_CODE = "iseedeadpeople";
    private const string WIN_CODE = "akusayangangkatan221";
    public static bool isFlyCheat = false;
    public TextMeshProUGUI info;
    private void Start()
    {
        inputs = new LinkedList<string>();
    }

    private void Update()
    {
        if (!Input.anyKeyDown)
            return;

        var currentInput = Input.inputString.ToLower();

        inputs.AddLast(currentInput);
        if (inputs.Count > 30)
            inputs.RemoveFirst();

        var isCheatCode = false;
        var inputString = string.Join("", inputs);

        if (inputString.Contains("hesoyam"))
        {
            instance.setFullHP();
            instance.setFullStamina();
            isCheatCode = true;
        }

        else if (inputString.Contains("iloveyou"))
        {
            instance.setHealth(0f);
            isCheatCode = true;
        }

        else if (inputString.Contains("rataampas221"))
        {
            WizardSkillScript.damageBasicAttack = 1000f;
            PaladinSkillScript.damageBasicAttack = 1000f;
            isCheatCode = true;
        }

        else if (inputString.Contains("ihateyou"))
        {
            WizardAnimation.movementSpeed = 0.08f;
            PaladinAnimation.movementSpeed = 0.5f;
            isCheatCode = true;
        }

        else if (inputString.Contains("akusayangangkatan221"))
        {
            WinScreen.isWin = true;
            isCheatCode = true;
        }

        else if (inputString.Contains("icanfly"))
        {
            isFlyCheat = true;
            isCheatCode = true;
        }
        //if (inputString.Contains(HEAL_CODE))
        //{
        //    Datastore.Get().PlayerCharacter.OnHesoyam();
        //    isCheatCode = true;
        //}
        //else if (inputString.Contains(RATAAMPAS_CODE))
        //{
        //    Datastore.Get().PlayerCharacter.AttackDamage = 100;
        //    isCheatCode = true;
        //}
        //else if (inputString.Contains(SPEED_CODE))
        //{
        //    Datastore.Get().PlayerCharacter.MovementSpeed += 3;
        //    Datastore.Get().PlayerCharacter.SprintSpeed += 3;
        //    isCheatCode = true;
        //}
        //else if (inputString.Contains(DEATH_CODE))
        //{
        //    Datastore.Get().PlayerCharacter.OnDamage(100);
        //    isCheatCode = true;
        //}
        //else if (inputString.Contains(FLIGHT_CODE))
        //{
        //    var character = Datastore.Get().PlayerCharacter;
        //    if (character is Wizard)
        //    {
        //        // todo: add flight duration
        //    }

        //    isCheatCode = true;
        //}
        //else if (inputString.Contains(SWITCH_CODE))
        //{
        //    var character = Datastore.Get().PlayerCharacter;

        //    Vector3 position = default;
        //    if (!ReferenceEquals(character, null))
        //    {
        //        position = character.characterModel.transform.position;
        //        Destroy(character.characterModel);
        //    }

        //    Datastore.Get().PlayerCharacter = character switch
        //    {
        //        Wizard => new Paladin(),
        //        Paladin => new Wizard(),
        //        _ => Datastore.Get().PlayerCharacter
        //    };

        //    MasterScript.WorldScript.SpawnPlayer(position);
        //    isCheatCode = true;
        //}
        //else if (inputString.Contains(MAZE_CODE))
        //{
        //    // todo: add maze map
        //    isCheatCode = true;
        //}
        //else if (inputString.Contains(WIN_CODE))
        //{
        //    if (MasterScript.Get() is GameMasterScript)
        //    {
        //        Debug.Log("is game");
        //        ((GameMasterScript)MasterScript.Get()).MissionManager.Complete();
        //    }
        //    isCheatCode = true;
        //}

        if (!isCheatCode)
            return;

        inputs.Clear();
        StartCoroutine(waitPopUp());
        info.SetText("Cheat Activated!");
        //MasterScript.BottomNotification.SetText("Cheat code activated!");
        //MasterScript.BottomNotification.Show();
        //StartCoroutine(DelayedHideNotification());
    }

    private IEnumerator DelayedHideNotification()
    {
        yield return new WaitForSeconds(3);
        //MasterScript.BottomNotification.Hide();
    }

    public IEnumerator waitPopUp()
    {
        _itemAnimator.SetBool("isShowing", true);
        yield return new WaitForSeconds(2f);
        _itemAnimator.SetBool("isShowing", false);
    }
}
