using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("System/Level"), fileName = ("Level"))]
public class LevelCreationScriptable : ScriptableObject
{
    [Header("Basic")]
    public int level;
    public Vector3 PlayerSpawnPos;
    public float MinDistance;
    public float MaxDistance;
    public PlayerState[] LevelState;
    [Range(4, 25)]
    public int[] ObstacleCount;
    [Space(5)]
    [Header("Running Race")]
    public GameObject[] RunningObstackleToSpawn;
    public GameObject[] RununingField;



    [Space(5)]
    [Header("Swiming Race")]
    public GameObject[] SwimingObstackleToSpawn;
    public GameObject[] SwimingFileldField;




    [Space(5)]
    [Header("Cycling Race")]
    public CyclingCurve[] curves;
    public GameObject[] straightField;
}
