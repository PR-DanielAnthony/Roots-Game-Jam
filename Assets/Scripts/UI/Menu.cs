using UnityEngine;

public abstract class Menu : MonoBehaviour
{
    protected MenuManager menuManager;

    private void Awake()
    {
        menuManager = GetComponentInParent<MenuManager>();
    }

    public abstract void Initialize();

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}