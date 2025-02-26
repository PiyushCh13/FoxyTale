using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData" , menuName = "Data/Level",order = 1)]
public class LevelDataSO : ScriptableObject
{
    public SceneList levelName;
    public bool isUnlocked;
    public int diamondsCollected;
    public int diamondsTotal;
}
