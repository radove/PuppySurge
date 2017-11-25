using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class addScore : MonoBehaviour
{
    private string secretKey = "cheese"; // Edit this value and make sure it's the same as the one stored on the server
    public string addScoreURL = "http://www.aliensurge.com/api/addScore.php"; //be sure to add a ? to your url
    public string highscoreURL = "http://localhost/unity_test/display.php";
    public scoreController score;
    private string playerName = "Anonymous";

    void Start()
    {

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            score = gameControllerObject.GetComponent<scoreController>();
        }
        if (score == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }

        StartCoroutine(PostScores(playerName));
    }

    // remember to use StartCoroutine when calling this function!
    IEnumerator PostScores(string name)
    {
        Debug.Log("Posting HighScore");
        string platform = SystemInfo.deviceModel + ", " + SystemInfo.deviceName + ", " + SystemInfo.operatingSystem;
        //This connects to a server side php script that will add the name and score to a MySQL DB.
        // Supply it with a string representing the players name and the players score.
        string hash = Md5Sum(name + score.getScore() + secretKey);

        //string post_url = addScoreURL + "name=" + WWW.EscapeURL(name) + "&score=" + score.score + "&level=" + score.level + "&stars=" + score.totalStars + "&hash=" + hash + "&platform=" + platform;

        // Post the URL to the site and create a download object to get the result.
        WWWForm form = new WWWForm();
        form.AddField("name", WWW.EscapeURL(name));
        form.AddField("score", score.getScore());
        form.AddField("level", score.getLevel());
        form.AddField("stars", score.getTotalStars());
        form.AddField("platform", platform);
        form.AddField("hash", hash);

        WWW hs_post = new WWW(addScoreURL, form);
        yield return hs_post; // Wait until the download is done

        if (hs_post.error != null)
        {
            Debug.Log("There was an error posting the high score: " + hs_post.error);
        }

        Destroy(this.gameObject);
    }

    // Get the scores from the MySQL DB to display in a GUIText.
    // remember to use StartCoroutine when calling this function!
    IEnumerator GetScores()
    {
        //gameObject.guiText.text = "Loading Scores";
        WWW hs_get = new WWW(highscoreURL);
        yield return hs_get;

        if (hs_get.error != null)
        {
            print("There was an error getting the high score: " + hs_get.error);
        }
        else
        {
           // gameObject.guiText.text = hs_get.text; // this is a GUIText that will display the scores in game.
        }
    }

    public string Md5Sum(string strToEncrypt)
    {
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);

        // encrypt bytes
        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);

        // Convert the encrypted bytes back to a string (base 16)
        string hashString = "";

        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');
    }

    void setName(string nickname)
    {
        playerName = nickname;
    }

}