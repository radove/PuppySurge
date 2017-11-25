using UnityEngine;
using System.Collections;

public class genericBullet : MonoBehaviour {

	private Vector2 movement;
	public scoreController score;
	private Vector2 playerLocation;
	public int speed = 1500;
	public GameObject smallExplosion;
	public GameObject smallExplosionSound;

    // Use this for initialization
    void Start () {
		//GetComponent<Rigidbody2D>().velocity = movement;
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null)
		{
			score = gameControllerObject.GetComponent <scoreController>();
		}
		if (score == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		playerLocation = player.transform.position;
        //GetComponent<Rigidbody2D>().AddForce ((player.transform.position - transform.position).normalized * Time.smoothDeltaTime * 350);
    }
	
	// Update is called once per frame
	
	void Update () {
		this.transform.position = Vector2.MoveTowards (this.transform.position, playerLocation, speed * Time.deltaTime);
		
		if (this.transform.position.x == playerLocation.x && this.transform.position.y == playerLocation.y) {
			Instantiate(smallExplosionSound, gameObject.transform.position, Quaternion.identity);
			Vector3 position = gameObject.transform.position;
			Destroy(gameObject);
			GameObject boom = (GameObject)Instantiate(smallExplosion, position, Quaternion.identity);
		}
		
	}
	void OnBecameInvisible() {
		Destroy(gameObject);
	}
}