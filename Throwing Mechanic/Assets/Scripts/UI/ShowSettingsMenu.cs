using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSettingsMenu : MonoBehaviour {

    public GameObject options;
    public GameObject settings;

    public void DisplaySettings()
    {
        options.SetActive(false);
        settings.SetActive(true);
    }

    public void HideSettings()
    {
        options.SetActive(true);
        settings.SetActive(false);
    }
}
