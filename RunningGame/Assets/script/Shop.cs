using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public int usableitem;
    //public void Item(int value)
    //{

    //    if(value == 0)
    //    {
    //        ShopItemUse(powerUp.shield);
    //    }else if (value == 1)
    //    {
    //        ShopItemUse(powerUp.Speedbooster);
    //    }else if(value == 2)
    //    {
    //        ShopItemUse(powerUp.noobsticle);
    //    }
    //}
    public void ShopItemUse(int power)
    {
        if (usableitem > 0)
        {
            usableitem--;

        //    GameManager.instance.plalyermoment.booster = power;

            //play bostuseitemsound
            if (power == (int)powerUp.shield && PlayerPrefs.GetInt("shildbooster") > 0)
            {
                GameManager.instance.plalyermoment.booster = powerUp.shield;

                StartCoroutine(poweruptimer(Duration.shildduration));
                PlayerPrefs.SetInt("shildbooster",PlayerPrefs.GetInt("shildbooster") - 1);
            }
            else if (power == (int)powerUp.Speedbooster  && PlayerPrefs.GetInt("Speedbooster") > 0)
            {
                GameManager.instance.plalyermoment.booster = powerUp.Speedbooster;
                StartCoroutine(poweruptimer(Duration.bostduratio));
            }
            else if (power == (int)powerUp.noobsticle && PlayerPrefs.GetInt("noobsticle") > 0)
            {
                GameManager.instance.plalyermoment.booster = powerUp.noobsticle;
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
