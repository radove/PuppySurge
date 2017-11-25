using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GenericObjectPooler : MonoBehaviour {

    public static GenericObjectPooler current;

    public List<string> keys = new List<string>();
    public List<GameObject> values = new List<GameObject>();
    private Dictionary<string, GameObject> poolMapper = new Dictionary<string, GameObject>();
    private Dictionary<string, List<GameObject>> poolDataMapper = new Dictionary<string, List<GameObject>>();
    private Dictionary<string, DateTime> expirationDateTracker = new Dictionary<string, DateTime>();

    public int pooledAmount = 20;
    public bool willGrow = true;
    
    void Awake()
    {
        current = this;
    }
    
    void Start ()
    {
        poolMapper = new Dictionary<string, GameObject>();
        for (int i = 0; i < keys.Count; i++)
        {
            poolMapper.Add(keys[i], values[i]);
        }

        foreach (KeyValuePair<string, GameObject> keyValue in poolMapper)
            {
                List<GameObject> listOfObj = new List<GameObject>();
                for (int i = 0; i < pooledAmount; i++)
                {
                    GameObject obj = (GameObject)Instantiate(keyValue.Value);
                    obj.SetActive(false);
                    listOfObj.Add(obj);
                }
                poolDataMapper.Add(keyValue.Key, listOfObj);
        }
        //StartCoroutine(ageOff());
    }

    IEnumerator ageOff()
    {
        while (true)
        {
            int agedOffObjects = 0;
            yield return new WaitForSeconds(60f);
            Debug.Log("[GenericObjectPooler] Aging Off Objects");
            foreach (string key in expirationDateTracker.Keys)
            {
                DateTime lastAccessed = expirationDateTracker[key];
                TimeSpan diff = lastAccessed - DateTime.Now;
                if (diff.TotalSeconds > 60)
                {
                    List<GameObject> listOfObj = poolDataMapper[key];
                    if (listOfObj.Count > pooledAmount)
                    {
                        for (int i = 0; i < (listOfObj.Count - pooledAmount); i++)
                        {
                            if (!listOfObj[i].activeInHierarchy)
                            {
                                Destroy(listOfObj[i]);
                                listOfObj.Remove(listOfObj[i]);
                                agedOffObjects++;
                            }
                        }
                        poolDataMapper[key] = listOfObj;
                    }
                }
            }

            Debug.Log("[GenericObjectPooler] Aged Off " + agedOffObjects + "  Objects");
        }
    }
    public GameObject GetGenericGameObject(string key)
    {
        List<GameObject> listOfObj = poolDataMapper[key];

        for (int i = 0; i < listOfObj.Count; i++)
        {
            if (!listOfObj[i].activeInHierarchy)
            {
                return listOfObj[i];
            }
        }

        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(poolMapper[key]);
            listOfObj.Add(obj);
            poolDataMapper[key] = listOfObj;
            return obj;
        }
        return null;

    }
}
