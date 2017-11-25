using UnityEngine;
using System.Collections;

public class badBubble : MonoBehaviour {
	
	private Vector2 destination;
	private float randomPositionYStar;
	private float randomPositionXStar;
    private float rotationSpeed = 2f;
    private float speed = 300;
    private float waitSec = 0.5f;
    private Transform dropLocation;

    // Use this for initialization
    void Start () {
		randomPositionYStar = Random.Range (-600, 800);
		randomPositionXStar = Random.Range (-2400, -2500);
        destination = new Vector2(randomPositionXStar, randomPositionYStar);
        foreach (Transform child in transform)
        {
            if (child.gameObject.name.Equals("bombDropLocation"))
            {
                dropLocation = child.gameObject.transform;
            }
        }
        StartCoroutine(launchBullet());
    }

    void OnEnable()
    {
        randomPositionYStar = Random.Range(-600, 800);
        randomPositionXStar = Random.Range(-2400, -2500);
        destination = new Vector2(randomPositionXStar, randomPositionYStar);

        StartCoroutine(launchBullet());
    }

    // Update is called once per frame
    void Update () {
		
		//this.gameObject.transform.localEulerAngles = new Vector3(0f,0f,-270f);

		float currentRotation = this.gameObject.transform.localEulerAngles.z;
		float newRotation = currentRotation + rotationSpeed;
		this.gameObject.transform.localEulerAngles = new Vector3(0f,0f,newRotation);

		this.transform.position = Vector2.MoveTowards (this.transform.position, destination, speed * Time.deltaTime);

	}

    IEnumerator launchBullet() {
        yield return StartCoroutine(Wait(1f));
        while (true)
        {
            GameObject badBubbleSmall = GenericObjectPooler.current.GetGenericGameObject("badBubbleSmall");
            if (badBubbleSmall != null)
            {
                badBubbleSmall.transform.localScale = new Vector3(8f, 8f, 1f);
                badBubbleSmall.transform.position = dropLocation.position;
                badBubbleSmall.SetActive(true);
            }
            yield return StartCoroutine(Wait(waitSec));
        }
    }

    void setSpeed(float theSpeed)
    {
        speed = theSpeed;
    }

    void setRotationSpeed(float theSpeed)
    {
        rotationSpeed = theSpeed;
    }

    void setSpawnSpeed(float theSpeed)
    {
        waitSec = theSpeed;
    }

    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
