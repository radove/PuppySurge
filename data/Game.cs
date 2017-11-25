using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Game {
    public static Game current;
    public string name;
    public int totalStarsCollected;
    public int bestPersonalScore;
    public int totalEnemiesKilled;
    public List<int> unlockedLevels;
    public List<int> unlockedShips;
    public List<int> unlockedModes;
    public string uuid;
    public Game()
    {
        this.name = "";
        this.totalStarsCollected = 0;
        this.bestPersonalScore = 0;
        this.totalEnemiesKilled = 0;
        this.unlockedLevels = new List<int>();
        this.unlockedShips = new List<int>();
        this.unlockedModes = new List<int>();
        this.uuid = System.Guid.NewGuid().ToString();
        this.unlockedLevels.Add(1);
        this.unlockedShips.Add(0);
        this.unlockedModes.Add(0);
    }
}
