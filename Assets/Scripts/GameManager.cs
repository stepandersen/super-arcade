using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int World { get; private set; }
    public int Stage { get; private set; }
    public int Lives { get; private set; }
    public int Coins { get; private set; }

    private void Awake()
    {
        if (Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this) {
            Instance = null;
        }
    }

    private void Start()
    {
        Application.targetFrameRate = 60;

        NewGame();
    }

    public void NewGame()
    {
        Lives = 3;
        Coins = 0;

        LoadLevel(1, -1);
    }

    public void GameOver()
    {
        // TODO: show game over screen

        NewGame();
    }

    public void LoadLevel(int world, int stage)
    {
        this.World = world;
        this.Stage = stage;

        if(stage >= 0)
            SceneManager.LoadScene($"{world}-{stage}");
        else
            SceneManager.LoadScene($"{world}");
    }

    public void NextLevel()
    {
        LoadLevel(World, Stage + 1);
    }

    public void ResetLevel(float delay)
    {
        Invoke(nameof(ResetLevel), delay);
    }

    public void ResetLevel()
    {
        Lives--;

        if (Lives > 0) {
            LoadLevel(World, Stage);
        } else {
            GameOver();
        }
    }

    public void AddCoin()
    {
        Coins++;

        if (Coins == 100)
        {
            Coins = 0;
            AddLife();
        }
    }

    public void AddLife()
    {
        Lives++;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

}
