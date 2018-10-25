using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public GameObject player;

    public GameObject[] projectile;
    private int currentProjectile;

    public delegate void SpaceBarPress();
    public static event SpaceBarPress OnSpaceBarPress;

    private Vector3 projectilePosition;

    private float smoothing = 0.05f;

	// Use this for initialization
	void Awake () {
        currentProjectile = 0;
        projectilePosition = projectile[currentProjectile].GetComponent<Transform>().position;
        player.SendMessage("SetProjectile", projectile[currentProjectile]);
    }

    // Update is called once per frame
    void Update () {
        MovePlayer(Input.GetAxis("Horizontal") * smoothing);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log(currentProjectile);
            projectile[currentProjectile].SendMessage("Explode");
            //if (currentProjectile == 0)
            //{
            //    projectile[0].SendMessage("Explode");
            //}
            //else
            //{
            //    projectile[currentProjectile - 1].SendMessage("Explode");
            //}
        }
    }

    private void MovePlayer(float num)
    {
        player.transform.Translate(num, 0, 0);
        projectilePosition.x = player.transform.position.x;
        player.GetComponent<ShowTrajectory>().UpdateProjectilePosition(projectilePosition);
    }

    private void ResetObjectPosition()
    {
        projectile[currentProjectile].transform.position = projectilePosition;
        projectile[currentProjectile].GetComponent<Rigidbody>().isKinematic = true;
        projectile[currentProjectile].GetComponent<Rigidbody>().velocity = Vector3.zero;
        projectile[currentProjectile].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        projectile[currentProjectile].GetComponent<Transform>().rotation = Quaternion.identity;
    }

    IEnumerator SetUpNextThrow(float time)
    {
        yield return new WaitForSeconds(time);

        if (OnSpaceBarPress != null)
        {
            OnSpaceBarPress();
        }

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
