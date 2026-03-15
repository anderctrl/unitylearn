using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverButtons : MonoBehaviour
{
    public Button playButton;
    public Button exitButton;

    private void Start()
    {
        playButton.onClick.AddListener(PlayAgain);
        exitButton.onClick.AddListener(ExitGame);
    }

    private void Oestroy()
    {
        playButton.onClick.RemoveAllListeners();        
        exitButton.onClick.RemoveAllListeners();        
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("main");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}