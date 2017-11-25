using UnityEngine;
using System.Collections;

public class floatingMenu : MonoBehaviour {

    public float speed;
    private float randomPositionYStar;
    private float randomPositionXStar;
    private Vector2 destination;
    private Vector2 originalLocation;

    // Use this for initialization
    void Start ()
    {
        originalLocation = transform.position;
        randomPositionYStar = originalLocation.y + Random.Range(-2f, 2f);
        randomPositionXStar = originalLocation.x + Random.Range(-2f, 2f);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Vector3.Distance(transform.position, destination) < 0.1f)
        {
            randomPositionXStar = originalLocation.x + Random.Range(-2f, 2f);
            randomPositionYStar = originalLocation.y + Random.Range(-2f, 2f);
        }
        destination = new Vector2(randomPositionXStar, randomPositionYStar);
        this.transform.position = Vector2.MoveTowards(this.transform.position, destination, speed);
        
    }
}
