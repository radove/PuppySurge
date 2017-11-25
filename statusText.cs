using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class statusText : MonoBehaviour
{
    private string textData = "";
    private Text myTextComponent;
    private int typeOfAction = 0;

    // Use this for initialization
    void Start()
    {
        myTextComponent = gameObject.GetComponentInChildren<Text>();
        myTextComponent.text = textData;
        if (typeOfAction == 1)
        {
            Destroy(gameObject, 0.5f);
            myTextComponent.color = Color.cyan;
        }
        else if (typeOfAction == 2)
        {
            Destroy(gameObject, 3f);
            myTextComponent.color = Color.red;
        }
        else if (typeOfAction == 3)
        {
            Destroy(gameObject, 3f);
            myTextComponent.color = Color.green;
        }
        else if (typeOfAction == 4)
        {
            Destroy(gameObject, 0.5f);
            myTextComponent.color = Color.yellow;
        }
        else
        {
            Destroy(gameObject, 0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 finaleSize = this.transform.localScale * +1.2f;
        this.transform.localScale = Vector3.Lerp(this.transform.localScale, finaleSize, Time.deltaTime);
        this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(0, 5000), 300f * Time.deltaTime);
    }

    void SetText(int t)
    {
        textData = "" + t;
    }

    void SetType(int t)
    {
        typeOfAction = t;
    }
}
