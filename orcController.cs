using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class orcController : MonoBehaviour {
	
	public Animator anim;
	private scoreController score;
	public GameObject powerUpSound;
	public GameObject playerHit;
	public GameObject starPowerUp;

	/// <summary>
	/// 1 - The speed of the ship
	/// </summary>
	public Vector2 speed = new Vector2(1600, 1600);
	public float bulletSpeed = 2.0f;
	
	// 2 - Store the movement
	private Vector2 movement;
	private bool touched = false;
	private bool menuOpen = false;
	private Canvas menuCanvas;
	private Canvas scoreMenuCanvas;
    private Transform bombDropLocation;
    private float oneFourth;
    private float oneHalf;
    private GameObject starCenter;
    private int circle = 0;

    void Start()
    {

        GameObject puppyShip = GameObject.Find("puppyShip");
        GameObject craftShip = GameObject.Find("craftShip");
        puppyShip.SetActive(false);
        craftShip.SetActive(true);

        starCenter = GameObject.FindWithTag("starCenter");

        Cursor.visible = false;
        //Screen.lockCursor = true;
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            score = gameControllerObject.GetComponent<scoreController>();
        }
        if (score == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }

        GameObject menu = GameObject.FindGameObjectWithTag("menu");
        menuCanvas = menu.GetComponent<Canvas>();
        menuCanvas.enabled = false;

        foreach (Transform child in transform)
        {
            Debug.Log(child.gameObject.name);
            if (child.gameObject.name.Equals("bombDropLocation"))
            {
                bombDropLocation = child.gameObject.transform;
            }
        }



        StartCoroutine(AutoBulletProcess());
        StartCoroutine(AutoSpecialBulletProcess());
        oneFourth = Screen.width / 4;
        oneHalf = Screen.width / 2;
    }

    void Update()
	{
		menuOpen = menuCanvas.enabled;
		
		// 3 - Retrieve axis information
		//float inputX = Input.GetAxis("Horizontal");
		//float inputY = Input.GetAxis("Vertical");


		// 4 - Movement per direction

		int touchCount = Input.touchCount;
		//If a touch is detected
		//float topFloat = Screen.height / 3;
		//Debug.Log ("topFloat: " + topFloat);
		if (touchCount > 0) {
			for (int i = 0; i < touchCount; i++) {
				Touch touch = Input.GetTouch (i);
				bool processTouch = true;
				// Check if the mouse was clicked over a UI element
				if(EventSystem.current.IsPointerOverGameObject(touch.fingerId))
				{
					GameObject menuBar = EventSystem.current.currentSelectedGameObject;
					if (menuBar != null && menuBar.tag.Equals ("uiButton")) {
						processTouch = false;
					}
				}
				if (processTouch) {
					if (touch.position.x < oneHalf + oneFourth) {
						if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved) {
							Vector2 touchPosition = Camera.main.ScreenToWorldPoint (new Vector2 (touch.position.x, touch.position.y));
							Vector2 destinationPosition;
							destinationPosition = new Vector2(touchPosition.x, touchPosition.y + 200f);
							transform.position = Vector2.Lerp (transform.position, destinationPosition, Time.deltaTime * 200);
						} 
					} else if (touch.position.x > oneHalf + oneFourth) {
						if (touch.phase == TouchPhase.Ended) {
						//right side touch
							launchBulletSpecial();
						}
					}
				}
			}

		} else {

			if (Input.mousePresent) {
				movement = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				transform.position = Vector3.Lerp (transform.position, movement, Time.deltaTime * 10);
			}
			//float inputX = Input.GetAxis("Mouse X");
			//float inputY = Input.GetAxis("Mouse Y");
			//float mousePos = Input.mousePosition;
			//movement = new Vector2 (
			//speed.x * inputX,
			//speed.y * inputY);
		//	transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
			//transform.position = Vector3.MoveTowards(transform.position, movement, Time.deltaTime * 500);
			//transform.position = Vector3.MoveTowards(transform.position, movement, Time.deltaTime * 500);
			//GetComponent<Rigidbody2D>().velocity = movement;
			if (menuOpen == false) {
				if (Input.GetMouseButtonUp (1)) {
						launchBulletSpecial();
				}
				
				if (Input.GetMouseButtonUp (0)) {
						launchBulletSpecial();
				}
			}

			if (Input.GetKeyDown (KeyCode.Escape)) {
				if (menuCanvas.enabled == false) {
					menuCanvas.enabled = true;
					Cursor.visible = true;
					Time.timeScale = 0;
					menuOpen = menuCanvas.enabled;
				} else {
					menuCanvas.enabled = false;
					Cursor.visible = false;
					Time.timeScale = 1;
					menuOpen = menuCanvas.enabled;
				}
			}

		}

	}
    
	void launchBullet() {
        GameObject theBullet = GenericObjectPooler.current.GetGenericGameObject("bulletNormal");

        if (theBullet != null)
        {
            theBullet.transform.localScale = new Vector3(12.5f, 12.5f, 1f);
            theBullet.transform.position = bombDropLocation.position;
            theBullet.SetActive(true);
        }
	}

    void untagStar()
    {
        try
        {
            GameObject[] specialStars = GameObject.FindGameObjectsWithTag("starShield");
            if (specialStars != null && specialStars.Length > 0)
            {
                for (int x = 0; x < specialStars.Length; x++)
                {
                    if (specialStars[x] != null)
                    {
                        specialStars[x].tag = "Untagged";
                        return;
                    }
                }
            }
        }

        catch (System.IndexOutOfRangeException e) {
                // thats ok.
        }
    }

    void launchBulletSpecial() {
        if (score.getStars() > 0)
        {
            for (int x = 0; x < 3; x++)
            {
                GameObject bullet = GenericObjectPooler.current.GetGenericGameObject("bulletSpecial");
                if (bullet != null)
                {
                    Vector3 pos = RandomCircle(starCenter.transform.position, 100f);
                    bullet.transform.localScale = new Vector3(15f, 15f, 1f);
                    bullet.transform.position = pos;
                    bullet.SetActive(true);
                }
            }
            untagStar();
            score.updateStars(-1);
        }
	}
	
	void FixedUpdate()
	{
		// X axis
		if (transform.position.x < -680f) {
			transform.position = new Vector2(-680f, transform.position.y);
		} else if (transform.position.x > 1275) {
			transform.position = new Vector2(1275f, transform.position.y);
		}
		
		// Y axis
		if (transform.position.y < -480f) {
			transform.position = new Vector2(transform.position.x, -480f);
		} else if (transform.position.y > 515f) {
			transform.position = new Vector2(transform.position.x, 515f);
		}

		//rigidbody2D.velocity = movement;
	}
	
	IEnumerator AutoBulletProcess()
	{
		while (true)
        {
            yield return new WaitForSeconds(0.35f);
            launchBullet();
			// Wait...
		}
	}
	
	IEnumerator AutoSpecialBulletProcess()
	{
		while (true) {
			launchRandomStars();
			yield return new WaitForSeconds(2f);
			// Wait...
		}
	}
	
	void launchRandomStars() {
		if (score.getStars() > 0) {
				try {
					GameObject[] starShields = GameObject.FindGameObjectsWithTag ("starShield");
					int s = Random.Range (0, starShields.Length);
					if (starShields [s] != null) {
                        GameObject bullet = GenericObjectPooler.current.GetGenericGameObject("bulletStar");
                        if (bullet != null)
                        {
                            bullet.transform.localScale = new Vector3(12.5f, 12.5f, 1f);
                            bullet.transform.position = starShields[s].transform.position;
                            bullet.SetActive(true);
                        }
					}
				} catch (System.IndexOutOfRangeException e) {
					// thats ok.
				}
			}
    }

    void ShipExplode()
    {
        GameObject explosion = GenericObjectPooler.current.GetGenericGameObject("explosionShip");
        if (explosion != null)
        {
            explosion.transform.position = transform.position;
            explosion.SetActive(true);
        }
    }

    void ShipDeath()
    {
        GameObject explosion = GenericObjectPooler.current.GetGenericGameObject("explosionDeath");
        if (explosion != null)
        {
            explosion.transform.position = transform.position;
            explosion.SetActive(true);
        }
    }

    void OnParticleCollision(GameObject other)
    {
        ShipExplode();
        GameObject soundEffect = GenericObjectPooler.current.GetGenericGameObject("soundPlayerHit");
        if (soundEffect != null)
        {
            soundEffect.transform.position = transform.position;
            soundEffect.SetActive(true);
        }
        score.updateHealth(-25);
    }

    void OnCollisionEnter2D (Collision2D collision) {
        
        if (collision.gameObject.tag == "orb") {
                collision.gameObject.SetActive(false);
				if (collision.gameObject.name.Contains ("health")) {
						score.updateHealth(10);
				}
				if (collision.gameObject.name.Contains ("star")) {

                    score.updateStars(1);
                    if (score.getStars() > 50)
                    {
                        score.updateStars(-1);
                    } else
                    {
                        Vector3 pos = RandomCircle(starCenter.transform.position, 100.0f);
                        GameObject starPower = GenericObjectPooler.current.GetGenericGameObject("starPower");
                        if (starPower != null)
                        {
                            starPower.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                            starPower.transform.position = pos;
                            if (starCenter != null)
                            {
                                starPower.transform.parent = starCenter.transform;
                            }
                            starPower.SetActive(true);
                        }
                    }
				}
                GameObject soundEffect = GenericObjectPooler.current.GetGenericGameObject("soundPowerUp");
                if (soundEffect != null)
                {
                    soundEffect.transform.position = transform.position;
                    soundEffect.SetActive(true);
                }
		} 
		else if (collision.gameObject.tag == "bomb" || collision.gameObject.tag == "bulletSpecial") {

		}
        else
        {
            ShipExplode();
            GameObject soundEffect = GenericObjectPooler.current.GetGenericGameObject("soundPlayerHit");
            if (soundEffect != null)
            {
                soundEffect.transform.position = transform.position;
                soundEffect.SetActive(true);
            }
            score.updateHealth(-35);
            if (score.getHealth() <=0 )
            {
                ShipDeath();
            }
        }
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
}

