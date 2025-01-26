using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollection : MonoBehaviour
{
    #region Inspector Variables

    [SerializeField] private LevelManager levelManager;

    #endregion

    #region Member Variables

    public List<ItemBase> ItemCollections {  get; private set; }

    #endregion

    #region Public Methods

    #endregion

    #region Private Methods

    public void CollectionItem(ItemBase item)
    {
        levelManager.RemoveRequiredItemFromDict(item.type);
    }

    private void RemoveItemFromCollection(ItemBase item) { 
        ItemCollections.Remove(item);
    }

    #endregion
}
