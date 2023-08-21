using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public string startScene;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            SceneManager.LoadScene(startScene);
    }
}
