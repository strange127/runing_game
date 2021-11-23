using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public List<PlayerMoment> player = new List<PlayerMoment>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerMoment>().Type == PlayerType.AirtificialInteligence && other.GetComponent<PlayerMoment>().Target == null)
            {
                other.GetComponent<PlayerMoment>().Target = this.transform;
                player.Add(other.GetComponent<PlayerMoment>());
            }
        }
    }

}
