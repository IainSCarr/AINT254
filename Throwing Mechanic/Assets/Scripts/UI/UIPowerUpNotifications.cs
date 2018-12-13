using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPowerUpNotifications : MonoBehaviour {

    public GameObject notification;
    private Text notificationText;


    // Use this for initialization
    void Start () {
        notificationText = notification.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowNotification(string text, PowerUpType type)
    {
        if (notification.activeSelf)
        {
            StartCoroutine(Wait(text, type));
        }
        else
        {
            if (type == PowerUpType.Good)
            {
                notificationText.color = Color.green;
            }
            else if (type == PowerUpType.Bad)
            {
                notificationText.color = Color.red;

            }
            else
            {
                notificationText.color = Color.yellow;
            }

            notificationText.text = text;
            notification.SetActive(true);
        }
    }

    IEnumerator Wait(string text, PowerUpType type)
    {
        yield return new WaitForSeconds(1f);
        ShowNotification(text, type);
        yield break;
    }

    void HandleOnPowerUpActivated(string name, PowerUpType type)
    {
        ShowNotification(name, type);
    }

    void HandleOnPowerDeactived(string name)
    {

    }


    void OnEnable()
    {
        PowerUp.OnPowerUpActivated += HandleOnPowerUpActivated;
        PowerUp.OnPowerUpDeactivated += HandleOnPowerDeactived;
    }

    void OnDisable()
    {
        PowerUp.OnPowerUpActivated -= HandleOnPowerUpActivated;
        PowerUp.OnPowerUpDeactivated -= HandleOnPowerDeactived;
    }
}
