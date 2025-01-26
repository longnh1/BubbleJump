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
    [SerializeField] private AudioClip winSong;
    [SerializeField] private AudioClip menuSong;
    [SerializeField] private AudioClip gameSong;
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
        PlayMenuMusic(id);
    }
    
    public void PlayMenuMusic(UIID id){
    	AudioSource auSo = GetComponent<AudioSource>();
        if (id==UIID.GamePlayCanvas){
            auSo.Stop();
	    auSo.clip=gameSong;
	    auSo.Play();
        }else if(id==UIID.WinCanvas){
            auSo.Stop();
            auSo.clip=winSong;
            auSo.Play();
        }else{
            if (auSo.clip!=menuSong){
            	auSo.Stop();
                auSo.clip=menuSong;
                auSo.Play();
            }
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

