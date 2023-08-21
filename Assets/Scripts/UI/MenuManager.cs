using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private Menu[] menus;
    private Menu currentMenu;
    private readonly Stack<Menu> history = new();

    protected virtual void Start()
    {
        menus = GetComponentsInChildren<Menu>(true);

        foreach (var menu in menus)
        {
            menu.Initialize();
            menu.Hide();

            if (menu == menus.First())
                Show(menu);
        }
    }

    public void Show(Menu menu, bool remember = true)
    {
        if (currentMenu)
        {
            if (remember)
                history.Push(currentMenu);

            currentMenu.Hide();
        }

        menu.Show();
        currentMenu = menu;
    }

    public void Show<T>(bool remember = true, bool hide = true) where T : Menu
    {
        foreach (var menu in menus)
            if (menu is T)
            {
                if (currentMenu)
                {
                    if (remember)
                        history.Push(currentMenu);

                    if (hide)
                        currentMenu.Hide();
                }

                menu.Show();
                currentMenu = menu;
            }
    }

    public void Back()
    {
        if (history.Count > 0)
            Show(history.Pop(), false);
    }

    public T GetMenu<T>() where T : Menu
    {
        foreach (var menu in menus)
            if (menu is T tView)
                return tView;

        return null;
    }

    public Menu GetCurrentMenu()
    {
        return currentMenu;
    }
}