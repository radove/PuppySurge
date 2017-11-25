using UnityEngine;
using System.Collections;

public class bossSmall : MonoBehaviour {
	
	scoreController score;
	public int speed = 1;
	private float randomPositionYStar;
	private float randomPositionXStar;
    private GameObject player;
	
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
		randomPositionYStar = Random.Range (-1600, 2000);
		randomPositionXStar = Random.Range (-2500, -2600);
		GameObject bossGameObject = GameObject.FindWithTag ("bossBubble");
		if (bossGameObject != null) {
			this.transform.parent = bossGameObject.transform;
        }
        player = GameObject.FindGameObjectWithTag("Player");

    }
	
	// Update is called once per frame
	void Update () {
		
		//this.gameObject.transform.localEulerAngles = new Vector3(0f,0f,-270f);

		//float currentRotation = this.gameObject.transform.localEulerAngles.z;
		//float newRotation = currentRotation + 0.2f;
		//this.gameObject.transform.localEulerAngles = new Vector3(0f,0f,newRotation);
		
		Vector2 destination = new Vector2 (randomPositionXStar, randomPositionYStar);
        
		this.gameObject.transform.position = Vector2.MoveTowards (this.gameObject.transform.position, player.transform.position, 200 * Time.deltaTime);



	}
	
	void OnBecameInvisible() {
		//Destroy(gameObject);
	}
	
	void OnDestroy () {
		//Debug.Log ("Destroyed starFall");
	}
	
	/** 
	 * 			if (g.name.Equals("targetArea")) {
				// Get Current X/Y:
				float currentX = g.transform.localScale.x;
				float currentRotation = g.transform.localEulerAngles.z;
				float damageDone = Random.Range (5,10);
				float rotationProcess = Random.Range (0,2);
				float newX = currentX - damageDone;
				float newRotation = currentRotation + rotationProcess;
				
				if (newX > 0)
				{
					g.transform.localScale = new Vector3(newX,100f,1f);
					g.transform.localEulerAngles = new Vector3(0f,0f,newRotation);
					g.transform.renderer.material.color = Color.yellow;
				}
				else
				{
					g.transform.localScale = new Vector3(1000f,100f,1f);
					g.transform.localEulerAngles = new Vector3(0f,0f,0f);
					g.transform.renderer.material.color = Color.green;
					float randomPositionX = Random.Range (-1500,1500);
					float randomPositionY = Random.Range (0,800);
					g.transform.position = new Vector3(randomPositionX,randomPositionY,0);
				}
				
			}
**/
}