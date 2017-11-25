using UnityEngine;
using System.Collections;

public class alignGUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0,1,0));
	}
}
