using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public GameObject Target;
    public float CameraMomentSpeed;
    public float Ycontroler;
    public float ZControler;
    public void Update()
    {
        if (Target)
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(Target.transform.position.x, Target.transform.position.y + Ycontroler, Target.transform.position.z + ZControler), CameraMomentSpeed);
    }
}
