using UnityEngine;
using TMPro;

public class GameMenu : Menu
{
    [SerializeField] private TextMeshProUGUI sporeCount;
    [SerializeField] private TextMeshProUGUI timer;

    public override void Initialize()
    {
        UpdateSporeCount();
    }

    private void Update()
    {
        if (this != menuManager.GetCurrentMenu())
            return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Time.timeScale = 0f;
            menuManager.Show<PauseMenu>();
        }

        //timer.text = GameManager.gameManagerReference.timeTillRespawn.ToString();
        UpdateSporeCount();
    }

    private void UpdateSporeCount()
    {
        sporeCount.text = $"{GameManager.gameManagerReference.DeadMushroomsAllowed}";
    }
}