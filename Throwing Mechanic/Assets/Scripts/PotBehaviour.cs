using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;
using UnityEngine.UI;

public class PotBehaviour : MonoBehaviour {

    private ObjectManager manager;
    private SelectedObjectHighlight imageManager;

    private GameObject[] objects;
    private Image[] objectImages;

    private string target;

    private SpriteRenderer render;

    private void Awake()
    {
        manager = FindObjectOfType<ObjectManager>();
        imageManager = FindObjectOfType<SelectedObjectHighlight>();
        objects = manager.GetObjects();
        objectImages = imageManager.GetImages();
        render = GetComponentInChildren<SpriteRenderer>();
    }

    // Use this for initialization
    void Start() {
        iTween.MoveFrom(gameObject, iTween.Hash("y", 20));

        int num = Random.Range(0, objects.Length);
        target = objects[num].name;

        render.sprite = objectImages[num].sprite;
    }

    public void SetRotatingTargets()
    {
        GetComponent<AutoMoveAndRotate>().enabled = true;
    }

    public void Hit()
    {
        Invoke("FlyAway", 1f);
        AudioManager.instance.PlaySound("TargetHit");
        SendMessageUpwards("ObjectDestroyed", transform.parent);
        SendMessage("DoSendScore");
    }

    private void FlyAway()
    {
        Invoke("Die", 2f);
        AudioManager.instance.PlaySound("TargetDeath");
        iTween.MoveTo(gameObject, iTween.Hash("y", 20, "time", 2f));
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        CancelInvoke();
    }

    public string GetTarget()
    {
        return target;
    }
}
