using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public int usableitem;
    
    public void ShopItemUse(powerUp power)
    {
        if (usableitem > 0)
        {
            usableitem--;

            GameManager.instance.plalyermoment.booster = power;

            //play bostuseitemsound
            if (power == powerUp.shield && PlayerPrefs.GetInt("shildbooster") > 0)
            {
                StartCoroutine(poweruptimer(Duration.shildduration));
            }
            else if (power == powerUp.Speedbooster  && PlayerPrefs.GetInt("Speedbooster") > 0)
            {
                StartCoroutine(poweruptimer(Duration.bostduratio));
            }
            else if (power == powerUp.noobsticle && PlayerPrefs.GetInt("noobsticle") > 0)
            {
                StartCoroutine(poweruptimer(Duration.noobsticaleduration));
            }
        }
    }
    IEnumerator poweruptimer(int duration)
    {
        yield return new WaitForSeconds(duration);
        GameManager.instance.plalyermoment.booster = powerUp.none;
    }

}
