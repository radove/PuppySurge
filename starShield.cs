using UnityEngine;
using System.Collections;

public class starShield : MonoBehaviour {

	private int speed = 600;
	
	private float randomPositionYStar = 0;
	private float randomPositionXStar = 0;
    private Vector2 originalSize;
    //private Collider2D gameCollider;


	// Use this for initialization
	void Start () {
		randomPositionYStar = Random.Range(1800, 1900);
        randomPositionXStar = this.transform.position.x;
        //gameCollider = gameObject.GetComponent<Collider2D>();
        //gameCollider.enabled = true;

    }

    void OnEnable()
    {
        gameObject.tag = "starShield";
        randomPositionYStar = Random.Range(1800, 1900);
        randomPositionXStar = this.transform.position.x;
       // gameCollider = gameObject.GetComponent<Collider2D>();
        //gameCollider.enabled = true;
    }
	
	// Update is called once per frame
	void Update () {
		if (gameObject.tag == "Untagged") {
           // gameCollider.enabled = false;
			transform.parent = null;
			//transform.localEulerAngles = new Vector3 (0, 0, 90);
			Vector2 destination = new Vector2 (randomPositionXStar, randomPositionYStar);
			transform.position = Vector2.MoveTowards (transform.position, destination, speed * Time.deltaTime);
            Vector3 finaleSize = this.transform.localScale * + 3f;
            transform.localScale = Vector3.Lerp(transform.localScale, finaleSize, Time.deltaTime);
            StartCoroutine(LateCall());
        }
	}

	void OnCollisionEnter2D (Collision2D collision) {
		
		if (collision.gameObject.tag == "bomb" || collision.gameObject.tag == "bulletSpecial"
		    || collision.gameObject.tag == "orb" || collision.gameObject.tag == "Player" || collision.gameObject.tag == "starShield" ) {
			// Do Nothing
		} else {
			gameObject.tag = "Untagged";
		}
    }

    IEnumerator LateCall()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
