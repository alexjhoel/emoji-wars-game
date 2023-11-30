using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "ScriptableObjects/WaveData", order = 1)]
public class WaveData : ScriptableObject
{
    //SCritpableObject que representa una horda/oleada de enemigos

    public List<GameObject> enemiesList;

    public float delay;
    public bool isBigWave;
}