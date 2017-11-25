using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class startOver : MonoBehaviour {
    
    public GameObject AddScoreWidget;
    private int ymin = -365;
    private int ymax = 465;
    private int xmin = 1300;
    private int xmax = 1400;
    private bool adTryAgain = false;

    public void uiSound ()
    {
        GameObject soundEffect = GenericObjectPooler.current.GetGenericGameObject("soundUIAction");
        if (soundEffect != null)
        {
            soundEffect.transform.position = transform.position;
            soundEffect.SetActive(true);
        }
    }
    public void restartGame () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		Destroy (gameControllerObject);

		GameObject menu = GameObject.FindGameObjectWithTag ("menu");
		Canvas menuCanvas = menu.GetComponent<Canvas>();
		menuCanvas.enabled = false;
		Cursor.visible = false;
		Time.timeScale = 1;
        
        SceneManager.LoadScene(0);
    }

    public void resumeOrTryText()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        scoreController score = gameControllerObject.GetComponent<scoreController>();
        
        GameObject resumeTextGameObject = GameObject.FindWithTag("resumeOrTryAgain");
        Text textObj = resumeTextGameObject.GetComponent<Text>();
        if (score != null)
        {
            if (score.getHealth() > 0)
            {
                textObj.text = "RESUME";
            }
            else
            {
                textObj.text = "TRY AGAIN";
            }
            if (score.getScore() == 0)
            {
                textObj.text = "NEW GAME";
            }
        }
    }

    public void resumeOrTryAction()
    {

        uiSound();

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        scoreController score = gameControllerObject.GetComponent<scoreController>();
        if (score != null)
        {
            if (score.getScore() == 0)
            {
                showSelectLevel();
                return;
            }
            if (score.getHealth() > 0)
            {
                resumeGame();
            }
            else
            {
                adTryAgain = true;
                showAd();
            }
        }
    }

    public void tryAgain () {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            scoreController score = gameControllerObject.GetComponent<scoreController>();
            if (score != null)
            {
                loadLevel(score.getLevel());
            }
        }
    }

    public void showSelectLevel()
    {
        resetGUI();
        GameObject menuGUI = GameObject.FindGameObjectWithTag("menu");
        Transform selectLevelMenu = menuGUI.transform.FindChild("selectLevel");
        selectLevelMenu.gameObject.SetActive(true);

        Dropdown levelDropdown = GameObject.Find("SelectLevelDropdown").GetComponent<Dropdown>();

        List<string> levelsList = new List<string>();

        levelsList.Add("Level 1" + isLocked(1));
        levelsList.Add("Level 2" + isLocked(2));
        levelsList.Add("Level 3" + isLocked(3));
        levelsList.Add("Level 4" + isLocked(4));
        levelsList.Add("Level 5" + isLocked(5));
        levelsList.Add("Level 6" + isLocked(6));
        levelsList.Add("Level 7" + isLocked(7));
        levelsList.Add("Level 8" + isLocked(8));
        levelsList.Add("Level 9" + isLocked(9));
        levelsList.Add("Endless Mode" + isLocked(10));

        levelDropdown.ClearOptions();
        levelDropdown.AddOptions(levelsList);

    }

    private string isLocked(int level)
    {
        if (Game.current.unlockedLevels.Contains(level))
        {
            return "";
        }
        else
        {
            return " (Locked)";
        }
    }

    public void hideSelectLevel()
    {
        GameObject menuGUI = GameObject.FindGameObjectWithTag("menu");
        Transform selectLevelMenu = menuGUI.transform.FindChild("selectLevel");
        selectLevelMenu.gameObject.SetActive(false);
    }

    public void showHighScores()
    {
        resetGUI();
        GameObject menuGUI = GameObject.FindGameObjectWithTag("menu");
        Transform highScoresMenu = menuGUI.transform.FindChild("highScores");
        highScoresMenu.gameObject.SetActive(true);
        StartCoroutine(GetScores());
    }

    public void hideHighScores()
    {
        GameObject menuGUI = GameObject.FindGameObjectWithTag("menu");
        Transform highScoresMenu = menuGUI.transform.FindChild("highScores");
        highScoresMenu.gameObject.SetActive(false);
    }

    public void showMainView()
    {
        resetGUI();
        GameObject menuGUI = GameObject.FindGameObjectWithTag("menu");
        Transform mainMenu = menuGUI.transform.FindChild("mainView");
        mainMenu.gameObject.SetActive(true);

        GameObject.Find("totalStars").GetComponent<Text>().text = "Total Stars Earned: " + Game.current.totalStarsCollected;
        GameObject.Find("totalKills").GetComponent<Text>().text = "Total Enemies Destroyed: " + Game.current.totalEnemiesKilled;
        GameObject.Find("scoreRanking").GetComponent<Text>().text = "Score Ranking: N/A";
        GameObject.Find("bestScore").GetComponent<Text>().text = "Best Personal Score: " + Game.current.bestPersonalScore;

        resumeOrTryText();
    }

    public void hideMainView()
    {
        GameObject menuGUI = GameObject.FindGameObjectWithTag("menu");
        Transform selectLevelMenu = menuGUI.transform.FindChild("mainView");
        selectLevelMenu.gameObject.SetActive(false);
    }

    public void showSelectShip()
    {
        resetGUI();
        GameObject menuGUI = GameObject.FindGameObjectWithTag("menu");
        Transform selectLevelMenu = menuGUI.transform.FindChild("selectShip");
        selectLevelMenu.gameObject.SetActive(true);
    }

    public void hideSelectShip()
    {
        GameObject menuGUI = GameObject.FindGameObjectWithTag("menu");
        Transform selectLevelMenu = menuGUI.transform.FindChild("selectShip");
        selectLevelMenu.gameObject.SetActive(false);
    }

    public void resetGUI()
    {
        uiSound();
        hideMainView();
        hideSelectLevel();
        hideSelectShip();
        hideHighScores();
        hideInfoDialog();
    }

    public void hideMenu()
    {
        GameObject menu = GameObject.FindGameObjectWithTag("menu");
        Canvas menuCanvas = menu.GetComponent<Canvas>();
        menuCanvas.enabled = false;
    }

    public void loadLevelFromForm()
    {
        uiSound();
        GameObject levelListBox = GameObject.Find("SelectLevelDropdown");
        Dropdown levelDropdown = levelListBox.GetComponent<Dropdown>();

        GameObject gameEngine = GameObject.Find("GameEngine");
        gameEngine engine = gameEngine.GetComponent<gameEngine>();

        if (Game.current.unlockedLevels.Contains(levelDropdown.value + 1))
        {
            loadLevel(levelDropdown.value + 1); // true ListBox values start at 0
        }
        else
        {
            showInfoDialog("You must first clear previous levels before you will be able to access this level!");
        }

    }

    public void showInfoDialog(string data)
    {
        uiSound();
        GameObject menuGUI = GameObject.FindGameObjectWithTag("menu");
        Transform infoDialog = menuGUI.transform.FindChild("infoDialog");
        infoDialog.gameObject.SetActive(true);

        Text infoDialogText = GameObject.Find("infoDialogText").GetComponent<Text>();
        infoDialogText.text = data;
    }

    public void hideInfoDialog()
    {
        uiSound();
        GameObject menuGUI = GameObject.FindGameObjectWithTag("menu");
        Transform infoDialog = menuGUI.transform.FindChild("infoDialog");
        infoDialog.gameObject.SetActive(false);
    }

    public void loadLevel(int level)
    {
        hideMenu();
        Cursor.visible = false;
        Time.timeScale = 1;

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            scoreController score = gameControllerObject.GetComponent<scoreController>();
            if (score != null)
            {
                score.updateLevelText(level);
                score.resetStars();
                score.resetScore();
                score.resetHealth();
            }
        }

        SceneManager.LoadScene(0);

        GameObject gameEngine = GameObject.Find("GameEngine");
        gameEngine engine = gameEngine.GetComponent<gameEngine>();
        engine.startLevel(level, false);
    }

    public void addHighScore()
    {
        GameObject highScore = Instantiate(AddScoreWidget, new Vector2(0f, 0f), Quaternion.identity) as GameObject;
    }

    public void musicToggle()
    {
        uiSound();
        GameObject music = GameObject.FindGameObjectWithTag ("music");
		AudioSource bg = music.GetComponent<AudioSource> ();
		if (bg.mute == true) {
			bg.mute = false;
		} else {
			bg.mute = true;
		}
	}

	public void shipToggle() {
		GameObject ship = GameObject.Find ("playerShip");
		Transform puppyShip = ship.transform.FindChild ("puppyShip");
		Transform craftShip = ship.transform.FindChild ("craftShip");
		if (puppyShip.gameObject.activeSelf == true) {
			puppyShip.gameObject.SetActive (false);
			craftShip.gameObject.SetActive (true);
		} else {
			puppyShip.gameObject.SetActive (true);
			craftShip.gameObject.SetActive (false);

		}
	}

	public void resumeGame() {
        hideMenu();
        Cursor.visible = false;
		Time.timeScale = 1;
	}

    public void showAd()
    {
        Cursor.visible = true;
        Time.timeScale = 0;
        ShowOptions options = new ShowOptions();
        options.resultCallback = HandleShowResult;
        Advertisement.Show("video", options);
    }

    public void spawnOrb()
    {
        float randomPositionYStar = Random.Range(ymin, ymax);
        float randomPositionXStar = Random.Range(xmin, xmax);
        GameObject starOrb = GenericObjectPooler.current.GetGenericGameObject("starOrb");
        if (starOrb != null)
        {
            starOrb.transform.localScale = new Vector3(7.5f, 7.5f, 0);
            starOrb.transform.position = new Vector3(randomPositionXStar, randomPositionYStar, 1f);
            starOrb.SetActive(true);
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        if (adTryAgain)
        {
            tryAgain();
        }
        else
        {
            Cursor.visible = false;
            Time.timeScale = 1;

            switch (result)
            {
                case ShowResult.Finished:
                    spawnOrb();
                    spawnOrb();
                    spawnOrb();
                    spawnOrb();
                    spawnOrb();
                    spawnOrb();
                    spawnOrb();
                    spawnOrb();
                    spawnOrb();
                    spawnOrb();
                    break;
                case ShowResult.Skipped:
                    break;
                case ShowResult.Failed:
                    break;
            }

        }
    }

    public void showMenu() {
        GameObject menu = GameObject.FindGameObjectWithTag ("menu");
		Canvas menuCanvas = menu.GetComponent<Canvas>();
		menuCanvas.enabled = true;
		Cursor.visible = true;
		Time.timeScale = 0;
        resetGUI();
        showMainView();
        SaveLoad.Save();
    }
    
    public void startScreen()
    {
        GameObject menu = GameObject.FindGameObjectWithTag("menu");
        Canvas menuCanvas = menu.GetComponent<Canvas>();
        menuCanvas.enabled = true;
        Cursor.visible = true;
        Time.timeScale = 0;
        resetGUI();
        showSelectLevel();
    }

    public void exitGame()
    {
        uiSound();
        SaveLoad.Save();
        Application.Quit ();
	}

    IEnumerator GetScores()
    {
        uiSound();
        Text scoreDataText = GameObject.Find("scoreDataText").GetComponent<Text>();
        scoreDataText.text = "Loading...";
        WWW hs_get = new WWW("http://www.aliensurge.com/api/getScores.php");
        yield return hs_get;

        if (hs_get.error != null)
        {
            print("There was an error getting the high score: " + hs_get.error);
        }
        else
        {
            scoreDataText.text = hs_get.text;
        }
    }
}
