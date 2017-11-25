using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Analytics;

public class gameEngine : MonoBehaviour {

    public GameObject bossLevelOne;
    public GameObject bossLevelTwo;
    public GameObject bossLevelThree;
    public scoreController score;
    public int minPercent = 0;
    public int maxPercent = 100;

    //private Material[] mats;
    private float waitSec = 1f;
    private int ymin = -365;
    private int ymax = 465;
    private int xmin = 1300;
    private int xmax = 1400;
    private Material colorCache;
    private startOver menu;

    // Use this for initialization
    void Start () {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            score = gameControllerObject.GetComponent<scoreController>();
        }
        if (score == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }

        GameObject menuControls = GameObject.Find("menuControls");
        menu = menuControls.GetComponent<startOver>();
        menu.startScreen();
    }



    public void Awake()
    {
        DontDestroyOnLoad(this);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }

        GameObject menuControls = GameObject.Find("menuControls");
        menu = menuControls.GetComponent<startOver>();
    }

    public void startLevel(int level, bool showAd)
    {
        score.updateLevelText(level);
        StopAllCoroutines();
        StartCoroutine(levelLauncher(level, showAd));

    }
    IEnumerator levelLauncher(int level, bool showAd)
    {

        GameObject levelClearedSound = GenericObjectPooler.current.GetGenericGameObject("soundLevelComplete");
        if (levelClearedSound != null)
        {
            levelClearedSound.transform.position = transform.position;
            levelClearedSound.SetActive(true);
        }
        int timer = 0;
        if (level == 10)
        {
            score.launchTutorial("Endless Mode");
        }
        else
        {
            score.launchTutorial("Level " + level);
        }
        StartCoroutine(SpawnHealthOrbs());
        while (timer < 5)
        {
            spawnOrbs(100);
            yield return StartCoroutine(Wait(waitSec));
            timer++;
        }
        
        if (showAd)
        {
            menu.showAd();
        }

        while (timer < 10)
        {
            spawnOrbs(100);
            yield return StartCoroutine(Wait(waitSec));
            timer++;
        }

        if (level == 1) { StartCoroutine(level1()); }
        if (level == 2) { StartCoroutine(level2()); }
        if (level == 3) { StartCoroutine(level3()); }
        if (level == 4) { StartCoroutine(level4()); }
        if (level == 5) { StartCoroutine(level5()); }
        if (level == 6) { StartCoroutine(level6()); }
        if (level == 7) { StartCoroutine(level7()); }
        if (level == 8) { StartCoroutine(level8()); }
        if (level == 9) { StartCoroutine(level9()); }
        if (level == 10) { StartCoroutine(endless()); }
        //menu.addHighScore();
        //menu.showMenu();
    }


    IEnumerator level1()
    {
        int timer = 0;

        while (timer < 30)
        {
            yield return StartCoroutine(Wait(waitSec));
            spawnSmallEnemy(50, 0);
            timer++;
        }

        while (timer < 60)
        {
            spawnBadBubble(50, 1, new Color(0f, 255f, 213f, 255f), 200f, 1f, 1f);
            yield return StartCoroutine(Wait(waitSec));
            spawnOrbEnemy(50, 1);
            spawnSmallEnemy(50, -1);
            timer++;
        }
        spawnLightRounder(100, 1);

        while (timer < 90)
        {
            //Wait 1 second
            spawnBadBubble(50, 1, new Color(0f, 255f, 213f, 255f), 200f, 1f, 1f);
            yield return StartCoroutine(Wait(waitSec));
            spawnOrbEnemy(50, 2);
            spawnSmallEnemy(50, -1);
            timer++;
        }

        startLevel(2, true);
    }

    IEnumerator level2()
    {
        int timer = 0;
        while (timer < 30)
        {
            yield return StartCoroutine(Wait(waitSec));
            spawnOrbEnemy(50, 2);
            spawnBadBubble(50, 2, new Color(255f, 255f, 255f, 255f), 300f, 0.8f, 1f);
            timer++;
        }

        spawnLightGlider(100, 1);
        while (timer < 60)
        {
            yield return StartCoroutine(Wait(waitSec));
            spawnOrbEnemy(50, 2);
            spawnBadBubble(50, 3, new Color(255f, 255f, 255f, 255f), 300f, 0.6f, 1f);
            timer++;
        }
        spawnBossLevelTwo();

        while (timer < 90)
        {
            yield return StartCoroutine(Wait(waitSec));
            spawnBadBubble(50, 2, new Color(255f, 255f, 255f, 255f), 300f, 0.6f, 1f);
            timer++;
        }

        startLevel(3, true);
    }

    IEnumerator level3()
    {
        int timer = 0;
        while (timer < 30)
        {
            yield return StartCoroutine(Wait(waitSec));
            spawnAlienAttack(100, 1);
            timer++;
        }
        spawnLightRounder(100, 1);

        while (timer < 60)
        {
            spawnSmallEnemy(50, 0);
            spawnAlienAttack(100, 2);
            yield return StartCoroutine(Wait(waitSec));
            timer++;
        }
        spawnBossLevelThree();

        while (timer < 90)
        {
            spawnSmallEnemy(50, 0);
            spawnAlienAttack(100, 1);
            yield return StartCoroutine(Wait(waitSec));
            timer++;
        }

        yield return StartCoroutine(Wait(5f));

        startLevel(4, true);
    }

    IEnumerator level4()
    {
        int timer = 0;
        while (timer < 30)
        {
            spawnBlackAlien(100, 10, 800, 2, 0.5f);
            yield return StartCoroutine(Wait(waitSec));
            timer++;
        }
        spawnLightGlider(100, 1);


        while (timer < 60)
        {
            spawnBlackAlien(100, 20, 800, 2, 0.5f);
            spawnOrbEnemy(50, 1);
            yield return StartCoroutine(Wait(waitSec));
            timer++;
        }
        spawnLightRounder(100, 1);

        while (timer < 90)
        {
            spawnBlackAlien(100, 100, 800, 2, 0.5f);
            spawnOrbEnemy(50, 2);
            yield return StartCoroutine(Wait(waitSec));
            timer++;
        }
        startLevel(5, true);
    }

    IEnumerator level5()
    {
        int timer = 0;
        while (timer < 30)
        {
            spawnDinoHead(100, 25);
            spawnOrbEnemy(50, 1);
            yield return StartCoroutine(Wait(waitSec));
            timer++;
        }
        spawnLightRounder(100, 1);
        while (timer < 60)
        {
            spawnBlackAlien(100, 100, 800, 2, 0.5f);
            spawnDinoHead(100, 25);
            spawnOrbEnemy(50, 2);
            yield return StartCoroutine(Wait(waitSec));
            timer++;
        }
        while (timer < 90)
        {
            spawnAlienAttack(100, 1);
            spawnDinoHead(100, 25);
            spawnOrbEnemy(50, 2);
            yield return StartCoroutine(Wait(waitSec));
            timer++;
        }

        startLevel(6, true);
    }

    IEnumerator level6()
    {
        int timer = 0;
        while (timer < 30)
        {
            spawnBadBubble(100, 4, new Color(255f, 255f, 255f, 255f), 300f, 0.6f, 1f);
            spawnBadBubble(100, 4, new Color(255f, 255f, 255f, 255f), 300f, 0.6f, 1f);
            yield return StartCoroutine(Wait(waitSec));
            timer++;
        }
        spawnBossLevelTwo();
        while (timer < 60)
        {
            spawnOrbEnemy(50, 2);
            spawnBadBubble(50, 2, new Color(255f, 255f, 255f, 255f), 300f, 0.6f, 1f);
            yield return StartCoroutine(Wait(waitSec));
            timer++;
        }
        spawnLightGlider(100, 1);

        while (timer < 90)
        {
            spawnDinoHead(100, 25);
            spawnOrbEnemy(50, 2);
            spawnBadBubble(50, 3, new Color(255f, 255f, 255f, 255f), 300f, 0.6f, 1f);
            yield return StartCoroutine(Wait(waitSec));
            timer++;
        }


        startLevel(7, true);
    }

    IEnumerator level7()
    {
        spawnBossLevelTwo();
        int timer = 0;
        while (timer < 15)
        {
            spawnOrbEnemy(100, 2);
            spawnSmallEnemy(100, 0);
            yield return StartCoroutine(Wait(waitSec));
            timer++;
        }
        spawnLightRounder(100, 1);

        while (timer < 60)
        {
            spawnBadBubble(100, 1, new Color(255f, 0f, 0f, 255f), 400f, 2.5f, 7f);
            spawnOrbEnemy(50, 2);
            spawnSmallEnemy(50, 0);
            yield return StartCoroutine(Wait(waitSec));
            timer++;
        }

        spawnBossLevelThree();
        while (timer < 120)
        {
            spawnBadBubble(100, 1, new Color(255f, 0f, 0f, 255f), 400f, 2.5f, 7f);
            spawnSmallEnemy(50, 0);
            spawnOrbEnemy(100, 4);
            yield return StartCoroutine(Wait(waitSec));
            timer++;
        }

        startLevel(8, true);
    }
    
    IEnumerator level8()
    {
        int timer = 0;
        while (timer < 90)
        {
            spawnForEndless();
            yield return StartCoroutine(Wait(0.90f));
            timer++;
        }

        startLevel(9, true);
    }

    IEnumerator level9()
    {
        int timer = 0;
        while (timer < 90)
        {
            spawnForEndless();
            yield return StartCoroutine(Wait(0.70f));
            timer++;
        }

        startLevel(10, true);
    }

    IEnumerator endless()
    {
        float endlessWaitSec = 2f;
        int y = 0;
        int x = 0;
        while (true)
        {
            if (x > 30)
            {
                int timer = 0;
                x = 0;
                y++;
                endlessWaitSec = endlessWaitSec - 0.2f;
                score.launchTutorial("Waves Completed: " + y);

                while (timer < 5)
                {
                    spawnOrbs(100);
                    yield return StartCoroutine(Wait(waitSec));
                    timer++;
                }

                int randomNum = Random.Range(1, 3);

                if (randomNum == 1)
                {
                    spawnBossLevelThree();
                }
                if (randomNum == 2)
                {
                    spawnBossLevelTwo();
                }
                if (randomNum == 3)
                {
                    spawnBossLevelOne();
                }

                while (timer < 5)
                {
                    spawnOrbs(100);
                    yield return StartCoroutine(Wait(waitSec));
                    timer++;
                }
            }
            x++;
            spawnForEndless();
            yield return StartCoroutine(Wait(endlessWaitSec));
        }
    }

    IEnumerator SpawnHealthOrbs()
    {
        while (true)
        {
            spawnHealthOrb(50);
            yield return StartCoroutine(Wait(10f));
        }
    }

    IEnumerator SpawnStarOrbs()
    {
        while (true)
        {
            spawnOrbs(100);
            yield return StartCoroutine(Wait(5f));
        }
    }

    void spawnForEndless()
    {
        int randomNum = Random.Range(1, 10);
        if (randomNum == 1)
        {
            spawnSmallEnemy(100, 5);
        }
        if (randomNum == 2)
        {
            spawnBadBubble(100, 5, new Color(0f, 255f, 213f, 255f), 200f, 1f, 1f);
        }
        if (randomNum == 3)
        {
            spawnBadBubble(100, 5, new Color(255f, 255f, 255f, 255f), 300f, 0.8f, 1f);
        }
        if (randomNum == 4)
        {
            spawnOrbEnemy(100, 5);
        }
        if (randomNum == 5)
        {
            spawnAlienAttack(100, 5);
        }
        if (randomNum == 6)
        {
            spawnLightGlider(20, 1);
        }
        if (randomNum == 7)
        {
            spawnBlackAlien(100, 10, 800, 2, 0.5f);
        }
        if (randomNum == 8)
        {
            spawnDinoHead(100, 25);
        }
        if (randomNum == 9)
        {
            spawnLightRounder(20, 1);
        }
    }
    void spawnStarFall(float percent)
    {

        float percentChance = Random.Range(minPercent, maxPercent);
        if (percentChance < percent)
        {
            float randomPositionY = Random.Range(ymin, ymax);
            float randomPositionX = Random.Range(xmin, xmax);

            GameObject starFall = GenericObjectPooler.current.GetGenericGameObject("starFall");
            if (starFall != null)
            {
                starFall.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
                starFall.transform.localScale = new Vector3(100f, 100f, 1f);
                starFall.transform.position = new Vector3(randomPositionX, randomPositionY, 0);
                starFall.SetActive(true);
            }
        }

    }

    void spawnBossLevelOne()
    {
        float randomPositionY = Random.Range(ymin, ymax);
        float randomPositionX = Random.Range(xmin, xmax);

        bossLevelOne.transform.localScale = new Vector3(150f, 150f, 1f);
        bossLevelOne.transform.position = new Vector3(randomPositionX, randomPositionY, 0);
        Instantiate(bossLevelOne, bossLevelOne.transform.position, Quaternion.identity);
    }

    void spawnBossLevelTwo()
    {
        float randomPositionY = Random.Range(ymin, ymax);
        float randomPositionX = Random.Range(xmin, xmax);
        
        bossLevelTwo.transform.localScale = new Vector3(100f, 100f, 1f);
        bossLevelTwo.transform.position = new Vector3(randomPositionX, randomPositionY, 0);
        Instantiate(bossLevelTwo, bossLevelTwo.transform.position, Quaternion.identity);
    }

    void spawnBossLevelThree()
    {
        float randomPositionY = Random.Range(ymin, ymax);
        float randomPositionX = Random.Range(xmin, xmax);

        bossLevelThree.transform.localScale = new Vector3(0.4f, 0.4f, 1f);
        bossLevelThree.transform.position = new Vector3(randomPositionX, randomPositionY, 0);
        Instantiate(bossLevelThree, bossLevelThree.transform.position, Quaternion.identity);
    }

    void spawnOrbs(float percent)
    {
        float percentChance = Random.Range(minPercent, maxPercent);

        if (percentChance < percent)
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
    }

    void spawnHealthOrb(float percent)
    {
        float percentChance = Random.Range(minPercent, maxPercent);
        if (percentChance < percent)
        {
            float randomPositionYHealth = Random.Range(ymin, ymax);
            float randomPositionXHealth = Random.Range(xmin, xmax);
            GameObject healthOrb = GenericObjectPooler.current.GetGenericGameObject("healthOrb");
            if (healthOrb != null)
            {
                healthOrb.transform.localScale = new Vector3(7.5f, 7.5f, 1f);
                healthOrb.transform.position = new Vector3(randomPositionXHealth, randomPositionYHealth, 0);
                healthOrb.SetActive(true);
            }
        }
    }
    void spawnSmallEnemy(float percent, float minSpawn)
    {
        int currentSpawned = GameObject.FindGameObjectsWithTag("enemySmall").Length;
        float percentChance = Random.Range(minPercent, maxPercent);
        if (percentChance < percent)
        {
            float randomPositionY = Random.Range(ymin, ymax);
            float randomPositionX = Random.Range(xmin, xmax);
            GameObject enemySmall = GenericObjectPooler.current.GetGenericGameObject("enemySmall");
            if (enemySmall != null)
            {
                enemySmall.transform.localScale = new Vector3(75f, 75f, 1f);
                enemySmall.transform.position = new Vector3(randomPositionX, randomPositionY, 0);
                //enemySmall.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
                enemySmall.SetActive(true);
            }
        }
        if (currentSpawned <= minSpawn)
        {
            float randomPositionY = Random.Range(ymin, ymax);
            float randomPositionX = Random.Range(xmin, xmax);
            GameObject enemySmall = GenericObjectPooler.current.GetGenericGameObject("enemySmall");
            if (enemySmall != null)
            {
                enemySmall.transform.localScale = new Vector3(75f, 75f, 1f);
                enemySmall.transform.position = new Vector3(randomPositionX, randomPositionY, 0);
                //enemySmall.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
                enemySmall.SetActive(true);
            }
        }
    }

    void spawnAlienAttack(float percent, float maxSpawned)
    {
        if (GameObject.FindGameObjectsWithTag("alienAttacker").Length < maxSpawned)
        {
            float percentChance = Random.Range(minPercent, maxPercent);
            if (percentChance < percent)
            {
                float randomPositionY = Random.Range(ymin, ymax);
                float randomPositionX = Random.Range(xmin, xmax);
                GameObject alienAttack = GenericObjectPooler.current.GetGenericGameObject("alienAttack");
                if (alienAttack != null)
                {
                    alienAttack.transform.localScale = new Vector3(1f, 1f, 0f);
                    alienAttack.transform.position = new Vector3(randomPositionX, randomPositionY, 0);
                    alienAttack.SetActive(true);
                }
            }
        }
    }

    void spawnDinoHead(float percent, float maxSpawned)
    {
        if (GameObject.FindGameObjectsWithTag("dinoHead").Length < maxSpawned)
        {
            float percentChance = Random.Range(minPercent, maxPercent);
            if (percentChance < percent)
            {
                float randomPositionY = Random.Range(ymin, ymax);
                float randomPositionX = Random.Range(xmin, xmax);
                GameObject dinoHead = GenericObjectPooler.current.GetGenericGameObject("dinoHead");
                if (dinoHead != null)
                {
                    dinoHead.transform.localScale = new Vector3(1f, 1f, 0f);
                    dinoHead.transform.position = new Vector3(randomPositionX, randomPositionY, 0);
                    dinoHead.SetActive(true);
                }
            }
        }
    }

    void spawnLightGlider(float percent, float maxSpawned)
    {
        if (GameObject.FindGameObjectsWithTag("lightGlider").Length < maxSpawned)
        {
            float percentChance = Random.Range(minPercent, maxPercent);
            if (percentChance < percent)
            {
                float randomPositionY = Random.Range(ymin, ymax);
                float randomPositionX = Random.Range(xmin, xmax);
                GameObject lightGlider = GenericObjectPooler.current.GetGenericGameObject("lightGlider");
                if (lightGlider != null)
                {
                    lightGlider.transform.localScale = new Vector3(1f, 1f, 0f);
                    lightGlider.transform.position = new Vector3(randomPositionX, randomPositionY, 0);
                    lightGlider.SetActive(true);
                }
            }
        }
    }


    void spawnLightRounder(float percent, float maxSpawned)
    {
        if (GameObject.FindGameObjectsWithTag("lightRounder").Length < maxSpawned)
        {
            float percentChance = Random.Range(minPercent, maxPercent);
            if (percentChance < percent)
            {
                float randomPositionY = Random.Range(ymin, ymax);
                float randomPositionX = Random.Range(xmin, xmax);
                GameObject lightGlider = GenericObjectPooler.current.GetGenericGameObject("lightRounder");
                if (lightGlider != null)
                {
                    lightGlider.transform.localScale = new Vector3(1.5f, 1.5f, 0f);
                    lightGlider.transform.position = new Vector3(randomPositionX, randomPositionY, 0);
                    lightGlider.SetActive(true);
                }
            }
        }
    }

    void spawnBlackAlien(float percent, float maxSpawned, float speed, float howManyShouldSpawnEachProcess, float size)
    {
        if (GameObject.FindGameObjectsWithTag("blackAlien").Length < maxSpawned)
        {
            float percentChance = Random.Range(minPercent, maxPercent);
            if (percentChance < percent)
            {
                float randomPositionY = Random.Range(ymin, ymax);
                float randomPositionX = Random.Range(xmin, xmax);
                for (int i = 0; i < howManyShouldSpawnEachProcess; i++)
                {
                    GameObject blackAlien = GenericObjectPooler.current.GetGenericGameObject("blackAlien");
                    randomPositionX = randomPositionX + 100f;
                    if (blackAlien != null)
                    {
                        blackAlien.transform.localScale = new Vector3(size, size, 0f);
                        blackAlien.transform.position = new Vector3(randomPositionX, randomPositionY, 0);
                        blackAlien.SetActive(true);
                        blackAlien.SendMessage("setSpeed", speed);
                    }
                }
            }
        }
    }

    void spawnOrbEnemy(float percent, float maxSpawned)
    {
        if (GameObject.FindGameObjectsWithTag("orbAttacker").Length < maxSpawned)
        {
            float percentChance = Random.Range(minPercent, maxPercent);
            if (percentChance < percent)
            {
                float randomPositionY = Random.Range(ymin, ymax);
                float randomPositionX = Random.Range(xmin, xmax);
                GameObject orbAttacker = GenericObjectPooler.current.GetGenericGameObject("orbEnemy");
                if (orbAttacker != null)
                {
                    orbAttacker.transform.localScale = new Vector3(15f, 15f, 0f);
                    orbAttacker.transform.position = new Vector3(randomPositionX, randomPositionY, 0);
                    orbAttacker.SetActive(true);
                }
            }
        }
    }

    void spawnBadBubble(float percent, float maxSpawned, Color color, float speed, float spawnSpeed, float rotationSpeed)
    {
        int currentSpawned = GameObject.FindGameObjectsWithTag("badBubble").Length;
        if (currentSpawned < maxSpawned)
        {
            float percentChance = Random.Range(minPercent, maxPercent);
            if (percentChance < percent || currentSpawned == 0)
            {
                float randomPositionY = Random.Range(ymin, ymax);
                float randomPositionX = Random.Range(xmin, xmax);
                GameObject badBubble = GenericObjectPooler.current.GetGenericGameObject("badBubble");
                if (badBubble != null)
                {
                    badBubble.transform.localScale = new Vector3(25f, 25f, 1f);
                    badBubble.transform.position = new Vector3(randomPositionX, randomPositionY, 0);
                    badBubble.SetActive(true);
                    SpriteRenderer spriteRenderer = badBubble.GetComponent<SpriteRenderer>();
                    spriteRenderer.color = color;
                    badBubble.SendMessage("setSpeed", speed);
                    badBubble.SendMessage("setRotationSpeed", rotationSpeed);
                    badBubble.SendMessage("setSpawnSpeed", spawnSpeed);

                }
            }
        }
    }

    /**  
      IEnumerator ReportMetrics(string reportTitle)
      {
        yield return StartCoroutine(Wait(5f));
          GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
          Analytics.CustomEvent("playerProgression", new Dictionary<string, object>
          {
              { "title", reportTitle },
              { "level", score.getLevel() },
              { "health", score.getHealth() },
              { "stars", score.getTotalStars() },
              { "score", score.getScore() },
              { "numberOfGameObjects", allObjects.Length },
              { "deviceModel", SystemInfo.deviceModel },
              { "deviceName", SystemInfo.deviceName },
              { "deviceID", SystemInfo.deviceUniqueIdentifier },
              { "deviceOS", SystemInfo.operatingSystem },
              { "deviceProcessorType", SystemInfo.processorType },
              { "deviceMemorySize", SystemInfo.systemMemorySize }
          });
}
      **/

IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }


}
