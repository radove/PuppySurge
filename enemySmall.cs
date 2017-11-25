using UnityEngine;
using System.Collections;

public class enemySmall : MonoBehaviour {
	
	scoreController score;
	public int speed = 400;
	public GameObject bullet;
    private Transform bombDropLocation;

    // Use this for initialization
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.name.Equals("bombDropLocation"))
            {
                bombDropLocation = child.gameObject.transform;
            }
        }
        this.gameObject.transform.localEulerAngles = new Vector3(0f, 0f, -270f);
    }
	// Update is called once per frame
	void Update () {
		
		//this.gameObject.transform.localEulerAngles = new Vector3(0f,0f,-270f);

		float randomPositionYStar = this.transform.position.y;
		float randomPositionXStar = Random.Range (-2500, -2600);
		Vector2 destination = new Vector2 (randomPositionXStar, randomPositionYStar);
		this.gameObject.transform.position = Vector2.MoveTowards (this.gameObject.transform.position, destination, speed * Time.deltaTime);
		
	}

	void launchBullet() {
		bullet.transform.localScale = new Vector3(20f,20f,1f);
		Instantiate(bullet, bombDropLocation.transform.position, Quaternion.identity);
	}

	void OnBecameInvisible() {
		//Destroy(gameObject);
	}
	
}
