using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType { Good, Bad, Random }

public abstract class PowerUp : MonoBehaviour {

    protected string label;
    protected PowerUpType type;
    protected bool isActive;
    protected float resetTime;
    protected ActivatePowerUps manager;

    public delegate void PowerUpActivated(string name, PowerUpType type);
    public static event PowerUpActivated OnPowerUpActivated;

    public delegate void PowerUpDeactivated(string name);
    public static event PowerUpDeactivated OnPowerUpDeactivated;

    protected abstract void Activate();
    protected abstract void Deactivate();



    public void Enable()
    {
        if (!isActive)
        {
            Activate();
            isActive = true;
            Debug.Log(label + " ACTIVATED");

            StartCoroutine(DisableAfterTime(resetTime));

            if (OnPowerUpActivated != null)
            {
                OnPowerUpActivated(label, type);
            }
        }
    }

    private IEnumerator DisableAfterTime(float time)
    {
        Debug.Log("Waiting");
        yield return new WaitForSeconds(resetTime);

        Deactivate();
        isActive = false;
        Debug.Log(label + " DEACTIVATED");

        manager.PowerUpDisabled(type);

        if (OnPowerUpDeactivated != null)
        {
            OnPowerUpDeactivated(label);
        }

        yield break;
    }

    public virtual bool GetIsPossible()
    {
        return true;
    }

    public string GetName()
    {
        return label;
    }

    public bool GetIsActive()
    {
        return isActive;
    }

    public float GetResetTime()
    {
        return resetTime;
    }

    public PowerUp SetManager(ActivatePowerUps parent)
    {
        manager = parent;
        return this;
    }
}
