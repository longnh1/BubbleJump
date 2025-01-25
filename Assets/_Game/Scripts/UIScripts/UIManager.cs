using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIID
{
    MainMenuCanvas,
    LevelSelectCanvas,
    GamePlayCanvas,
    WinCanvas
}

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private List<UICanvas> uiCanvas;

    private Dictionary<UIID, UICanvas> uiCanvasDict = new ();

    void Awake()
    {
        //Setup UI dict
        foreach (var item in uiCanvas) { 
            if (!uiCanvasDict.ContainsKey(item.UIID)) {
                uiCanvasDict.Add (item.UIID, item);
            }
        }

        HideAllCanvas();
    }

    public void ShowCanvas(UIID id)
    {
        if (uiCanvasDict.ContainsKey(id))
        {
            uiCanvasDict[id].Show();
        }
    }

    public void HideCanvas(UIID id)
    {
        if (uiCanvasDict.ContainsKey(id))
        {
            uiCanvasDict[id].Hide();
        }
    }

    private void HideAllCanvas()
    {
        foreach (var item in uiCanvas) { 
            item.gameObject.SetActive(false);
        }
    }
}

