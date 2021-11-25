using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UiContains : MonoBehaviour
{
    public static UiContains instace;
    public int speedbostersaved;
    public int shildbostersaved;
    public int noobsticalsaved;
    public TextMeshProUGUI CoinText;
    public Button leftbutton;
    public Button rightbutton;
  //  public TextMeshProUGUI CoinText;

    private void Awake()
    {
        instace = this;
    }
    private void Start()
    {
        CoinText.text = GameManager.instance.coin.ToString();
    }
    public void ShopItemParchase(int i)
    {


        //money will be minus
        if ((int)powerUp.Speedbooster == i)
        {
            if (GameManager.instance.coin >= speedbostersaved)
            {
                PlayerPrefs.SetInt("Speedbooster", PlayerPrefs.GetInt("Speedbooster") + 1);
                GameManager.instance.coin -= speedbostersaved;
                CoinText.text = GameManager.instance.coin.ToString();
                PlayerPrefs.SetInt("Coin", GameManager.instance.coin);
                CoinText.text = GameManager.instance.coin.ToString();
            }
        }
        else if ((int)powerUp.shield == i)
        {
            if (GameManager.instance.coin >= shildbostersaved)
            {
                PlayerPrefs.SetInt("shildbooster", PlayerPrefs.GetInt("shildbooster") + 1);
                GameManager.instance.coin -= shildbostersaved;
                CoinText.text = GameManager.instance.coin.ToString();
                PlayerPrefs.SetInt("Coin", GameManager.instance.coin);
                CoinText.text = GameManager.instance.coin.ToString();
            }


        }
        else if ((int)powerUp.noobsticle == i)
        {

            if (GameManager.instance.coin >= noobsticalsaved)
            {

                PlayerPrefs.SetInt("noobsticle", PlayerPrefs.GetInt("noobsticle") + 1);
                GameManager.instance.coin -= noobsticalsaved;
                CoinText.text = GameManager.instance.coin.ToString();
                PlayerPrefs.SetInt("Coin", GameManager.instance.coin);
                CoinText.text = GameManager.instance.coin.ToString();
            }
        }

    }
}