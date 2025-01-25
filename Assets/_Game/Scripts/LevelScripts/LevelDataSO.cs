using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects")]
public class LevelDataSO : ScriptableObject
{
    public List<ItemType> itemList;
}

public enum ItemType
{
    Flower,
    Bee
}