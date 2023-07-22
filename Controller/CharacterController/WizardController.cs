using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WizardController : MonoBehaviour
{

    //[SerializeField] private Animator _animator = null;
    //public Transform cam;

    private PlayerObject instance = PlayerObject.getInstance();
    public GameObject wizardObj, loadingCanvas;
    public Light wizardSpotlight;
    public Light centerLight;
    public TextMeshPro wizardTxt;
    public Button stageBtn;
    public Camera camera;
    public GameObject particleEffects;

    // Start is called before the first frame update
    void Start()
    {
        wizardSpotlight.enabled = false;
        centerLight.enabled = false;
        wizardTxt.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseEnter()
    {
        wizardSpotlight.enabled = true;
        centerLight.enabled = true;
        wizardTxt.enabled = true;
        particleEffects.SetActive(true);
    }

    private void OnMouseExit()
    {
        wizardSpotlight.enabled = false;
        centerLight.enabled = false;
        wizardTxt.enabled = false;
        particleEffects.SetActive(false);
    }

    private void OnMouseDown()
    {
        instance.setIdx(0);
        loadingCanvas.SetActive(true);
        ExecuteEvents.Execute(stageBtn.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
    }
}
