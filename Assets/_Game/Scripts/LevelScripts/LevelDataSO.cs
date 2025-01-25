using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects")]
public class LevelDataSO : ScriptableObject
{
    public int id;
    public List<ItemType> itemList;
}

public enum ItemType
{
    Flower,
    Sapling,
    Water,
    Fruit,
    Bee
}