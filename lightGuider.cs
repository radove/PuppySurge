using UnityEngine;
using System.Collections;

public class lightGuider : MonoBehaviour
{
    public int speed = 400;
    public float rotationSpeed = 2f;
    private float randomPositionYStar;
    private float randomPositionXStar;
    private Vector2 destination;

    void Start()
    {

    }

    void OnEnable()
    {
        foreach (Transform child in gameObject.GetComponentInChildren<Transform>(true))
        {
            child.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        float randomPositionYStar = this.transform.position.y;
        float randomPositionXStar = Random.Range(-2500, -2600);
        Vector2 destination = new Vector2(randomPositionXStar, randomPositionYStar);
        this.gameObject.transform.position = Vector2.MoveTowards(this.gameObject.transform.position, destination, speed * Time.deltaTime);

        float currentRotation = this.gameObject.transform.localEulerAngles.z;
        float newRotation = currentRotation + rotationSpeed;
        this.gameObject.transform.localEulerAngles = new Vector3(0f, 0f, newRotation);

        //lerpClosestObject("bomb");
    }

    void DestroyCollisionBullet(Collision2D coll)
    {
        coll.gameObject.SetActive(false);
        ExplodeLarge(coll.transform.position);
    }

    void ExplodeLarge(Vector2 pos)
    {
        GameObject soundEffect = GenericObjectPooler.current.GetGenericGameObject("soundSmallBomb");
        if (soundEffect != null)
        {
            soundEffect.transform.position = pos;
            soundEffect.SetActive(true);
        }
        GameObject explosion = GenericObjectPooler.current.GetGenericGameObject("explosionBig");
        if (explosion != null)
        {
            explosion.transform.position = pos;
            explosion.SetActive(true);
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "starFall")
        {
            DestroyCollisionBullet(collision);
        }

        if (collision.gameObject.tag == "alienAttacker")
        {
            DestroyCollisionBullet(collision);
        }
        if (collision.gameObject.tag == "dinoHead")
        {
            DestroyCollisionBullet(collision);
        }
        if (collision.gameObject.tag == "orbAttacker")
        {
            DestroyCollisionBullet(collision);
        }

        if (collision.gameObject.tag == "badBubble")
        {
            DestroyCollisionBullet(collision);
        }

        if (collision.gameObject.tag == "badBubbleSmall")
        {
            DestroyCollisionBullet(collision);
        }

        if (collision.gameObject.tag == "bubbleSmallBoss")
        {
            DestroyCollisionBullet(collision);
        }

        if (collision.gameObject.tag == ("enemySmall"))
        {
            DestroyCollisionBullet(collision);
        }

        if (collision.gameObject.tag.Equals("blackAlien"))
        {
            DestroyCollisionBullet(collision);
        }

        if (collision.gameObject.tag.Equals("alienAttackerSummoned"))
        {
            DestroyCollisionBullet(collision);
        }
    }

    void lerpClosestObject(string tag)
    {

        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in objectsWithTag)
        {
            foreach (Transform child in transform)
            {
                if (Vector3.Distance(child.position, obj.transform.position) <= 50f)
                {
                    Debug.Log("Is this working?");
                    Vector2 destinationPosition = new Vector2(obj.transform.position.x + 50f, obj.transform.position.y + 50f);
                    obj.transform.position = Vector3.Lerp(obj.transform.position, destinationPosition, Time.deltaTime * 15);
                }
            }
        }
    }
}
