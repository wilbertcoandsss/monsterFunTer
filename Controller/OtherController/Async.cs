using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Async : MonoBehaviour
{
    public int sceneIdx;
    public Slider slider;
    public GameObject loadingScreen;

    public GameObject loadingScreenfab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadSceneAsync()
    {
        StartCoroutine(LoadAsync(sceneIdx));
    }
    private IEnumerator LoadAsync(int sceneIdx)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIdx);
        loadingScreen.SetActive(true);

        GameObject progressBar = Instantiate(loadingScreenfab, loadingScreen.transform);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            yield return null;
        }
    }
}
