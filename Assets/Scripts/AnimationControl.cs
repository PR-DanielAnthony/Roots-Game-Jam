using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationControl : MonoBehaviour
{
    [SerializeField] private string startScene;

    [SerializeField] private AudioClip sparkle;

    // insert first level name here
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            SceneManager.LoadScene(startScene);
    }

    public void ClickToContinue()
    {
        FindObjectsOfType<ClickPrompt>(true)[0].gameObject.SetActive(true);
    }
    public void PlayClip()
    {
        GetComponent<AudioSource>().Play();
        GetComponent<AudioSource>().PlayOneShot(sparkle);
    }
}