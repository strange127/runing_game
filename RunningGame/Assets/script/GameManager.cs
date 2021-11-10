using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int MaxPlayer = 5;
    public GameObject PlayerSpawnObject;
    public PlayerMoment plalyermoment;
    public GameObject PlayerStandingPoint;
    public PlayerCamera Camera;

    public LevelCreationScriptable AutoLevelMaking;
    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        MaxPlayer = 5;
        LevelCreate();

    }
    public Transform SpawnHOlder;

    void LevelCreate()
    {
        PlayerSpawn();
        SpawnField();

    }

    void RunningField(int obstacke)
    {
        int obstacklespawnid = Random.Range(0, AutoLevelMaking.RunningObstackleToSpawn.Length);

        Vector3 pos = AutoLevelMaking.PlayerSpawnPos;
        for (int i = 0; i < 5; i++)
        {
            pos = new Vector3(pos.x + 10, pos.y, Size);

            for (int j = 0; j < AutoLevelMaking.ObstacleCount[obstacke]; j++)
            {
                float dis = Random.Range(AutoLevelMaking.MinDistance, AutoLevelMaking.MaxDistance);
                pos = new Vector3(pos.x, pos.y, pos.z + dis);


                Instantiate(AutoLevelMaking.RunningObstackleToSpawn[obstacklespawnid], pos, Quaternion.identity, SpawnHOlder);
            }
        }
    }
    void SwimingField(int obstacke)
    {
    //    int obstacklespawnid = Random.Range(0, AutoLevelMaking.SwimingObstackleToSpawn.Length);

        Vector3 pos = AutoLevelMaking.PlayerSpawnPos;
        for (int i = 0; i < 5; i++)
        {
            //pos = new Vector3(pos.x + 10, pos.y, Size);

            //for (int j = 0; j < AutoLevelMaking.ObstacleCount[obstacke]; j++)
            //{
            //    float dis = Random.Range(AutoLevelMaking.MinDistance, AutoLevelMaking.MaxDistance);
            //    pos = new Vector3(pos.x, pos.y, pos.z + dis);


            ////    Instantiate(AutoLevelMaking.SwimingObstackleToSpawn[obstacklespawnid], pos, Quaternion.identity, SpawnHOlder);
            //}
        }
    }

    private List<GameObject> Road = new List<GameObject>();
    void CyclingField()
    {
        for (int i = 0; i < AutoLevelMaking.curves.Length; i++)
        {
            if (i == 0)
            {
                Road.Add(Instantiate(AutoLevelMaking.straightField[0], new Vector3(AutoLevelMaking.PlayerSpawnPos.x + 5, AutoLevelMaking.PlayerSpawnPos.y - 2, Size), Quaternion.identity, SpawnHOlder));
            }
            else
            {
                Road.Add(Instantiate(AutoLevelMaking.straightField[i], Road[i - 1].GetComponent<Filed>().pos.position, Quaternion.identity, SpawnHOlder));
                Road[i - 1].GetComponent<Filed>().curentRotation.y = Road[i - 1].transform.eulerAngles.y;


                print(Road[i - 1].GetComponent<Filed>().curentRotation.y);
                float y = Road[i - 1].GetComponent<Filed>().Roration.y + Road[i - 1].GetComponent<Filed>().curentRotation.y;
                print(y + " " + i);
                //  Road[i].transform.rotation =Quaternion.eu Road[i - 1].GetComponent<ScriptRotation>().Roration + Road[i - 1].GetComponent<ScriptRotation>().curentRotation;
                Road[i].transform.Rotate(0, y, 0);
            }
        }

    }
    private float Size;
    private int FieldID;
    void SpawnField()
    {
        for (int j = 0; j < AutoLevelMaking.LevelState.Length; j++)
        {
            Vector3 pos = AutoLevelMaking.PlayerSpawnPos;
            if (j == 0)
            {
                if (AutoLevelMaking.LevelState[j] == PlayerState.Running)
                {
                    RunningField(j);
                    Size = AutoLevelMaking.MaxDistance * AutoLevelMaking.ObstacleCount[j];
                    FieldID = Random.Range(0, AutoLevelMaking.RununingField.Length);
                    for (int i = 0; i < 5; i++)
                    {
                        pos = new Vector3(pos.x + 10, pos.y, Size / 2);
                        GameObject obj = Instantiate(AutoLevelMaking.RununingField[FieldID], new Vector3(pos.x, pos.y - 2, pos.z), Quaternion.identity, SpawnHOlder);
                        obj.transform.localScale = new Vector3(10, 0.5f, Size);
                    }
                }
                else if (AutoLevelMaking.LevelState[j] == PlayerState.Swiming)
                {
                    SwimingField(j);
                    Size = AutoLevelMaking.MaxDistance * AutoLevelMaking.ObstacleCount[j];
                    FieldID = Random.Range(0, AutoLevelMaking.SwimingFileldField.Length);
                    for (int i = 0; i < 5; i++)
                    {
                        pos = new Vector3(pos.x + 10, pos.y, Size / 2);
                        GameObject obj = Instantiate(AutoLevelMaking.SwimingFileldField[FieldID], new Vector3(pos.x, pos.y - 2, pos.z), Quaternion.identity, SpawnHOlder);
                        obj.transform.localScale = new Vector3(10, 0.5f, Size);

                    }
                }

            }
            else
            {
                if (AutoLevelMaking.LevelState[j] == PlayerState.Running)
                {
                    FieldID = Random.Range(0, AutoLevelMaking.RununingField.Length);
                    float newplatformsize = AutoLevelMaking.MaxDistance * AutoLevelMaking.ObstacleCount[j];

                    RunningField(j);
                    for (int i = 0; i < 5; i++)
                    {
                        pos = new Vector3(pos.x + 10, pos.y, Size + newplatformsize / 2);
                        GameObject obj = Instantiate(AutoLevelMaking.RununingField[FieldID], new Vector3(pos.x, pos.y - 2, pos.z), Quaternion.identity, SpawnHOlder);

                        obj.transform.localScale = new Vector3(10, 0.5f, newplatformsize);
                    }
                    Size = newplatformsize + Size;
                }
                else if (AutoLevelMaking.LevelState[j] == PlayerState.Swiming)
                {
                    FieldID = Random.Range(0, AutoLevelMaking.SwimingFileldField.Length);
                 //   float newplatformsize = AutoLevelMaking.MaxDistance * AutoLevelMaking.ObstacleCount[j];
                   // SwimingField(j);
                   
                        pos = new Vector3(pos.x + 30, pos.y, Size + 100 / 2);
                        GameObject obj = Instantiate(AutoLevelMaking.SwimingFileldField[FieldID], new Vector3(pos.x, pos.y - 2, pos.z), Quaternion.identity, SpawnHOlder);

                       // obj.transform.localScale = new Vector3(10, 0.5f, newplatformsize);
                    
                    //Size = newplatformsize + Size;
                }
                else if (AutoLevelMaking.LevelState[j] == PlayerState.cycling)
                {
                    CyclingField();
                }
            }



        }

    }
    void PlayerSpawn()
    {
        int playno = Random.Range(0, 5);
        Vector3 pos = AutoLevelMaking.PlayerSpawnPos;
        for (int i = 0; i < MaxPlayer; i++)
        {
            pos = new Vector3(pos.x + 10, pos.y, pos.z);
            Vector3 standingpoint = new Vector3(pos.x, pos.y - 2, pos.z);
            GameObject obj = Instantiate(PlayerSpawnObject, pos, Quaternion.identity);
            Instantiate(PlayerStandingPoint, standingpoint, Quaternion.identity);
            if (i != playno)
            {
                obj.GetComponent<PlayerMoment>().Type = PlayerType.AirtificialInteligence;
            }
            else
            {
                obj.GetComponent<PlayerMoment>().Type = PlayerType.Player;
                plalyermoment = obj.GetComponent<PlayerMoment>();
                //   Button1.onClick.AddListener(() => obj.GetComponent<PlayerMoment>().click());
                //  Button1.onClick.AddListener(() => obj.GetComponent<PlayerMoment>().click());
                Camera.Target = obj;
            }

        }
    }
}
