using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public void Awake()
    {
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        /*int maxScreenHeight = 1280;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }*/
    }

    private void Start()
    {
        //TODO: Load scene
        UIManager.Instance.ShowCanvas(UIID.MainMenuCanvas);

    }

    public static void PauseGame()
    {
        Time.timeScale = 0;
    }

    public static void ContinueGame()
    {
        Time.timeScale = 1;
    }

}
