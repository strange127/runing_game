using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public GameObject Target;
    public float CameraMomentSpeed;
    public float Ycontroler;
    public float ZControler;
    public void Start()
    {
        StartCoroutine(CameraSet());
    }
    IEnumerator CameraSet()
    {
        yield return new WaitUntil(() => Target);
        if (Target)
        {
            this.transform.parent = Target.transform;
            transform.localPosition = new Vector3(0, Ycontroler, ZControler);
        }
    }
}
