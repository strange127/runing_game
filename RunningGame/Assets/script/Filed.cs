using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Filed : MonoBehaviour
{

    public Vector3 Roration;
    public Transform pos;
    public Vector3 curentRotation;

    private void Awake()
    {
       // print(transform.rotation.y);
       // print(Quaternion.Euler(transform.localEulerAngles));
    }

}
