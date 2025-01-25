using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectCanvas : UICanvas
{
    //Hard-coding since we're running out of time ehehe
    public void OnLevelOneClick()
    {
        UIManager.Instance.ShowCanvas(UIID.GamePlayCanvas);
        EventManager.SendSimpleEvent(Events.START_GAME);
        Hide();
    }

    public void OnBackClick()
    {
        UIManager.Instance.ShowCanvas(UIID.MainMenuCanvas);
        Hide();
    }
}
