using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private List<PlayerMoment> player = new List<PlayerMoment>();
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerMoment>().Type == PlayerType.AirtificialInteligence && other.GetComponent<PlayerMoment>().Target == null)
        {
            other.GetComponent<PlayerMoment>().Target = this.transform;
            player.Add(other.GetComponent<PlayerMoment>());
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameManager.instance.coin++;
        foreach (var item in player)
        {
            item.Target = null;
        }
        //play sound
        Destroy(this.gameObject);
    }
}
