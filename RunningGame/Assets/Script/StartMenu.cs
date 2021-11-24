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
        UiContains.instace.ShopItemParchase(item);
    }
}
