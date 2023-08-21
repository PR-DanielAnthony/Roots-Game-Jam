using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleControl : MonoBehaviour
{
    [SerializeField] private string startScene;

    private GameObject mixer;
    private MusicManager music;

    public void StartGame()
    {
        SceneManager.LoadScene(startScene);
        mixer = GameObject.Find("Music Manager");
        music = mixer.GetComponent<MusicManager>();
        music.self.LiveTheMusic(Time.deltaTime, 2F);
    }

    public void QuitGame()
    {
        Application.Quit(0);
    }
}