using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public struct PooledObject
{
    public string name;
    public GameObject prefab;
    public int amount;
}


public class PoolManager : MonoBehaviour
{

    public static PoolManager current;

    public PooledObject[] pooledObjects;

    private Hashtable mainPool = new Hashtable();

    private List<GameObject> tempList;

    void OnEnable()
    {
        current = this;
    }

    void Start()
    {
        tempList = new List<GameObject>();

        for (int i = 0; i < pooledObjects.Length; i++)
        {
            List<GameObject> objList = new List<GameObject>();

            for (int j = 0; j < pooledObjects[i].amount; j++)
            {
                GameObject obj = (GameObject)Instantiate(pooledObjects[i].prefab);
                obj.SetActive(false);
                objList.Add(obj);
            }

            mainPool.Add(pooledObjects[i].name, objList);
        }
    }

    public GameObject GetPooledObject(string name)
    {
        if (mainPool.ContainsKey(name))
        {
            tempList = mainPool[name] as List<GameObject>;

            for (int i = 0; i < tempList.Count; i++)
                if (tempList[i] != null)
                    if (!tempList[i].activeInHierarchy)
                        return tempList[i];
        }
        return null;
    }


    public void ResetPool()
    {
        for (int i = 0; i < tempList.Count; i++)
            if (tempList[i] != null)
                if (tempList[i].activeInHierarchy)
                    tempList[i].SetActive(false);
    }
}