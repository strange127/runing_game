using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class PlayerMoment : MonoBehaviour
{  
    [Header("General Settings")]
    public PlayerType Type;
    public powerUp booster = powerUp.none;
    public PlayerState state;
    private CharacterController controler;
    public float speed;
    public float maxspeed;
    public float speedmuliplyer;
    public Vector3 velocity;
    private float timer=0;
    public Animator anime;
    public int inteligent =5;
    public GameObject Cyclingobj;
    public Transform Target;
    public Transform RayCastPostion;
    [Header("Running")]
    #region RuningState
    private bool isgrounded;
    public float gravity;
    public LayerMask groundMask;
    public float distnce;
    public Transform groundcheck;
    public float jumpHight;
    #endregion
    [Header("Swimming")]
    #region SwimingState
    public bool insarface;
    public float swiminggravity;
    public LayerMask Watermask;
    public float waterdistace;
    public Transform serfacecheck;
    public float diveforce;
    private GameObject oxygenbar;
    private GameObject Fill;
    #endregion
    [Header("Cycling")]
    #region cycling
    [SerializeField] private float angleofrotation = 10;
    [SerializeField] private float acc = 0.5f;
    // [SerializeField] private float drag = 0.2f;
    [SerializeField] private bool clicked;
    public float sensourAngle = 30;
    public float sensorLenth = 2f;

    #endregion
    private void Start()
    {   
        anime = GetComponentInChildren<Animator>();
        //congratspanel = GameObject.Find("Completed").GetComponent<GameObject>();
        controler = GetComponent<CharacterController>();
        if (Type == PlayerType.Player)
        {
            oxygenbar = GameObject.Find("Canvas/oxybar");
            Fill = GameObject.Find("Canvas/oxybar/Fill");
            oxygenbar.gameObject.SetActive(false);
        }
        GameManager.instance.UI.leftbutton.onClick.AddListener(() => click());
        GameManager.instance.UI.rightbutton.onClick.AddListener(() => click());
        sensorLenth = Random.Range(35, 50);
        sensourAngle = Random.Range(35, 45);
        //GameManager.instance.UI.leftbutton.gameObject.SetActive(false);
        //GameManager.instance.UI.rightbutton.gameObject.SetActive(false);
    }
    private void Update()
    {
        Vector3 move = transform.forward * 1;
        controler.Move(move * speed * Time.deltaTime);
        if (state == PlayerState.Running)
        {
            anime.SetBool("Swiming", false);
            anime.SetBool("Running", true);
            SpeedControler();
            isgrounded = Physics.CheckSphere(groundcheck.position, distnce, groundMask);
            if (isgrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }
            Jumping();
            velocity.y += gravity * Time.deltaTime;
            controler.Move(velocity * Time.deltaTime);
        }
        else if (state == PlayerState.Swiming)
        {
            velocity.y = -7;

            anime.SetBool("Swiming",true);
            anime.SetBool("Running", false);
#if UNITY_EDITOR

            if (Input.GetKeyDown(KeyCode.P)&&Type == PlayerType.Player)
               speed += acc;
            
#endif
            if (clicked == false)
            {
                speed -= acc * Time.deltaTime;
                if (speed < 0)
                    speed = 0;
            }
            if (transform.localPosition.y < -9)
            {
                velocity.y = 2;
            }
            else
            {
                velocity.y = -2;
            }
            //else if(transform.position.y < -9)
            //{
            //    velocity.y -= swiminggravity * Time.deltaTime;
            //}
            //if (velocity.y < -3)
            //    velocity.y = -3f;

            //if (transform.position.y < -5)
            //    velocity.y += swiminggravity * Time.deltaTime;
            //else if (transform.position.y > -5.5)
            //    velocity.y -= swiminggravity * Time.deltaTime;
            Diving();
            controler.Move(velocity * Time.deltaTime);


          //  swimmingtimer();
           
        }
        else if (state == PlayerState.cycling)
        {
           
            anime.SetBool("Swiming", false);
            isgrounded = Physics.CheckSphere(groundcheck.position, distnce, groundMask);
            if (isgrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }
            if (clicked == false)
            {
                speed -= acc * Time.deltaTime;
                if (speed < 0)
                    speed = 0;
            }
            Cycling();
            velocity.y += gravity * Time.deltaTime;
            controler.Move(velocity * Time.deltaTime);

        }
        SpeedControler();
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            GameManager.instance.congratspanel.SetActive(true);
        }



    }

    private IEnumerator Speed()
    {
        yield return new WaitForSeconds(1f);
        clicked = false;
    }
    public void click()
    {
        if (Type == PlayerType.AirtificialInteligence)
            return;

        clicked = true;
        if (speed < maxspeed)
        {
            speed += acc;

        }
        StartCoroutine(Speed());

        //charac.Move(new Vector3(0, 0, speed * Time.deltaTime));
    }
    void Jumping()
    {
        if (isgrounded)
        {
#if UNITY_EDITOR
            if (Input.GetButtonDown("Jump") && Type == PlayerType.Player)
            {
                anime.SetBool("Jump", true);
                velocity.y = Mathf.Sqrt(jumpHight * -2f * gravity);
                StartCoroutine(jumpstop());
            }
#endif
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began && Type == PlayerType.Player)
            {
                anime.SetBool("Jump", true);
                velocity.y = Mathf.Sqrt(jumpHight * -2f * gravity);
                StartCoroutine(jumpstop());
            }
            else if (Type == PlayerType.AirtificialInteligence)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, Vector3.forward, out hit, 3f))
                {
                    if (hit.collider.gameObject.CompareTag("Obstakle"))
                    {
                        int rang = Random.Range(0, 1000);
                        if (rang < inteligent)
                        {
                            anime.SetBool("Jump", true);
                            velocity.y = Mathf.Sqrt(jumpHight * -2f * gravity);
                            StartCoroutine(jumpstop());
                        }
                    }
                }
            }
        }
    }
    IEnumerator jumpstop()
    {
        yield return new WaitForSeconds(1.3f);

        anime.SetBool("Jump", false);
    }
    void Diving()
    {

        if (Type == PlayerType.Player) 
        {
            float FILL = (float)speed / maxspeed;
            //  float FILL = (float)speed / maxspeed;
      
            if (speed < maxspeed)
            {
                if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
                    speed += acc;
            }
            Fill.GetComponent<Image>().fillAmount = FILL;
          //  velocity.y -= Mathf.Sqrt(diveforce * -2f * swiminggravity);
        }
        else if (Type == PlayerType.AirtificialInteligence)
        {
            int rang = Random.Range(0, 1000);
           
           if (rang < inteligent)
           {
                clicked = true;
                if (speed < maxspeed)
                {
                    speed += acc;

                }
           }
                //if (curotrineconnect)
                //    StartCoroutine(NPCDivingSpeed());   
            
        }
    }
    void Sensour()
    {
        RaycastHit hit;
  

        //if ((Physics.Raycast(startingpos, Quaternion.AngleAxis(0, transform.up) * transform.forward, out hit, sensorLenth)))
        //{
        //    if (!hit.collider.CompareTag("Coin"))
        //    {
                
        //        if (Physics.Raycast(startingpos, Quaternion.AngleAxis(sensourAngle, transform.up) * transform.forward, out hit, sensorLenth))
        //        {

        //            controler.transform.Rotate(0, -angleofrotation * Time.deltaTime, 0);
        //            return;
        //        }
        //        else if (Physics.Raycast(startingpos, Quaternion.AngleAxis(-sensourAngle, transform.up) * transform.forward, out hit, sensorLenth))
        //        {
        //            controler.transform.Rotate(0, angleofrotation * Time.deltaTime, 0);
        //            return;

        //        }
        //        controler.transform.Rotate(0, -angleofrotation * Time.deltaTime, 0);

        //    }
        //}


        if (Physics.Raycast(RayCastPostion.position, Quaternion.AngleAxis(sensourAngle, transform.up) * transform.forward, out hit, sensorLenth))
        {
            if (!hit.collider.CompareTag("Coin") && !hit.collider.CompareTag("Finished"))
            {

                controler.transform.Rotate(0, -angleofrotation * Time.deltaTime, 0);
            }


        }
        else if (Physics.Raycast(RayCastPostion.position, Quaternion.AngleAxis(-sensourAngle, transform.up) * transform.forward, out hit, sensorLenth))
        {

            if (!hit.collider.CompareTag("Coin")&&!hit.collider.CompareTag("Finished"))
            {

                controler.transform.Rotate(0, angleofrotation * Time.deltaTime, 0);
            }



        }

    }
    void Cycling()
    {
        if (isgrounded)
        {
            if (Type == PlayerType.Player)
            {
                if (Input.acceleration.x > .1f)
                {
                    controler.transform.Rotate(0, angleofrotation * Time.deltaTime, 0);
                }
                if (Input.acceleration.x < -0.1)
                {
                    controler.transform.Rotate(0, -angleofrotation * Time.deltaTime, 0);
                }
                #region UnityEditor


                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {

                    controler.transform.Rotate(0, -angleofrotation, 0);


                    //charac.Move(new Vector3( -drag * Time.deltaTime, 0,0 ));
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {

                    controler.transform.Rotate(0, angleofrotation, 0);


                    // charac.Move(new Vector3(drag * Time.deltaTime, 0, 0));
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    if (speed < maxspeed)
                    {
                        speed -= acc * Time.deltaTime;
                    }
                    // deaccelerate(speed);
                }

                #endregion
            }

            else if (Type == PlayerType.AirtificialInteligence)
            {

                if (clicked == true)
                {
                    StartCoroutine(Speed());

                }
                else
                {
                    int range = Random.Range(0, 1000);
                    if (range < inteligent)
                    {
                        clicked = true;
                        if (speed < maxspeed)
                        {
                            speed += acc;

                        }
                    }
                }
                Sensour();
            }
        }
    }
    public void SpeedControler()
    {
        if (speed < maxspeed)
        {
            if (state == PlayerState.Running)
            {
                speed += speedmuliplyer * Time.deltaTime;
            }

            if (booster != powerUp.Speedbooster)
            {
                if (speed > maxspeed)
                {
                    speed = maxspeed;

                }

            }
            if (speed <= 0)
            {
                speed = 1;
            }
        }
        
        if (booster == powerUp.Speedbooster)
        {
            speed = maxspeed + 2;
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstakle"))
        {
            if (booster == powerUp.none)
                speed = 0f;
            //    print(other.gameObject.GetComponentInChildren<CapsuleCollider>().isTrigger);
            other.gameObject.GetComponentInChildren<CapsuleCollider>().isTrigger = true;

        }
       else if (other.CompareTag("ChnageToSwimer"))
        {
            state = PlayerState.Swiming;
            if (Type == PlayerType.Player)
            {
                oxygenbar.gameObject.SetActive(true);
            }

        }
        else if (other.CompareTag("ChangeToCycling"))
        {
            if(state != PlayerState.cycling)
            {

                state = PlayerState.cycling;
                speed = 0;
                GameObject obj= Instantiate(Cyclingobj, transform.position, Quaternion.identity, this.transform);
           //     transform.rotation = Quaternion.Euler(Vector3.zero);
                transform.GetChild(0).gameObject.SetActive(false);
                if (Type == PlayerType.Player)
                {
                    //make player color green
                    oxygenbar.gameObject.SetActive(false);
                    GameManager.instance.UI.leftbutton.gameObject.SetActive(true);
                    GameManager.instance.UI.rightbutton.gameObject.SetActive(true);
                }
            }
          
          
            clicked = false;
        }
        else if (other.CompareTag("ChangeToRunner"))
        {
            state = PlayerState.Running;
            if (Type == PlayerType.Player)
            {
                oxygenbar.gameObject.SetActive(false);
            }
        }else if (other.CompareTag("Finished"))
        {
            GameManager.instance.Posstion++;
            if(Type== PlayerType.Player)
            {
                PlayerPrefs.SetInt("SaveGame", PlayerPrefs.GetInt("SaveGame")+1);
                GameManager.instance.congratspanel.SetActive(true);
                PlayerPrefs.SetInt("Coin", GameManager.instance.coin);
            }
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit power)

    {
        if (power.collider.tag == "Obstakle")
        {

            if (booster == powerUp.noobsticle)
                power.collider.GetComponent<CapsuleCollider>().isTrigger = true;

            if (booster == powerUp.shield)
            {
                power.collider.GetComponent<CapsuleCollider>().isTrigger = true;
                if (power.collider == null)
                    booster = powerUp.none;
            }
        }
        else if (power.collider.CompareTag("Coin"))
        {
            if (Type == PlayerType.Player)
                GameManager.instance.coin++;
            foreach (var item in power.collider.GetComponent<Coin>().player)
            { 
                item.Target = null;
            }
            Destroy(power.collider.gameObject);

        }
      
    }
   

}



