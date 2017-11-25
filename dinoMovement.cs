using UnityEngine;
using System.Collections;

public class dinoMovement : MonoBehaviour {
    public int speed = 800;
    public float rotationSpeed = 2f;
    private float randomPositionYStar;
    private float randomPositionXStar;
    private Vector2 destination;
    
    void Start()
    {

    }
    
    void Update () {
        float randomPositionYStar = this.transform.position.y;
        float randomPositionXStar = Random.Range(-2500, -2600);
        Vector2 destination = new Vector2(randomPositionXStar, randomPositionYStar);
        this.gameObject.transform.position = Vector2.MoveTowards(this.gameObject.transform.position, destination, speed * Time.deltaTime);

    }
}
