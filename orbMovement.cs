using UnityEngine;
using System.Collections;

public class orbMovement : MonoBehaviour {
	public int speed;
	private float randomPositionYStar;
	private float randomPositionXStar;
	private Vector2 previousLocation;
	private Vector2 destination;

    // Use this for initialization
    void Start () {
        randomPositionYStar = Random.Range(-300, 300);
        randomPositionXStar = Random.Range(-500, 1200);
    }
	
	// Update is called once per frames
	void Update ()
    {
        if (Vector3.Distance(transform.position, destination) < 0.1f)
        {
            if (this.transform.position.x > 1100)
            {
                randomPositionXStar = Random.Range(-400, -500);
            }
            else
            {
                randomPositionXStar = Random.Range(1100, 1200);
            }
            randomPositionYStar = Random.Range(-300, 300);
        }
        destination = new Vector2 (randomPositionXStar, randomPositionYStar);
		this.transform.position = Vector2.MoveTowards (this.transform.position, destination, speed * Time.deltaTime);

    }
}
