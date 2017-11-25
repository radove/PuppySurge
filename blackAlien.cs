using UnityEngine;
using System.Collections;

public class blackAlien : MonoBehaviour {
	private float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float randomPositionYStar = this.transform.position.y;
		float randomPositionXStar = Random.Range (-2500, -2600);
		Vector2 destination = new Vector2 (randomPositionXStar, randomPositionYStar);
		this.gameObject.transform.position = Vector2.MoveTowards (this.gameObject.transform.position, destination, speed * Time.deltaTime);

	}

	void setSpeed(float theSpeed) {
		speed = theSpeed;
	}
}
