using UnityEngine;
using System.Collections;

public class starFall : MonoBehaviour {

	scoreController score;
	public int speed;
	private float randomPositionXStar;

	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null)
		{
			score = gameControllerObject.GetComponent <scoreController>();
		}
		if (score == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
		randomPositionXStar = Random.Range (-2500, -2600);

	}
	
	// Update is called once per frame
	void Update () {

		float currentRotation = this.transform.localEulerAngles.z;
		float newRotation = currentRotation + 2f;
		this.transform.localEulerAngles = new Vector3(0f,0f,newRotation);

		float randomPositionYStar = this.transform.position.y;
		Vector2 destination = new Vector2 (randomPositionXStar, this.transform.position.y);
		this.transform.position = Vector2.MoveTowards (this.transform.position, destination, speed * Time.deltaTime);

	}
}
