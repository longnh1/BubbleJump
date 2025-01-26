using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayCanvas : UICanvas
{
    [SerializeField] private ItemUnit[] itemUnits;

    private void Start()
    {
        foreach (var unit in itemUnits) { 
            unit.Init();        
        }
    }

    public void CheckItemByType(ItemType type)
    {
        foreach (ItemUnit itemUnit in itemUnits) {
            if (itemUnit.IsChecked) continue;

            if (itemUnit.type == type) {
                itemUnit.ShowCheckIcon(true);
                break;
            }
        
        }
    }
}
