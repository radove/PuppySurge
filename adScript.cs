using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements; // Using the Unity Ads namespace.

public class adScript : MonoBehaviour
{	
	void Start() {
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }
	}
}