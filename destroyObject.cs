using UnityEngine;
using System.Collections;

public class destroyObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(LateCall());
    }
    
    void OnEnable()
    {
        StartCoroutine(LateCall());
    }

    IEnumerator LateCall()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

}
