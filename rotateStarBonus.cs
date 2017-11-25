using UnityEngine;
using System.Collections;

public class rotateStarBonus : MonoBehaviour {
    
	
	// Update is called once per frame
	void Update () {
		
		        //this.gameObject.transform.localEulerAngles = new Vector3(0f,0f,-270f);
		
		        float currentRotation = this.gameObject.transform.localEulerAngles.z;
		        float newRotation = currentRotation + -1f;
		        Vector3 newVectorRotation = new Vector3 (0f, 0f, newRotation);
		        transform.localEulerAngles = newVectorRotation;
	}
}
