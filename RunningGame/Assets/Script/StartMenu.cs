using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

   public void ShopParchaseItem(int item)
    {
        if (item == 0)
        {
            UiContains.instace.ShopItemParchase(powerUp.shield);
        } else if (item == 1)
            UiContains.instace.ShopItemParchase(powerUp.Speedbooster);
        else if (item == 2)      
            UiContains.instace.ShopItemParchase(powerUp.noobsticle);
    }
}
