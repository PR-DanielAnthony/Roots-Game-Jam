using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : Menu
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button quitButton;

    public override void Initialize()
    {
        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(ContinueGame);

        quitButton.onClick.RemoveAllListeners();
        quitButton.onClick.AddListener(QuitGame);
    }

    private void ContinueGame()
    {
        Time.timeScale = 1f;
        menuManager.Show<GameMenu>();
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