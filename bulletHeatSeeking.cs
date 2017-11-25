using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class bulletHeatSeeking : MonoBehaviour
{
    private Vector2 destination;
    private List<GameObject> objectsInScene;
    private bool fixatedTarget = false;
    private bool completed = false;
    private GameObject targetGameObject;
    
    void Start () {
       
        //objectsInScene = FindGameObjectsWithTags(GenericObjectPooler.current.getEnemyTags());
        StartCoroutine(TargetObjectProcess());
    }

    List<GameObject> FindGameObjectsWithTags(List<string> tags)
    {
        List<GameObject> gameObjectList = new List<GameObject>();
        for (int i = 0; i < tags.Count; i++)
        {
            string tag = tags[i];
            GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
            gameObjectList.AddRange(objects);
        }
        return gameObjectList;

    }

    // Update is called once per frame
    void Update ()
    {
        if (targetGameObject != null)
        {
            Debug.Log("Still Trailing: " + targetGameObject.name);
            this.transform.position = Vector2.MoveTowards(this.transform.position, targetGameObject.transform.position, 1500 * Time.deltaTime);
        } else
        {
            fixatedTarget = false;
        }
        //rigidBody.velocity = (destination - transform.position).normalized * 2000;
        //rigidBody.MovePosition(rigidBody.position + defaultSpeed * Time.deltaTime);
    }

    IEnumerator TargetObjectProcess()
    {
        for (int x = 0; x < objectsInScene.Count; x++)
        {
            targetGameObject = objectsInScene[x];
            if (targetGameObject != null) {
                fixatedTarget = true;
            }
            while (fixatedTarget)
            {
                yield return new WaitForSeconds(1f);
            }
        }
        completed = true;
    }

}
