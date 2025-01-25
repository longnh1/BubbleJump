using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCanvas : UICanvas
{
    public void OnNextLevelClick()
    {
        SceneManager.LoadScene(0);
    }

    public void OnRestartClick()
    {
        SceneManager.LoadScene(0);
    }

    public void OnMainMenuClick()
    {
        UIManager.Instance.ShowCanvas(UIID.MainMenuCanvas);
        Hide();
    }
}
