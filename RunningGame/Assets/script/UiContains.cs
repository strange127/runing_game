using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UiContains : MonoBehaviour
{
    public static UiContains instace;
    public int speedbostersaved;
    public int shildbostersaved;
    public int noobsticalsaved;
    public Button leftbutton;
    public Button rightbutton;

    private void Awake()
    {
        instace = this;
    }
    public void ShopItemParchase(powerUp power)
    {
        if (GameManager.instance.coin >= 100)
        {
            GameManager.instance.coin -= 100;
            PlayerPrefs.SetInt("Coin", GameManager.instance.coin);
            //money will be minus
            if (power == powerUp.Speedbooster)
            {
                PlayerPrefs.SetInt("Speedbooster", PlayerPrefs.GetInt("Speedbooster") + 1);

            }
            else if (power == powerUp.shield)
            {
                PlayerPrefs.SetInt("shildbooster", PlayerPrefs.GetInt("shildbooster") + 1);
            }
            else if (power == powerUp.noobsticle)
            {
                PlayerPrefs.SetInt("noobsticle", PlayerPrefs.GetInt("noobsticle") + 1);
            }
        }

    }
}