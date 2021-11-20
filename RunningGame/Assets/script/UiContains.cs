using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UiContains : MonoBehaviour
{
    public int speedbostersaved;
    public int shildbostersaved;
    public int noobsticalsaved;
    public Button leftbutton;
    public Button rightbutton;

    public void ShopItemParchase(powerUp power)
    {

        //money will be minus
        if(power == powerUp.Speedbooster)
        {
            PlayerPrefs.SetInt("Speedbooster", PlayerPrefs.GetInt("Speedbooster") + 1);
        }else if(power == powerUp.shield)
        {
            PlayerPrefs.SetInt("shildbooster", PlayerPrefs.GetInt("shildbooster") + 1);
        }else if(power == powerUp.noobsticle)
        {
            PlayerPrefs.SetInt("noobsticle", PlayerPrefs.GetInt("noobsticle") + 1);
        }
    }
}
