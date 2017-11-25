using UnityEngine;
using System.Collections;

public class badBubbleSmall : MonoBehaviour {
	
	scoreController score;
	private int speed = 300;
	private float randomPositionYStar;
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
		randomPositionYStar = Random.Range (-500, 500);
		randomPositionXStar = Random.Range (-1600, -1700);
		
	}

    void OnEnable()
    {
        randomPositionYStar = Random.Range(-500, 500);
        randomPositionXStar = Random.Range(-1600, -1700);
    }

    // Update is called once per frame
    void Update () {
		
		float currentRotation = transform.localEulerAngles.z;
		float newRotation = currentRotation + 2f;
        transform.localEulerAngles = new Vector3(0f,0f,newRotation);

		Vector2 destination = new Vector2 (randomPositionXStar, randomPositionYStar);
        transform.position = Vector2.MoveTowards (transform.position, destination, speed * Time.deltaTime);
		
	}
	
}