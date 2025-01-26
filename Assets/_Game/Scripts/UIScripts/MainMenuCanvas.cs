using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCanvas : UICanvas
{
    public void OnPlayClick()
    {
        UIManager.Instance.ShowCanvas(UIID.LevelSelectCanvas);
        Hide();
    }

    public void OnCreditsClick()
    {

    }

    public void OnQuitClick()
    {
        Application.Quit();
    }
}
