using UnityEngine;
using System.Collections;

public class alienAttackerSummon : MonoBehaviour {
	public int speed;

	// Update is called once per frames
	void Update () {

		this.transform.localEulerAngles = new Vector3 (0, 0, 90);
		float randomPositionYStar = this.transform.position.y;
		float randomPositionXStar = Random.Range (-2800, -2600);
		Vector2 destination = new Vector2 (randomPositionXStar, randomPositionYStar);
		this.transform.position = Vector2.MoveTowards (this.transform.position, destination, speed * Time.deltaTime);
		
	}
}