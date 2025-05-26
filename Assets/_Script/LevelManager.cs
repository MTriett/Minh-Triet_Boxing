using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public int currentLevel = 1;

    public float enemyHealth;
    
    public int extraEnemies = 0;

    public TextMeshProUGUI levelText;

    private void Start()
    {
        
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            currentLevel = PlayerPrefs.GetInt("Level", 1);
            GenerateLevelStats(currentLevel);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GenerateLevelStats(int level)
    {
        currentLevel = level;
        enemyHealth = 100 + level * 10;
        extraEnemies = Mathf.Max(1, level / 2);
        if (levelText != null)
        {
            levelText.text = "LEVEL " + currentLevel;
        }
    }

    public void LoadNextLevel()
    {
        currentLevel++;
        PlayerPrefs.SetInt("Level", currentLevel);
        GenerateLevelStats(currentLevel);
        SceneManager.LoadScene("GameScene");
    }

    public void RestartLevel()
    {
        GenerateLevelStats(currentLevel);
        SceneManager.LoadScene("GameScene");
    }
}
