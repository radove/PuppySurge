using UnityEngine;
using System.Collections;

public class specialBulletScript : MonoBehaviour
{
    private GameObject starCenter;
    private Vector3 pos;
    private Vector2 moveTowardsLocationAfter;
    private bool fire = false;

    void Start()
    {
        starCenter = GameObject.FindWithTag("starCenter");
        pos = RandomCircle(starCenter.transform.position, 20f);
        moveTowardsLocationAfter = new Vector2(2000f, Random.Range(pos.y - 100f, pos.y + 100f));
    }

    void OnEnable()
    {
        starCenter = GameObject.FindWithTag("starCenter");
        fire = false;
        pos = RandomCircle(starCenter.transform.position, 20f);
        moveTowardsLocationAfter = new Vector2(2000f, Random.Range(pos.y - 100f, pos.y + 100f));
    }

    Vector3 RandomCircle(Vector3 center, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }

    void Update()
    {
        //rigidBody.velocity = (pos - transform.position).normalized * 100;
        if (fire)
        {
            this.transform.position = Vector2.MoveTowards(transform.position, moveTowardsLocationAfter, 1800f * Time.deltaTime);
        }
        else
        {
            this.transform.position = Vector2.MoveTowards(transform.position, pos, 400f * Time.deltaTime);
        }

        if (Vector2.Distance(transform.position, pos) < 0.2f)
        {
            fire = true;
        }
    }
}
