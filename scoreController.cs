using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class scoreController : MonoBehaviour {

    private int previousScore = 0;
    private int previousHealth = 100;
    private int previousStars = 0;
    public GameObject scoreIncreaseTextWidget;
    private int score = 0;
    private int level = 1;
    private int stars = 0;
    private int totalStars = 0;
    private int playerHealth = 100;
	private Text scoreBoard;
	private Text levelBoard;
	public GameObject tutorialTextWidget;
    private Slider healthBar;
    private Text playerHealthText;
    public startOver menuTools;
    private Text totalStarsText;

	// Use this for initialization
	void Start () {
		scoreBoard = GameObject.Find ("Score").GetComponent<Text>();
		levelBoard = GameObject.Find ("LevelBoard").GetComponent<Text>();
        healthBar = GameObject.FindWithTag("healthSlider").GetComponent<Slider>();
        playerHealthText = GameObject.FindGameObjectWithTag("healthBarText").GetComponent<Text>();
        totalStarsText = GameObject.Find("StarTotalEarned").GetComponent<Text>();
        SaveLoad.Load();
        if (Game.current == null)
        {
            Game.current = new Game();
        }
        resetStars();
        StartCoroutine(autoSaveProcess());
    }

	public void Awake()
	{
		DontDestroyOnLoad(this);
		
		if (FindObjectsOfType(GetType()).Length > 1)
		{
			Destroy(gameObject);
        }
    }

    public int getLevel()
    {
        return level;
    }


    public int getTotalStars()
    {
        return totalStars;
    }
    
    public int getHealth()
    {
        return playerHealth;
    }

    public int getScore()
    {
        return score;
    }

    public int getStars()
    {
        return stars;
    }

    public void updateStars(int newStars)
    {
        stars = stars + newStars;
        if (newStars > 0) {
           totalStars = totalStars + newStars;
           Game.current.totalStarsCollected = Game.current.totalStarsCollected + newStars;
        }
        totalStarsText.text = stars + " / " + Game.current.totalStarsCollected;
    }

    public void resetStars()
    {
        stars = 0;
        totalStars = 0;
        totalStarsText.text = totalStars + " / " + Game.current.totalStarsCollected;
    }

    public void resetScore()
    {
        score = 0;
        updateScore(0);
    }

    public void resetHealth()
    {
        playerHealth = 100;
        updateHealth(100);
    }

    public void updateLevelText(int newLevel)
    {
        level = newLevel;
        levelBoard.text = "Level: " + level;
        if (!Game.current.unlockedLevels.Contains(level))
        {
            Game.current.unlockedLevels.Add(level);
        }
    }

    public void updateScore(int newScore)
    {
        Game.current.totalEnemiesKilled++;
        score = score + newScore;
        scoreBoard.text = (score.ToString());

        if (previousScore != score)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Vector3 playerPos = player.transform.position;
            Vector3 spawnTextPos = new Vector2(playerPos.x, playerPos.y + 200f);
            GameObject scoreInc = (GameObject)Instantiate(scoreIncreaseTextWidget, spawnTextPos, Quaternion.identity);
            scoreInc.SendMessage("SetText", score - previousScore);
            scoreInc.SendMessage("SetType", 1);
            previousScore = score;

        }
        if (Game.current.bestPersonalScore < score)
        {
            Game.current.bestPersonalScore = score;
        }
    }

    public void updateHealth(int health)
    {

        playerHealth = playerHealth + health;

        if (playerHealth > 100)
        {
            playerHealth = 100;
        }
        
        healthBar.value = playerHealth;

        playerHealthText.text = playerHealth + "%";
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 playerPos = player.transform.position;
        Vector3 spawnTextPos = new Vector2(playerPos.x, playerPos.y + 100f);
        GameObject scoreInc = (GameObject)Instantiate(scoreIncreaseTextWidget, spawnTextPos, Quaternion.identity);
        scoreInc.SendMessage("SetText", health);
        if (health < 0)
        {
            scoreInc.SendMessage("SetType", 2);
        }
        else
        {
            scoreInc.SendMessage("SetType", 3);
        }

        if (playerHealth <= 0)
        {
            StartCoroutine(waitAndShowMenu());
        }


    }

    public void launchTutorial(string tutorial)
    {
        GameObject textObj = Instantiate(tutorialTextWidget, new Vector2(300f, 0f), Quaternion.identity) as GameObject;
        textObj.SendMessage("SetText", tutorial);
    }

    IEnumerator autoSaveProcess()
    {
        while (true)
        {
            yield return new WaitForSeconds(60f);
            SaveLoad.Save();
        }
    }

    IEnumerator waitAndShowMenu()
    {
        menuTools.addHighScore();
        yield return new WaitForSeconds(1f);
        menuTools.showMenu();
    }
}