using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    //private EventSystem eventSystem;

    //private Canvas popUpCanvas;
    //private Stack<PopUpUI> popUpStack;

    ////private Canvas windowCanvas;
    ////private List<WindowUI> windowList;

    //private void Awake()
    //{
    //    eventSystem = GameManager.Resource.Instantiate<EventSystem>("UI/EventSystem");
    //    eventSystem.transform.parent = transform;

    //    popUpCanvas = GameManager.Resource.Instantiate<Canvas>("UI/GameMenuCanvas");
    //    popUpCanvas.gameObject.name = "PopUpCanvas";
    //    popUpCanvas.sortingOrder = 100;
    //    popUpStack = new Stack<PopUpUI>();

    //    //windowCanvas = GameManager.Resource.Instantiate<Canvas>("UI/InventoryUI");
    //    //windowCanvas.gameObject.name = "WindowCanvas";
    //    //windowCanvas.sortingOrder = 10;
    //    //windowList = new List<WindowUI>();
    //}
    //public T ShowPopUpUI<T>(T popUpui) where T : PopUpUI
    //{
    //    if (popUpStack.Count > 0)
    //    {
    //        PopUpUI prevUI = popUpStack.Peek();
    //        prevUI.gameObject.SetActive(false);
    //    }

    //    T ui = GameManager.Pool.GetUI(popUpui);
    //    ui.transform.SetParent(popUpCanvas.transform, false);

    //    popUpStack.Push(ui);

    //    Time.timeScale = 0;
    //    return ui;
    //}

    //public PopUpUI ShowPopUpUI(string path)
    //{
    //    PopUpUI ui = GameManager.Resource.Load<PopUpUI>(path);
    //    return ShowPopUpUI(ui);
    //}

    //public void ClosePopUpUI()
    //{
    //    PopUpUI ui = popUpStack.Pop();
    //    GameManager.Pool.ReleaseUI(ui.gameObject);

    //    if (popUpStack.Count > 0)
    //    {
    //        PopUpUI curUI = popUpStack.Peek();
    //        curUI.gameObject.SetActive(true);
    //    }

    //    if (popUpStack.Count == 0)
    //    {
    //        Time.timeScale = 1f;
    //    }
    //}

    //public T ShowWindowUI<T>(T windowUI) where T : WindowUI
    //{
    //    windowList.Add(windowUI);

    //    T ui = GameManager.Pool.GetUI(windowUI);
    //    // 상대 위치
    //    ui.transform.SetParent(windowCanvas.transform, false);
    //    return ui;
    //}

    //public void ShowWindowUI(string path)
    //{
    //    WindowUI ui = GameManager.Resource.Load<WindowUI>(path);
    //    ShowWindowUI(ui);
    //}
    
    //public void SelectWindowUI(WindowUI windowUI)
    //{

    //    windowUI.transform.SetAsLastSibling();
    //}

    //public void CloseWindowUI(WindowUI windowUI)
    //{
    //    GameManager.Pool.ReleaseUI(windowUI.gameObject);
    //}


}
