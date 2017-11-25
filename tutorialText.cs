using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class tutorialText : MonoBehaviour {
	public GameObject textSound;
	private string textData = "";
	private Text myTextComponent;
	
	// Use this for initialization
	void Start () {
		Instantiate(textSound, transform.position, Quaternion.identity);
		Destroy (gameObject, 10f);
		myTextComponent = gameObject.GetComponentInChildren<Text>();
		myTextComponent.text = textData;
	}
    
    public void Awake()
    {
        DontDestroyOnLoad(this);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
        Vector3 finalSize = this.transform.localScale * 1.5f;
        if (finalSize.x > 3f)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(300, 2000f), 600f * Time.deltaTime);
        }
        else
        {
            this.transform.localScale = Vector3.Lerp(this.transform.localScale, finalSize, Time.deltaTime);
        }

    }

    void SetText (string t) {
		textData = t;
	}
}
