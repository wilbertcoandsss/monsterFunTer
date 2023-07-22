using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GraphicController : MonoBehaviour
{
    public TMP_Dropdown graphicDropdown;
    // Start is called before the first frame update
    void Start()
    {
        string[] graphicsOptions = QualitySettings.names;
        graphicDropdown.AddOptions(new List<string>(graphicsOptions));

        graphicDropdown.value = QualitySettings.GetQualityLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGraphicsDropdownChanged(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }
}
