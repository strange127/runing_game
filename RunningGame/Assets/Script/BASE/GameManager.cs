using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject CoinObject;
    public static GameManager instance;
    private int MaxPlayer = 5;
    public LevelCreationScriptable AutoLevelMaking;
    [HideInInspector]public PlayerMoment plalyermoment;
    [SerializeField]private GameObject PlayerSpawnObject;
    [SerializeField]private GameObject PlayerStandingPoint;
    public GameObject congratspanel;
    public PlayerCamera PlayerFlowCamera;
    private float Size;
    private int FieldID;
    public UiContains UI;
    public Transform SpawnHolder;
    public Collider filalline;
    public int coin;
    public int levelLoad;
    public List<GameObject> Road = new List<GameObject>();
    public int Posstion;
    Vector3 pos;
    public GameObject FinishedLIne;
    private void Awake()
    {
        AutoLevelMaking = null;
        if (instance == null)
        {
            instance = this;
        }
        MaxPlayer = 5;

        if (PlayerPrefs.HasKey("SaveGame"))
        {
            levelLoad = PlayerPrefs.GetInt("SaveGame");
        }
        else
        {
            levelLoad = 1;
            PlayerPrefs.SetInt("SaveGame",1);
        }
        coin = PlayerPrefs.GetInt("Coin");
        UI.CoinText.text =coin.ToString();
    }
    public void LevelCreate()
    {
        //PlayerFlowCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerCamera>(); 

        SpawnField();
        PlayerSpawn();
        GameObject obj=Instantiate(FinishedLIne, Road[AutoLevelMaking.LevelState.Length - 1].GetComponent<Filed>().pos.position,Quaternion.identity,SpawnHolder.GetChild(1));
        float y = Road[AutoLevelMaking.LevelState.Length - 1].GetComponent<Filed>().Roration.y + Road[AutoLevelMaking.LevelState.Length - 1].GetComponent<Filed>().curentRotation.y;
        //   Road[AutoLevelMaking.LevelState.Length-1].GetComponent<Filed>().pos.gameObject.tag = "Finished";
        obj.transform.Rotate(0, y, 0);

    }
    
    void RunningField()
    {
        int obstacklespawnid = Random.Range(0, AutoLevelMaking.RunningObstackleToSpawn.Length);
        pos.x = 10;
        for (int i = 0; i < 5; i++)
        {
            pos = new Vector3(pos.x + 10, pos.y, pos.z);
            Vector3 postion = new Vector3(pos.x + 10, pos.y, pos.z);

            for (int j = 0; j < AutoLevelMaking.ObstacleCount[0]; j++)
            {
                float dis = Random.Range(AutoLevelMaking.MinDistance, AutoLevelMaking.MaxDistance);
                pos = new Vector3(pos.x, pos.y, pos.z + dis);

                Instantiate(CoinObject, new Vector3(pos.x, pos.y + 7, pos.z), Quaternion.identity,SpawnHolder.GetChild(1));
                Instantiate(AutoLevelMaking.RunningObstackleToSpawn[obstacklespawnid], pos, Quaternion.identity, SpawnHolder.GetChild(1));
            }
            pos = new Vector3(pos.x, pos.y, postion.z);
        }
    }
    //void SwimingField(int obstacke)
    //{
    ////    int obstacklespawnid = Random.Range(0, AutoLevelMaking.SwimingObstackleToSpawn.Length);

    //    Vector3 pos = AutoLevelMaking.PlayerSpawnPos;
    //    //for (int i = 0; i < 5; i++)
    //    //{
    //    //    //pos = new Vector3(pos.x + 10, pos.y, Size);

    //    //    //for (int j = 0; j < AutoLevelMaking.ObstacleCount[obstacke]; j++)
    //    //    //{
    //    //    //    float dis = Random.Range(AutoLevelMaking.MinDistance, AutoLevelMaking.MaxDistance);
    //    //    //    pos = new Vector3(pos.x, pos.y, pos.z + dis);


    //    //    ////    Instantiate(AutoLevelMaking.SwimingObstackleToSpawn[obstacklespawnid], pos, Quaternion.identity, SpawnHOlder);
    //    //    //}
    //    //}
    //}

   
    void CyclingField(int i,int j)
    {
         
        if(AutoLevelMaking.curves[i] == CyclingCurve.Right)
        {
            Road.Add(Instantiate(AutoLevelMaking.RightCurve[0], Road[j - 1].GetComponent<Filed>().pos.position, Quaternion.identity, SpawnHolder));
            Road[j - 1].GetComponent<Filed>().curentRotation.y = Road[j - 1].transform.eulerAngles.y;
            float y = Road[j - 1].GetComponent<Filed>().Roration.y + Road[j - 1].GetComponent<Filed>().curentRotation.y;
            Road[j].transform.Rotate(0, y, 0);
        }
        else if(AutoLevelMaking.curves[i] == CyclingCurve.Left)
        {
            Road.Add(Instantiate(AutoLevelMaking.LeftCurve[0], Road[j - 1].GetComponent<Filed>().pos.position, Quaternion.identity, SpawnHolder.GetChild(0)));
            Road[j - 1].GetComponent<Filed>().curentRotation.y = Road[j - 1].transform.eulerAngles.y;
            float y = Road[j - 1].GetComponent<Filed>().Roration.y + Road[j - 1].GetComponent<Filed>().curentRotation.y;
            Road[j].transform.Rotate(0, y, 0);
        }
        else if(AutoLevelMaking.curves[i] == CyclingCurve.Straight)
        {
            Road.Add(Instantiate(AutoLevelMaking.straightField[0], Road[j - 1].GetComponent<Filed>().pos.position, Quaternion.identity, SpawnHolder.GetChild(0)));
            Road[j - 1].GetComponent<Filed>().curentRotation.y = Road[j - 1].transform.eulerAngles.y;
            float y = Road[j - 1].GetComponent<Filed>().Roration.y + Road[j - 1].GetComponent<Filed>().curentRotation.y;
            Road[j].transform.Rotate(0, y, 0);
        }
           // if (i == 0)
           // {

           //     Road.Add(Instantiate(AutoLevelMaking.straightField[0], Road[j - 1].GetComponent<Filed>().pos.position, Quaternion.identity, SpawnHOlder));
      
           //     return;
           // }
           // else
           // {
           //     Road.Add(Instantiate(AutoLevelMaking.straightField[0], Road[j - 1].GetComponent<Filed>().pos.position, Quaternion.identity, SpawnHOlder));
           //     Road[j- 1].GetComponent<Filed>().curentRotation.y = Road[j - 1].transform.eulerAngles.y;


           //     print(Road[j - 1].GetComponent<Filed>().curentRotation.y);
           //     float y = Road[j - 1].GetComponent<Filed>().Roration.y + Road[j- 1].GetComponent<Filed>().curentRotation.y;
           ////     print(y + " " + i);
           //     //  Road[i].transform.rotation =Quaternion.eu Road[i - 1].GetComponent<ScriptRotation>().Roration + Road[i - 1].GetComponent<ScriptRotation>().curentRotation;
           //     Road[j].transform.Rotate(0, y, 0);
           // }
       
    }
    
   
    void SpawnField()
    {

        int i = 0;
        for (int j = 0; j < AutoLevelMaking.LevelState.Length; j++)
        {
            
            if (j == 0)
            {
                pos = AutoLevelMaking.PlayerSpawnPos;
                if (AutoLevelMaking.LevelState[j] == PlayerState.Running)
                {
                    Size = AutoLevelMaking.MaxDistance * AutoLevelMaking.ObstacleCount[0];
                    RunningField();
                    pos = AutoLevelMaking.PlayerSpawnPos;
                    FieldID = Random.Range(0, AutoLevelMaking.RununingField.Length);
                    pos = new Vector3(pos.x + 30, pos.y, pos.z);
                    GameObject obj = Instantiate(AutoLevelMaking.RununingField[FieldID], new Vector3(pos.x, pos.y - 2, pos.z), Quaternion.identity, SpawnHolder.GetChild(0));
                    Road.Add(obj);
                    obj.transform.localScale = new Vector3(1, 1, Size);
                    pos = obj.GetComponent<Filed>().pos.position;

                }
                else if (AutoLevelMaking.LevelState[j] == PlayerState.Swiming)
                {

                    FieldID = Random.Range(0, AutoLevelMaking.SwimingFileldField.Length);
                    pos = new Vector3(40, pos.y, pos.z);
                    GameObject obj = Instantiate(AutoLevelMaking.SwimingFileldField[FieldID], new Vector3(pos.x, pos.y - 2, pos.z), Quaternion.identity, SpawnHolder.GetChild(0));
                    Road.Add(obj);
                    pos = obj.GetComponent<Filed>().pos.position;
                }

            }
            else
            {
                if (AutoLevelMaking.LevelState[j] == PlayerState.Running)
                {
                    FieldID = Random.Range(0, AutoLevelMaking.RununingField.Length);
                    RunningField();
                    
                    Size = AutoLevelMaking.MaxDistance * AutoLevelMaking.ObstacleCount[0];
                    pos = new Vector3(40, pos.y, pos.z);
                    GameObject obj = Instantiate(AutoLevelMaking.RununingField[FieldID], new Vector3(pos.x, pos.y, pos.z), Quaternion.identity, SpawnHolder.GetChild(0));
                    Road.Add(obj);
                    obj.transform.localScale = new Vector3(1, 1, Size);
                    pos = obj.GetComponent<Filed>().pos.position;
               
                
                }
                else if (AutoLevelMaking.LevelState[j] == PlayerState.Swiming)
                {
                    FieldID = Random.Range(0, AutoLevelMaking.SwimingFileldField.Length);
                    
                   
                      //  pos = new Vector3(pos.x + 30, pos.y, Size + 100 / 2);
                        GameObject obj = Instantiate(AutoLevelMaking.SwimingFileldField[FieldID], new Vector3(pos.x, pos.y, pos.z), Quaternion.identity, SpawnHolder.GetChild(0));
                    Road.Add(obj);
                    pos = obj.GetComponent<Filed>().pos.position;
                    // obj.transform.localScale = new Vector3(10, 0.5f, newplatformsize);

                    //Size = newplatformsize + Size;
                }
                else if (AutoLevelMaking.LevelState[j] == PlayerState.cycling)
                {
                    CyclingField(i,j);
                    i++;
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
            GameObject obj = Instantiate(PlayerSpawnObject, pos, Quaternion.identity,SpawnHolder.GetChild(2));
            Instantiate(PlayerStandingPoint, standingpoint, Quaternion.identity);
            if (i != playno)
            {
                obj.GetComponent<PlayerMoment>().Type = PlayerType.AirtificialInteligence;
                obj.GetComponentInChildren<SkinnedMeshRenderer>().materials[2].color = Color.red;
            }
            else
            {
                obj.GetComponent<PlayerMoment>().Type = PlayerType.Player;
                plalyermoment = obj.GetComponent<PlayerMoment>();

                //   Button1.onClick.AddListener(() => obj.GetComponent<PlayerMoment>().click());
                //  Button1.onClick.AddListener(() => obj.GetComponent<PlayerMoment>().click());
                PlayerFlowCamera.transform.parent = obj.transform;
                PlayerFlowCamera.transform.localPosition = new Vector3(0, PlayerFlowCamera.Ycontroler, PlayerFlowCamera.ZControler);
                PlayerFlowCamera.transform.rotation = Quaternion.Euler(30, 0, 0);

            }

        }
    }
}
