

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Animator mAnimator;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseEnter()
    {
        mAnimator.SetTrigger("TrHover");
        audioSource.volume = 1f;
        audioSource.Play();
    }
    
    private void OnMouseExit()
    {
        mAnimator.SetTrigger("TrIdle");
        audioSource.Stop();
    }
}
