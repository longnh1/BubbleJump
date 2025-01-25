using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    #region Inspector Variables

    [SerializeField] private LevelDataSO m_LevelDataSO;
    [SerializeField] private PlayerCollection playerCollection;


    #endregion

    #region Properties

    public int levelID { get; private set; }
    public bool IsLevelStart { get; private set; }


    #endregion

    #region Member Variables

    private Dictionary<ItemType, int> requireItems = new();

    #endregion

    #region Unity Methods

    private void OnEnable()
    {
        EventManager.Connect(Events.CHECK_WIN_CONDITION, CheckWin);
        EventManager.Connect(Events.START_GAME, ()=> { IsLevelStart = true; });
    }

    private void OnDisable()
    {
        EventManager.Disconnect(Events.CHECK_WIN_CONDITION, CheckWin);
        EventManager.Disconnect(Events.START_GAME, () => { IsLevelStart = true; });
    }

    #endregion

    #region Public Methods

    //TODO: Check if the player has collected everything on the list
    public void CheckWin()
    {
        if (requireItems.Count <= 0) { 
            //WIN
            IsLevelStart = false;

            UIManager.Instance.HideCanvas(UIID.GamePlayCanvas);
            UIManager.Instance.ShowCanvas(UIID.WinCanvas);
        }
    }

    public void RemoveRequiredItemFromDict(ItemType item)
    {
        if (requireItems.ContainsKey(item)) {
            if (requireItems[item] > 0) { 
                requireItems[item]--; 
                if (requireItems[item] <= 0) 
                    requireItems.Remove(item);
            }
        }
    }
    
    #endregion

    #region Private Methods

    private void LoadLevelData()
    {
        levelID = m_LevelDataSO.id;
        foreach (var item in m_LevelDataSO.itemList)
        {
            if (requireItems.ContainsKey(item))
            {
                requireItems[item]++;
            }
            else
            {
                requireItems.Add(item, 1);
            }
        }
    }

    #endregion
}
