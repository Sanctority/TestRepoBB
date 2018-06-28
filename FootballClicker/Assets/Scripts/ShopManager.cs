using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ShopManager : MonoBehaviour {

    private GameManager _gm;
    private int _coins;

    [SerializeField]
    private GameObject _currentSelectedObject;

    [SerializeField]
    private TextMeshProUGUI _txtCoins;


    private void Start()
    {
        _gm = GameManager._instance;
        UpdateCoins();
    }

    public void BuyItem(int ItemID)
    {
        _currentSelectedObject = EventSystem.current.currentSelectedGameObject;
        UnlockItem(ItemID);
    }

    private void UnlockItem(int num)        // this function will be used to unlock the hat if the player has the right amount of coins
    {
        int _holder = num;
        switch (num)
        {
            case 0:
                if(PlayerPrefs.GetInt(_holder.ToString()) == 0)
                {
                    if (_gm.BuyItem(100) == true)
                    {
                        PlayerPrefs.SetInt(_holder.ToString(), 1);
                        _currentSelectedObject.GetComponent<ItemChecker>().UpdateText();
                        UpdateCoins();
                    }
                }
                

                break;
            case 1:
                UpdateCoins();
                break;
            default:
                UpdateCoins();
                break;
        }
    }

    private void UpdateCoins()
    {
        _coins = _gm.ReturnCoins();
    }
}
