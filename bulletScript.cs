using UnityEngine;
using System.Collections;

public class bulletScript : MonoBehaviour {
    
    private Vector3 pos;

    //private Rigidbody2D rigidBody;

    // Use this for initialization
    void Start () {
        // rigidBody = GetComponent<Rigidbody2D>();
        pos = new Vector2(2000f, transform.position.y);
    }
    void OnEnable()
    {
        pos = new Vector2(2000f, transform.position.y);
    }

    void Update()
    {
        this.transform.position = Vector2.MoveTowards(transform.position, pos, 1400f * Time.deltaTime);

       // rigidBody.velocity = defaultSpeed;
       // float currentRotation = rigidBody.transform.localEulerAngles.z;
        //float newRotation = currentRotation + 5;
        //rigidBody.transform.localEulerAngles = new Vector3(0f, 0f, newRotation);
    }
}
