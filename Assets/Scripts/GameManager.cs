using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class GameManager : MonoBehaviour
{
    private static GameManager m_Instance;
    public static GameManager Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindObjectOfType<GameManager>();
                if (m_Instance == null)
                {
                    GameObject go = new GameObject("Game Manager");
                    m_Instance = go.AddComponent<GameManager>();
                }
            }
            return m_Instance;
        }
    }

    public GameObject[] levelPrefabs;
    private GameObject currentLevel;

    private Ball ball;
    private Plataforma paddle;
    private Bricks[] bricks;

    private int score = 0;
    private int lives = 3;

    private void Awake()
    {
        if (m_Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        m_Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        NewGame();
    }

    public void LoadLevel(int levelIndex)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }

        if (levelIndex >= 0 && levelIndex < levelPrefabs.Length)
        {
            currentLevel = Instantiate(levelPrefabs[levelIndex]);
            FindLevelReferences(currentLevel);
        }
        else
        {
            Debug.LogWarning("Level index out of range.");
        }
    }

    private void FindLevelReferences(GameObject level)
    {
        ball = level.GetComponentInChildren<Ball>();
        paddle = level.GetComponentInChildren<Plataforma>();
        bricks = level.GetComponentsInChildren<Bricks>();
    }

    public void OnBallMiss()
    {
        lives--;

        if (lives > 0)
        {
            ResetLevel();
        }
        else
        {
            GameOver();
        }
    }

    private void ResetLevel()
    {
        paddle.ResetPaddle();
        ball.ResetBall();
    }

    private void GameOver()
    {
        // Implement your game over logic here
        // For example, you can reset the game or load a game over scene
        NewGame();
    }

    private void NewGame()
    {
        score = 0;
        lives = 3;

        LoadNextLevel();
    }

    public void OnBrickHit(Bricks brick)
    {
        score += brick.points;

        if (Cleared())
        {
            LoadNextLevel();
        }
    }

    private bool Cleared()
    {
        for (int i = 0; i < bricks.Length; i++)
        {
            if (bricks[i].gameObject.activeInHierarchy && !bricks[i].unbreakable)
            {
                return false;
            }
        }

        return true;
    }

    private void LoadNextLevel()
    {
        // Choose a random index to select the next level
        int randomIndex = Random.Range(0, levelPrefabs.Length);
        LoadLevel(randomIndex);

        // Reiniciar la pelota después de cargar el próximo nivel
        ResetBall();
    }

    private void ResetBall()
    {
        if (ball != null)
        {
            ball.ResetBall();
        }

    }
}