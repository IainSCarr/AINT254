using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class DisableOnAnimateFinish : MonoBehaviour {

    private Animation clip;
    private float clipLenth;

    private void Awake()
    {
        clip = GetComponent<Animation>();
        clipLenth = clip.clip.length;
        Debug.Log(clipLenth);
    }

    private void OnEnable()
    {
        StartCoroutine(DisableAfterTime());
    }

    IEnumerator DisableAfterTime()
    {
        yield return new WaitForSeconds(1f/6f);
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(2f/3f);
        Time.timeScale = 1f;
        yield return new WaitForSeconds(1f/6f);
        gameObject.SetActive(false);

        yield break;
    }
}
