using UnityEngine;
using System.Collections;

public class orbFall : MonoBehaviour {

	scoreController score;
	public int speed;


    Vector2 destination;

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
        float randomPositionXStar = Random.Range(-2500, -2600);
        float randomPositionYStar = this.gameObject.transform.position.y;
        destination = new Vector2(randomPositionXStar, randomPositionYStar);


    }

    void OnEnable()
    {
        float randomPositionXStar = Random.Range(-2500, -2600);
        float randomPositionYStar = this.gameObject.transform.position.y;
        destination = new Vector2(randomPositionXStar, randomPositionYStar);
    }

    // Update is called once per frame
    void Update () {
			//g.rigidbody2D.AddForce(Vector3.forward * speed * Time.deltaTime);
			//g.transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * Time.deltaTime*speed);
			
			//g.rigidbody2D.velocity = new Vector2(0,-1);
			//float currentRotation = this.gameObject.transform.localEulerAngles.z;
            //float rotationProcess = 1f;
			//float newRotation = currentRotation + rotationProcess;
			//this.gameObject.transform.localEulerAngles = new Vector3(0f,0f,newRotation);
            this.gameObject.transform.position = Vector2.MoveTowards(this.gameObject.transform.position, destination, speed * Time.deltaTime);

    }

}
