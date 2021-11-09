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
    [SerializeField] Slider oxygenbar;
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
        controler = GetComponent<CharacterController>();
    }
    private void Update()
    {
        Vector3 move = transform.forward * 1;
        controler.Move(move * speed * Time.deltaTime);
        if (state == PlayerState.Running)
        {
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
            oxygenbar = GameObject.Find("oxybar").GetComponent<Slider>();
            oxygenbar.transform.localScale = new Vector3(6, 6, 6);
            oxygenbar.enabled = true;
            SpeedControler();
            swimmingtimer();
            if(Input.touchCount>0 && Input.touches[0].phase == TouchPhase.Began)
              oxygenbar.value += 3;
            
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.P))
               oxygenbar.value += 3;
            
#endif

            insarface = Physics.CheckSphere(serfacecheck.position, waterdistace, Watermask);
            if (oxygenbar.value == 0)
                speed = 0;
            
            if (insarface && velocity.y > 0)
                velocity.y = 2f;
            
            if (transform.position.y < 0)
                velocity.y += swiminggravity * Time.deltaTime;
            else
                velocity.y -= swiminggravity * Time.deltaTime;
            Diving();
            controler.Move(velocity * Time.deltaTime);
        }
        else if (state == PlayerState.cycling)
        {
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
            if (Input.GetButtonDown("Jump") && Type == PlayerType.Player)
                velocity.y = Mathf.Sqrt(jumpHight * -2f * gravity);
            else if (Type == PlayerType.AirtificialInteligence)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, Vector3.forward, out hit, 5f))
                {
                    if (hit.collider.gameObject.CompareTag("Obstakle"))
                    {
                        int rang = Random.Range(0, 250);
                        if (rang < 200)
                        {
                            velocity.y = Mathf.Sqrt(jumpHight * -2f * gravity);
                        }
                    }
                }
            }
        }
    }
    void Diving()
    {
        if (insarface)
        {
            if (Input.GetButtonDown("Jump") && Type == PlayerType.Player)
                velocity.y -= Mathf.Sqrt(diveforce * -2f * swiminggravity);
            else if (Type == PlayerType.AirtificialInteligence)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, Vector3.forward, out hit, 1.25f))
                {
                    if (hit.collider.gameObject.CompareTag("Obstakle"))
                    {
                        int rang = Random.Range(0, 250);
                        if (rang < 10)
                        {
                            velocity.y -= Mathf.Sqrt(diveforce * -2f * swiminggravity);
                        }
                    }
                }
            }
        }
       

    }
    
    void swimmingtimer()//swimming timer
    {
        //oxygenbar depletion 
        timer += Time.deltaTime;
        if (timer > 0.5)//for how much time
        {
            oxygenbar.value -= 6; //amount of oxygen
            timer = 0;
        }
    }

    void Sensour()
    {
        RaycastHit hit;
        Vector3 startingpos = this.transform.position;



        if (Physics.Raycast(startingpos, transform.forward, out hit, sensorLenth))
        {

        }
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
            speed += speedmuliplyer * Time.deltaTime;


            if (speed > maxspeed)
            {
                speed = maxspeed;

            }

            if (booster == powerUp.Speedbooster)
            {
                speed = maxspeed + 1;
            }
        }
        if (booster == powerUp.none)
        {
            if (speed > maxspeed)
            {
                speed = maxspeed;

            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstakle"))
        {
            if (booster == powerUp.none)
                speed = 0f;
            print(other.gameObject.GetComponentInChildren<CapsuleCollider>().isTrigger);
            other.gameObject.GetComponentInChildren<CapsuleCollider>().isTrigger = true;
        }
        if (other.CompareTag("ChnageToSwimer"))
        {
            state = PlayerState.Swiming;
        }
        if (other.CompareTag("ChangeToCycling"))
        {
            state = PlayerState.cycling;
            speed = 0;
            clicked = false;
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
        if (power.collider.tag == "nonobs")
        {
            Debug.Log("collide");
            Destroy(power.collider.gameObject);
            booster = powerUp.noobsticle;

            StartCoroutine(poweruptimer());
        }
        if (power.collider.CompareTag("speedbooster"))
        {
            Destroy(power.collider.gameObject);
            booster = powerUp.Speedbooster;
            StartCoroutine(poweruptimer());
        }
        if (power.collider.CompareTag("shield"))
        {
            Destroy(power.collider.gameObject);
            booster = powerUp.shield;
            StartCoroutine(poweruptimer());
        }
    }
    IEnumerator poweruptimer()
    {
        yield return new WaitForSeconds(2);
        booster = powerUp.none;
    }

}



