using UnityEngine;
using TMPro;

public class DialogueWindow : MonoBehaviour
{
    [SerializeField] private GameObject window;
    [SerializeField] private string dialogue;
    [SerializeField] private KeyCode closeKey;
    [SerializeField] private TextMeshPro textMesh;

    private void Start()
    {
        window.SetActive(false);
    }

    private void Update()
    {
        if (window.activeSelf)
            if (Input.GetKeyDown(closeKey))
                CloseWindow();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ShowWindow();
    }

    public void ShowWindow()
    {
        window.SetActive(true);
        SetDialogue(dialogue);
        Time.timeScale = 0f;
    }

    public void CloseWindow()
    {
        Time.timeScale = 1f;
        window.SetActive(false);
    }

    public void SetDialogue(string text)
    {
        textMesh.text = text;
    }
}