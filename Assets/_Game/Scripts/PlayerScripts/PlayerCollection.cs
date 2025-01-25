using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollection : MonoBehaviour
{
    #region Member Variables

    public List<ItemBase> ItemCollections {  get; private set; }

    #endregion

    #region Public Methods

    #endregion

    #region Private Methods

    private void AddItemToCollection(ItemBase item)
    {
        ItemCollections.Add(item);
    }

    private void RemoveItemFromCollection(ItemBase item) { 
        ItemCollections.Remove(item);
    }

    #endregion
}
