using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PaladinController : MonoBehaviour
{
    private PlayerObject instance = PlayerObject.getInstance();
    public Light paladinSpotlight;
    public Light centerLight;
    public TextMeshPro txtPaladin;
    public Button stageBtn;
    public GameObject loadingCanvas;
    public GameObject particleEffects;
    // Start is called before the first frame update
    void Start()
    {
        paladinSpotlight.enabled = false;
        centerLight.enabled = false;
        txtPaladin.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseEnter()
    {
        paladinSpotlight.enabled = true;
        centerLight.enabled = true;
        txtPaladin.enabled = true;
        particleEffects.SetActive(true);
    }

    private void OnMouseExit()
    {
        paladinSpotlight.enabled = false;
        centerLight.enabled = false;
        txtPaladin.enabled = false;
        particleEffects.SetActive(false);
    }
    private void OnMouseDown()
    {
        instance.setIdx(1);
        loadingCanvas.SetActive(true);
        ExecuteEvents.Execute(stageBtn.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
    }
}
