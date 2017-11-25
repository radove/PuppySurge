using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class bossLevelOne : MonoBehaviour {

	public GameObject bullet;
	private scoreController score;
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
		StartCoroutine("AutoBulletProcess");
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        transform.localEulerAngles = new Vector3(0f, 0f, 90f);
        transform.position = Vector2.MoveTowards (transform.position, player.transform.position, 200f * Time.deltaTime);
        
    }

	IEnumerator AutoBulletProcess()
	{
		while (true) {
			bullet.transform.localScale = new Vector3(15f,15f,1f);
			Instantiate(bullet, transform.position, Quaternion.identity);
			yield return new WaitForSeconds(0.75f);
		}
	}
}