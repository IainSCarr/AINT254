using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Text powerup;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void PowerUpNotification(string name)
    {
        powerup.text = name + " ACTIVATED";
        StartCoroutine(WaitSeconds(5));
    }

    IEnumerator WaitSeconds(int num)
    {
        yield return new WaitForSeconds(num);
        powerup.text = "";
    }
}
