using UnityEngine;
using System.Collections;

public class groundCollision : MonoBehaviour {
    
	void OnCollisionEnter2D (Collision2D collision) {
        if (collision.gameObject.tag.Equals("bomb"))
        {
            collision.gameObject.SetActive(false);
        }
        else if (!collision.gameObject.tag.Equals("ground"))
        {
            collision.gameObject.SetActive(false);
        }
    }

}
