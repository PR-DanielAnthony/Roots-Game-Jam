using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;

    private MusicManager music;

    private void Start()
    {
        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(StartGame);

        quitButton.onClick.RemoveAllListeners();
        quitButton.onClick.AddListener(QuitGame);
    }

    private void StartGame()
    {
        SceneManager.LoadScene("Tutorial Level");
        music = FindAnyObjectByType<MusicManager>();
        music.self.LiveTheMusic(Time.deltaTime, 2f);
    }

    // this can be in the game manager instead
    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}