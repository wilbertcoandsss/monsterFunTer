using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator _animator;
    private float timeLight = 0f;
    public Material skyboxDay;
    public Material skyboxNight;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - timeLight >= 20f)
        {
            if (!_animator.GetBool("goToCycle"))
            {
                _animator.SetBool("goToCycle", true);
                timeLight = Time.time;
            }
            if (_animator.GetBool("isDay"))
            {
                _animator.SetBool("isDay", false);
                timeLight = Time.time;
            }
            else
            {
                _animator.SetBool("isDay", true);
                timeLight = Time.time;
            }

        }

        float t = Time.time / 20f;
        RenderSettings.skybox = Mathf.PingPong(t, 1.0f) < 0.5f ? skyboxDay : skyboxNight;
    }
}
