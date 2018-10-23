using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public GameObject player;

    public GameObject[] projectile;
    private int currentProjectile;

    public delegate void SpaceBarPress();
    public static event SpaceBarPress OnSpaceBarPress;

    private Vector3 startingPoint;

    private float smoothing = 0.1f;

	// Use this for initialization
	void Awake () {
        currentProjectile = 0;
        startingPoint = projectile[currentProjectile].GetComponent<Transform>().position;
        player.SendMessage("SetProjectile", projectile[currentProjectile]);
    }

    // Update is called once per frame
    void Update () {
        MovePlayer(Input.GetAxis("Horizontal") * smoothing);
	}

    private void MovePlayer(float num)
    {
        player.transform.Translate(num, 0, 0);
    }

    private void ResetObjectPosition()
    {
        projectile[currentProjectile].transform.position = startingPoint;
        projectile[currentProjectile].GetComponent<Rigidbody>().velocity = Vector3.zero;
        projectile[currentProjectile].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        projectile[currentProjectile].GetComponent<Transform>().rotation = Quaternion.identity;
    }

    IEnumerator SetUpNextThrow(float time)
    {
        if (OnSpaceBarPress != null)
        {
            OnSpaceBarPress();
        }

        Debug.Log("Resetting throw");
        yield return new WaitForSeconds(time);

        if (currentProjectile == projectile.Length - 1)
        {
            currentProjectile = 0;
        }
        else
        {
            currentProjectile++;
        }

        player.SendMessage("SetProjectile", projectile[currentProjectile]);
        ResetObjectPosition();
        player.SendMessage("Reset");



    }
}
