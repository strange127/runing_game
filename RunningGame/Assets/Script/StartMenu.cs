using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public void LevelPracticeMode(int vlaue)
    {
        LoadingScreen.Loading.LoadingScence(vlaue);
    }

    public void ShopParchaseItem(int item)
    { 
        GameManager.instance.UI.ShopItemParchase(item);
    }
}
