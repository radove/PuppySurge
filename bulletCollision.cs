using UnityEngine;
using System.Collections;

public class bulletCollision : MonoBehaviour {
	
	public scoreController score;
	public GameObject smallBombExplosion;

    private Transform alienBossDropLocation;

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
    }


    void DestroyBullet()
    {
        gameObject.SetActive(false);
    }

    void DestroyCollisionBullet(Collision2D coll)
    {
        coll.gameObject.SetActive(false);
    }

    void ExplodeMini()
    {
        GameObject soundEffect = GenericObjectPooler.current.GetGenericGameObject("soundBubblePop");
        if (soundEffect != null)
        {
            soundEffect.transform.position = transform.position;
            soundEffect.SetActive(true);
        }
        GameObject explosion = GenericObjectPooler.current.GetGenericGameObject("explosionMini");
        if (explosion != null)
        {
            explosion.transform.position = transform.position;
            explosion.SetActive(true);
        }
    }

    void ExplodeLarge()
    {
        GameObject soundEffect = GenericObjectPooler.current.GetGenericGameObject("soundSmallBomb");
        if (soundEffect != null)
        {
            soundEffect.transform.position = transform.position;
            soundEffect.SetActive(true);
        }
        GameObject explosion = GenericObjectPooler.current.GetGenericGameObject("explosionBig");
        if (explosion != null)
        {
            explosion.transform.position = transform.position;
            explosion.SetActive(true);
        }
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    void spawnOrb(Vector3 location)
    {
        GameObject starOrb = GenericObjectPooler.current.GetGenericGameObject("starOrb");
        if (starOrb != null)
        {
            starOrb.transform.localScale = new Vector3(7.5f, 7.5f, 0);
            starOrb.transform.position = location;
            starOrb.SetActive(true);
        }
    }

    void OnBecameInvisible() {
		if (gameObject.tag != "starShield")
        {
            gameObject.SetActive(false);
        }
	}
	
	void OnCollisionEnter2D (Collision2D collision) {
        if (collision.gameObject.tag == "starFall")
        {
            DestroyBullet();
            DestroyCollisionBullet(collision);
            ExplodeMini();
			score.updateScore(10);
        }
		if (collision.gameObject.tag == "alienBoss") {
            
            GameObject soundEffect = GenericObjectPooler.current.GetGenericGameObject("soundAlienHurt");
            if (soundEffect != null)
            {
                soundEffect.transform.position = transform.position;
                soundEffect.SetActive(true);
            }

            DestroyBullet();

            foreach (Transform child in collision.gameObject.transform)
            {
                if (child.gameObject.name.Equals("alienDropLocation"))
                {
                    alienBossDropLocation = child.gameObject.transform;
                }
            }

            GameObject alienSummon = GenericObjectPooler.current.GetGenericGameObject("alienSummon");
            if (alienSummon != null)
            {
                alienSummon.transform.position = alienBossDropLocation.position;
                alienSummon.SetActive(true);
            }

            ExplodeMini();
			//Instantiate(alienHitSound, collision.gameObject.transform.position, Quaternion.identity);
			//float currentRotation = collision.gameObject.transform.localEulerAngles.z;
			//float newRotation = currentRotation + 5f;
			//collision.gameObject.transform.localEulerAngles = new Vector3(0f,0f,newRotation);


			Vector2 finaleSize = new Vector2(collision.gameObject.transform.localScale.x - 0.5f, collision.gameObject.transform.localScale.y - 0.5f);
			collision.gameObject.transform.localScale = Vector3.Lerp(collision.gameObject.transform.localScale, finaleSize, Time.deltaTime);
			Vector3 compareSize = new Vector3(0.1f,0.1f);
			if (compareSize.x > collision.gameObject.transform.localScale.x) {
				Destroy (collision.gameObject);
                //Instantiate(popBubbleSound, collision.gameObject.transform.position, Quaternion.identity);
                Vector3 position = collision.gameObject.transform.position;
                ExplodeLarge();
                //boom.renderer.sortingLayerName="Foreground";
                //boom.transform.localScale = new Vector3(100f,100f,1f);
                //if(boom.GetComponent<ParticleSystem>().isPlaying)
                //{
                //	Debug.Log ("Its Playing");
                //}
                spawnOrb(position);
                spawnOrb(position);

                //smallExplosion.transform.localScale = new Vector3(1f,100f,1f);
                //smallExplosion.transform.position = transform.position;

                score.updateScore(1000);
			}
		}
		
		if (collision.gameObject.tag == "alienAttacker")
        {
            foreach (Transform child in collision.gameObject.transform) {
				if (child.gameObject.name.Equals ("alienDropLocation")) {
                    alienBossDropLocation = child.gameObject.transform;
                }
            }

            GameObject alienSummon = GenericObjectPooler.current.GetGenericGameObject("alienSummon");
            if (alienSummon != null)
            {
                alienSummon.transform.position = alienBossDropLocation.position;
                alienSummon.SetActive(true);
            }
            ExplodeMini();
            //Instantiate(alienHitSound, collision.gameObject.transform.position, Quaternion.identity);

            DestroyBullet();
            Vector3 finaleSize = collision.gameObject.transform.localScale * -1.25f;
			collision.gameObject.transform.localScale = Vector3.Lerp(collision.gameObject.transform.localScale, finaleSize, Time.deltaTime);
			Vector3 compareSize = new Vector3(0.75f,0.75f);
			if (compareSize.x > collision.gameObject.transform.localScale.x) {
                DestroyCollisionBullet(collision);
                //Instantiate(popBubbleSound, collision.gameObject.transform.position, Quaternion.identity);
                Vector3 position = collision.gameObject.transform.position;

                ExplodeLarge();
                //boom.renderer.sortingLayerName="Foreground";
                //boom.transform.localScale = new Vector3(100f,100f,1f);
                //if(boom.GetComponent<ParticleSystem>().isPlaying)
                //{
                //	Debug.Log ("Its Playing");
                //}
                spawnOrb(position);
                spawnOrb(position);

                //smallExplosion.transform.localScale = new Vector3(1f,100f,1f);
                //smallExplosion.transform.position = transform.position;

                score.updateScore(100);
			}
        }
        if (collision.gameObject.tag == "lightGlider")
        {
            DestroyBullet();
            ExplodeMini();
        }

        if (collision.gameObject.tag == "lightRounder")
        {
            DestroyBullet();
            ExplodeMini();
        }
        if (collision.gameObject.tag == "dinoHead")
        {
            Vector3 position = collision.gameObject.transform.position;
            ExplodeLarge();
            DestroyBullet();
            DestroyCollisionBullet(collision);
            spawnOrb(position);
            score.updateScore(25);
        }
        if (collision.gameObject.tag == "orbAttacker") {
                DestroyBullet();
                DestroyCollisionBullet(collision);
                //Instantiate(popBubbleSound, collision.gameObject.transform.position, Quaternion.identity);
                Vector3 position = collision.gameObject.transform.position;
            
                ExplodeLarge();

                spawnOrb(position);

            score.updateScore(100);
		}
		
		if (collision.gameObject.tag == "badBubble") {
            DestroyBullet();
            Vector3 finaleSize = collision.gameObject.transform.localScale * -2f;
			collision.gameObject.transform.localScale = Vector3.Lerp(collision.gameObject.transform.localScale, finaleSize, Time.deltaTime);
            ExplodeMini();
            Vector3 compareSize = new Vector3(15f,15f);
			if (compareSize.x > collision.gameObject.transform.localScale.x) {
                DestroyCollisionBullet(collision);
                //Instantiate(popBubbleSound, collision.gameObject.transform.position, Quaternion.identity);
                Vector3 position = collision.gameObject.transform.position;
                ExplodeLarge();
                //boom.renderer.sortingLayerName="Foreground";
                //boom.transform.localScale = new Vector3(100f,100f,1f);
                //if(boom.GetComponent<ParticleSystem>().isPlaying)
                //{
                //	Debug.Log ("Its Playing");
                //}
                spawnOrb(position);
                spawnOrb(position);

                //smallExplosion.transform.localScale = new Vector3(1f,100f,1f);
                //smallExplosion.transform.position = transform.position;

                score.updateScore(100);
			}
		}
		

		if (collision.gameObject.tag == "bossBubble") {
            DestroyBullet();

            GameObject soundEffect = GenericObjectPooler.current.GetGenericGameObject("soundAlienHurt");
            if (soundEffect != null)
            {
                soundEffect.transform.position = transform.position;
                soundEffect.SetActive(true);
            }
            Vector3 finaleSize = collision.gameObject.transform.localScale * -0.05f;
			collision.gameObject.transform.localScale = Vector3.Lerp(collision.gameObject.transform.localScale, finaleSize, Time.deltaTime);
			Vector3 compareSize = new Vector3(20f,20f);
            ExplodeMini();
            if (compareSize.x > collision.gameObject.transform.localScale.x) {
				Destroy (collision.gameObject);
                //Instantiate(popBubbleSound, collision.gameObject.transform.position, Quaternion.identity);
                Vector3 position = collision.gameObject.transform.position;
                ExplodeLarge();
                //boom.renderer.sortingLayerName="Foreground";
                //boom.transform.localScale = new Vector3(100f,100f,1f);
                //if(boom.GetComponent<ParticleSystem>().isPlaying)
                //{
                //	Debug.Log ("Its Playing");
                //}
                spawnOrb(position);
                spawnOrb(position);

                //smallExplosion.transform.localScale = new Vector3(1f,100f,1f);
                //smallExplosion.transform.position = transform.position;

                score.updateScore(1000);
			}
		}
		
		if (collision.gameObject.tag == "badBubbleSmall") {
            DestroyCollisionBullet(collision);
            DestroyBullet();
            //Instantiate(popBubbleSound, collision.gameObject.transform.position, Quaternion.identity);
            Vector3 position = collision.gameObject.transform.position;
            ExplodeMini();
            //boom.renderer.sortingLayerName="Foreground";
            //boom.transform.localScale = new Vector3(100f,100f,1f);
            //if(boom.GetComponent<ParticleSystem>().isPlaying)
            //{
            //	Debug.Log ("Its Playing");
            //}
            //Destroy (boom, 3f);
            //Instantiate(bonusItem, position, Quaternion.identity);

            //smallExplosion.transform.localScale = new Vector3(1f,100f,1f);
            //smallExplosion.transform.position = transform.position;

            score.updateScore(5);
        }
		
		if (collision.gameObject.tag == "bubbleSmallBoss") {
			Destroy (collision.gameObject);
            DestroyBullet();
            //Instantiate(popBubbleSound, collision.gameObject.transform.position, Quaternion.identity);
            Vector3 position = collision.gameObject.transform.position;
            ExplodeMini();
            score.updateScore(5);
        }
		
		if (collision.gameObject.tag == ("enemySmall"))
        {
            DestroyBullet();
            DestroyCollisionBullet(collision);
            //Instantiate(smallExplosionSound, collision.gameObject.transform.position, Quaternion.identity);
            Vector3 position = collision.gameObject.transform.position;
            ExplodeLarge();
            //boom.renderer.sortingLayerName="Foreground";
            //boom.transform.localScale = new Vector3(100f,100f,1f);
            //if(boom.GetComponent<ParticleSystem>().isPlaying)
            //{
            //	Debug.Log ("Its Playing");
            //}
            spawnOrb(position);

            //smallExplosion.transform.localScale = new Vector3(1f,100f,1f);
            //smallExplosion.transform.position = transform.position;

            score.updateScore(50);
        }

		if (collision.gameObject.tag.Equals ("blackAlien"))
        {
            DestroyBullet();
            DestroyCollisionBullet(collision);
            //Instantiate(popBubbleSound, collision.gameObject.transform.position, Quaternion.identity);
            Vector3 position = collision.gameObject.transform.position;
            spawnOrb(position);
            ExplodeMini();
            //boom.renderer.sortingLayerName="Foreground";
            //boom.transform.localScale = new Vector3(100f,100f,1f);
            //if(boom.GetComponent<ParticleSystem>().isPlaying)
            //{
            //	Debug.Log ("Its Playing");
            //}

            //smallExplosion.transform.localScale = new Vector3(1f,100f,1f);
            //smallExplosion.transform.position = transform.position;

            score.updateScore(50);
        }

		
		if (collision.gameObject.tag == "bossBullet")
        {
            DestroyBullet();
			Vector3 position = collision.gameObject.transform.position;
			Destroy (collision.gameObject);
            ExplodeMini();
            Instantiate(smallBombExplosion, position, Quaternion.identity);
		}
    }
}
