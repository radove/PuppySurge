using UnityEngine;
using System.Collections;

public class boss : MonoBehaviour {
	
	scoreController score;
	public int speed = 300;
	public GameObject bubble;
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
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(Process());
        StartCoroutine(LaunchBullet());
    }
	
	// Update is called once per frame
	void Update () {
		
		//this.gameObject.transform.localEulerAngles = new Vector3(0f,0f,-270f);

		float currentRotation = this.gameObject.transform.localEulerAngles.z;
		float newRotation = currentRotation + 0.5f;
		Vector3 newVectorRotation = new Vector3 (0f, 0f, newRotation);
		transform.localEulerAngles = newVectorRotation;

		Vector2 movePosition = new Vector2 (950, -player.transform.position.y);
		this.gameObject.transform.position = Vector2.MoveTowards (this.gameObject.transform.position, movePosition, 100 * Time.deltaTime);
		
	}

    IEnumerator LaunchBullet()
    {
        while (true) {
            if (GameObject.FindGameObjectsWithTag("badBubbleSmall").Length < 20)
            {
                foreach (Transform child in transform)
                {
                    if (child.gameObject.name.Equals("bombDropLocation"))
                    {
                        GameObject badBubbleSmall = GenericObjectPooler.current.GetGenericGameObject("badBubbleSmall");
                        if (badBubbleSmall != null)
                        {
                            badBubbleSmall.transform.localScale = new Vector3(8f, 8f, 1f);
                            badBubbleSmall.transform.position = child.gameObject.transform.position;
                            badBubbleSmall.SetActive(true);
                            //badBubbleSmall.transform.parent = gameObject.transform;
                        }
                    }
                }
            }
            yield return StartCoroutine(Wait(1f));
        }
	}

	
	IEnumerator Process()
	{
		while (true) {
			foreach (Transform child in transform) {
				float percentChance = Random.Range (0,100);
				if (percentChance > 50) {
					if (child.gameObject.tag.Equals ("badBubbleSmall")) {
						child.gameObject.transform.parent = null;
					}
				}
			}
			yield return StartCoroutine(Wait(5f));
		}
	}

	IEnumerator Wait(float seconds)
	{
		yield return new WaitForSeconds(seconds);
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