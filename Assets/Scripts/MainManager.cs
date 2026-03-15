using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public MegaBrick MegaBrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;

    private bool m_Started = false;
    private int m_Points;

    public int score
    {
        get { return m_Points; }
        private set
        {
            m_Points = Mathf.Max(0, value);
            ScoreText.text = $"Score : {m_Points}";
        }
    }

    // ENCAPSULATION
    public int bestScore
    {
        get { return PlayerPrefs.GetInt("BestScore", 0); }
        private set { PlayerPrefs.SetInt("BestScore", value); }
    }

    private void SpawnBricks()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        var megaPosition = new Vector3(0, 2.5f + LineCount * 0.3f, 0);
        var megaBrick = Instantiate(MegaBrickPrefab, megaPosition, Quaternion.identity);
        megaBrick.onDestroyed.AddListener(AddPoint);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void LaunchBall()
    {
        float randomDirection = Random.Range(-1.0f, 1.0f);
        Vector3 forceDir = new Vector3(randomDirection, 1, 0).normalized;

        Ball.transform.SetParent(null);
        Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
    }

    void Start()
    {
        SpawnBricks(); // ABSTRACTION
    }

    private void Update()
    {
        if (!m_Started && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            m_Started = true;
            LaunchBall(); // ABSTRACTION
        }
    }

    void AddPoint(int point)
    {
        score += point;
    }

    public void GameOver()
    {
        if (score > bestScore)
        {
            bestScore = score;
        }

        SceneManager.LoadScene("gameover");
    }
}
