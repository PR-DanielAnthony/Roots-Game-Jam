using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : Menu
{
    [SerializeField] private Button retryButton;
    [SerializeField] private Button quitButton;

    public override void Initialize()
    {
        retryButton.onClick.RemoveAllListeners();
        retryButton.onClick.AddListener(RetryGame);

        quitButton.onClick.RemoveAllListeners();
        quitButton.onClick.AddListener(QuitGame);
    }

    private void RetryGame()
    {
        // reload level or something?
        //menuManager.Show<GameMenu>();
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