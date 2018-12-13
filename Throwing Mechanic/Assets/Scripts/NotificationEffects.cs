using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationEffects : MonoBehaviour {

    private Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}

    void OnEnable()
    {
        iTween.ScaleFrom(gameObject, iTween.Hash("x", 0, "y", 0, "easetype", iTween.EaseType.easeOutBounce));
        Invoke("FadeOut", 1.5f);
    }

    private void FadeOut()
    {
        Invoke("DisableText", 0.6f);
        text.CrossFadeAlpha(0, 0.5f, false);
    }

    private void DisableText()
    {
        gameObject.SetActive(false);
    }

    public void SetText(string newText)
    {
        text.text = newText;
    }
}
