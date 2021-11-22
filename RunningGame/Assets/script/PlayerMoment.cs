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
    public Transform Target;
    public int abc;
    public Animator anime;
    public int inteligent =5;

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
        curotrineconnect = true;
        anime = GetComponentInChildren<Animator>();
        controler = GetComponent<CharacterController>();
        if (Type == PlayerType.Player)
        {
            oxygenbar = GameObject.Find("Canvas/oxybar");
            Fill = GameObject.Find("Canvas/oxybar/Fill");
            oxygenbar.gameObject.SetActive(false);
        }
      //  GameManager.instance.UI.leftbutton.onClick.AddListener(() => click());
       // GameManager.instance.UI.rightbutton.onClick.AddListener(() => click());
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

            //oxygenbar.transform.localScale = new Vector3(6, 6, 6);
            // oxygenbar.gameObject.SetActive(true);
          

            anime.SetBool("Swiming",true);
            anime.SetBool("Running", false);



#if UNITY_EDITOR

            if (Input.GetKeyDown(KeyCode.P)&&Type == PlayerType.Player)
               speed += acc;
            
#endif

            insarface = Physics.CheckSphere(serfacecheck.position, waterdistace, Watermask);



            if (velocity.y < -4)
                velocity.y = -4f;

            if (transform.position.y < -5)
                velocity.y += swiminggravity * Time.deltaTime;
            else if (transform.position.y > -5.5)
                velocity.y -= swiminggravity * Time.deltaTime;


            swimmingtimer();
            Diving();
            SpeedControler();
            controler.Move(velocity * Time.deltaTime);
        }
        else if (state == PlayerState.cycling)
        {

            if (Target)
            {
                transform.LookAt(Target.position);
            }
           
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
                velocity.y = Mathf.Sqrt(jumpHight * -2f * gravity);
#endif
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began && Type == PlayerType.Player)
            {
                velocity.y = Mathf.Sqrt(jumpHight * -2f * gravity);
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
                            velocity.y = Mathf.Sqrt(jumpHight * -2f * gravity);
                        }
                    }
                }
            }
        }
    }
    bool curotrineconnect = true;
    void Diving()
    {

        if (Type == PlayerType.Player) 
        {
            float FILL = (float)speed / maxspeed;
            //  float FILL = (float)speed / maxspeed;
            print(FILL);
           
            if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
                speed+=acc;
            Fill.GetComponent<Image>().fillAmount = FILL;
          //  velocity.y -= Mathf.Sqrt(diveforce * -2f * swiminggravity);
        }
        else if (Type == PlayerType.AirtificialInteligence)
        {
            int rang = Random.Range(0, 1000);
            if (rang < inteligent) 
            {
                if (curotrineconnect)
                    StartCoroutine(NPCDivingSpeed());   
            } 
        }
    }
    IEnumerator NPCDivingSpeed()
    {
        curotrineconnect = false;
        yield return new WaitForSeconds(.1f);
        
        speed+=acc;
        curotrineconnect = true;
    
    
    }
    void swimmingtimer()//swimming timer
    {
        //oxygenbar depletion 
        timer += Time.deltaTime;
        if (timer > 0.5)//for how much time
        {
            speed-= acc; //amount of oxygen
            timer = 0;
        }
    }

    void Sensour()
    {
        RaycastHit hit;
        Vector3 startingpos = this.transform.position;



    
        if (Physics.Raycast(startingpos, Quaternion.AngleAxis(sensourAngle, transform.up) * transform.forward, out hit, sensorLenth))
        {

            controler.transform.Rotate(0, -angleofrotation * Time.deltaTime, 0);
        }
        if (Physics.Raycast(startingpos, Quaternion.AngleAxis(-sensourAngle, transform.up) * transform.forward, out hit, sensorLenth))
        {
            controler.transform.Rotate(0, angleofrotation * Time.deltaTime, 0);

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
                    int range = Random.Range(0, 200);
                    if (range < 1)
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
                speed = 0;
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
                if (Type == PlayerType.Player)
                {
                    oxygenbar.gameObject.SetActive(false);
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

                //Open windows panel
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
      
    }
   

}



