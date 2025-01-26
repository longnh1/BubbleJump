using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUnit : MonoBehaviour
{
    [SerializeField] private GameObject checkIconObj;
    public ItemType type;
    public bool IsChecked { get; private set; }

    public void Init()
    {
        ShowCheckIcon(false);
    }

    public void ShowCheckIcon(bool show)
    {
        checkIconObj.SetActive(show);

        IsChecked = show;
    }
}
