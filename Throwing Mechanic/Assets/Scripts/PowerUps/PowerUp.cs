using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour {

    protected string label;
    protected bool isActive;
    protected float resetTime;

    public abstract void Activate();
    public abstract void Deactivate();

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
}
